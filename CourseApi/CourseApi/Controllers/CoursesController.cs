using System;
using System.Collections.Generic;
using System.Linq;
using CourseApi.Data.Entities;
using CourseApi.Dtos.CourseDtos;
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
                    Name = x.Name
                })
                .ToList();

            return StatusCode(200, data);
        }

        [HttpGet("{id}")]
        public ActionResult<CourseGetIdDto> GetById(int id)
        {
            var data = _context.Courses.Find(id);
            if (data == null)
            {
                return StatusCode(404, data);
            }
            CourseGetIdDto courseGetIdDto = new CourseGetIdDto()
            {
                Name = data.Name
            };

            return StatusCode(200, courseGetIdDto);
        }

        [HttpPost("")]
        public ActionResult Create(CourseCreateDto courseDto)
        {
            Course course = new Course
            {
                Name = courseDto.Name
            };

            _context.Courses.Add(course);
            _context.SaveChanges();

             return StatusCode(201); 
        }

        [HttpPut("{id}")]
        public ActionResult Update(CourseUpdateDto courseUpdateDto)
        {
            var existingCourse = _context.Courses.Find(courseUpdateDto.Id);
            if (existingCourse == null)
            {
                return StatusCode(404, courseUpdateDto);
            }
            existingCourse.Name = courseUpdateDto.Name;

            _context.Courses.Update(existingCourse);
            _context.SaveChanges();

            return StatusCode(200);
        }
    }
}
