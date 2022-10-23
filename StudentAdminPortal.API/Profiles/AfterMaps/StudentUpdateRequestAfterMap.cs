using AutoMapper;
using StudentAdminPortal.API.DomainModels;

namespace StudentAdminPortal.API.Profiles.AfterMaps
{
    public class StudentUpdateRequestAfterMap : IMappingAction<StudentUpdateRequest, StudentAdminPortal.API.Models.Student>
    {
        public void Process(StudentUpdateRequest source, StudentAdminPortal.API.Models.Student destination, ResolutionContext context)
        {
            destination.Address = new StudentAdminPortal.API.Models.Address
            {
                PhysicalAddress = source.PhysicalAddress,
                PostalAddress = source.PostalAddress
            };
        }
    }
}
