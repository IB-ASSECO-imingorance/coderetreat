using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Text;
using Tests;

namespace Kata {
    [TestClass]
    public class AccountTest {

        private DateTime firstDateTime = new DateTime(2015, 12, 24);
        private DateTime secondDateTime = new DateTime(2016, 8, 23);
        private Mock<IDateTimeRepository> dateTimeRepositoryMock = new Mock<IDateTimeRepository>();

        [TestMethod]
        public void GivenAnAmountToDepositItShouldBeAddedInTheAccount() {
            Account account = new Account(dateTimeRepositoryMock.Object);

            dateTimeRepositoryMock.Setup(r => r.GetCurrentDateTime()).Returns(firstDateTime);
            account.Deposit(400);

            Assert.AreEqual(400, account.Amount);

        }

        [TestMethod]
        public void GivenAnAmountToWithdrawItShouldBeSubtractedFromTheAccount() {
            Account account = new Account(dateTimeRepositoryMock.Object);
            dateTimeRepositoryMock.SetupSequence(r => r.GetCurrentDateTime()).Returns(firstDateTime).Returns(secondDateTime);
            account.Deposit(500);
            account.Withdraw(200);

            Assert.AreEqual(300, account.Amount);

        }
        [TestMethod]
        public void ItShouldPrintTheStatementPropertly() {
            string expectedResult = getStatement();
            Account account = new Account(dateTimeRepositoryMock.Object);
            dateTimeRepositoryMock.SetupSequence(r => r.GetCurrentDateTime()).Returns(firstDateTime).Returns(secondDateTime);

            account.Deposit(500);
            account.Withdraw(100);


            string statement = account.PrintStatement();
            Assert.AreEqual(expectedResult, statement);
        }

        private string getStatement() {
            StringBuilder statement = new StringBuilder();

            return statement.AppendLine("Date" + Account.FIELD_SEPARATOR + "Amount" + Account.FIELD_SEPARATOR + "Balance")
                            .AppendLine("24.12.2015\t+500\t500")
                            .AppendLine("23.8.2016\t-100\t400")
                            .ToString();

        }
    }
}
