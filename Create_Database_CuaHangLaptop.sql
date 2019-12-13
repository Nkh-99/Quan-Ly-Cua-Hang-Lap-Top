use master;
go

if DB_ID('Quanlybanhang001') is not null
	drop database Quanlybanhang001;
go

create database Quanlybanhang001;
go

use Quanlybanhang001;
go

alter table ChitietHD check
	constraint FK_ChitietHDs_Hang;
GO
alter table ChitietHD drop
	constraint FK_ChitietHDs_Hang;
GO
alter table ChitietHD check
	constraint FK_ChitietHDs_HoaDon
GO
alter table ChitietHD drop
	constraint FK_ChitietHDs_HoaDon
GO
alter table Hang check
	constraint FK_Hangs_NhaCC
GO
alter table Hang drop
	constraint FK_Hangs_NhaCC
GO
alter table HoaDon check 
	constraint FK_HoaDons_Khach
GO
alter table HoaDon drop 
	constraint FK_HoaDons_Khach
GO
alter table HoaDon check 
	constraint FK_HoaDons_NhanVien
GO
alter table HoaDon drop 
	constraint FK_HoaDons_NhanVien
GO
alter table Login check 
	constraint FK_Login_NhanVien
GO
alter table Login drop
	constraint FK_Login_NhanVien
GO

if OBJECT_ID('Hang') is not null
	drop table Hang;

create table Hang(
		MaHang nvarchar(50) not null,
		TenHang nvarchar(50) not null,
		MaNCC int not null,
		Soluong float not null,
		GiaNhap float not null,
		GiaBan float not null,
		Anh nvarchar (150) not null,
		Ghichu nvarchar(150) not null,
		primary key (Mahang),
		check (Soluong >= 0),
		);
GO

if OBJECT_ID('HoaDon') is not null
	drop table HoaDon;

create table HoaDon(
		MaHD nvarchar(50) not null,
		MaNV nvarchar(10) not null,
		Ngayban datetime not null,
		MaKH nvarchar(10) not null,
		Tongtien float not null,
		primary key (MaHD),
		check (Tongtien > 0)
		);
GO

if OBJECT_ID('ChitietHD') is not null
	drop table ChitietHD;

create table ChitietHD(
		MaHD nvarchar(50) not null,
		MaHang nvarchar(50) not null,
		Soluong float not null,
		Dongia float not null,
		Giamgia float not null,
		Thanhtien float not null,
		primary key (MaHD,MaHang),
		check (Soluong > 0),
		check (Dongia > 0),
		check (Giamgia < 100),
		check (Thanhtien > 0)
		);
GO

if OBJECT_ID('Khach') is not null
	drop table Khach;

create table Khach(
		MaKH nvarchar(10) not null,
		TenKH nvarchar(50) not null,
		Diachi nvarchar(50) not null,
		Dienthoai nvarchar(50) not null,
		primary key (MaKH)
		);
GO

if OBJECT_ID('Login') is not null
	drop table Login
create table Login(
		tendangnhap nvarchar(30) not null,
		matkhau nvarchar(30) not null,
		loainguoidung  int not null,
		MaNV nvarchar(10) ,
		primary key (tendangnhap)
		);
GO

if OBJECT_ID('NhaCC') is not null
	drop table NhaCC

create table NhaCC(
		MaNCC int identity,
		TenNCC nvarchar(50) not null
		primary key (MaNCC)
		);
GO

if OBJECT_ID('NhanVien') is not null
	drop table NhanVien

create table NhanVien(
		MaNV nvarchar(10) not null,
		TenNV nvarchar(50) not null,
		Gioitinh nvarchar(10) not null,
		Diachi nvarchar(50) not null,
		Dienthoai nvarchar(15) not null,
		Ngaysinh datetime not null,
		primary key (MaNV)
		);
GO

alter table ChitietHD 
	with check add constraint FK_ChitietHDs_Hang 
	foreign key (MaHang) references Hang (MaHang);
GO

alter table ChitietHD
	check constraint FK_ChitietHDs_Hang
GO
alter table ChitietHD
	with check add constraint FK_ChitietHDs_HoaDon
	foreign key (MaHD) references HoaDon (MaHD)
GO
alter table ChitietHD
	check constraint FK_ChitietHDs_HoaDon
GO
alter table Hang with check add constraint FK_Hangs_NhaCC
	foreign key (MaNCC) references NhaCC (MaNCC)
GO
alter table Hang check constraint FK_Hangs_NhaCC
GO
alter table HoaDon with  check add constraint Fk_HoaDons_Khach
	foreign key (MaKH) references Khach (MaKH)
GO
alter table HoaDon check constraint FK_HoaDons_Khach
GO
alter table HoaDon with check add constraint FK_HoaDons_NhanVien
	foreign key (MaNV) references NhanVien (MaNV)
GO
alter table HoaDon check constraint FK_HoaDons_NhanVien
GO
alter table Login with check add constraint FK_Login_NhanVien
	foreign key (MaNV) references NhanVien (MaNV)
GO
alter table Login check constraint FK_Login_NhanVien
GO
use Quanlybanhang001;
GO
If EXISTS(Select name from sys.indexes
			Where name = N'IX_Login_MaNV')
	drop index IX_Login_MaNV ON Login;
GO
create unique index IX_Login_MaNV on Login (MaNV)
GO
USE [master]
GO
ALTER DATABASE [Quanlybanhang001] SET  READ_WRITE 
GO