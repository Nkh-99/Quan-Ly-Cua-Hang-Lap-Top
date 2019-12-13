using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity.Core.Objects;
using System.Data;

namespace QuanLy002.BS_Layer
{
    static class BLNhanVien
    {
        public static DataTable SearchEmployee(string ten, string gt, string dchi, string dt)
        {
            DataTable d = EmployeeTable();
            foreach(var em in QuanlyContext.MyContext.sp_SearchEmployee(ten, gt, dchi, dt).ToList())
            {
                d.Rows.Add(em.MaNV, em.TenNV, em.Gioitinh, em.Ngaysinh, em.Dienthoai, em.Diachi);
            }
            return d;
        }
        public static DataTable EmployeeTable()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add(nameof(NhanVien.MaNV));
            dt.Columns.Add("Ten Nhan Vien");
            dt.Columns.Add("Nam/Nu");
            dt.Columns.Add("DOB:");
            dt.Columns.Add("Phone:");
            dt.Columns.Add("Address:");
            return dt;
        }
        public static DataTable GetEmployee()
        {
            DataTable d = EmployeeTable();
            foreach(var em in QuanlyContext.MyContext.NhanViens.ToList())
            {
                d.Rows.Add(em.MaNV, em.TenNV, em.Gioitinh, em.Ngaysinh, em.Dienthoai, em.Diachi);
            }
            return d;
        }
        public static bool InsertEmployee(string Ten, DateTime ngaysinh, string Dchi, string Phone, bool Gioitinh, ref string err)
        {
            ObjectParameter res = new ObjectParameter("Result", "");
            try
            {
                string men =  "Men";  ///  mac dinh la con trai
                if (Gioitinh == !true)
                    men = "Women";
                QuanlyContext.MyContext.sp_CreateMaNV(res);
                NhanVien employee = new NhanVien
                {
                    MaNV = res.Value.ToString(),
                    TenNV = Ten,
                    Ngaysinh = ngaysinh,
                    Diachi = Dchi,
                    Dienthoai = Phone,
                    Gioitinh = men
                };
                QuanlyContext.MyContext.NhanViens.Add(employee);
                QuanlyContext.MyContext.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                err = ex.Message;
                return !true;
            }
        }

        public static bool UpdateEmployee(string MaNV, string Ten, DateTime ngaysinh, string Dchi, string Phone, bool Gioitinh, ref string err)
        {
            NhanVien employee = QuanlyContext.MyContext.NhanViens.Find(MaNV);
            employee.TenNV = Ten;
            employee.Ngaysinh = ngaysinh;
            employee.Diachi = Dchi;
            employee.Dienthoai = Phone;
            string men = "Men";  ///  mac dinh la con trai
            if (Gioitinh == !true)
                men = "Women";
            employee.Gioitinh = men;
            try
            {
                QuanlyContext.MyContext.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                err = ex.Message;
                return !true;
            }
        }

        public static bool DeleteEmployee(string MaNV, ref string err)
        {
            NhanVien employee = QuanlyContext.MyContext.NhanViens.Find(MaNV);
            QuanlyContext.MyContext.NhanViens.Remove(employee);
            try
            {
                QuanlyContext.MyContext.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                err = ex.Message;
                return !true;
            }
        }
    }
}
