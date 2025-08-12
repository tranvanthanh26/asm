# Hướng dẫn sử dụng chức năng CRUD cho Student

## Tổng quan
Chức năng CRUD (Create, Read, Update, Delete) cho Student đã được tích hợp vào hệ thống SIMS Web App, sử dụng SQL Server Management Studio 20 làm cơ sở dữ liệu.

## Các tính năng chính

### 1. Thêm học sinh mới (Create)
- **Đường dẫn**: `/Student/Create`
- **Chức năng**: 
  - Nhập thông tin học sinh mới
  - Tự động tạo mã học sinh từ họ tên
  - Validation dữ liệu đầu vào
  - Kiểm tra mã học sinh không trùng lặp

### 2. Xem danh sách học sinh (Read)
- **Đường dẫn**: `/Student/Index`
- **Chức năng**:
  - Hiển thị danh sách tất cả học sinh
  - Sắp xếp theo tên
  - Tìm kiếm và lọc dữ liệu
  - Phân trang với DataTables
  - Các nút thao tác: Xem, Sửa, Xóa

### 3. Xem chi tiết học sinh (Read)
- **Đường dẫn**: `/Student/Details/{id}`
- **Chức năng**:
  - Hiển thị đầy đủ thông tin học sinh
  - Nút chỉnh sửa nhanh
  - Thông tin ngày tạo và cập nhật

### 4. Chỉnh sửa học sinh (Update)
- **Đường dẫn**: `/Student/Edit/{id}`
- **Chức năng**:
  - Chỉnh sửa thông tin học sinh
  - Validation dữ liệu
  - Kiểm tra mã học sinh không trùng lặp
  - Cập nhật thời gian sửa đổi

### 5. Xóa học sinh (Delete)
- **Đường dẫn**: `/Student/Delete/{id}`
- **Chức năng**:
  - Xóa mềm (soft delete) - chỉ đánh dấu không hoạt động
  - Xác nhận trước khi xóa
  - Không xóa dữ liệu thực sự khỏi database

## Cấu trúc cơ sở dữ liệu

### Bảng Students
```sql
CREATE TABLE Students (
    StudentID int IDENTITY(1,1) PRIMARY KEY,
    FullName nvarchar(100) NOT NULL,
    StudentCode nvarchar(20) NOT NULL UNIQUE,
    DateOfBirth date NOT NULL,
    Gender nvarchar(10) NOT NULL,
    Address nvarchar(200) NOT NULL,
    PhoneNumber nvarchar(15) NOT NULL,
    Email nvarchar(100) NULL,
    Class nvarchar(20) NOT NULL,
    CreatedAt datetime2(7) NOT NULL DEFAULT GETDATE(),
    UpdatedAt datetime2(7) NULL,
    IsActive bit NOT NULL DEFAULT 1
)
```

### Indexes
- `IX_Students_StudentCode`: Unique index cho mã học sinh
- `IX_Students_FullName`: Index cho tên học sinh
- `IX_Students_Class`: Index cho lớp

## Cài đặt và chạy

### 1. Tạo cơ sở dữ liệu
1. Mở SQL Server Management Studio 20
2. Kết nối đến SQL Server instance
3. Chạy script `Database/Scripts/CreateStudentsTable.sql`
4. Kiểm tra bảng Students đã được tạo thành công

### 2. Chạy ứng dụng
1. Mở project trong Visual Studio
2. Build project
3. Chạy ứng dụng
4. Truy cập `/Student` để sử dụng chức năng

### 3. Kiểm tra kết nối database
- Kiểm tra connection string trong `appsettings.json`
- Đảm bảo SQL Server đang chạy
- Kiểm tra quyền truy cập database

## Cấu trúc code

### Models
- `StudentViewModel`: ViewModel cho form và hiển thị
- `Student`: Entity model

### Controllers
- `StudentController`: Xử lý các request HTTP

### Services
- `StudentService`: Business logic cho Student

### Repositories
- `IStudentRepository`: Interface định nghĩa các phương thức
- `StudentRepository`: Implementation của repository

### Views
- `Index.cshtml`: Danh sách học sinh
- `Create.cshtml`: Form thêm mới
- `Edit.cshtml`: Form chỉnh sửa
- `Details.cshtml`: Hiển thị chi tiết

## Validation và bảo mật

### Validation
- Họ tên: Bắt buộc, tối đa 100 ký tự
- Mã học sinh: Bắt buộc, tối đa 20 ký tự, unique
- Ngày sinh: Bắt buộc, định dạng date
- Giới tính: Bắt buộc, chọn từ danh sách
- Địa chỉ: Bắt buộc, tối đa 200 ký tự
- Số điện thoại: Bắt buộc, định dạng phone, tối đa 15 ký tự
- Email: Không bắt buộc, định dạng email, tối đa 100 ký tự
- Lớp: Bắt buộc, tối đa 20 ký tự

### Bảo mật
- CSRF protection với `[ValidateAntiForgeryToken]`
- Input validation và sanitization
- Soft delete để bảo toàn dữ liệu

## Tính năng bổ sung

### Tự động tạo mã học sinh
- Khi nhập họ tên, hệ thống tự động tạo mã học sinh
- Format: [Chữ cái đầu họ][Chữ cái đầu tên][Năm][Số thứ tự]

### DataTables integration
- Tìm kiếm và lọc dữ liệu
- Phân trang
- Sắp xếp theo cột
- Hỗ trợ tiếng Việt

### Responsive design
- Giao diện thân thiện với mobile
- Bootstrap 5 components
- Font Awesome icons

## Xử lý lỗi

### Các loại lỗi thường gặp
1. **Lỗi kết nối database**: Kiểm tra connection string và SQL Server
2. **Lỗi validation**: Kiểm tra dữ liệu đầu vào
3. **Lỗi duplicate key**: Mã học sinh đã tồn tại
4. **Lỗi không tìm thấy**: Học sinh không tồn tại

### Logging
- Lỗi được log vào console
- Sử dụng try-catch để xử lý exception
- Hiển thị thông báo lỗi thân thiện với người dùng

## Mở rộng tính năng

### Có thể thêm
- Import/Export Excel
- Tìm kiếm nâng cao
- Báo cáo thống kê
- Upload ảnh học sinh
- Quản lý điểm số
- Quản lý lớp học

### API endpoints
- RESTful API cho mobile app
- JSON response format
- Authentication và authorization

## Hỗ trợ

Nếu gặp vấn đề, vui lòng kiểm tra:
1. Connection string trong `appsettings.json`
2. SQL Server service đang chạy
3. Database SIMS đã được tạo
4. Bảng Students đã được tạo với script SQL
5. Logs trong console của ứng dụng
