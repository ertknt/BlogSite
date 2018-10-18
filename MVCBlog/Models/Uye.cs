namespace MVCBlog.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Uye")]
    public partial class Uye
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Uye()
        {
            Makale = new HashSet<Makale>();
            Yorum = new HashSet<Yorum>();
        }

        public int Id { get; set; }

        [Required]
        [Display(Name = "Kullanýcý Adý")]
        [StringLength(50)]
        public string KullaniciAdi { get; set; }

        [Required]
        [DataType(DataType.EmailAddress, ErrorMessage = "Geçersiz E-mail Adresi")]
        [Display(Name = "E-mail")]
        [StringLength(50)]
        public string Email { get; set; }

        [Required]
        [Display(Name = "Þifre")]
        [StringLength(20)]
        public string Sifre { get; set; }

        [Required]
        [Display(Name ="Ad Soyad")]
        [StringLength(50)]
        public string AdSoyad { get; set; }

        [Display(Name = "Fotoðraf")]
        [StringLength(250)]
        public string Foto { get; set; }

        [Display(Name = "Yetki")]
        public int? YetkiId { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Makale> Makale { get; set; }

        public virtual Yetki Yetki { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Yorum> Yorum { get; set; }
    }
}
