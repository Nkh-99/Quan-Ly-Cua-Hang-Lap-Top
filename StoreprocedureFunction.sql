use Quanlybanhang001;
GO

if OBJECT_ID('spLoginAccount') is not null 
	drop procedure spLoginAccount;
GO
create proc spLoginAccount
		@TK as nvarchar(30) NULL,
		@MK as nvarchar(30) NULL,
		@Loai as int NULL OUTPUT,
		@MaNV as nvarchar(10) NULL OUTPUT,
		@Result int OUT
as
Begin
	select @Result = count(*)
	from dbo.Login
	where tendangnhap = @TK and matkhau = @MK
	if (@Result = 1)
		begin
			select @Loai = loainguoidung, @MaNV = MaNV
			from dbo.Login
			where tendangnhap = @TK			
		end
End

--declare @l int;
--declare @mnv nvarchar(10);
--declare @result int;
--exec @result = spLoginAccount 'a', 'a', @l output, @mnv output;

GO
if OBJECT_ID('sp_GetUserByEmployeeId') is not null
	 drop proc sp_GetUserByEmployeeId;
GO
create proc sp_GetUserByEmployeeId
			@MaNV as nvarchar(10),
			@Result as nvarchar(30) OUT
AS
Begin
	(select @Result = tendangnhap from Login where MaNV = @MaNV);
END
GO

if OBJECT_ID('sp_ChangePassword') is not null
	drop proc sp_ChangePassword;
GO

create proc sp_ChangePassword
		@User nvarchar(30),
		@Pass nvarchar(30)
AS
Begin
	Update [dbo].Login set matkhau = @Pass where tendangnhap = @User
End
------Vendor

if OBJECT_ID('View_VendorsGeneral') is not null
	drop view View_VendorsGeneral;
GO

create view View_VendorsGeneral
as
select * 
from NhaCC

GO
if OBJECT_ID(N'sp_SearchVendor', N'P') is not null
	drop proc sp_SearchVendor;
GO

create proc sp_SearchVendor
		@VendorName nvarchar(50)
as
declare @sql as nvarchar(1000);
set @sql = N'Select * from NhaCC where 1 = 1 ' + 
	case when @VendorName is not null then N'and TenNCC Like ''%'' + @VendorName + ''%'''
	else N'' END
print @sql
EXEC sp_executesql
		@stmt = @sql,
		@params = N' @VendorName as nvarchar(50)',
		@VendorName = @VendorName;
GO

if OBJECT_ID('spInsertNhaCC') is not null
	drop procedure spInsertNhaCC;
Go

create proc spInsertNhaCC
	@TenNCC as nvarchar(50) NULL
AS
Begin
	Insert into dbo.NhaCC(TenNCC) values (@TenNCC);
END
GO

if OBJECT_ID('sp_UpdateNhaCC') is not null
	drop proc sp_UpdateNhaCC;
GO

create proc sp_UpdateNhaCC
		@Id int,
		@TenNCC as nvarchar(50) NULL
AS
Begin
	Update [dbo].[NhaCC] 
    set TenNCC = @TenNCC
    where MaNCC = @Id;
END
GO

if OBJECT_ID('sp_DeleteNhaCC') is not null
	drop proc sp_DeleteNhaCC;
GO

Create proc sp_DeleteNhaCC
		@Id int
AS
Begin
	DELETE FROM [dbo].[NhaCC]
    where MaNCC = @Id
END
GO

---------------Khach Hang
if OBJECT_ID('fn_GetKhachById') is not null
	drop function fn_GetKhachById;
Go
create function fn_GetKhachById(@MaKH nvarchar(10))
Returns table
	Return (select * from [dbo].Khach where [dbo].Khach.MaKH =  @MaKH);
GO
if OBJECT_ID('sp_CreateIDKhach') is not null
	drop proc sp_CreateIDKhach;
GO
create proc sp_CreateIDKhach
	@Result as nvarchar(10) NULL OUT
AS
Begin
	declare @a nvarchar(10);
	select @a
	declare @b int;
	select top 1 @a = MaKH from Khach order by MaKH desc
	if (@a is null)
		Begin
			Set @Result = N'KH0000001';
		End
	else
		Begin
			set @b = cast (RIGHT(@a, 7) as int)
			if (LEN(@b + 1) = LEN(@b))
				select @Result = LEFT(@a, 9 - LEN(@b)) + cast(@b + 1 as nvarchar);
			else
				select @Result = LEFT(@a, 9 - LEN(@b) -1) + cast(@b + 1 as nvarchar);
		End
