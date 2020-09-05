using AuthService.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AuthService.Tests.Model
{
    [TestClass]
    public class AccountTest
    {
        [TestMethod]
        public void CreateAccountIsCorrect()
        {
            var account = new Account("some_login", "P@ssword!", UserRole.Regular);

            Assert.AreEqual("some_login", account.Login);
            Assert.AreEqual(UserRole.Regular, account.Role);
            Assert.AreNotEqual(default, account.Created);
            Assert.AreEqual(64, account.PasswordHash.Length);
            Assert.AreEqual(128, account.PasswordSalt.Length);
        }

        [DataTestMethod]
        [DataRow("P@ssword!", "P@ssword!", true)]
        [DataRow("123-qwe-!@#", "123-qwe-!@#", true)]
        [DataRow("   -   ", "   -   ", true)]
        [DataRow("Pass", "pass", false)]
        [DataRow("password1", "password2", false)]
        [DataRow("1234", "123", false)]
        public void VerifyPasswordIsCorrect(string password1, string password2, bool compareResult)
        {
            var account = new Account("some_login", password1, UserRole.Regular);
            bool result = account.VerifyPassword(password2);
            Assert.AreEqual(compareResult, result);
        }
    }
}
