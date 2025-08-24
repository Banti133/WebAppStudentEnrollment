using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebApStudentEnrolment.Data;
using WebApStudentEnrolment.Models;
using WebApStudentEnrolment.Repositories;

namespace WebApStudentEnrolment.Controllers
{
    public class EnrollmentController : Controller
    {
        private readonly IEnrolments _enrollmentRepo;
        private readonly StudentEnrolmentContext _context;
        public EnrollmentController(IEnrolments enrollmentRepo,StudentEnrolmentContext context)
        {
            _enrollmentRepo = enrollmentRepo;
            _context = context;
        }

        // GET: Enrollments
        public async Task<IActionResult> Index()
        {
            var enrollments = await _enrollmentRepo.GetAllEnrolments();
            return View(enrollments);
        }

        // GET: Enrollments/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var enrollment = await _enrollmentRepo.GetEnrolmentById(id);
            if (enrollment == null)
            {
                return NotFound();
            }
            return View(enrollment);
        }

        // GET: Enrollments/Create
        public IActionResult Create()
        {
            ViewData["StudentId"] = new SelectList(_context.Students, "Id", "Name");
            ViewData["CourseId"] = new SelectList(_context.Courses, "Id", "Name");
            return View();
        }

        // POST: Enrollments/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,StudentId,CourseId,EnrolmentDate")] Enrolment enrollment)
        {
            
            if (ModelState.IsValid)
            {
                await _enrollmentRepo.AddEnrolment(enrollment);
                return RedirectToAction(nameof(Index));
            }
            ViewData["StudentId"] = new SelectList(_context.Students, "Id", "Name", enrollment.StudentId);
            ViewData["CourseId"] = new SelectList(_context.Courses, "Id", "Name", enrollment.CourseId);
            return View(enrollment);
        }

        // GET: Enrollments/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var enrollment = await _enrollmentRepo.GetEnrolmentById(id);
            if (enrollment == null)
            {
                return NotFound();
            }
            ViewData["StudentId"] = new SelectList(_context.Students, "Id", "Name");
            ViewData["CourseId"] = new SelectList(_context.Courses, "Id", "Name");
            return View(enrollment);
        }

        // POST: Enrollments/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,StudentId,CourseId,EnrolmentDate")] Enrolment enrolment)
        {
            if (id != enrolment.Id)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                try
                {
                    await _enrollmentRepo.UpdateEnrolment(id,enrolment);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (await _enrollmentRepo.GetEnrolmentById(id) == null)
                    {
                        return NotFound();
                    }
                    throw;
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["StudentId"] = new SelectList(_context.Students, "Id", "Name", enrolment.StudentId);
            ViewData["CourseId"] = new SelectList(_context.Courses, "Id", "Name", enrolment.CourseId);
            return View(enrolment);
        }
        // GET: Courses/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var course = await _enrollmentRepo.GetEnrolmentById(id);
            if (course == null)
            {
                return NotFound();
            }
            return View(course);
        }
        // POST: Courses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var course = await _enrollmentRepo.GetEnrolmentById(id);
            if (course != null)
            {
                await _enrollmentRepo.DeleteEnrolment(id);
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
