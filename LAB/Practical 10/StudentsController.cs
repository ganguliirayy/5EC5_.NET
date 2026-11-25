using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System.Data;
using StudentManagementAPI.Models;

namespace StudentManagementAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
        private readonly string _connectionString;

        public StudentsController(IConfiguration configuration)
        {
            _connectionString =
                configuration.GetConnectionString("DefaultConnection")
                ?? throw new InvalidOperationException(
                    "Database connection string not found.");
        }

        // GET: api/Students
        [HttpGet]
        public async Task<IActionResult> GetStudents()
        {
            var students = new List<Student>();

            await using var connection =
                new SqlConnection(_connectionString);

            await connection.OpenAsync();

            string query =
                "SELECT Id, Name, Email, Course FROM Students";

            await using var command =
                new SqlCommand(query, connection);

            await using var reader =
                await command.ExecuteReaderAsync();

            while (await reader.ReadAsync())
            {
                students.Add(new Student
                {
                    Id = reader.GetInt32(0),
                    Name = reader.GetString(1),
                    Email = reader.GetString(2),
                    Course = reader.GetString(3)
                });
            }

            return Ok(students);
        }

        // GET: api/Students/2
        [HttpGet("{id}")]
        public async Task<IActionResult> GetStudentById(int id)
        {
            await using var connection =
                new SqlConnection(_connectionString);

            await connection.OpenAsync();

            string query =
                "SELECT Id, Name, Email, Course FROM Students WHERE Id=@Id";

            await using var command =
                new SqlCommand(query, connection);

            command.Parameters.Add("@Id", SqlDbType.Int).Value = id;

            await using var reader =
                await command.ExecuteReaderAsync();

            if (!await reader.ReadAsync())
            {
                return NotFound("Student not found.");
            }

            var student = new Student
            {
                Id = reader.GetInt32(0),
                Name = reader.GetString(1),
                Email = reader.GetString(2),
                Course = reader.GetString(3)
            };

            return Ok(student);
        }

        // POST: api/Students
        [HttpPost]
        public async Task<IActionResult> AddStudent(Student student)
        {
            await using var connection =
                new SqlConnection(_connectionString);

            await connection.OpenAsync();

            string query = @"
                INSERT INTO Students (Name, Email, Course)
                OUTPUT INSERTED.Id
                VALUES (@Name, @Email, @Course)";

            await using var command =
                new SqlCommand(query, connection);

            command.Parameters.Add("@Name", SqlDbType.NVarChar, 100)
                .Value = student.Name;

            command.Parameters.Add("@Email", SqlDbType.NVarChar, 150)
                .Value = student.Email;

            command.Parameters.Add("@Course", SqlDbType.NVarChar, 100)
                .Value = student.Course;

            student.Id = (int)(await command.ExecuteScalarAsync() ?? 0);

            return CreatedAtAction(
                nameof(GetStudentById),
                new { id = student.Id },
                student);
        }

        // PUT: api/Students/2
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateStudent(
            int id, Student student)
        {
            await using var connection =
                new SqlConnection(_connectionString);

            await connection.OpenAsync();

            string query = @"
                UPDATE Students
                SET Name=@Name, Email=@Email, Course=@Course
                WHERE Id=@Id";

            await using var command =
                new SqlCommand(query, connection);

            command.Parameters.Add("@Id", SqlDbType.Int).Value = id;

            command.Parameters.Add("@Name", SqlDbType.NVarChar, 100)
                .Value = student.Name;

            command.Parameters.Add("@Email", SqlDbType.NVarChar, 150)
                .Value = student.Email;

            command.Parameters.Add("@Course", SqlDbType.NVarChar, 100)
                .Value = student.Course;

            int rows = await command.ExecuteNonQueryAsync();

            if (rows == 0)
            {
                return NotFound("Student not found.");
            }

            student.Id = id;

            return Ok(student);
        }

        // DELETE: api/Students/2
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStudent(int id)
        {
            await using var connection =
                new SqlConnection(_connectionString);

            await connection.OpenAsync();

            string query = "DELETE FROM Students WHERE Id=@Id";

            await using var command =
                new SqlCommand(query, connection);

            command.Parameters.Add("@Id", SqlDbType.Int).Value = id;

            int rows = await command.ExecuteNonQueryAsync();

            if (rows == 0)
            {
                return NotFound("Student not found.");
            }

            return Ok("Student deleted successfully.");
        }
    }
}
