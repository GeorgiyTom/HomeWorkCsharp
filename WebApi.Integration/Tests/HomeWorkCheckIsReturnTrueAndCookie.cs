using Xunit;
using WebApi.Integration.Services;
using System;
using System.Threading.Tasks;
using static System.Net.WebRequestMethods;
using System.Net;
using System.IO;
using Xunit.Abstractions;

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
        public async Task CheckReturn() //RPRY: название должно быть осмысленным. Здесь и в остальных тестах 
        {
            //Arrange
            var name = Guid.NewGuid().ToString();
            var password = Guid.NewGuid().ToString();

            //Act
            //RPRY: зачем два одинаковых вызова? Нужно получить authResponse, затем оттуда вытащить куки
            var authResponse = await _cookieToken.GetCookieInternalAsync(name, password);
            var cookie = await _cookieToken.GetCookieAsync(name, password);

            //Assert
            Assert.Equal(HttpStatusCode.OK, authResponse.StatusCode);
            string data = await authResponse.Content.ReadAsStringAsync(); //Получаем данные с сервера
            //output.WriteLine(data);
            bool isTrue = Convert.ToBoolean(data);
            Assert.True(isTrue);
            Assert.NotNull(cookie);
        }
        
        //RPRY: не хватает негативных тестов
    }
}
