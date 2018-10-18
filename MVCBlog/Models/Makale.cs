namespace MVCBlog.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using System.Web.Mvc;

    [Table("Makale")]
    public partial class Makale
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Makale()
        {
            Yorum = new HashSet<Yorum>();
            Etiket = new HashSet<Etiket>();
        }

        public int Id { get; set; }

        [Required]
        [Display(Name ="Makale Baþlýðý")]
        [StringLength(150)]
        public string Baslik { get; set; }

        [Required]
        [AllowHtml]
        [UIHint("tinymce_full_compressed")]
        [Display(Name = "Makale Ýçeriði")]
        public string Icerik { get; set; }

        [Display(Name = "Fotoðraf")]
        [StringLength(500)]
        public string Foto { get; set; }

        public DateTime? Tarih { get; set; }

        public int? KategoriId { get; set; }

        public int? UyeId { get; set; }

        [Display(Name = "Okunma Sayýsý")]
        public int? Okunma { get; set; }

        public virtual Kategori Kategori { get; set; }

        public virtual Uye Uye { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Yorum> Yorum { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Etiket> Etiket { get; set; }
    }
}
