using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Windows.Forms;

namespace QuanLy002.BS_Layer
{
    static class BLNhaCC
    {
        public static List<View_VendorsGeneral> View_Vendors()
        {
            return QuanlyContext.MyContext.View_VendorsGeneral.ToList();
        }

        public static DataTable SearchVendor(string Name)
        {
            DataTable d = VendorTable();
            foreach (var item in QuanlyContext.MyContext.sp_SearchVendor(Name).ToList())
            {
                d.Rows.Add(item.MaNCC, item.TenNCC);
            }
            return d;
        }


        public static void CBoxVendor(ComboBox cb)
        {
            cb.DataSource = View_Vendors();
            cb.DisplayMember = nameof(NhaCC.TenNCC);
            cb.ValueMember = nameof(NhaCC.MaNCC);
        }

        public static DataTable VendorTable()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("ID");
            dt.Columns.Add("Ten Nha Cung Cap");
            return dt;
        }
        public static DataTable GetNhaCC()
        {
            DataTable d = VendorTable();
            foreach (var item in QuanlyContext.MyContext.NhaCCs.ToList())
            {
                d.Rows.Add(item.MaNCC, item.TenNCC);
            }
            return d;
        }

        public static bool InsertNCC(string TenNcc, ref string errr)
        {
            NhaCC nhaCC = new NhaCC { TenNCC = TenNcc };
            QuanlyContext.MyContext.NhaCCs.Add(nhaCC);
            try
            {
                QuanlyContext.MyContext.SaveChanges();
                return true;
            }
            catch(Exception ex)
            {
                errr = ex.Message;
                return !true;
            }
        }

        public static bool UpdateNCC(int Mancc, string Tenncc, ref string errr)
        {
            NhaCC temp = QuanlyContext.MyContext.NhaCCs.Find(Mancc);
            temp.TenNCC = Tenncc;
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

        public static bool DeleteNCC(int Mancc, ref string errr)
        {
            NhaCC temp = QuanlyContext.MyContext.NhaCCs.Find(Mancc);
            QuanlyContext.MyContext.NhaCCs.Remove(temp);
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
