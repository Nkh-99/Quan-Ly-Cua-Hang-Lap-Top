using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity.Core.Objects;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuanLy002.BS_Layer
{
    static class BLHoaDon
    {
        public static DataTable SearchInvoice(string MaHD, string MaNV, string MaKH, string month, string year, string total)
        {
            int? Month, Year;
            if (Int16.TryParse(month, out short Month1) == false)
                Month = null;
            else Month = Month1;
            if (Int16.TryParse(year, out short Year1) == false)
                Year = null;
            else Year = Year1;
            double? Total;
            if (double.TryParse(total, out double Total1) == false)
                Total = null;
            else Total = Total1;
            DataTable d = InvoiceTable();
            foreach(var inv in QuanlyContext.MyContext.sp_SearchInvoice(MaHD, MaNV, MaKH, Month, Year, Total).ToList())
            {
                d.Rows.Add(inv.MaHD, inv.MaNV, inv.Ngayban, inv.MaKH, inv.Tongtien);
            }
            return d;
        }
        public static DataTable InvoiceTable()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("Invoice ID");
            dt.Columns.Add("Employee ID");
            dt.Columns.Add("Invoice Date");
            dt.Columns.Add("Customer ID");
            dt.Columns.Add("Amount Total");
            return dt;
        }
        public static HoaDon GetInvoiceById(string MaHD)
        {
            return QuanlyContext.MyContext.fn_GetInvoiceById(MaHD).FirstOrDefault();
        }        

        public static void CBoxHoaDon(ComboBox cb, string MaNV, int Level)
        {
            cb.DisplayMember = nameof(HoaDon.MaHD);
            cb.ValueMember = nameof(HoaDon.MaHD);
            if (Level == 2) // nhan vien: NV ban hang chi xem duoc hoa don co MaNV cua minh
            {
                cb.DataSource = QuanlyContext.MyContext.fn_GetInvoiceByEmployeeId(MaNV).ToList();
            }
            else if ( (Level&1) == 1)   // admin =1, NV ke toan =3
            {
                cb.DataSource = QuanlyContext.MyContext.HoaDons.ToList();
            }           
        }
        public static bool InsertInvoice(string MaNV, string MaKH, DataTable dt, ref string err)
        {
            ObjectParameter MaHD = new ObjectParameter("Result", "");
            QuanlyContext.MyContext.sp_CreateInvoiceId(MaHD);

            string MaHD1 = MaHD.Value.ToString();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                dt.Rows[i][nameof(ChitietHD.MaHD)] = MaHD1;
            }

            SqlParameter[] parameters =
            {
                new SqlParameter
                {
                    SqlDbType = SqlDbType.NVarChar,
                    Direction = ParameterDirection.Input,
                    ParameterName = "MaHD",
                    Value = MaHD1
                },
                new SqlParameter
                {
                    SqlDbType = SqlDbType.NVarChar,
                    Direction = ParameterDirection.Input,
                    ParameterName = "MaNV",
                    Value = MaNV
                },
                new SqlParameter
                {
                    SqlDbType = SqlDbType.NVarChar,
                    Direction = ParameterDirection.Input,
                    ParameterName = "MaKH",
                    Value = MaKH
                },
                new SqlParameter
                {
                    SqlDbType = SqlDbType.Structured,
                    Direction = ParameterDirection.Input,
                    ParameterName = "InvoiceDetails",
                    TypeName = "[dbo].[Line1Items]",
                    SqlValue = dt
                },
                new SqlParameter
                {
                    SqlDbType = SqlDbType.Bit,
                    Direction = ParameterDirection.Output,
                    ParameterName = "Finished",
                    Value = false
                }
            };

            try
            {
                QuanlyContext.MyContext.Database.ExecuteSqlCommand(System.Data.Entity.TransactionalBehavior.EnsureTransaction,
                "EXEC sp_InsertInvoice @MaHD, @MaNV, @MaKH, @InvoiceDetails, @Finished out", parameters);

                return (bool)parameters[4].Value;
            }
            catch (Exception ex)
            {
                err = ex.Message;
                return false;
            }

        }
        public static bool DeleteInvoice(string MaHD, ref string err)
        {
            try
            {
                ObjectParameter Done = new ObjectParameter("Finished", false);
                QuanlyContext.MyContext.sp_DeleteInvoice(MaHD, Done);
                QuanlyContext.MyContext.SaveChanges();
                return Convert.ToBoolean(Done.Value);
            }
            catch (Exception ex)
            {
                err = ex.Message;
                return false;
            }
        }
    }
}
