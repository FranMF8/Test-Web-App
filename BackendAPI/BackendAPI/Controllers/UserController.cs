using BackendAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace BackendAPI.Controllers
{
    [ApiController]
    [Route("login")]
    public class UserController : ControllerBase
    {
        [HttpGet]
        [Route("list")]
        public dynamic ListUsers() {

            List<User> users = new List<User>() {
                new User {
                    id = 1,
                    email = "cejita678@gmail.com",
                    password = "fran123456789",
                },
                new User {
                    id = 2,
                    email = "cejita678@hotmail.com",
                    password = "@Fran48987685",
                }
            };

            return users;
        }

        [HttpGet]
        [Route("listbyid")]
        public dynamic GetUser(int _id) {

            return new User {
                id = _id,
                email = "cejita678@gmail.com",
                password = "fran123456789",
            };
        }

        [HttpPost]
        [Route("save")]
        public dynamic SaveUser(string _email, string _password) {
            int _id = 1;

            return new {
                success = true,
                message = "ususario registrado",
                id = _id,
                email = _email,
                password = _password
            };
        }

        [HttpPost]
        [Route("delete")]
        public dynamic DeleteUser(User _user) {
            string token = Request.Headers.Where(x => x.Key == "Autorizacion").FirstOrDefault().Value;

            if (token != "token_valido")
            {
                return new
                {
                    success = false,
                    message = "token invalido",
                    result = ""
                };
            }

            return new
            {
                success = true,
                message = "usuario eliminado",
                result = _user
            };
        }
    }
}
