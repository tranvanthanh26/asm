using System.ComponentModel.DataAnnotations;

namespace SIMSWebApp.Models
{
    public class StudentViewModel
    {
        public int StudentID { get; set; }
        
        [Required(ErrorMessage = "Họ tên là bắt buộc")]
        [Display(Name = "Họ tên")]
        [StringLength(100, ErrorMessage = "Họ tên không được vượt quá 100 ký tự")]
        public string FullName { get; set; } = null!;
        
        [Required(ErrorMessage = "Mã học sinh là bắt buộc")]
        [Display(Name = "Mã học sinh")]
        [StringLength(20, ErrorMessage = "Mã học sinh không được vượt quá 20 ký tự")]
        public string StudentCode { get; set; } = null!;
        
        [Required(ErrorMessage = "Ngày sinh là bắt buộc")]
        [Display(Name = "Ngày sinh")]
        [DataType(DataType.Date)]
        public DateTime DateOfBirth { get; set; } = DateTime.Now.AddYears(-18);
        
        [Required(ErrorMessage = "Giới tính là bắt buộc")]
        [Display(Name = "Giới tính")]
        public string Gender { get; set; } = null!;
        
        [Required(ErrorMessage = "Địa chỉ là bắt buộc")]
        [Display(Name = "Địa chỉ")]
        [StringLength(200, ErrorMessage = "Địa chỉ không được vượt quá 200 ký tự")]
        public string Address { get; set; } = null!;
        
        [Required(ErrorMessage = "Số điện thoại là bắt buộc")]
        [Display(Name = "Số điện thoại")]
        [Phone(ErrorMessage = "Số điện thoại không hợp lệ")]
        [StringLength(15, ErrorMessage = "Số điện thoại không được vượt quá 15 ký tự")]
        public string PhoneNumber { get; set; } = null!;
        
        [Display(Name = "Email")]
        [EmailAddress(ErrorMessage = "Email không hợp lệ")]
        [StringLength(100, ErrorMessage = "Email không được vượt quá 100 ký tự")]
        public string? Email { get; set; }
        
        [Required(ErrorMessage = "Lớp là bắt buộc")]
        [Display(Name = "Lớp")]
        [StringLength(20, ErrorMessage = "Lớp không được vượt quá 20 ký tự")]
        public string Class { get; set; } = null!;
        
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public bool IsActive { get; set; } = true;
    }
}
