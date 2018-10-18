namespace MVCBlog.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ZorunluAlanlarinEklenmesi : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Etiket", "İsim", c => c.String(nullable: false, maxLength: 50));
            AlterColumn("dbo.Kategori", "Isim", c => c.String(nullable: false, maxLength: 50));
            AlterColumn("dbo.Uye", "KullaniciAdi", c => c.String(nullable: false, maxLength: 50));
            AlterColumn("dbo.Uye", "Email", c => c.String(nullable: false, maxLength: 50));
            AlterColumn("dbo.Uye", "Sifre", c => c.String(nullable: false, maxLength: 20));
            AlterColumn("dbo.Uye", "AdSoyad", c => c.String(nullable: false, maxLength: 50));
            AlterColumn("dbo.Yorum", "Icerik", c => c.String(nullable: false, maxLength: 500));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Yorum", "Icerik", c => c.String(maxLength: 500));
            AlterColumn("dbo.Uye", "AdSoyad", c => c.String(maxLength: 50));
            AlterColumn("dbo.Uye", "Sifre", c => c.String(maxLength: 20));
            AlterColumn("dbo.Uye", "Email", c => c.String(maxLength: 50));
            AlterColumn("dbo.Uye", "KullaniciAdi", c => c.String(maxLength: 50));
            AlterColumn("dbo.Kategori", "Isim", c => c.String(maxLength: 50));
            AlterColumn("dbo.Etiket", "İsim", c => c.String(maxLength: 50));
        }
    }
}
