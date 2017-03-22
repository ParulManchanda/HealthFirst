using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using EmailTokenService.Controllers;

namespace EmailTokenService.Tests
{
    [TestClass]
    public class UnitTestTokenService
    {
        [TestMethod]
        public void testForNullEmailAddress()
        {
            var controller = new EmailTokenController();

            string email = "";
            string token = controller.Post(email);
            Assert.IsNull(token);
        }

        [TestMethod]
        public void testForInvalidEmailAddress()
        {
            var controller = new EmailTokenController();

            string email = "xyz";
            string token = controller.Post(email);
            Assert.IsNull(token);
        }

        [TestMethod]
        public void testForValidEmailAddress()
        {
            var controller = new EmailTokenController();

            string email = "xyz@xyz.com";
            string token = controller.Post(email);
            Assert.IsNotNull(token);
        }
    }
}
