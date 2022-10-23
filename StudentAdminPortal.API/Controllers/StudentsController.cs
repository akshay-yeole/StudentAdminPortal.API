using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using StudentAdminPortal.API.DataRepository;
using StudentAdminPortal.API.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudentAdminPortal.API.Controllers
{
    [ApiController]
    public class StudentsController : Controller
    {
        private readonly IStudentRepository repository;
        private readonly IMapper mapper;

        public StudentsController(IStudentRepository repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }

        [HttpGet]
        [Route("[controller]")]
        public async Task<IActionResult> GetAllStudents()
        {
            var students = await repository.GetStudentsAsync();
            return Ok(mapper.Map<List<Student>>(students));
        }

        [HttpGet]
        [Route("[controller]/{studentId:guid}"), ActionName("GetStudent")]
        public async Task<IActionResult> GetStudent([FromRoute] Guid studentId)
        {
            var student = await repository.GetStudentAsync(studentId);

            if (student == null)
            {
                return NotFound();
            }
            return Ok(mapper.Map<Student>(student));
        }

        [HttpPut]
        [Route("[controller]/{studentId:guid}")]
        public async Task<IActionResult> UpdateStudentAsync([FromRoute] Guid studentId, [FromBody] StudentUpdateRequest request)
        {
            if (await repository.Exists(studentId))
            {
                //update details
                var updatedStudent = await repository.UpdateStudentDetails(studentId, mapper.Map<StudentAdminPortal.API.Models.Student>(request));

                if (updatedStudent != null)
                {
                    return Ok(mapper.Map<Student>(updatedStudent));
                }
            }
            return NotFound();
        }

        [HttpDelete]
        [Route("[controller]/{studentId:guid}")]
        public async Task<IActionResult> DeleteStudentAsync([FromRoute] Guid studentId)
        {
            if (await repository.Exists(studentId))
            {
                var student = await repository.DeleteStudentAsync(studentId);
                return Ok(mapper.Map<DomainModels.Student>(student));
            }
            return NotFound();
        }

        [HttpPost]
        [Route("[controller]/Add")]
        public async Task<IActionResult> AddStudentAsync([FromBody] StudentAddRequest request)
        {
            var student = await repository.AddStudentAsync(mapper.Map<StudentAdminPortal.API.Models.Student>(request));

            if (student != null)
            {
                return CreatedAtAction(nameof(GetStudent), new { studentId = student.Id}, mapper.Map<Student>(student));
            }

            return NotFound();
        }
    }
}
