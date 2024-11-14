namespace LabG9.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Lab_g9_3 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Facturas", "MetodoPago", c => c.String(nullable: false));
            AlterColumn("dbo.Facturas", "DireccionEntrega", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Facturas", "DireccionEntrega", c => c.String());
            DropColumn("dbo.Facturas", "MetodoPago");
        }
    }
}
