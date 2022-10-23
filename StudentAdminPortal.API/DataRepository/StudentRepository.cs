using Microsoft.EntityFrameworkCore;
using StudentAdminPortal.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudentAdminPortal.API.DataRepository
{
    public class StudentRepository : IStudentRepository
    {
        private readonly StudentAdminPortalContext context;

        public StudentRepository(StudentAdminPortalContext context)
        {
            this.context = context;
        }

        public async Task<Student> AddStudentAsync(Student request)
        {
            var stud = await context.Students.AddAsync(request);
            await context.SaveChangesAsync();
            return stud.Entity;
        }

        public async Task<Student> DeleteStudentAsync(Guid studentId)
        {
            var student =await GetStudentAsync(studentId);
            if (student != null)
            {
                context.Students.Remove(student);
                await context.SaveChangesAsync();
                return student;
            }
            return null;
        }

        public async Task<bool> Exists(Guid studentId)
        {
           return await context.Students.AnyAsync(x => x.Id == studentId);
        }

        public async Task<List<Gender>> GetGendersAsync()
        {
            return await context.Genders.ToListAsync();
        }

        public async Task<Student> GetStudentAsync(Guid studentId)
        {
            return await context.Students.Include(nameof(Gender))
                .Include(nameof(Address))
                .FirstOrDefaultAsync(x => x.Id == studentId);
        }

        public async Task<List<Student>> GetStudentsAsync()
        {
            return await context.Students.Include(nameof(Gender)).Include(nameof(Address)).ToListAsync();
        }

        public async Task<Student> UpdateStudentDetails(Guid studentId, Student request)
        {
            var foundStudent = await GetStudentAsync(studentId);

            if (foundStudent != null)
            {
                foundStudent.FirstName = request.FirstName;
                foundStudent.LastName = request.LastName;
                foundStudent.Email = request.Email;
                foundStudent.DateOfBirth = request.DateOfBirth;
                foundStudent.Mobile = request.Mobile;
                foundStudent.GenderId = request.GenderId;
                foundStudent.Address.PhysicalAddress = request.Address.PhysicalAddress;
                foundStudent.Address.PostalAddress = request.Address.PostalAddress;

                await context.SaveChangesAsync();

                return foundStudent;
            }

            return null;
        }
    }
}
