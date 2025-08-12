using Microsoft.AspNetCore.Mvc;
using SIMSWebApp.Models;
using SIMSWebApp.Services;

namespace SIMSWebApp.Controllers
{
    public class StudentController : Controller
    {
        private readonly StudentService _studentService;

        public StudentController(StudentService studentService)
        {
            _studentService = studentService;
        }

        // GET: Student
        public async Task<IActionResult> Index()
        {
            try
            {
                var students = await _studentService.GetAllStudentsAsync();
                var viewModels = students.Select(s => _studentService.MapToViewModel(s));
                return View(viewModels);
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = $"Lỗi khi tải danh sách học sinh: {ex.Message}";
                return View(new List<StudentViewModel>());
            }
        }

        // GET: Student/Create
        public IActionResult Create()
        {
            return View(new StudentViewModel());
        }

        // POST: Student/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("FullName,StudentCode,DateOfBirth,Gender,Address,PhoneNumber,Email,Class")] StudentViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await _studentService.CreateStudentAsync(viewModel);
                    TempData["SuccessMessage"] = "Thêm học sinh thành công!";
                    return RedirectToAction(nameof(Index));
                }
                catch (InvalidOperationException ex)
                {
                    ModelState.AddModelError("", ex.Message);
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", $"Lỗi khi thêm học sinh: {ex.Message}");
                }
            }
            return View(viewModel);
        }

        // GET: Student/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var student = await _studentService.GetStudentByIdAsync(id.Value);
            if (student == null)
            {
                return NotFound();
            }

            var viewModel = _studentService.MapToViewModel(student);
            return View(viewModel);
        }

        // POST: Student/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("StudentID,FullName,StudentCode,DateOfBirth,Gender,Address,PhoneNumber,Email,Class")] StudentViewModel viewModel)
        {
            if (id != viewModel.StudentID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _studentService.UpdateStudentAsync(viewModel);
                    TempData["SuccessMessage"] = "Cập nhật học sinh thành công!";
                    return RedirectToAction(nameof(Index));
                }
                catch (InvalidOperationException ex)
                {
                    ModelState.AddModelError("", ex.Message);
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", $"Lỗi khi cập nhật học sinh: {ex.Message}");
                }
            }
            return View(viewModel);
        }

        // GET: Student/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var student = await _studentService.GetStudentByIdAsync(id.Value);
            if (student == null)
            {
                return NotFound();
            }

            var viewModel = _studentService.MapToViewModel(student);
            return View(viewModel);
        }

        // POST: Student/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                var result = await _studentService.DeleteStudentAsync(id);
                if (result)
                {
                    TempData["SuccessMessage"] = "Xóa học sinh thành công!";
                }
                else
                {
                    TempData["ErrorMessage"] = "Không thể xóa học sinh. Học sinh không tồn tại.";
                }
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = $"Lỗi khi xóa học sinh: {ex.Message}";
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