END
GO

if OBJECT_ID(N'sp_SearchCustomer', N'P') is not null
	drop proc sp_SearchCustomer;
GO
create proc sp_SearchCustomer
		@Name nvarchar(50) NULL,
		@Address nvarchar(50) NULL,
		@Phone nvarchar(50) NULL
AS
Declare @sql as nvarchar(1000);
Set @sql = N'select * from [dbo].Khach where 1=1 ' +
	case when @Name is not null then N'AND TenKH like ''%''+@name+ ''%''' ELSE N'' END
	+ case when @Address is not null then N'AND Diachi like ''%''+@address+ ''%''' ELSE N'' END
	+ case when @Phone is not null then N'AND Dienthoai like ''%''+@phone+ ''%''' ELSE N'' END
exec sp_executesql @stmt = @sql,
					@params = N'@name as nvarchar(50), @address as nvarchar(50), @phone as nvarchar(50)',
					@name = @Name,
					@address = @Address,
					@phone = @Phone
GO

if OBJECT_ID('sp_InsertKhach') is not null
	drop procedure sp_InsertKhach;
Go

create proc sp_InsertKhach
	@MaKH as nvarchar(10),
	@TenKH as nvarchar(50) NULL,
	@Dchi as nvarchar(50) NULL,
	@SDT as nvarchar(50) NULL
AS
Begin
	Insert into dbo.Khach values (@MaKH, @TenKH, @Dchi, @SDT);
END
GO

if OBJECT_ID('sp_UpdateKhach') is not null
	drop proc sp_UpdateKhach;
GO

create proc sp_UpdateKhach
		@Id nvarchar(10),
		@TenKH as nvarchar(50) NULL,
		@Dchi as nvarchar(50) NULL,
		@SDT as nvarchar(50) NULL
AS
Begin
	Update [dbo].[Khach] 
    set TenKH = @TenKH, Diachi = @Dchi, Dienthoai = @SDT
    where MaKH = @Id;
END
GO

if OBJECT_ID('sp_DeleteKhach') is not null
	drop proc sp_DeleteKhach;
GO

Create proc sp_DeleteKhach
		@Id nvarchar(10)
AS
Begin
	DELETE FROM [dbo].[Khach]
    where MaKH = @Id
END
GO
------Nhan Vien

if OBJECT_ID('sp_CreateMaNV') is not null
	drop proc sp_CreateMaNV;
GO
create proc sp_CreateMaNV
	@Result as nvarchar(10) NULL OUT
AS
Begin
	declare @a nvarchar(10);
	select @a
	declare @b int;
	select top 1 @a = MaNV from NhanVien order by MaNV desc
	if (@a is null)
		Begin
			Set @Result = N'EM0000001';
		End
	else
		Begin
			set @b = cast (RIGHT(@a, 7) as int)
			if (LEN(@b + 1) = LEN(@b))
				select @Result = LEFT(@a, 9 - LEN(@b)) + cast(@b + 1 as nvarchar);
			else
				select @Result = LEFT(@a, 9 - LEN(@b) -1) + cast(@b + 1 as nvarchar);
		End
END
GO
if OBJECT_ID('sp_SearchEmployee', N'P') is not null
	drop proc sp_SearchEmployee;
GO
create proc sp_SearchEmployee
			@Ten nvarchar(50) NULL,
			@Gioitinh nvarchar(10) NULL,
			@Diachi nvarchar(50) NULL,
			@Dienthoai nvarchar(50) NULL
as
declare @sql as nvarchar(1000);
Set @sql = N'select * from [dbo].NhanVien where 1=1 ' +
		case when  @Ten is not null then N'AND TenNV like ''%''+@ten+ ''%''' ELSE N'' END
		+ case when @Gioitinh is not null then N'AND Gioitinh like ''%''+@gt+ ''%''' ELSE N'' END
		+ case when @Diachi is not null then N'AND Diachi like ''%''+@dc+ ''%''' ELSE N'' END
		+ case when @Dienthoai is not  null then N'AND Dienthoai like ''%''+@dt+ ''%''' ELSE N'' END
