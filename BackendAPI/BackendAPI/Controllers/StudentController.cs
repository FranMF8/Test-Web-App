using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using users.database;

namespace BackendAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : Controller
    {
        UsersContext _usersContext;

        public StudentController(UsersContext context)
        {
            _usersContext = context;
        }
        [HttpGet("getAll")]
        public IActionResult GetAll()
        {
            List<Student> studentsList;

            studentsList = _usersContext.Students.ToList();

            return Json(studentsList);
        }

        [HttpPost("modify")]
        public async Task<ActionResult<Student>> Edit(Student student)
        {
            var returningStudent = _usersContext.Students.Find(student.id);

            if (returningStudent == null) return NotFound(new { message = "Usuario no encontrado" });

            returningStudent.id = student.id;
            returningStudent.name = student.name;
            returningStudent.dni = student.dni;
            returningStudent.email = student.email;
            _usersContext.Students.Update(returningStudent);
            await _usersContext.SaveChangesAsync();

            try
            {
                return Ok(new { message = "Usuario modificado exitosamente" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error en el servidor" });
            }
        }

        [HttpPost("add")]
        public async Task<ActionResult<Student>> AddStudent(Student student)
        {

            var newStudent = _usersContext.Students.Find(student.id);

            if (newStudent is null) {

                newStudent = new Student();

                newStudent.name = student.name;
                newStudent.dni = student.dni;
                newStudent.email = student.email;
                _usersContext.Add(newStudent);
                await _usersContext.SaveChangesAsync();
                return Ok(new { message = "Usuario creado exitosamente" });

            }
            return Unauthorized(new { message = "El usuario ya existe" });

        }
        [HttpDelete("deleteStudent/{id}")]
        public async Task<ActionResult<Student>> Delete(int id)
        {
            var toDeleteUser = _usersContext.Students.Find(id);

            if (toDeleteUser is null) return NotFound(new { message = "Usuario no encontrado" });

            _usersContext.Students.Remove(toDeleteUser);
            await _usersContext.SaveChangesAsync();
            return Ok(new { message = "Usuario eliminado exitosamente" });
        }
    }


}
