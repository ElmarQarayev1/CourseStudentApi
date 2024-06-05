using System;
using System.Collections.Generic;
using System.Linq;
using CourseApi.Data.Entities;
using CourseApi.Dtos.CourseDtos;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace CourseApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CoursesController : ControllerBase
    {
        private readonly AppDbContext _context;

        public CoursesController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet("")]
        public ActionResult<List<CourseGetAllDto>> GetAll(int pageNumber = 1, int pageSize = 1)
        {
            if (pageNumber <= 0 || pageSize <= 0)
            {
                return BadRequest("Page number and page size must be greater than zero!!!");
            }
            var data = _context.Courses.Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .Select(x => new CourseGetAllDto
                {
                    Name = x.Name,
                    Limit=x.Limit,
                    StudentCount=x.Students.Count

                })
                .ToList();

            return StatusCode(200, data);
        }

        [HttpGet("{id}")]
        public ActionResult<CourseDetailsDto> GetById(int id)
        {
            var data = _context.Courses.Find(id);
            if (data == null)
            {
                return StatusCode(404, data);
            }
            CourseDetailsDto courseGetIdDto = new CourseDetailsDto()
            {
                Name = data.Name,
                Limit=data.Limit
            };

            return StatusCode(200, courseGetIdDto);
        }

        [HttpPost("")]
        public ActionResult Create(CourseCreateDto courseDto)
        {
            if (_context.Courses.Any(x => x.Name == courseDto.Name && !x.IsDeleted))
                return StatusCode(409);

            Course course = new Course
            {
                Name = courseDto.Name,
                Limit=courseDto.Limit
            };

            _context.Courses.Add(course);
            _context.SaveChanges();

             return StatusCode(201); 
        }

        [HttpPut("{id}")]
        public ActionResult Update(CourseUpdateDto courseUpdateDto)
        {
            if (_context.Courses.Any(x => x.Name == courseUpdateDto.Name && !x.IsDeleted))
                return StatusCode(409);

            var existingCourse = _context.Courses.Find(courseUpdateDto.Id);
            if (existingCourse == null)
            {
                return StatusCode(404, courseUpdateDto);
            }
            existingCourse.Name = courseUpdateDto.Name;
            existingCourse.Limit = courseUpdateDto.Limit;
            existingCourse.ModifiedAt = DateTime.Now;

            _context.Courses.Update(existingCourse);
            _context.SaveChanges();

            return StatusCode(200);
        }

        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            var existingCourse = _context.Courses.Find(id);
            if (existingCourse == null)
            {
                return StatusCode(404,existingCourse);
            }

            existingCourse.IsDeleted = true;
            _context.Courses.Update(existingCourse);
            _context.SaveChanges();

            return StatusCode(204);
        }

    }
}
