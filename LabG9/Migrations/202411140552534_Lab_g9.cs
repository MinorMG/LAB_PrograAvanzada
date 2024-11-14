namespace LabG9.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Lab_g9 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Categorias",
                c => new
                    {
                        IdCategoria = c.Int(nullable: false, identity: true),
                        NombreCategoria = c.String(nullable: false, maxLength: 50),
                        Descripcion = c.String(maxLength: 255),
                    })
                .PrimaryKey(t => t.IdCategoria);
            
            CreateTable(
                "dbo.Productoes",
                c => new
                    {
                        IdProducto = c.Int(nullable: false, identity: true),
                        Nombre = c.String(nullable: false, maxLength: 100),
                        IdCategoria = c.Int(nullable: false),
                        Precio = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Descripcion = c.String(maxLength: 255),
                        Estado = c.Boolean(nullable: false),
                        ImagenUrl = c.String(maxLength: 255),
                    })
                .PrimaryKey(t => t.IdProducto)
                .ForeignKey("dbo.Categorias", t => t.IdCategoria, cascadeDelete: true)
                .Index(t => t.IdCategoria);
            
            CreateTable(
                "dbo.DetalleFacturas",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        IdFactura = c.Int(nullable: false),
                        IdProducto = c.Int(nullable: false),
                        Cantidad = c.Int(nullable: false),
                        PrecioUnitario = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Facturas", t => t.IdFactura, cascadeDelete: true)
                .ForeignKey("dbo.Productoes", t => t.IdProducto, cascadeDelete: true)
                .Index(t => t.IdFactura)
                .Index(t => t.IdProducto);
            
            CreateTable(
                "dbo.Facturas",
                c => new
                    {
                        IdFactura = c.Int(nullable: false, identity: true),
                        ClienteId = c.String(nullable: false, maxLength: 128),
                        FechaPedido = c.DateTime(nullable: false),
                        DireccionEntrega = c.String(),
                        Estado = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.IdFactura)
                .ForeignKey("dbo.AspNetUsers", t => t.ClienteId, cascadeDelete: true)
                .Index(t => t.ClienteId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.DetalleFacturas", "IdProducto", "dbo.Productoes");
            DropForeignKey("dbo.DetalleFacturas", "IdFactura", "dbo.Facturas");
            DropForeignKey("dbo.Facturas", "ClienteId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Productoes", "IdCategoria", "dbo.Categorias");
            DropIndex("dbo.Facturas", new[] { "ClienteId" });
            DropIndex("dbo.DetalleFacturas", new[] { "IdProducto" });
            DropIndex("dbo.DetalleFacturas", new[] { "IdFactura" });
            DropIndex("dbo.Productoes", new[] { "IdCategoria" });
            DropTable("dbo.Facturas");
            DropTable("dbo.DetalleFacturas");
            DropTable("dbo.Productoes");
            DropTable("dbo.Categorias");
        }
    }
}
