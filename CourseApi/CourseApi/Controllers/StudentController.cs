using System;
using CourseApi.Data.Entities;
using CourseApi.Dtos.CourseDtos;
using CourseApi.Dtos.StudentDtos;
using Microsoft.AspNetCore.Mvc;

namespace CourseApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController:Controller
	{
        private readonly AppDbContext _context;

        public StudentController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet("")]
        public ActionResult<List<StudentGetAllDto>> GetAll(int pageNumber = 1, int pageSize = 1)
        {
            if (pageNumber <= 0 || pageSize <= 0)
            {
                return BadRequest("Page number and page size must be greater than zero!!!");
            }
            var data = _context.Students.Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .Select(x => new StudentGetAllDto
                {
                    FullName = x.FullName,
                    Email = x.Email,
                   CourseName = x.Course.Name,
                    Birthdate= x.BirthDate
                })
                .ToList();
            return StatusCode(200, data);
        }

        [HttpGet("{id}")]
        public ActionResult<StudentDetailsDto> GetById(int id)
        {
            var data = _context.Students.Find(id);
            if (data == null)
            {
                return StatusCode(404, data);
            }
            StudentDetailsDto courseGetIdDto = new StudentDetailsDto()
            {
                FullName = data.FullName,
                Email = data.Email,
                Birthdate=data.BirthDate,
               CourseName=data.Course.Name
            };

            return StatusCode(200, courseGetIdDto);
        }
        [HttpPost("")]
        public ActionResult Create(StudentCreateDto studentDto)
        {
            Course course = _context.Courses.FirstOrDefault(x => x.Id == studentDto.CourseId);
            if (course == null)
            {
                return StatusCode(404,studentDto);
            }

            if (course.Limit <= _context.Students.Count(s => s.CourseId == studentDto.CourseId))
            {
                return StatusCode(400, "Course limit reached.");
            }

            Student student = new Student
            {
                FullName = studentDto.FullName,
                Email = studentDto.Email,
                BirthDate = studentDto.Birthdate,
                CourseId = studentDto.CourseId
            };

            _context.Students.Add(student);
            _context.SaveChanges();

            return StatusCode(201);
        }
        [HttpPut("{id}")]
        public ActionResult Update(int id, StudentUpdateDto studentDto)
        {
            var existingStudent = _context.Students.Find(id);
            if (existingStudent == null)
            {
                return StatusCode(404,studentDto);
            }

            Course course = _context.Courses.FirstOrDefault(x => x.Id == studentDto.CourseId);
            if (course == null)
            {
                return StatusCode(404, "Course not found.");
            }

           
            if (course.Limit <= _context.Students.Count(s => s.CourseId == studentDto.CourseId && s.Id != id))
            {
                return StatusCode(400, "Course limit reached.");
            }

            existingStudent.FullName = studentDto.FullName;
            existingStudent.Email = studentDto.Email;
            existingStudent.BirthDate = studentDto.Birthdate;
            existingStudent.CourseId = studentDto.CourseId;
            existingStudent.ModifiedAt = DateTime.Now;

            _context.Students.Update(existingStudent);
            _context.SaveChanges();

            return StatusCode(204);
        }

        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            var studentToDelete = _context.Students.Find(id);
            if (studentToDelete == null)
            {
                return NotFound();
            }
          
            var course = _context.Courses.FirstOrDefault(c => c.Id == studentToDelete.CourseId);
            if (course != null)
            {
                course.Students.Remove(studentToDelete); 
                _context.Courses.Update(course); 
            }
            _context.Students.Remove(studentToDelete);
            _context.SaveChanges();
            return NoContent();
        }
    }
}

