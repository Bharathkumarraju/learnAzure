using Benday.YamlDemoApp.Api.Security;
using Benday.YamlDemoApp.WebUi.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Benday.YamlDemoApp.WebUi.Controllers
{
    [AllowAnonymous]
    public partial class SecurityController : Controller
    {
        private ISecurityConfiguration _Configuration;
        
        public SecurityController(ISecurityConfiguration configuration)
        {
            if (configuration == null)
            {
                throw new ArgumentNullException(nameof(configuration), $"{nameof(configuration)} is null.");
            }
            
            _Configuration = configuration;
        }
        
        public IActionResult Index()
        {
            return View();
        }
        
        public IActionResult Login()
        {
            var model = new SecurityLoginModel();
            
            if (_Configuration.MicrosoftAccount == true)
            {
                AddLoginType(model, "Microsoft Account", "microsoftaccount", "ms-symbollockup_signin_light.svg");
            }
            
            if (_Configuration.Google == true)
            {
                AddLoginType(model, "Google", "google");
            }
            
            if (_Configuration.AzureActiveDirectory == true)
            {
                AddLoginType(model, "Work or School Account", "aad", "ms-symbollockup_signin_dark.svg");
            }
            
            if (_Configuration.DevelopmentMode == true)
            {
                AddLoginTypeKeyValue(
                model, "Local Development",
                GetUrlForDevelopmentLogin(),
                null);
            }
            
            return View(model);
        }
        
        private string GetUrlForDevelopmentLogin()
        {
            if (this.Request != null && this.Request.Query.ContainsKey("ReturnUrl") == true)
            {
                return Url.Action("DevelopmentLogin", new { ReturnUrl = this.Request.Query["ReturnUrl"] });
            }
            else
            {
                return Url.Action("DevelopmentLogin");
            }
        }
        
        private string GetPostActionRedirectUri()
        {
            if (Request.Query.ContainsKey("ReturnUrl") == false)
            {
                return "/";
            }
            else
            {
                return Request.Query["ReturnUrl"];
            }
        }
        
        public IActionResult DevelopmentLogin()
        {
            if (_Configuration.DevelopmentMode == false)
            {
                return NotFound();
            }
            else
            {
                var model = new DevelopmentLoginModel();
                
                var redirectUrl = GetPostActionRedirectUri();
                
                if (redirectUrl != "/")
                {
                    model.RedirectUrl = redirectUrl;
                }
                
                return View(model);
            }
        }
        
        [HttpPost]
        public async Task<IActionResult> DevelopmentLogin(DevelopmentLoginModel model)
        {
            if (_Configuration.DevelopmentMode == false)
            {
                return NotFound();
            }
            else
            {
                var claims = new List<Claim>
                {
                    new Claim(SecurityConstants.Claim_X_MsClientPrincipalId, Guid.NewGuid().ToString()),
                    new Claim(
                    SecurityConstants.Claim_X_MsClientPrincipalIdp,
                    SecurityConstants.Idp_DevelopmentMode),
                    new Claim(SecurityConstants.Claim_X_MsClientPrincipalName, model.Username)
                };
                
                var temp = new ClaimsPrincipal(new ClaimsIdentity(claims,
                CookieAuthenticationDefaults.AuthenticationScheme));
                
                await HttpContext.SignInAsync(temp, new AuthenticationProperties
                {
                    IsPersistent = model.KeepMeLoggedIn,
                    AllowRefresh = true
                });
                
                if (model.RedirectUrl != null)
                {
                    return Redirect(model.RedirectUrl);
                }
                else
                {
                    return Redirect(_Configuration.PostLoginPath);
                }
            }
        }
        
        public async Task<IActionResult> Logout()
        {
            if (_Configuration.DevelopmentMode == false)
            {
                var easyAuthLogoutUrl = "/.auth/logout?post_logout_redirect_uri=" +
                GetPostActionRedirectUri();
                
                return Redirect(easyAuthLogoutUrl);
            }
            else
            {
                await HttpContext.SignOutAsync();
                
                return RedirectToAction("Login");
            }
        }
        
        private void AddLoginType(SecurityLoginModel model, string displayName, string argName)
        {
            AddLoginTypeKeyValue(model, displayName, GetAuthUrlForProvider(argName), GetLogoUrl(argName));
        }
        
        private void AddLoginType(SecurityLoginModel model, string displayName, string argName, string logoFilename)
        {
            AddLoginTypeKeyValue(model, displayName, GetAuthUrlForProvider(argName), logoFilename);
        }
        
        private void AddLoginTypeKeyValue(SecurityLoginModel model, string key, string value, string logoUrl)
        {
            if (model.LoginTypes == null)
            {
                model.LoginTypes = new List<SecurityLoginOption>();
            }
            
            model.LoginTypes.Add(new SecurityLoginOption()
            {
                Key = key,
                Value = value,
                LogoFilename = logoUrl
            });
            
        }
        
        private string GetAuthUrlForProvider(string provider)
        {
            if (string.IsNullOrEmpty(provider))
            {
                throw new ArgumentException($"{nameof(provider)} is null or empty.", nameof(provider));
            }
            
            var temp = string.Format("/.auth/login/{0}?post_login_redirect_uri={1}",
            provider,
            GetPostActionRedirectUri());
            
            return temp;
        }
        
        private string GetLogoUrl(string provider)
        {
            if (string.IsNullOrEmpty(provider))
            {
                throw new ArgumentException($"{nameof(provider)} is null or empty.", nameof(provider));
            }
            
            var temp = string.Format("{0}.png", provider);
            
            return temp;
        }
    }
}