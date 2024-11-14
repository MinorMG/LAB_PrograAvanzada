use LabG9


INSERT INTO [LabG9].[dbo].[Categorias] ([NombreCategoria], [Descripcion])
VALUES 
    ('Frutas', 'Productos frescos y org�nicos como manzanas, pl�tanos y naranjas.'),
    ('Verduras', 'Verduras frescas y naturales como lechuga, zanahorias y tomates.'),
    ('L�cteos', 'Productos derivados de la leche como yogur, queso y leche.'),
    ('Cereales', 'Cereales integrales y org�nicos como avena, arroz y quinoa.');

	CREATE TRIGGER trg_DesactivarProductoes
ON Productoes
AFTER UPDATE
AS
BEGIN
    IF UPDATE(cantidad)
    BEGIN
        UPDATE Productoes
        SET Estado = 0
        WHERE IdProducto IN (SELECT IdProducto FROM inserted WHERE Cantidad = 0)
    END
END
