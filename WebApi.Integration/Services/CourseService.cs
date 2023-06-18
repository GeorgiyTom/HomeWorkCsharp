using System.Linq;
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
        if (courseModel.Price == 0)
            return HttpStatusCode.BadRequest;
        else if (courseModel.Name.Equals(null))
            return HttpStatusCode.BadRequest;
        //RPRY все выше в теле метода удалить
        await AddCourseInternalAsync(courseModel, cookie);
        return HttpStatusCode.OK;
    }

    //RPRY мена параметров начинаются с малых букв
    public async Task<HttpStatusCode> PutCourse(int Id, CourseModel courseModel, string cookie = null) //RPRY: к асинхронным методам добавляется Async
    {
        //RPRY этого тут не нужно. Метод должен не проверять, а совершать действие редактирования
        CourseModel oldCourse = await _applicationHttpClient.GetDeserializeCourseAsync(Id, cookie);
        if (oldCourse.Price == courseModel.Price || oldCourse.Name == courseModel.Name) //Если что-то не изменилось в новом курсе, то выбрасываем ошибку
            return HttpStatusCode.BadRequest;
        await _applicationHttpClient.EditCourseAsync(Id, courseModel, cookie);
        return HttpStatusCode.OK;
    }

    public async Task<bool> DeleteCourse(int Id, CourseModel courseModel, string cookie = null) //RPRY аргумент courseModel удалить
    {
        try
        {
            await _applicationHttpClient.DeleteCourseAsync(Id); // Удаляем курс //RPRY передать куки
            courseModel.Deleted = true; //RPRY удалить эту строку
            return courseModel.Deleted;
        } 
        catch //RPRY try-catch не нужен
        {
            return false;
        }
    }

}