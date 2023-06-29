using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using Benday.YamlDemoApp.UnitTests.Fakes;
using Benday.YamlDemoApp.UnitTests.Fakes.Security;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Benday.YamlDemoApp.IntegrationTests
{
    public abstract class AspNetIntegrationTestFixtureBase<TEntryPoint> where TEntryPoint : class
    {
        [TestCleanup]
        public void OnTestCleanup()
        {
            Console.WriteLine("Calling OnTestCleanup()...");
            if (_webApplicationInstance != null)
            {
                _webApplicationInstance.Dispose();
            }

            Reset();
        }

        protected void Reset()
        {
            _webApplicationInstance = null;
            _client = null;
            _scope = null;
            _hostServices = null;
        }

        protected WebApplicationFactory<TEntryPoint> _webApplicationInstance;
        protected WebApplicationFactory<TEntryPoint> WebApplicationInstance
        {
            get
            {
                if (_webApplicationInstance == null)
                {
                    try
                    {
                        _webApplicationInstance = CreateWebApplicationFactory();
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Error while creating instance of web application factory.  {0}", ex);
                        throw;
                    }
                }

                return _webApplicationInstance;
            }
            set => _webApplicationInstance = value;
        }

        protected virtual WebApplicationFactory<TEntryPoint> CreateWebApplicationFactory()
        {
            return new WebApplicationFactory<TEntryPoint>();
        }

        protected void InitializeSecurityWithMock(
            string policyName, bool isAuthorizedReturnValue)
        {
            Reset();

            _webApplicationInstance =
            new WebApplicationFactory<TEntryPoint>().WithWebHostBuilder(
            builder =>
            {
                builder.ConfigureTestServices(services =>
                {
                    services.AddAuthentication("Test")
                    .AddScheme<AuthenticationSchemeOptions, MockAuthenticationHandler>("Test", options =>
                    {

                    });

                    services.AddAuthorization(options =>
                    {
                        options.AddPolicy(policyName,
                        policy => policy.Requirements.Add(
                        new MockAuthorizationRequirement(isAuthorizedReturnValue)));
                    });

                    services.AddSingleton<IAuthorizationHandler, MockAuthorizationHandler>();
                });
            });

            InitializeClientWithSecurity(isAuthorizedReturnValue);
        }

        private void InitializeClientWithSecurity(bool isAuthorizedReturnValue)
        {
            _client = WebApplicationInstance.CreateClient(new WebApplicationFactoryClientOptions
            {
                AllowAutoRedirect = false
            });

            if (isAuthorizedReturnValue == true)
            {
                _client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Test");
            }
        }

        protected T CreateInstance<T>()
        {
            var provider = Scope.ServiceProvider;

            var returnValue = provider.GetService<T>();

            return returnValue;
        }

        protected HttpClient _client;
        protected HttpClient Client
        {
            get
            {
                if (_client == null)
                {
                    var webAppInstance = WebApplicationInstance;


                    try
                    {
                        _client = webAppInstance.CreateDefaultClient();
                    }
                    catch (AggregateException ex)
                    {
                        CheckForDependencyInjectionError(ex);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Error while creating instance of default client.  {0}", ex);
                        throw;
                    }
                }

                return _client;
            }
        }

        private void EnsureClientIsInitialized()
        {
            if (Client == null)
            {
                Assert.Fail("Client could not be initialized.");
            }
        }

        protected IServiceProvider _hostServices;
        protected IServiceProvider HostServices
        {
            get
            {
                if (_hostServices == null)
                {
                    _hostServices = WebApplicationInstance.Services;
                }

                return _hostServices;
            }
        }

        protected IServiceScope _scope;
        protected IServiceScope Scope
        {
            get
            {
                if (_scope == null)
                {
                    EnsureClientIsInitialized();

                    var scopeFactory = HostServices.GetService(
                    typeof(IServiceScopeFactory)) as IServiceScopeFactory;

                    _scope = scopeFactory.CreateScope();
                }

                return _scope;
            }
        }

        private const string START_OF_DI_ERROR_MSG_FULL = "System.InvalidOperationException: Unable to resolve service for type ";
        private const string START_OF_DI_ERROR_MSG_VARIATION_2 = "System.InvalidOperationException: No service for type '";
        private const string END_OF_DI_ERROR_MSG_VARIATION_2 = "' has been registered.";
        private const string START_OF_DI_ERROR_MSG = ": Unable to resolve service for type ";
        private const string MIDDLE_OF_DI_ERROR_MSG = "while attempting to activate ";
        protected string IsDependencyInjectionError(string content)
        {
            if (content.StartsWith(START_OF_DI_ERROR_MSG_FULL) == true)
            {
                return "variation1";
            }
            if (content.StartsWith(START_OF_DI_ERROR_MSG_VARIATION_2) == true &&
            content.Contains(END_OF_DI_ERROR_MSG_VARIATION_2) == true)
            {
                return "variation2";
            }
            else
            {
                return null;
            }
        }

        protected string GetDependencyInjectionErrorInfo(string content, string errorVariation)
        {
            if (errorVariation == "variation1")
            {
                return GetDependencyInjectionErrorInfoVariation1(content);
            }
            else
            {
                return GetDependencyInjectionErrorInfoVariation2(content);
            }
        }


        protected string GetDependencyInjectionErrorInfoVariation1(string content)
        {
            var indexOfStartOfMessage = content.IndexOf(START_OF_DI_ERROR_MSG);
            var indexOfFailedInterface = indexOfStartOfMessage + START_OF_DI_ERROR_MSG.Length;
            var indexOfFailedClassMessage = content.IndexOf(MIDDLE_OF_DI_ERROR_MSG);
            var indexOfFailedClass = indexOfFailedClassMessage + MIDDLE_OF_DI_ERROR_MSG.Length;
            var indexOfEndOfError = content.IndexOf("'.", indexOfFailedClass);

            if (indexOfStartOfMessage == -1 || indexOfFailedClassMessage == -1)
            {
                return null;
            }
            else
            {
                var failedInterface =
                content[indexOfFailedInterface..indexOfFailedClassMessage];

                var failedClass =
                content[indexOfFailedClass..indexOfEndOfError];

                return string.Format("Tried to create {0} for {1}'.  Check type registrations in Startup.cs.",
                failedInterface.Trim(), failedClass.Trim());
            }
        }

        protected string GetDependencyInjectionErrorInfoVariation2(string content)
        {
            content = content.Replace(START_OF_DI_ERROR_MSG_VARIATION_2, string.Empty);
            content = content.Replace(END_OF_DI_ERROR_MSG_VARIATION_2, Environment.NewLine);

            var reader = new StringReader(content);

            var line = reader.ReadLine();

            if (string.IsNullOrWhiteSpace(line) == true)
            {
                return null;
            }
            else
            {
                line = line.Trim();

                return string.Format(
                "Tried to create instance of {0}.  Check type registrations in Startup.cs.",
                line);
            }
        }

        protected async Task CheckForDependencyInjectionError(
            HttpResponseMessage response)
        {
            if (response.IsSuccessStatusCode == false)
            {
                var content = await response.Content.ReadAsStringAsync();

                Console.WriteLine("Content from server: {0}", content);

                var errorVariationInfo = IsDependencyInjectionError(content);
                if (errorVariationInfo != null)
                {
                    Assert.Fail(GetDependencyInjectionErrorInfo(content, errorVariationInfo));
                }
            }
        }

        protected void CheckForDependencyInjectionError(
            AggregateException ex)
        {
            if (ex.InnerException == null)
            {
                return;
            }

            if (ex.InnerException.Source == "Microsoft.Extensions.DependencyInjection")
            {
                var message = GetDependencyInjectionErrorInfoVariation1(ex.InnerException.Message);

                Assert.Fail(message);
            }
            else
            {
                return;
            }
        }
    }
}
