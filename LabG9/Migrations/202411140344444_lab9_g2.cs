namespace LabG9.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class lab9_g2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "nombreCompleto", c => c.String());
            AddColumn("dbo.AspNetUsers", "direccionEntrega", c => c.String());
            AddColumn("dbo.AspNetUsers", "numTelefono", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "numTelefono");
            DropColumn("dbo.AspNetUsers", "direccionEntrega");
            DropColumn("dbo.AspNetUsers", "nombreCompleto");
        }
    }
}