exec sp_executesql @stmt = @sql,
							@params = N'@ten as nvarchar(50), @gt as nvarchar(10), @dc as nvarchar(50), @dt as nvarchar(50)',
							@ten = @Ten,
							@gt = @Gioitinh,
							@dc = @Diachi,
							@dt = @Dienthoai
GO
if OBJECT_ID('sp_InsertEmployee') is not null
	drop procedure sp_InsertEmployee;
Go

create proc sp_InsertEmployee
	@Ma as nvarchar(10),
	@Ten as nvarchar(50) NULL,
	@Gioitinh as nvarchar(10) NULL,
	@Dchi as nvarchar(50) NULL,
	@SDT as nvarchar(15) NULL,
	@DOB as datetime NULL
AS
Begin
	Insert into dbo.NhanVien values (@Ma, @Ten, @Gioitinh, @Dchi, @SDT, @DOB);
END
GO

if OBJECT_ID('sp_UpdateEmployee') is not null
	drop proc sp_UpdateEmployee;
GO

create proc sp_UpdateEmployee
		@Ma as nvarchar(10),
		@Ten as nvarchar(50) NULL,
		@Gioitinh as nvarchar(10) NULL,
		@Dchi as nvarchar(50) NULL,
		@SDT as nvarchar(15) NULL,
		@DOB as datetime NULL
AS
Begin
	Update [dbo].NhanVien 
    set TenNV = @Ten, Gioitinh = @Gioitinh, Diachi = @Dchi, Dienthoai = @SDT, Ngaysinh = @DOB
    where MaNV = @Ma;
END
GO

if OBJECT_ID('sp_DeleteEmployee') is not null
	drop proc sp_DeleteEmployee;
GO

Create proc sp_DeleteEmployee
		@Id nvarchar(10)
AS
Begin
	DELETE FROM [dbo].NhanVien
    where MaNV = @Id
END
GO
---Hang

if OBJECT_ID('fn_GetItemsById') is not null
	drop function fn_GetItemsById;
GO
create function fn_GetItemsById(@MaHang nvarchar(50))
Returns table
	return (select * from [dbo].Hang where [dbo].Hang.MaHang = @MaHang);
GO

if OBJECT_ID('sp_CreateMaHang') is not null
	drop proc sp_CreateMaHang;
GO
create proc sp_CreateMaHang
	@Result as nvarchar(10) NULL OUT
AS
Begin
	declare @a nvarchar(10);
	select @a
	declare @b int;
	select top 1 @a = MaHang from Hang order by MaHang desc
	if (@a is null)
		Begin
			Set @Result = N'SP0000001';
		End
	else
		Begin
			set @b = cast (RIGHT(@a, 7) as int)
			if (LEN(@b + 1) = LEN(@b))
				select @Result = LEFT(@a, 9 - LEN(@b)) + cast(@b + 1 as nvarchar);
			else
				select @Result = LEFT(@a, 9 - LEN(@b) -1) + cast(@b + 1 as nvarchar);
		End
END
GO

if OBJECT_ID('sp_SearchItems', N'P') is not null
	drop proc sp_SearchItems;
Go
create proc sp_SearchItems
			@TenHang nvarchar(50) NULL,
			@MaNCC int NULL,
			@Soluong float NULL,
			@GiaBan float NULL,
			@Ghichu nvarchar(150)
AS
declare @sql as nvarchar(1111);
set @sql =  'select * from [dbo].Hang where 1=1 ' +
		case when @TenHang is not null then N'AND TenHang like ''%''+@ten+ ''%''' ELSE N'' END
		+ case when @MaNCC is not null then N'AND MaNCC = @mancc ' ELSE N'' END
		+ case when @Soluong is not null then N'AND Soluong = @solg ' ELSE N'' END
		+ case when @GiaBan is not null then N'AND GiaBan = @gia ' ELSE N'' END
		+ case when @Ghichu is not null then N'AND Ghichu like ''%''+@note+ ''%''' ELSE N'' END
exec sp_executesql @stmt = @sql,
						@params = N'@ten as nvarchar(50), @mancc as int, @solg as float, @gia as float, @note as nvarchar(150)',
						@ten = @TenHang,
						@mancc = @MaNCC,
						@solg = @Soluong,
						@gia = @GiaBan,
						@note = @Ghichu
