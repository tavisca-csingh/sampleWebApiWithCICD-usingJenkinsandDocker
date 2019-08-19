using System;
using Xunit;
using WebApi.Controllers;

namespace WebApi.Tests
{
    public class ValueControllerFixture
    {
       
        private ValuesController controller = new ValuesController();
        [Fact]
        public void When_UserSend_Hii()
        {
            Assert.Equal("Hello", controller.Get("Hii"));
        }
        [Fact]
        public void When_UserSend_Hello()
        {
            Assert.Equal("Hii", controller.Get("Hello"));
        }
        [Fact]
        public void When_UserSendNeither_HiiNorHello()
        {
            Assert.Equal("Invalid", controller.Get("welcome"));
        }
    }
    
}
