using System;
using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace Benday.YamlDemoApp.Api.DataAccess
{
    public class YamlDemoAppDesignTimeDbContextFactory :
        IDesignTimeDbContextFactory<YamlDemoAppDbContext>
    {
        public static YamlDemoAppDbContext Create()
        {
            var environmentName =
            Environment.GetEnvironmentVariable(
            "ASPNETCORE_ENVIRONMENT");

            var basePath = AppContext.BaseDirectory;

            return Create(basePath, environmentName);
        }

        public YamlDemoAppDbContext CreateDbContext(string[] args)
        {
            return Create(
            Directory.GetCurrentDirectory(),
            Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT"));
        }

        private static YamlDemoAppDbContext Create(string basePath, string environmentName)
        {
            var builder = new ConfigurationBuilder()
            .SetBasePath(basePath)
            .AddJsonFile("appsettings.json")
            .AddJsonFile($"appsettings.{environmentName}.json", true)
            .AddEnvironmentVariables();

            var config = builder.Build();

            var connstr = config.GetConnectionString("default");

            if (string.IsNullOrWhiteSpace(connstr) == true)
            {
                throw new InvalidOperationException(
                "Could not find a connection string named 'default'.");
            }
            else
            {
                return Create(connstr);
            }
        }

        private static YamlDemoAppDbContext Create(string connectionString)
        {
            if (string.IsNullOrEmpty(connectionString))
                throw new ArgumentException(
                $"{nameof(connectionString)} is null or empty.",
                nameof(connectionString));

            var optionsBuilder =
            new DbContextOptionsBuilder<YamlDemoAppDbContext>();

            optionsBuilder.UseSqlServer(connectionString);

            return new YamlDemoAppDbContext(optionsBuilder.Options);
        }
    }
}
