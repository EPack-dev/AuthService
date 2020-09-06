using System.Threading.Tasks;
using AuthService.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace AuthService.Tests.Model
{
    [TestClass]
    public class UserServiceTest
    {
        [DataTestMethod]
        [DataRow(0, UserRole.Admin)]
        [DataRow(1, UserRole.Regular)]
        [DataRow(42, UserRole.Regular)]
        public async Task RegisterIsCorrect(long accountsCount, UserRole expectedRole)
        {
            var accountRepository = new Mock<IAccountRepository>();
            accountRepository.Setup(x => x.Count()).ReturnsAsync(accountsCount);
            accountRepository.Setup(x => x.Add(It.IsAny<Account>()));

            var tokenProvider = new Mock<ITokenProvider>();

            var usersService = new UserService(accountRepository.Object, tokenProvider.Object);
            User user = await usersService.Register("login", "pwd");

            Assert.AreEqual(expectedRole, user.Role);
            accountRepository.Verify(x => x.Count(), Times.Once);
            accountRepository.Verify(x => x.Add(It.IsAny<Account>()), Times.Once());
        }
    }
}
