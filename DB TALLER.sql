CREATE DATABASE TALLER
GO
USE TALLER
GO

CREATE TABLE CLIENTES
(
CLAVE_CLIENTE INT PRIMARY KEY NOT NULL,
RFC CHAR(13),
RAZON_SOCIAL VARCHAR(90),
APELLIDO_PATERNO VARCHAR(30),
APELLIDO_MATERNO VARCHAR(30),
NOMBRES VARCHAR(30),
CALLE_DOMICILIO VARCHAR(30),
COLONIA_DOMICILIO VARCHAR (30),
NUMERO_DOMICILIO CHAR(4),
CORREO_ELECTRONICO VARCHAR(50),
TELEFONO VARCHAR(15),
TIPO_CLIENTE CHAR(1) NOT NULL,
CONSTRAINT CHK_TIPO_CLIENTE CHECK (TIPO_CLIENTE IN ('F', 'M'))
)
GO

CREATE TABLE PROVEEDORES
(
CLAVE_PROVEEDOR INT PRIMARY KEY NOT NULL,
RFC CHAR(13),
RAZON_SOCIAL VARCHAR(90),
APELLIDO_PATERNO VARCHAR(30),
APELLIDO_MATERNO VARCHAR(30),
NOMBRES VARCHAR(30),
CORREO_ELECTRONICO VARCHAR(50),
TELEFONO VARCHAR(15),
TIPO_PROVEEDOR CHAR(1) NOT NULL,
CONSTRAINT CHK_TIPO_PROVEEDOR CHECK (TIPO_PROVEEDOR IN ('F', 'M'))
)
GO

CREATE TABLE EMPLEADOS
(
CLAVE_EMPLEADO INT PRIMARY KEY NOT NULL,
NOMBRES VARCHAR(50),
RFC CHAR(13),
APELLIDO_PATERNO VARCHAR(30),
APELLIDO_MATERNO VARCHAR(30),
CALLE_DOMICILIO VARCHAR(30),
COLONIA_DOMICILIO VARCHAR (30),
NUMERO_DOMICILIO CHAR(4),
CORREO_ELECTRONICO VARCHAR(50),
TELEFONO VARCHAR(15)
)	
GO

CREATE TABLE PRODUCTOS
(
CLAVE_PRODUCTO INT PRIMARY KEY NOT NULL,
PROVEEDOR INT REFERENCES PROVEEDORES(CLAVE_PROVEEDOR),
NOMBRE_CORTO VARCHAR(25) NOT NULL,
DESCRIPCION VARCHAR(50),
PRECIO_VENTA DECIMAL NOT NULL,
PRECIO_COMPRA DECIMAL NOT NULL,
EXISTENCIA INT NOT NULL
)
GO

CREATE TABLE VENTAS
(
CLAVE_VENTA INT PRIMARY KEY NOT NULL,
CLAVE_CLIENTE INT NOT NULL REFERENCES CLIENTES(CLAVE_CLIENTE),
CLAVE_EMPLEADO_VENTA INT NOT NULL REFERENCES EMPLEADOS(CLAVE_EMPLEADO),
CLAVE_EMPLEADO_MANO_OBRA INT NULL REFERENCES EMPLEADOS(CLAVE_EMPLEADO),
FECHA_VENTA DATETIME NOT NULL
)	
GO

CREATE TABLE DETALLE_VENTAS
(
CLAVE_DETALLE_VENTA INT PRIMARY KEY NOT NULL,
CLAVE_VENTA INT NOT NULL REFERENCES VENTAS(CLAVE_VENTA),
CLAVE_PRODUCTO INT NOT NULL REFERENCES PRODUCTOS(CLAVE_PRODUCTO),
CANTIDAD_PRODUCTOS INT NOT NULL,
PRECIO_UNITARIO DECIMAL NOT NULL,
TOTAL_VENTA DECIMAL NOT NULL,
)
GO

CREATE TABLE COMPRAS
(
CLAVE_COMPRA INT PRIMARY KEY NOT NULL,
CLVE_PROVEEDOR INT NOT NULL REFERENCES PROVEEDORES(CLAVE_PROVEEDOR),
CLAVE_EMPLEADO_COMPRA INT NOT NULL REFERENCES EMPLEADOS(CLAVE_EMPLEADO),
FECHA_COMPRA DATETIME NOT NULL
)
GO

CREATE TABLE DETALLE_COMPRAS
(
CLAVE_DETALLE_COMPRA INT PRIMARY KEY NOT NULL,
CLAVE_COMPRA INT NOT NULL REFERENCES COMPRAS(CLAVE_COMPRA),
CLAVE_PRODUCTO INT NOT NULL REFERENCES PRODUCTOS(CLAVE_PRODUCTO),
CANTIDAD_PRODUCTOS INT NOT NULL,
PRECIO_UNITARIO DECIMAL NOT NULL,
TOTAL_COMPRA DECIMAL NOT NULL
)

CREATE TABLE USUARIOS
(
CLAVE_USUARIO INT PRIMARY KEY NOT NULL,
NOMBRE_USUARIO VARCHAR(30) NOT NULL,
CONTRASENIA_USUARIO VARCHAR(15) NOT NULL,
CLAVE_EMPLEADO INT NOT NULL REFERENCES EMPLEADOS(CLAVE_EMPLEADO)
)