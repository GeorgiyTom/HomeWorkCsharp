using Xunit;
using WebApi.Integration.Services;
using System;
using System.Threading.Tasks;
using static System.Net.WebRequestMethods;
using System.Net;
using System.IO;
using Xunit.Abstractions;
using Newtonsoft.Json;
using WebApi.Models;
using System.Collections.Generic;
using System.Linq;

namespace WebApi.Integration.Tests
{
    public class HomeWorkCheckIsReturnTrueAndCookie : IClassFixture<TestFixture>
    {

        private readonly CookieService _cookieToken;
        public HomeWorkCheckIsReturnTrueAndCookie(TestFixture testFixture)
        {
            _cookieToken = new CookieService();
        }

        [Fact]
        public async Task CheckIsReturnTrueAndNotNullCookies() //RPRY: название должно быть осмысленным. Здесь и в остальных тестах 
        {
            //Arrange
            var name1 = Guid.NewGuid().ToString();
            var password1 = Guid.NewGuid().ToString();

            string errorName = null;
            var password2 = Guid.NewGuid().ToString();

            var name2 = Guid.NewGuid().ToString();
            string errorPassword = null;

            //Act
            string cookie1 = await _cookieToken.GetCookieAsync(name1, password1);

            string cookie2 = await _cookieToken.GetCookieAsync(errorName, password2);

            string cookie3 = await _cookieToken.GetCookieAsync(name2, errorPassword);

            //Assert
            Assert.NotNull(cookie1);
            Assert.Null(cookie2)
            Assert.Null(cookie3)
        }
       
    }
}
