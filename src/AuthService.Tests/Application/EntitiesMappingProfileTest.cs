using System;
using System.Linq;
using AuthService.Application;
using AuthService.Application.Entities;
using AuthService.Model;
using AutoMapper;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AuthService.Tests.Application
{
    [TestClass]
    public class EntitiesMappingProfileTest
    {
        [TestInitialize]
        public void TestInitialize()
        {
            _configuration = new MapperConfiguration(cfg => { cfg.AddMaps(typeof(EntitiesMappingProfile)); });
            _mapper = _configuration.CreateMapper();
        }

        [TestMethod]
        public void ConfigurationIsValid()
        {
            _configuration.AssertConfigurationIsValid();
        }

        [TestMethod]
        public void MapAccountToEntityIsCorrect()
        {
            Account account = new ("username-1", "password-2", UserRole.Regular);

            AccountEntity entity = _mapper.Map<AccountEntity>(account);

            Assert.AreEqual("username-1", entity.Login);
            Assert.AreEqual(UserRole.Regular, entity.Role);
            Assert.AreEqual(account.Created, entity.Created);
            Assert.IsTrue(account.PasswordHash.SequenceEqual(entity.PasswordHash));
            Assert.IsTrue(account.PasswordSalt.SequenceEqual(entity.PasswordSalt));
        }

        [TestMethod]
        public void MapEntityToAccountIsCorrect()
        {
            AccountEntity entity = new ("username-1", UserRole.Regular, new DateTime(2020, 3, 18),
                new byte[] { 1, 2 }, new byte[] { 3, 4, 5 });

            Account account = _mapper.Map<Account>(entity);

            Assert.AreEqual("username-1", account.Login);
            Assert.AreEqual(UserRole.Regular, account.Role);
            Assert.AreEqual(new DateTime(2020, 3, 18), account.Created);
            Assert.IsTrue(account.PasswordHash.SequenceEqual(new byte[] { 1, 2 }));
            Assert.IsTrue(account.PasswordSalt.SequenceEqual(new byte[] { 3, 4, 5 }));
        }

        private MapperConfiguration _configuration = default!;
        private IMapper _mapper = default!;
    }
}
