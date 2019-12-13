using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity.Core.Objects;
using System.Data;
using System.Windows.Forms;

namespace QuanLy002.BS_Layer
{
    static class BLHang
    {
        public static DataTable SearchItems(string tenHang, string mancc, string solg, string gia, string ghichu)
        {
            int MaNCC = Convert.ToInt32(mancc);
            double? SOLG; double SOLG1;
            if (double.TryParse(solg, out SOLG1) == false)
            {
                SOLG = null;
            }
            else SOLG = SOLG1;
            double? GIA; double GIA1;
            if (double.TryParse(gia, out GIA1) == false)
            {
                GIA = null;
            }
            else GIA = GIA1;
            
            DataTable d = ItemsTable();
            foreach(var items in QuanlyContext.MyContext.sp_SearchItems(tenHang, MaNCC, SOLG, GIA, ghichu).ToList())
            {
                d.Rows.Add(items.MaHang, items.TenHang, items.MaNCC, items.Soluong, items.GiaNhap, items.GiaBan, items.Ghichu, items.Anh);
            }
            return d;
        }
        public static Hang GetItemsById(string MaHang)
        {
            return QuanlyContext.MyContext.fn_GetItemsById(MaHang).FirstOrDefault();
        }
        public static void CBoxItems(ComboBox cb)
        {
            cb.DisplayMember = nameof(Hang.TenHang);
            cb.ValueMember = nameof(Hang.MaHang);
            cb.DataSource = QuanlyContext.MyContext.Hangs.ToList();
        }
        public static DataTable ItemsTable()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("Ma SP");
            dt.Columns.Add("Ten San Pham");
            dt.Columns.Add("NhaCungCap");
            dt.Columns.Add("So luong");
            dt.Columns.Add("Gia Nhap");
            dt.Columns.Add("Gia Ban");
            dt.Columns.Add("Note:");
            dt.Columns.Add("Image:");
            return dt;
        } 
        public static DataTable GetItems()
        {
            DataTable d = ItemsTable();
            foreach(var items in QuanlyContext.MyContext.Hangs.ToList())
            {
                d.Rows.Add(items.MaHang, items.TenHang, items.MaNCC, items.Soluong, items.GiaNhap, items.GiaBan, items.Ghichu, items.Anh);
            }
            return d;
        }        
        public static bool InsertProduct(string name, int VendorID, double quantity, double GiaNhap, string Image, string Note, ref string err)
        {
            ObjectParameter res = new ObjectParameter("Result", "");
            try
            {
                QuanlyContext.MyContext.sp_CreateMaHang(res);
                Hang items = new Hang
                {
                    MaHang = res.Value.ToString(),
                    TenHang = name,
                    MaNCC = VendorID,
                    Soluong = quantity,
                    GiaNhap = GiaNhap,
                    Anh= Image,
                    Ghichu = Note
                };
                QuanlyContext.MyContext.Hangs.Add(items);
                QuanlyContext.MyContext.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                err = ex.Message;
                return !true;
            }
        }
        public static bool UpdateProduct(string ID, string name, int VendorID, double quantity, double GiaNhap, string Image, string Note, ref string err)
        {
            Hang items = QuanlyContext.MyContext.Hangs.Find(ID);
            items.TenHang = name;
            items.MaNCC = VendorID;
            items.Soluong = quantity;
            items.GiaNhap = GiaNhap;
            items.Anh = Image;
            items.Ghichu = Note;
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
        public static bool DeleteProduct(string ID, ref string errr)
        {
            Hang items = QuanlyContext.MyContext.Hangs.Find(ID);
            QuanlyContext.MyContext.Hangs.Remove(items);
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
