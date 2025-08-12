using SIMSWebApp.DatabaseContext.Entities;
using SIMSWebApp.Interfaces;
using SIMSWebApp.Models;

namespace SIMSWebApp.Services
{
    public class StudentService
    {
        private readonly IStudentRepository _studentRepository;

        public StudentService(IStudentRepository studentRepository)
        {
            _studentRepository = studentRepository;
        }

        public async Task<IEnumerable<Student>> GetAllStudentsAsync()
        {
            return await _studentRepository.GetAllStudentsAsync();
        }

        public async Task<Student?> GetStudentByIdAsync(int id)
        {
            return await _studentRepository.GetStudentByIdAsync(id);
        }

        public async Task<Student?> GetStudentByCodeAsync(string studentCode)
        {
            return await _studentRepository.GetStudentByCodeAsync(studentCode);
        }

        public async Task<Student> CreateStudentAsync(StudentViewModel viewModel)
        {
            // Kiểm tra mã học sinh đã tồn tại chưa
            if (await _studentRepository.StudentCodeExistsAsync(viewModel.StudentCode))
            {
                throw new InvalidOperationException("Mã học sinh đã tồn tại");
            }

            var student = new Student
            {
                FullName = viewModel.FullName,
                StudentCode = viewModel.StudentCode,
                DateOfBirth = viewModel.DateOfBirth,
                Gender = viewModel.Gender,
                Address = viewModel.Address,
                PhoneNumber = viewModel.PhoneNumber,
                Email = viewModel.Email,
                Class = viewModel.Class
            };

            return await _studentRepository.CreateStudentAsync(student);
        }

        public async Task<Student> UpdateStudentAsync(StudentViewModel viewModel)
        {
            var existingStudent = await _studentRepository.GetStudentByIdAsync(viewModel.StudentID);
            if (existingStudent == null)
            {
                throw new InvalidOperationException("Học sinh không tồn tại");
            }

            // Kiểm tra mã học sinh có bị trùng với học sinh khác không
            if (existingStudent.StudentCode != viewModel.StudentCode)
            {
                if (await _studentRepository.StudentCodeExistsAsync(viewModel.StudentCode))
                {
                    throw new InvalidOperationException("Mã học sinh đã tồn tại");
                }
            }

            var student = new Student
            {
                StudentID = viewModel.StudentID,
                FullName = viewModel.FullName,
                StudentCode = viewModel.StudentCode,
                DateOfBirth = viewModel.DateOfBirth,
                Gender = viewModel.Gender,
                Address = viewModel.Address,
                PhoneNumber = viewModel.PhoneNumber,
                Email = viewModel.Email,
                Class = viewModel.Class
            };

            return await _studentRepository.UpdateStudentAsync(student);
        }

        public async Task<bool> DeleteStudentAsync(int id)
        {
            return await _studentRepository.DeleteStudentAsync(id);
        }

        public StudentViewModel MapToViewModel(Student student)
        {
            return new StudentViewModel
            {
                StudentID = student.StudentID,
                FullName = student.FullName,
                StudentCode = student.StudentCode,
                DateOfBirth = student.DateOfBirth,
                Gender = student.Gender,
                Address = student.Address,
                PhoneNumber = student.PhoneNumber,
                Email = student.Email,
                Class = student.Class,
                CreatedAt = student.CreatedAt,
                UpdatedAt = student.UpdatedAt,
                IsActive = student.IsActive
            };
        }
    }
}
