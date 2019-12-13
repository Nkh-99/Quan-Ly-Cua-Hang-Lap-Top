using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLy002.BS_Layer
{
    static class BLAcount
    {
        public static bool CheckAccount(string TK, string MK, ref int level, ref string MaNV)
        {           
            bool Result = false;
            var lvlParameter = new ObjectParameter("Loai", level);
            var maNVParameter = MaNV != null ?
                new ObjectParameter("MaNV", MaNV) :
                new ObjectParameter("MaNV", typeof(string));
            var ResultParameter = new ObjectParameter("Result", Result);           
            QuanlyContext.MyContext.spLoginAccount(TK, MK, lvlParameter, maNVParameter, ResultParameter);
            if ((int)ResultParameter.Value == 1)
            {
                level = Convert.ToInt32(lvlParameter.Value);
                MaNV = maNVParameter.Value.ToString();
                return true;
            }
            return false;
        }

        public static string GetUserByEmployeeId(string MaNV)
        {
            var res = new ObjectParameter("Result", "");
            QuanlyContext.MyContext.sp_GetUserByEmployeeId(MaNV, res);
            return res.Value.ToString();
        }

        public static void UpdatePassword(string User, string Pass)
        {
            QuanlyContext.MyContext.sp_ChangePassword(User, Pass);
        }
    }
}
