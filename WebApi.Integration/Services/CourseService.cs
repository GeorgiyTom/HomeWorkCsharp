﻿using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using AutoFixture;
using Newtonsoft.Json;
using WebApi.Models;

namespace WebApi.Integration.Services;

public class CourseService
{
    private CourseApiClient _applicationHttpClient;
    public CourseService()
    {
        _applicationHttpClient = new CourseApiClient();
    }
    
   
    public async Task<int> CreateRandomCourseAsync(string cookie = null) 
    {
        var autoFixture = new Fixture();

        #region FSetup

        autoFixture.Behaviors.OfType<ThrowingRecursionBehavior>().ToList()
            .ForEach(b => autoFixture.Behaviors.Remove(b));
        autoFixture.Behaviors.Add(new OmitOnRecursionBehavior());

        #endregion

        var courseModel = autoFixture.Create<AddCourseModel>();
        return await AddCourseAsync(courseModel, cookie);
    }
    
    public async Task<int> AddCourseAsync(AddCourseModel courseModel, string cookie = null)
    {
        var addCourseResponse = await AddCourseInternalAsync(courseModel, cookie);
        return JsonConvert.DeserializeObject<int>(await addCourseResponse.Content.ReadAsStringAsync());
    }
    
    public async Task<HttpResponseMessage> AddCourseInternalAsync(AddCourseModel courseModel, string cookie = null)
    {
        return await _applicationHttpClient.CreateCourseAsync(courseModel, cookie);
    }

    public async Task<HttpStatusCode> PostCourse(AddCourseModel courseModel, string cookie = null) //Добавленный метод для создания курса
    {
        await AddCourseInternalAsync(courseModel, cookie);
        return HttpStatusCode.OK;
    }

    public async Task<HttpResponseMessage> PutCourse(int id, CourseModel courseModel, string cookie = null) //RPRY: к асинхронным методам добавляется Async
    {
        return await _applicationHttpClient.EditCourseAsync(id, courseModel, cookie);
    }

    public async Task<HttpResponseMessage> DeleteCourse(int id, string cookie = null)
    { 
        return await _applicationHttpClient.DeleteCourseAsync(id, cookie); 
    }

}