Go
if OBJECT_ID('sp_InsertHang') is not null
	drop procedure sp_InsertHang;
Go

create proc sp_InsertHang
	@Ma as nvarchar(10),
	@Ten as nvarchar(50) NULL,
	@Vendor as int NULL,
	@Solg as float NULL,
	@GiaNhap as float NULL,
	@Image as nvarchar(150),
	@Note as nvarchar(150)
AS
Begin
	declare @Price int = @Solg*1.1;
	Insert into dbo.Hang values (@Ma, @Ten, @Vendor, @Solg, @GiaNhap, @Price, @Image, @Note);
END
GO

if OBJECT_ID('sp_UpdateHang') is not null
	drop proc sp_UpdateHang;
GO

create proc sp_UpdateHang
		@Ma as nvarchar(10),
		@Ten as nvarchar(50) NULL,
		@Vendor as int NULL,
		@Solg as float NULL,
		@GiaNhap as float NULL,
		@Image as nvarchar(150),
		@Note as nvarchar(150)
AS
Begin
	declare @Price int = @Solg*1.1;
	Update [dbo].Hang 
    set TenHang=@Ten, MaNCC=@Vendor, Soluong=@Solg, GiaNhap=@GiaNhap, GiaBan=@Price, Anh=@Image, Ghichu=@Note
    where MaHang = @Ma;
END
GO

if OBJECT_ID('sp_DeleteHang') is not null
	drop proc sp_DeleteHang;
GO

Create proc sp_DeleteHang
		@Id nvarchar(10)
AS
Begin
	DELETE FROM [dbo].Hang
    where MaHang = @Id
END
GO

---Hoa don

if OBJECT_ID('sp_CreateInvoiceId') is not null
	drop proc sp_CreateInvoiceId;
GO
create proc sp_CreateInvoiceId
	@Result as nvarchar(50) NULL OUT
AS
Begin
	  Set @Result = N'HD' + REPLACE( convert(date, getdate()), '-', '') 
	  + REPLACE(convert(varchar(8), convert(time, getdate())) , ':', '');
END
GO

if exists (select * from sys.types where name = 'Line1Items')
	drop type Line1Items;
GO
 
 create type Line1Items as table
 (
	InvoiceId nvarchar(50) not null,
	ProductId nvarchar(50) not null,
	Quantity	float		not null,
	Price		float		not null,
	Discount	float		not null,
	Worth		float		not null
	primary key (InvoiceId, ProductId)
 );
 GO

if OBJECT_ID('sp_InsertInvoice') is not null
	drop proc sp_InsertInvoice;
Go
create proc sp_InsertInvoice
		@MaHD as nvarchar(50),		
		@MaNV as nvarchar(10),
		@MaKH as nvarchar(10),
		@InvoiceDetails dbo.Line1Items READONLY,
		@Finished as bit OUT
AS
Begin
	Begin try
		Begin tran
			Update [dbo].Hang
			Set [dbo].Hang.Soluong = Hang.Soluong - t2.Quantity 
			from @InvoiceDetails as t2
			where Hang.MaHang = t2.ProductId

			declare @Total float;
			select @Total = Sum(Worth) from @InvoiceDetails;
			Insert HoaDon values (@MaHD, @MaNV, GETDATE(), @MaKH, @Total);

			Insert into ChitietHD
			select * from @InvoiceDetails											

		Commit tran;
		set @Finished = 1;
	End try
	Begin catch
		IF (@@TRANCOUNT > 0)
		Begin
			set @Finished = 0;
			ROLLBACK;
		End
	End catch
End
Go

---test
------insert into HoaDon values ('a', 'EM0000001', GETDATE(), 'KH0000001',90);
------declare @table as Line1Items;
------insert into @table values ('ab', 'SP0000001', 1, 100, 0, 100);

------declare @test bit;
------exec sp_InsertInvoice 'ab','EM0000001', 'KH0000001', @table, @test out;
------select @test

GO
if OBJECT_ID('sp_DeleteInvoice') is not null
	drop proc sp_DeleteInvoice;
GO

create proc sp_DeleteInvoice
		@Id as nvarchar(50),
		@Finished as bit OUT
