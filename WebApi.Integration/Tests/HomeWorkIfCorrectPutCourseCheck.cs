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
        private readonly CookieService _cookieToken;
        private readonly CourseService _courseService;
        private readonly CourseApiClient _courseApiClient;
        Random rnd = new Random();
        public HomeWorkIfCorrectPutCourseCheck(TestFixture testFixture)
        {
            _cookieToken = new CookieService();
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
            HttpStatusCode response = await _courseService.PutCourse(newCourseId, course);

            //Assert
            Assert.Equal(HttpStatusCode.OK, response);
           
        }
    }
}
