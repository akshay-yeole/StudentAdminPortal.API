using AutoMapper;
using StudentAdminPortal.API.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudentAdminPortal.API.Profiles.AfterMaps
{
    public class StudentAddRequestAfterMap : IMappingAction<StudentAddRequest, StudentAdminPortal.API.Models.Student>
    {
        public void Process(StudentAddRequest source, Models.Student destination, ResolutionContext context)
        {
            destination.Id = new Guid();
            destination.Address = new StudentAdminPortal.API.Models.Address
            {
                Id = new Guid(),
                PhysicalAddress = source.PhysicalAddress,
                PostalAddress = source.PostalAddress
            };
        }
    }
}
