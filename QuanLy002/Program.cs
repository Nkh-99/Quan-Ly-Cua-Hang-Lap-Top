using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using QuanLy002.Forms;

namespace QuanLy002
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new FrmLogin());            
        }
    }
    public static class QuanlyContext
    {
        private static Quanlybanhang001Entities myContext;
        static QuanlyContext()
        {
            myContext = new Quanlybanhang001Entities();
            myContext.Configuration.ProxyCreationEnabled = false;
        }
        public static Quanlybanhang001Entities MyContext { get { return myContext; } set { myContext = value; } }        
    }
}
