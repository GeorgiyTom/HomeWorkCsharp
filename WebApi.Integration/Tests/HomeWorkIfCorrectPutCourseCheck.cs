using System.Net.Http;
using System;
using WebApi.Models;
using System.Threading.Tasks;
using System.Net.Http.Json;
using static System.Net.WebRequestMethods;
using Xunit;
using System.Net;
using WebApi.Integration.Services;

namespace WebApi.Integration.Tests
{
    public class HomeWorkIfCorrectPutCourseCheck
    {
        private readonly string _cookieToken;
        private readonly CourseService _courseService;
        private readonly CourseApiClient _courseApiClient;
        Random rnd = new Random();
        public HomeWorkIfCorrectPutCourseCheck(TestFixture testFixture)
        {
            _cookieToken = testFixture.AuthCookie;
            _courseService = new CourseService();
            _courseApiClient = new CourseApiClient();
        }

        [Fact]
        public async Task CheckCorrectPutRequest()
        {
            //Arrange
            int newCourseId = await _courseService.CreateRandomCourseAsync();
            CourseModel course = new CourseModel()
            {
                Price = rnd.Next(),
                Name = Guid.NewGuid().ToString()
            };

            //Act
            CourseModel oldCourse = await _courseApiClient.GetDeserializeCourseAsync(newCourseId, _cookieToken); //Получаем старый курс
            HttpResponseMessage response = await _courseService.PutCourse(newCourseId, course); //Меняем поля
            CourseModel newCourse = await _courseApiClient.GetDeserializeCourseAsync(newCourseId, _cookieToken); //Получаем измененный курс

            //Assert
            Assert.NotEqual(oldCourse.Price, newCourse.Price);
            Assert.NotEqual(oldCourse.Name, newCourse.Name);
            //RPRY получить курс и проверить что поля изменились
        }
    }
}
