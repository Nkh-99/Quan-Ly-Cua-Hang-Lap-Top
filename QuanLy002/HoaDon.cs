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
    
    public partial class HoaDon
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public HoaDon()
        {
            this.ChitietHDs = new HashSet<ChitietHD>();
        }
    
        public string MaHD { get; set; }
        public string MaNV { get; set; }
        public System.DateTime Ngayban { get; set; }
        public string MaKH { get; set; }
        public double Tongtien { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ChitietHD> ChitietHDs { get; set; }
        public virtual Khach Khach { get; set; }
        public virtual NhanVien NhanVien { get; set; }
    }
}