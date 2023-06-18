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
            var name = Guid.NewGuid().ToString();
            var password = Guid.NewGuid().ToString();

            //Act
            var authResponse = await _cookieToken.GetCookieInternalAsync(name, password);
            IEnumerable<string> cookies = authResponse.Headers.SingleOrDefault(header => header.Key == "Set-Cookie").Value;

            //Assert
            Assert.Equal(HttpStatusCode.OK, authResponse.StatusCode);
            string data = await authResponse.Content.ReadAsStringAsync(); //Получаем данные с сервера
            //output.WriteLine(data);
            bool isTrue = Convert.ToBoolean(data);
            Assert.True(isTrue);
            Assert.NotNull(cookies);
        }
        
        //RPRY: не хватает негативных тестов
    }
}
