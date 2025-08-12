-- Script tạo bảng Students cho SIMS Database
-- Chạy script này trong SQL Server Management Studio 20

USE [SIMS]
GO

-- Kiểm tra và xóa bảng nếu đã tồn tại
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Students]') AND type in (N'U'))
BEGIN
    DROP TABLE [dbo].[Students]
    PRINT 'Đã xóa bảng Students cũ'
END
GO

-- Tạo bảng Students
CREATE TABLE [dbo].[Students](
    [StudentID] [int] IDENTITY(1,1) NOT NULL,
    [FullName] [nvarchar](100) NOT NULL,
    [StudentCode] [nvarchar](20) NOT NULL,
    [DateOfBirth] [date] NOT NULL,
    [Gender] [nvarchar](10) NOT NULL,
    [Address] [nvarchar](200) NOT NULL,
    [PhoneNumber] [nvarchar](15) NOT NULL,
    [Email] [nvarchar](100) NULL,
    [Class] [nvarchar](20) NOT NULL,
    [CreatedAt] [datetime2](7) NOT NULL DEFAULT (GETDATE()),
    [UpdatedAt] [datetime2](7) NULL,
    [IsActive] [bit] NOT NULL DEFAULT (1),
    CONSTRAINT [PK_Students] PRIMARY KEY CLUSTERED ([StudentID] ASC)
)
GO

-- Tạo index unique cho StudentCode
CREATE UNIQUE NONCLUSTERED INDEX [IX_Students_StudentCode] ON [dbo].[Students]
(
    [StudentCode] ASC
) WHERE ([IsActive] = 1)
GO

-- Tạo index cho FullName để tìm kiếm nhanh
CREATE NONCLUSTERED INDEX [IX_Students_FullName] ON [dbo].[Students]
(
    [FullName] ASC
) WHERE ([IsActive] = 1)
GO

-- Tạo index cho Class để lọc theo lớp
CREATE NONCLUSTERED INDEX [IX_Students_Class] ON [dbo].[Students]
(
    [Class] ASC
) WHERE ([IsActive] = 1)
GO

-- Thêm dữ liệu mẫu
INSERT INTO [dbo].[Students] ([FullName], [StudentCode], [DateOfBirth], [Gender], [Address], [PhoneNumber], [Email], [Class])
VALUES 
    (N'Nguyễn Văn An', 'AN2024001', '2006-05-15', N'Nam', N'123 Đường ABC, Quận 1, TP.HCM', '0901234567', 'an.nguyen@email.com', N'10A1'),
    (N'Trần Thị Bình', 'BI2024002', '2006-08-22', N'Nữ', N'456 Đường XYZ, Quận 2, TP.HCM', '0901234568', 'binh.tran@email.com', N'10A1'),
    (N'Lê Văn Cường', 'CU2024003', '2006-03-10', N'Nam', N'789 Đường DEF, Quận 3, TP.HCM', '0901234569', 'cuong.le@email.com', N'10A2'),
    (N'Phạm Thị Dung', 'DU2024004', '2006-11-05', N'Nữ', N'321 Đường GHI, Quận 4, TP.HCM', '0901234570', 'dung.pham@email.com', N'10A2'),
    (N'Hoàng Văn Em', 'EM2024005', '2006-07-18', N'Nam', N'654 Đường JKL, Quận 5, TP.HCM', '0901234571', 'em.hoang@email.com', N'10A3'),
    (N'Vũ Thị Phương', 'PH2024006', '2006-09-30', N'Nữ', N'987 Đường MNO, Quận 6, TP.HCM', '0901234572', 'phuong.vu@email.com', N'10A3'),
    (N'Đặng Văn Giang', 'GI2024007', '2006-01-12', N'Nam', N'147 Đường PQR, Quận 7, TP.HCM', '0901234573', 'giang.dang@email.com', N'10A4'),
    (N'Bùi Thị Hoa', 'HO2024008', '2006-04-25', N'Nữ', N'258 Đường STU, Quận 8, TP.HCM', '0901234574', 'hoa.bui@email.com', N'10A4'),
    (N'Ngô Văn Inh', 'IN2024009', '2006-06-08', N'Nam', N'369 Đường VWX, Quận 9, TP.HCM', '0901234575', 'inh.ngo@email.com', N'10A5'),
    (N'Lý Thị Kim', 'KI2024010', '2006-12-14', N'Nữ', N'741 Đường YZA, Quận 10, TP.HCM', '0901234576', 'kim.ly@email.com', N'10A5')
GO

-- Hiển thị thông tin bảng đã tạo
SELECT 
    TABLE_NAME,
    COLUMN_NAME,
    DATA_TYPE,
    IS_NULLABLE,
    COLUMN_DEFAULT
FROM INFORMATION_SCHEMA.COLUMNS 
WHERE TABLE_NAME = 'Students'
ORDER BY ORDINAL_POSITION
GO

-- Hiển thị dữ liệu mẫu
SELECT * FROM [dbo].[Students] ORDER BY [StudentID]
GO

PRINT 'Đã tạo thành công bảng Students với dữ liệu mẫu!'
GO
