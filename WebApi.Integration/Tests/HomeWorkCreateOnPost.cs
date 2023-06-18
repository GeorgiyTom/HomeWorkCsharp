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

    public class HomeWorkCreateOnPost : IClassFixture<TestFixture>
    {
        private readonly string _cookie;
        private readonly CourseService _courseService;
        private readonly CourseApiClient _courseApiClient;
        public HomeWorkCreateOnPost(TestFixture testFixture)
        {
            _cookie = testFixture.AuthCookie;
            _courseService = new CourseService();
            _courseApiClient = new CourseApiClient();
        }

        [Fact]
        public async Task CheckPriceAndNotNullNameForPostCourse()
        {
            //Arrange 
            var courseModel = new AddCourseModel
            {
                Name = Guid.NewGuid().ToString(),
                Price = 15
            };

            //Act
            int idOfCourse = await _courseService.PostCourse(courseModel, _cookie);
            CourseModel course = await _courseApiClient.GetDeserializeCourseAsync(idOfCourse, _cookie);
            //Assert
            Assert.NotNull(course); //Проверка, что курс создан

            Assert.NotEqual(0, course.Price);
            Assert.Null(course.Name);

        }
        
        //RPRY: не хватает негативных тестов
        
    }
}
