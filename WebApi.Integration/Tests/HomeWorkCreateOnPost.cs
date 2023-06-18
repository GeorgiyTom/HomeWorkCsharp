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
        public HomeWorkCreateOnPost(TestFixture testFixture)
        {
            _cookie = testFixture.AuthCookie;
            _courseService = new CourseService();
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
            HttpStatusCode resultOfCreate = await _courseService.PostCourse(courseModel);

            //Assert
            Assert.Equal(HttpStatusCode.OK, resultOfCreate); //Проверка, что статус OK

            //RPRY: нужно также проверять что сущность создана, получив ее по идентификатору 
        }
        
        //RPRY: не хватает негативных тестов
        
    }
}
