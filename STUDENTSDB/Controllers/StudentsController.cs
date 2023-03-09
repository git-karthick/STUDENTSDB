using Microsoft.AspNetCore.DataProtection.Repositories;
using Microsoft.AspNetCore.Mvc;
using STUDENTSDB.Models;

namespace STUDENTSDB.Controllers

{
    public class StudentsController : Controller
    {
        private readonly IStudentsRepository _repository;

        public StudentsController(IStudentsRepository repository)
        {
            this._repository = repository;
        }
        public async Task<IActionResult> Index()
        {
            IEnumerable<Students> students = await _repository.GetAllStudentsAsync();
            return View(students);
        }
        [HttpPost]
        public async Task<IActionResult> Create(Students students)
        {
            if (ModelState.IsValid)
            {
                await _repository.AddStudentsAsync(students);
                return RedirectToAction("Index");
            }

            return View(students);
        }

        public IActionResult Create()
        {
            return View();
        }
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Students students = await _repository.GetStudentsByIdAsync(id.Value);

            if (students == null)
            {
                return NotFound();
            }

            return View(students);
        }

        // POST: Student/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind] Students students)
        {
            if (id != students.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                await _repository.UpdateStudentsAsync(students);
                return RedirectToAction(nameof(Index));
            }

            return View(students);
        }
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Students students = await _repository.GetStudentsByIdAsync(id.Value);

            if (students == null)
            {
                return NotFound();
            }

            return View(students);
        }

        // POST: Student/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _repository.DeleteStudentsAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
