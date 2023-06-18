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
        public HomeWorkDeleteCourse(TestFixture testFixture)
        {
            _courseService = new CourseService();
            _courseApiClient = new CourseApiClient();
        }

        [Fact]
        public async Task IsDeleted()
        {
            //Act
            int newCourseId = await _courseService.CreateRandomCourseAsync();
            CourseModel course = await _courseApiClient.GetDeserializeCourseAsync(newCourseId); //RPRY: не нужно

            //Arrange
            bool resultOfDelete = await _courseService.DeleteCourse(newCourseId, course);

            //Assert
            Assert.True(resultOfDelete);
            //RPRY Получить курс по идентификатору и проверить, что поле isDeleted = true;
        }
    }
}
