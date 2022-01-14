CREATE DATABASE DB_EXAMPLE;
GO
USE DB_EXAMPLE;


CREATE TABLE EXAMPLE(
    Id int IDENTITY(1,1) PRIMARY KEY,
    Descripcion VARCHAR(200),
    Valor INT
)


INSERT INTO EXAMPLE(Descripcion, Valor) VALUES
('Ejemplo 01',150000),
('Ejemplo 02',140000),
('Ejemplo 03',140000),
('Ejemplo 04',130000),
('Ejemplo 05',130000),
('Ejemplo 06',100000),
('Ejemplo 07',100000),
('Ejemplo 08',100000),
('Prueba 09',80000),
('Prueba 10',80000),
('Prueba 11',60000),
('Prueba 12',50000),
('Prueba 13',40000),
('Prueba 14',10000),
('Prueba 15',10000),
('Prueba 16',10000);