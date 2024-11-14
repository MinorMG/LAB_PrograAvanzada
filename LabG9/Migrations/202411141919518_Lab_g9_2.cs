namespace LabG9.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Lab_g9_2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Productoes", "cantidad", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Productoes", "cantidad");
        }
    }
}
