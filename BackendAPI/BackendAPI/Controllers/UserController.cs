﻿using Microsoft.AspNetCore.Mvc;
using users.database;

namespace BackendAPI.Controllers
{
    [Route("/api/login")]
    [ApiController] 
    public class UserController : ControllerBase
    {
        private UsersContext _context;

        public UserController(UsersContext context)
        {
            _context = context;
        }
        [HttpGet("list")]
        public IEnumerable<DBUser> GetAll() => _context.Users.ToList();

        [HttpPost]
        public IResult CheckLogin(int id)
        {

            var eUser = _context.Users.Find(id);
            if (eUser is null) return Results.NotFound();

            return Results.Ok(eUser);

        }
    }
}
