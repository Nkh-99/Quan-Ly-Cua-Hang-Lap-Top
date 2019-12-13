using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLy002.BS_Layer
{
    static class BLChitietHD
    {
        public static IList<ChitietHD> GetChitietHDByMaHD(string MaHD)
        {
            return QuanlyContext.MyContext.fn_GetChitietHDByMaHD(MaHD).ToList();
        }
    }
}
