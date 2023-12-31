﻿using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using WebApi.Models;

namespace WebApi.Integration.Services;

public class CourseApiClient
{
    private HttpClient _httpClient;
    private readonly string _baseUri;

    public CourseApiClient()
    {
        _httpClient = new HttpClient();
        var configuration = new ConfigurationBuilder()
            .AddJsonFile($"appsettings.json").Build();
        _baseUri = configuration["BaseUri"];
    }

    public async Task<HttpResponseMessage> CreateCourseAsync(AddCourseModel course, string cookie = null)
    {
        if (cookie != null)
        {
            AddAuthCookie(cookie);
        }
        return await _httpClient.PostAsJsonAsync($"{_baseUri}/course", course);
    }

    private void AddAuthCookie(string cookie)
    {
        _httpClient.DefaultRequestHeaders.Add("cookie", cookie);
    }

    public async Task<HttpResponseMessage> GetCourseAsync(int id, string cookie = null)
    {
        if (cookie != null)
        {
            AddAuthCookie(cookie);
        }
        return await _httpClient.GetAsync($"{_baseUri}/course/{id}");
    }

    public async Task<CourseModel> GetDeserializeCourseAsync(int id, string cookie = null)
    {
        var response = await GetCourseAsync(id, cookie);
        return JsonConvert.DeserializeObject<CourseModel>(await response.Content.ReadAsStringAsync());
    }

    public async Task<HttpResponseMessage> EditCourseAsync(int id, CourseModel courseModel, string cookie = null)
    {
        if (cookie != null)
        {
            AddAuthCookie(cookie);
        }
        return await _httpClient.PutAsJsonAsync($"{_baseUri}/course/{id}", courseModel);
    }

    public async Task<HttpResponseMessage> DeleteCourseAsync(int id, string cookie = null) //передать куки
    {
        var course = await GetDeserializeCourseAsync(id, cookie);
        course.Deleted = true;
        return await _httpClient.DeleteAsync($"{_baseUri}/course/{id}");
    }
}