AS
Begin
	Begin try
		Begin tran
			declare @table as table(ProductId nvarchar(50), Quantity float)
			insert into @table
			select MaHang, Soluong  from ChitietHD where MaHD = @Id;

			Update [dbo].Hang
			Set [dbo].Hang.Soluong = Hang.Soluong + t2.Quantity 
			from @table as t2
			where Hang.MaHang = t2.ProductId

			delete from ChitietHD where MaHD = @Id;

			delete from HoaDon where MaHD = @Id;
		commit tran;
		set @Finished = 1;
	End try
	Begin catch
		IF @@TRANCOUNT>0
			set @Finished = 0;
		ROLLBACK;
	End catch
END
Go
--declare @test bit;
--exec sp_DeleteInvoice 'a', @test out
--select @test
if OBJECT_ID('fn_GetInvoiceById') is not null
	drop function fn_GetInvoiceById;
Go

create function fn_GetInvoiceById(@Id nvarchar(50))
	Returns Table
Return
	(select * from [dbo].HoaDon where MaHD = @Id);
GO

if OBJECT_ID('fn_GetInvoiceByEmployeeId') is not null
	drop function fn_GetInvoiceByEmployeeId;
GO
create function fn_GetInvoiceByEmployeeId(@EmployeeId nvarchar(10))
	Returns Table
Return
	(Select * from [dbo].HoaDon where MaNV = @EmployeeId);
Go

if OBJECT_ID('sp_SearchInvoice', N'P') is not null
	drop proc sp_SearchInvoice;
GO
create proc sp_SearchInvoice
		@MaHD as nvarchar(50) NULL,	
		@MaNV as nvarchar(10) NULL,
		@MaKH as nvarchar(10) NULL,
		@Month as int NULL,
		@Year as int NULL,
		@Tongtien as float NULL
AS
declare @sql as nvarchar(1000);
set @sql = 'select * from [dbo].HoaDon where 1=1 ' 		
		+ case when @Tongtien is not null then N'AND Tongtien >= @tien ' ELSE N'' END
		+ case when @MaNV is not null then N'AND MaNV like ''%''+@manv+ ''%''' ELSE N'' END
		+ case when @MaKH is not null then N'AND MaKH like ''%''+@makh+ ''%''' ELSE N'' END
		+ case when @MaHD is not null then N'AND MaHD like ''%''+@mahd+ ''%''' ELSE N'' END
		+ case when @Month is not null then N'AND DATEPART(MONTH,Ngayban) = @month ' ELSE N'' END
		+ case when @Year is not null then N'AND DATEPART(YEAR,Ngayban) = @year ' ELSE N'' END
		
exec sp_executesql @stmt = @sql,
					@params = N'@mahd as nvarchar(50), @manv as nvarchar(10), @makh as nvarchar(10), @month as int,
								@year as int, @tien as float',
					@mahd = @MaHD,
					@manv=@MaNV,
					@makh=@MaKH,
					@month=@Month,
					@year=@Year,
					@tien=@Tongtien						
GO

--- Chi tiet Hoa Don

if OBJECT_ID('fn_GetChitietHDByMaHD') is not null
	drop function fn_GetChitietHDByMaHD;
GO
create function fn_GetChitietHDByMaHD(@MaHD nvarchar(50))
	Returns Table
Return
	(select * from [dbo].ChitietHD where [dbo].ChitietHD.MaHD = @MaHD)
GO

----trigger

if OBJECT_ID('trg_AF_Insert_Employee') is not null
	drop trigger trg_AF_Insert_Employee;
GO
create trigger trg_AF_Insert_Employee
		on [dbo].NhanVien
		After INSERT
AS

declare @manv nvarchar(10);
declare @tendn nvarchar(30);
declare @Time datetime;

select top 1 @Time = inserted.Ngaysinh from inserted;
select top 1 @tendn = inserted.TenNV from inserted;
select top 1 @manv = inserted.MaNV from inserted;
set @tendn = REPLACE(@tendn,' ','') + REPLACE( convert(date, @Time), '-', '');
Insert Login values( @tendn, '543216789', 4 , @manv);
GO

if OBJECT_ID('trg_InsteadOf_Delete_Employee') is not null
	drop trigger trg_InsteadOf_Delete_Employee;
GO

create trigger trg_InsteadOf_Delete_Employee
			on [dbo].NhanVien
			Instead of Delete AS
