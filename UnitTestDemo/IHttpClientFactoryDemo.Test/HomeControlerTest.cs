using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using IHttpClientFactoryDemo.Controllers;
using System.Net.Http;
using Moq;
using Xunit;

namespace IHttpClientFactoryDemo.Test
{
    public class HomeControllerTest
    {
        [Fact]
        public void ReturnActionResult()
        {
            // Arrange
            var mockRepo = new Mock<IHttpClientFactory>();

            var _homeController = new HomeController(mockRepo.Object);

            var result = _homeController.Index();
            var viewResult = Assert.IsType<ViewResult>(result);
        }


    }
}
