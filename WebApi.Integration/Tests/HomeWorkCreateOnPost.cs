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
            var correctCourseModel = new AddCourseModel { Name = Guid.NewGuid().ToString(), Price = 15 };

            var courseModelWithErrorPrice = new AddCourseModel { Name = Guid.NewGuid().ToString(), Price = 0 };

            var courseModelWithErrorName = new AddCourseModel { Name = null, Price = 20 };

            //Act
            int idOfCourse = await _courseService.PostCourse(correctCourseModel, _cookie);
            CourseModel course = await _courseApiClient.GetDeserializeCourseAsync(idOfCourse, _cookie);

            int idOfCourseWithErrorPrice = await _courseService.PostCourse(courseModelWithErrorPrice, _cookie);
            CourseModel courseWithError1 = await _courseApiClient.GetDeserializeCourseAsync(idOfCourseWithErrorPrice, _cookie);

            int idOfCourseWithErrorName = await _courseService.PostCourse(courseModelWithErrorName, _cookie);
            CourseModel courseWithError2 = await _courseApiClient.GetDeserializeCourseAsync(id, _cookie);
            //Assert
            Assert.NotNull(course); //Проверка, что курс создан
            Assert.Null(courseWithError1);
            Assert.Null(courseWithError2); 

            

        }
        
    }
}