Begin
	Begin try
		Begin tran
			declare @manv nvarchar(10);
			select @manv = MaNV from deleted;
			delete from [dbo].Login where [dbo].Login.MaNV = @manv;
			delete from [dbo].NhanVien where [dbo].NhanVien.MaNV = @manv;
		Commit tran;
	end try
	Begin catch
		IF @@TRANCOUNT>0
			ROLLBACK;
	End catch
End
GO

--exec sp_DeleteEmployee 'EM0000007'

---Index
If EXISTS(Select name from sys.indexes
			Where name = N'IX_NC_Vendor_VendorName')
	drop index IX_NC_Vendor_VendorName ON [dbo].NhaCC;
GO
create nonclustered index IX_NC_Vendor_VendorName on [dbo].NhaCC (TenNCC)
GO

If EXISTS(Select name from sys.indexes
			Where name = N'IX_NC_Customer_CustomerName')
	drop index IX_NC_Customer_CustomerName ON [dbo].Khach;
GO
create nonclustered index IX_NC_Customer_CustomerName on [dbo].Khach (TenKH, Diachi, Dienthoai)
GO
If EXISTS(Select name from sys.indexes
			Where name = N'IX_NC_Customer_CustomerPhone')
	drop index IX_NC_Customer_CustomerPhone ON [dbo].Khach;
GO
create nonclustered index IX_NC_Customer_CustomerPhone on [dbo].Khach (Dienthoai, TenKH, Diachi)
GO


If EXISTS(Select name from sys.indexes
			Where name = N'IX_NC_Employee_EmployeeName')
	drop index IX_NC_Employee_EmployeeName ON [dbo].NhanVien;
GO
create nonclustered index IX_NC_Employee_EmployeeName on [dbo].NhanVien (TenNV)
include (Gioitinh, Diachi, Dienthoai, Ngaysinh)
GO
If EXISTS(Select name from sys.indexes
			Where name = N'IX_NC_Employee_EmployeePhone')
	drop index IX_NC_Employee_EmployeePhone ON [dbo].NhanVien;
GO
create nonclustered index IX_NC_Employee_EmployeePhone on [dbo].NhanVien (Dienthoai)
include (TenNV, Gioitinh, Diachi, Ngaysinh)
GO


If EXISTS(Select name from sys.indexes
			Where name = N'IX_NC_Hang_ItemsName')
	drop index IX_NC_Hang_ItemsName ON [dbo].Hang;
GO
create nonclustered index IX_NC_Hang_ItemsName on [dbo].Hang (TenHang)
include (MaNCC, Soluong, GiaNhap, GiaBan, Anh, Ghichu)
GO
If EXISTS(Select name from sys.indexes
			Where name = N'IX_NC_Hang_VendorId')
	drop index IX_NC_Hang_VendorId ON [dbo].Hang;
GO
create nonclustered index IX_NC_Hang_VendorId on [dbo].Hang (MaNCC)
include (TenHang, Soluong, GiaNhap, GiaBan, Anh, Ghichu)
GO


if EXISTS(Select name from sys.indexes
			Where name = N'IX_NC_HoaDon_AmountTotal')
	drop index IX_NC_HoaDon_AmountTotal ON [dbo].HoaDon;
GO
create nonclustered index IX_NC_HoaDon_AmountTotal on [dbo].HoaDon (Tongtien)
include (MaNV, MaKH, Ngayban)
GO
if EXISTS(Select name from sys.indexes
			Where name = N'IX_NC_HoaDon_EmployeeId')
	drop index IX_NC_HoaDon_EmployeeId ON [dbo].HoaDon;
GO
create nonclustered index IX_NC_HoaDon_EmployeeId on [dbo].HoaDon (MaNV)
include (Tongtien, MaKH, Ngayban)
GO
if EXISTS(Select name from sys.indexes
			Where name = N'IX_NC_HoaDon_CustomerId')
	drop index IX_NC_HoaDon_CustomerId ON [dbo].HoaDon;
GO
create nonclustered index IX_NC_HoaDon_CustomerId on [dbo].HoaDon (MaKH)
include (Tongtien, MaNV, Ngayban)
GO

--exec sp_SearchInvoice NULL,NULL,'KH0000001',NULL,NULL,99
--exec sp_SearchInvoice NULL,NULL,NULL, 12, 2019, 99

--set statistics io, time on;
--go