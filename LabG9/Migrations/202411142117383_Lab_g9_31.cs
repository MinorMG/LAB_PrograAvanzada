namespace LabG9.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Lab_g9_31 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Facturas", "ClienteId", "dbo.AspNetUsers");
            DropIndex("dbo.Facturas", new[] { "ClienteId" });
            AlterColumn("dbo.Facturas", "ClienteId", c => c.String(maxLength: 128));
            AlterColumn("dbo.Facturas", "DireccionEntrega", c => c.String());
            AlterColumn("dbo.Facturas", "MetodoPago", c => c.String());
            AlterColumn("dbo.Facturas", "Estado", c => c.String());
            CreateIndex("dbo.Facturas", "ClienteId");
            AddForeignKey("dbo.Facturas", "ClienteId", "dbo.AspNetUsers", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Facturas", "ClienteId", "dbo.AspNetUsers");
            DropIndex("dbo.Facturas", new[] { "ClienteId" });
            AlterColumn("dbo.Facturas", "Estado", c => c.String(nullable: false));
            AlterColumn("dbo.Facturas", "MetodoPago", c => c.String(nullable: false));
            AlterColumn("dbo.Facturas", "DireccionEntrega", c => c.String(nullable: false));
            AlterColumn("dbo.Facturas", "ClienteId", c => c.String(nullable: false, maxLength: 128));
            CreateIndex("dbo.Facturas", "ClienteId");
            AddForeignKey("dbo.Facturas", "ClienteId", "dbo.AspNetUsers", "Id", cascadeDelete: true);
        }
    }
}
