using AutoMapper;
using StudentAdminPortal.API.Models;
using StudentAdminPortal.API.Profiles.AfterMaps;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DomainModel = StudentAdminPortal.API.DomainModels;

namespace StudentAdminPortal.API.Profiles
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<Student, DomainModel.Student>().ReverseMap();
            CreateMap<Gender, DomainModel.Gender>().ReverseMap();
            CreateMap<Address, DomainModel.Address>().ReverseMap();
            CreateMap<DomainModel.StudentUpdateRequest, StudentAdminPortal.API.Models.Student>()
                .AfterMap<StudentUpdateRequestAfterMap>();
            CreateMap<DomainModel.StudentAddRequest, StudentAdminPortal.API.Models.Student>()
               .AfterMap<StudentAddRequestAfterMap>();
        }
    }
}
