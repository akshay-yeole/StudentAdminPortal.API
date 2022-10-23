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
    public class GendersController : Controller
    {
        private readonly IStudentRepository repository;
        private readonly IMapper mapper;

        public GendersController(IStudentRepository repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }

        [HttpGet]
        [Route("[controller]")]
        public async Task<IActionResult> GetAllGenders()
        {
            var allGenders = await repository.GetGendersAsync();
            if (allGenders == null | !allGenders.Any())
            {
                return NotFound(); 
            }
            return Ok(mapper.Map<List<Gender>>(allGenders));
        }
    }
}
