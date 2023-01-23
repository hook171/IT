using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using domain.Services;
using domain.Models;
using WebProject.Views;
namespace WebProject.Controllers
{
    [ApiController]
    [Route("user")]
    public class UserController : ControllerBase
    {
        
        private readonly UserService _service;
          
        public UserController(UserService service)
        {
            _service = service;
        }

        [HttpPost("create_user")]
        public ActionResult Create(string username, string password, int id, string phone, string fio, Role role)
        {
            User user = new User(username, password, 0, phone, fio, role);
            var userRes = _service.CreateUser(user);
            if (userRes.IsFailure)
            {
                return Problem(statusCode: 404, detail: userRes.Error);
            }
            return Ok(userRes.Value);
        }

        [HttpGet("getuserbylogin")]
        public ActionResult<UserSearchView> GetUserByLogin(string login)
        {
            if (login == string.Empty)
            {
                return Problem(statusCode: 404, detail: "Пустой логин");
            }

            var userRes = _service.GetByLogin(login);
            if (userRes.IsFailure)
            {
                return Problem(statusCode: 404, detail: userRes.Error);
            }

            return Ok(new UserSearchView
            {
                Id = userRes.Value.Id,
                Phone = userRes.Value.Phone,
                FIO = userRes.Value.FIO,
                Role = userRes.Value.Role,
                Username = userRes.Value.Username,
                Password = userRes.Value.Password
            });
        }

        [HttpGet("isuserexists")]
        public ActionResult IsUserExists(string login,string password)
        {
            var userRes = _service.CheckExist(login,password);

            if (userRes.IsFailure)
            {
                return Problem(statusCode: 404, detail: userRes.Error);
            }

            return Ok(userRes.Value);
        }
    }
}
