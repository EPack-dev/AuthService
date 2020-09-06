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
        public void AppConfigBindIsCorrect()
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
    }
}
