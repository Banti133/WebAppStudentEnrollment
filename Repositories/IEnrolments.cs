using Microsoft.AspNetCore.Mvc;
using WebApStudentEnrolment.Models;

namespace WebApStudentEnrolment.Repositories
{
    public interface IEnrolments
    {
        int Count { get; }
        // Task<IActionResult> AddEnrolment(int enrolmentId, int studentId, int courseId, DateTime enrolmentDate);
        Task<Enrolment> AddEnrolment(Enrolment enrolment);
        Task<Enrolment> GetEnrolmentById(int enrolmentId);
        Task<IEnumerable<Enrolment>> GetAllEnrolments();
        Task<Enrolment> UpdateEnrolment(int enrolmentId, Enrolment enrolment);
        Task<Enrolment> DeleteEnrolment(int enrolmentId);



    }
}
