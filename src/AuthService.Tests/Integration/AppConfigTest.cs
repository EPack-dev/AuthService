using AuthService.WebApi;
using AuthService.WebApi.Config;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AuthService.Tests.Integration
{
    [TestClass]
    public class AppConfigTest
    {
        [TestMethod]
        public void MongoConfigBindIsCorrect()
        {
            var factory = new WebApplicationFactory<Startup>();
            using IServiceScope scope = factory.Services.CreateScope();
            MongoConfig mongoConfig = scope.ServiceProvider.GetRequiredService<MongoConfig>();

            Assert.AreEqual("AuthService", mongoConfig.Database);
            Assert.AreEqual("localhost", mongoConfig.Host);
            Assert.AreEqual(27017, mongoConfig.Port);
            Assert.AreEqual("root", mongoConfig.User);
            Assert.AreEqual("password", mongoConfig.Password);
        }

        [TestMethod]
        public void AuthConfigBindIsCorrect()
        {
            var factory = new WebApplicationFactory<Startup>();
            using IServiceScope scope = factory.Services.CreateScope();
            AuthConfig authConfig = scope.ServiceProvider.GetRequiredService<AuthConfig>();

            Assert.AreEqual("AuthService", authConfig.Issuer);
            Assert.AreEqual("AuthClient", authConfig.Audience);
            Assert.AreEqual("299A3DF3-FCF5-419D-A0D0-F879B97671B3", authConfig.SecurityKey);
            Assert.AreEqual(30, authConfig.LifetimeDays);
        }
    }
}
