using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity.Core.Objects;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuanLy002.BS_Layer
{
    static class BLKhachHang
    {
        public static DataTable SearchCustomer(string name, string address, string phone)
        {
            DataTable d = CustomerTable();
            foreach(var item in QuanlyContext.MyContext.sp_SearchCustomer(name, address, phone).ToList())
            {
                d.Rows.Add(item.MaKH, item.TenKH, item.Diachi, item.Dienthoai);
            }
            return d;
        }
        public static Khach GetKhachById(string MaKH)
        {
            return QuanlyContext.MyContext.fn_GetKhachById(MaKH).FirstOrDefault();
        }
        public static void CBoxCustomer(ComboBox cb)
        {
            cb.DisplayMember = nameof(Khach.TenKH);
            cb.ValueMember = nameof(Khach.MaKH);
            cb.DataSource = QuanlyContext.MyContext.Khaches.ToList();
        }
        public static DataTable CustomerTable()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("ID");
            dt.Columns.Add("Name");
            dt.Columns.Add("Address");
            dt.Columns.Add("Phone");
            return dt;
        }
        public static DataTable GetCustomer()
        {
            DataTable d = CustomerTable();
            foreach(var item in QuanlyContext.MyContext.Khaches.ToList())
            {
                d.Rows.Add(item.MaKH, item.TenKH, item.Diachi, item.Dienthoai);
            }
            return d;
        }

        public static bool InsertKH(string TenKH, string Dchi, string SDT, ref string errr)
        {
            ObjectParameter ResultParameter = new ObjectParameter("Result", "");            
            try
            {
                QuanlyContext.MyContext.sp_CreateIDKhach(ResultParameter);
                Khach khach = new Khach { MaKH = ResultParameter.Value.ToString(), TenKH = TenKH, Diachi = Dchi, Dienthoai = SDT };
                QuanlyContext.MyContext.Khaches.Add(khach);
                QuanlyContext.MyContext.SaveChanges();
                return true;
            }
            catch(Exception ex)
            {
                errr = ex.Message;
                return !true;
            }
        }

        public static bool UpdateKH(string MaKH, string TenKH, string Dchi, string SDT, ref string errr)
        {
            Khach temp = QuanlyContext.MyContext.Khaches.Find(MaKH);
            temp.TenKH = TenKH;
            temp.Diachi = Dchi;
            temp.Dienthoai = SDT;
            try
            {
                QuanlyContext.MyContext.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                errr = ex.Message;
                return !true;
            }
        }

        public static bool DeleteKH(string MaKH, ref string errr)
        {
            Khach temp = QuanlyContext.MyContext.Khaches.Find(MaKH);
            QuanlyContext.MyContext.Khaches.Remove(temp);
            try
            {
                QuanlyContext.MyContext.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                errr = ex.Message;
                return !true;
            }
        }
    }
}
