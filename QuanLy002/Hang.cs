//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace QuanLy002
{
    using System;
    using System.Collections.Generic;
    
    public partial class Hang
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Hang()
        {
            this.ChitietHDs = new HashSet<ChitietHD>();
        }
    
        public string MaHang { get; set; }
        public string TenHang { get; set; }
        public int MaNCC { get; set; }
        public double Soluong { get; set; }
        public double GiaNhap { get; set; }
        public double GiaBan { get; set; }
        public string Anh { get; set; }
        public string Ghichu { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ChitietHD> ChitietHDs { get; set; }
        public virtual NhaCC NhaCC { get; set; }
    }
}