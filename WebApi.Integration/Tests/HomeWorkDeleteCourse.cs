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
    public class HomeWorkDeleteCourse
    {
        private readonly CourseService _courseService;
        private readonly CourseApiClient _courseApiClient;
        private readonly string _cookie;
        public HomeWorkDeleteCourse(TestFixture testFixture)
        {
            _cookie = testFixture.AuthCookie;
            _courseService = new CourseService();
            _courseApiClient = new CourseApiClient();
        }

        [Fact]
        public async Task IsDeleted()
        {
            //Act
            int newCourseId = await _courseService.CreateRandomCourseAsync();

            //Arrange
            HttpResponseMessage resultOfDelete = await _courseService.DeleteCourse(newCourseId, _cookie);
            CourseModel course = await _courseApiClient.GetDeserializeCourseAsync(newCourseId, _cookie);
            //Assert
            Assert.True(course.Deleted);
            //RPRY Получить курс по идентификатору и проверить, что поле isDeleted = true;
        }
    }
}
