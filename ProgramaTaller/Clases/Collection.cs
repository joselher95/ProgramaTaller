using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgramaTaller.Clases
{
    class Collection
    {
        #region Variables
        SqlDataAdapter dapCollection;
        DataTable dtCollection;
        SqlCommand cmd;
        SqlConnection con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["Conexion"].ConnectionString);
        #endregion

        #region Contructor
        public Collection()
        {
        }
        #endregion

        #region Metodos publicos
        public Usuario buscarUsuariosPorNombreUsuario(string nombreUsuario)
        {
            #region Obtener catalogo
            con.Open();
            string strConsulta = "SELECT * FROM Usuarios WHERE NOMBRE_USUARIO = @NOMBRE_USUARIO";
            cmd = new SqlCommand(strConsulta, con);
            cmd.Parameters.AddWithValue("@NOMBRE_USUARIO", nombreUsuario);
            dapCollection = new SqlDataAdapter();
            dtCollection = new DataTable();
            dapCollection.SelectCommand = cmd;
            dapCollection.Fill(dtCollection);
            con.Close();
            #endregion

            #region crear arreglo
            Usuario Usuarios = null;
            if(dtCollection.Rows.Count != 0)
                Usuarios = new Usuario(Convert.ToInt32(dtCollection.Rows[0]["CLAVE_USUARIO"]));
            #endregion

            return Usuarios;
        }

        #region Métodos Clientes
        public Clientes[] CatalogoClientes()
        {
            #region Obtener catalogo
            con.Open();
            string strConsulta = "SELECT * FROM CLIENTES";
            cmd = new SqlCommand(strConsulta, con);
            dapCollection = new SqlDataAdapter();
            dtCollection = new DataTable();
            dapCollection.SelectCommand = cmd;
            dapCollection.Fill(dtCollection);
            con.Close();
            #endregion

            #region crear arreglo
            ArrayList arlClientes = new ArrayList();

            foreach (DataRow row in dtCollection.Rows)
            {
                Clientes cliente = new Clientes(Convert.ToInt32(row["CLAVE_CLIENTE"]));
                arlClientes.Add(cliente);
            }
            Clientes[] arrCLientes = new Clientes[arlClientes.Count];
            arlClientes.CopyTo(arrCLientes);
            #endregion

            return arrCLientes;
        }

        public int obtenerSiguienteCliente()
        {
            #region Consulta
            con.Open();
            string strConsulta = "SELECT MAX(CLAVE_CLIENTE)+1 CLAVE_CLIENTE FROM CLIENTES";
            cmd = new SqlCommand(strConsulta, con);
            dapCollection = new SqlDataAdapter(cmd);
            DataTable dtResultado = new DataTable();
            dapCollection.Fill(dtResultado);
            con.Close();

            #endregion

            #region Validar que no regrese 0
            int iResultado;
            if(dtResultado.Rows[0]["CLAVE_CLIENTE"] != DBNull.Value)
                iResultado = Convert.ToInt32(dtResultado.Rows[0]["CLAVE_CLIENTE"]);
            else
                iResultado = 1;
            #endregion

            return iResultado;
        }
        #endregion

        #region Métodos Proveedores
        public Proveedores[] CatalogoProveedores()
        {
            #region Obtener catalogo
            con.Open();
            string strConsulta = "SELECT * FROM PROVEEDORES";
            cmd = new SqlCommand(strConsulta, con);
            dapCollection = new SqlDataAdapter();
            dtCollection = new DataTable();
            dapCollection.SelectCommand = cmd;
            dapCollection.Fill(dtCollection);
            con.Close();
            #endregion

            #region crear arreglo
            ArrayList arlProveedores = new ArrayList();

            foreach (DataRow row in dtCollection.Rows)
            {
                Proveedores proveedor = new Proveedores(Convert.ToInt32(row["CLAVE_PROVEEDOR"]));
                arlProveedores.Add(proveedor);
            }
            Proveedores[] arrProveedores = new Proveedores[arlProveedores.Count];
            arlProveedores.CopyTo(arrProveedores);
            #endregion

            return arrProveedores;
        }

        public int obtenerSiguienteProveedor()
        {
            #region Consulta
            con.Open();
            string strConsulta = "SELECT MAX(CLAVE_PROVEEDOR)+1 CLAVE_PROVEEDOR FROM PROVEEDORES";
            cmd = new SqlCommand(strConsulta, con);
            dapCollection = new SqlDataAdapter(cmd);
            DataTable dtResultado = new DataTable();
            dapCollection.Fill(dtResultado);
            con.Close();

            #endregion

            #region Validar que no regrese 0
            int iResultado;
            if (dtResultado.Rows[0]["CLAVE_PROVEEDOR"] != DBNull.Value)
                iResultado = Convert.ToInt32(dtResultado.Rows[0]["CLAVE_PROVEEDOR"]);
            else
                iResultado = 1;
            #endregion

            return iResultado;
        }
        #endregion

        #region Métodos Ventas y Detalles de Ventas
        public int obtenerSiguienteVenta()
        {
            #region Consulta
            con.Open();
            string strConsulta = "SELECT MAX(CLAVE_VENTA)+1 CLAVE_VENTA FROM VENTAS";
            cmd = new SqlCommand(strConsulta, con);
            dapCollection = new SqlDataAdapter(cmd);
            DataTable dtResultado = new DataTable();
            dapCollection.Fill(dtResultado);
            con.Close();

            #endregion

            #region Validar que no regrese 0
            int iResultado;
            if (dtResultado.Rows[0]["CLAVE_VENTA"] != DBNull.Value)
                iResultado = Convert.ToInt32(dtResultado.Rows[0]["CLAVE_VENTA"]);
            else
                iResultado = 1;
            #endregion

            return iResultado;
        }

        public int obtenerSiguienteDetalleVenta()
        {
            #region Consulta
            con.Open();
            string strConsulta = "SELECT MAX(CLAVE_DETALLE_VENTA)+1 CLAVE_DETALLE_VENTA FROM DETALLE_VENTAS";
            cmd = new SqlCommand(strConsulta, con);
            dapCollection = new SqlDataAdapter(cmd);
            DataTable dtResultado = new DataTable();
            dapCollection.Fill(dtResultado);
            con.Close();

            #endregion

            #region Validar que no regrese 0
            int iResultado;
            if (dtResultado.Rows[0]["CLAVE_DETALLE_VENTA"] != DBNull.Value)
                iResultado = Convert.ToInt32(dtResultado.Rows[0]["CLAVE_DETALLE_VENTA"]);
            else
                iResultado = 1;
            #endregion

            return iResultado;
        }

        public DetalleVenta[] BuscarDetalleVenta(int claveVenta)
        {
            #region Obtener catalogo
            con.Open();
            string strConsulta = "SELECT * FROM DETALLE_VENTAS WHERE CLAVE_VENTA = @CLAVE_VENTA";
            cmd = new SqlCommand(strConsulta, con);
            cmd.Parameters.AddWithValue("@CLAVE_VENTA", claveVenta);
            dapCollection = new SqlDataAdapter();
            dtCollection = new DataTable();
            dapCollection.SelectCommand = cmd;
            dapCollection.Fill(dtCollection);
            con.Close();
            #endregion

            #region crear arreglo
            ArrayList arlDetalleCompra = new ArrayList();
            foreach (DataRow row in dtCollection.Rows)
            {
                DetalleVenta detalleCompra = new DetalleVenta(Convert.ToInt32(row["CLAVE_DETALLE_VENTA"]));
                arlDetalleCompra.Add(detalleCompra);
            }
            DetalleVenta[] arrDetalleVenta = new DetalleVenta[arlDetalleCompra.Count];
            arlDetalleCompra.CopyTo(arrDetalleVenta);
            #endregion

            return arrDetalleVenta;
        }
        #endregion

        #region Métodos Compras y Detalles de Compras
        public int obtenerSiguienteCompra()
        {
            #region Consulta
            con.Open();
            string strConsulta = "SELECT MAX(CLAVE_COMPRA)+1 CLAVE_COMPRA FROM COMPRAS";
            cmd = new SqlCommand(strConsulta, con);
            dapCollection = new SqlDataAdapter(cmd);
            DataTable dtResultado = new DataTable();
            dapCollection.Fill(dtResultado);
            con.Close();

            #endregion

            #region Validar que no regrese 0
            int iResultado;
            if (dtResultado.Rows[0]["CLAVE_COMPRA"] != DBNull.Value)
                iResultado = Convert.ToInt32(dtResultado.Rows[0]["CLAVE_COMPRA"]);
            else
                iResultado = 1;
            #endregion

            return iResultado;
        }

        public int obtenerSiguienteDetalleCompra()
        {
            #region Consulta
            con.Open();
            string strConsulta = "SELECT MAX(CLAVE_DETALLE_COMPRA)+1 CLAVE_DETALLE_COMPRA FROM DETALLE_COMPRAS";
            cmd = new SqlCommand(strConsulta, con);
            dapCollection = new SqlDataAdapter(cmd);
            DataTable dtResultado = new DataTable();
            dapCollection.Fill(dtResultado);
            con.Close();

            #endregion

            #region Validar que no regrese 0
            int iResultado;
            if (dtResultado.Rows[0]["CLAVE_DETALLE_COMPRA"] != DBNull.Value)
                iResultado = Convert.ToInt32(dtResultado.Rows[0]["CLAVE_DETALLE_COMPRA"]);
            else
                iResultado = 1;
            #endregion

            return iResultado;
        }

        public DetalleCompra[] BuscarDetalleCompra(int claveCompra)
        {
            #region Obtener catalogo
            con.Open();
            string strConsulta = "SELECT * FROM DETALLE_COMPRAS WHERE CLAVE_COMPRA = @CLAVE_COMPRA";
            cmd = new SqlCommand(strConsulta, con);
            cmd.Parameters.AddWithValue("@CLAVE_COMPRA", claveCompra);
            dapCollection = new SqlDataAdapter();
            dtCollection = new DataTable();
            dapCollection.SelectCommand = cmd;
            dapCollection.Fill(dtCollection);
            con.Close();
            #endregion

            #region crear arreglo
            ArrayList arlDetalleCompra = new ArrayList();
            foreach (DataRow row in dtCollection.Rows)
            {
                DetalleCompra detalleCompra = new DetalleCompra(Convert.ToInt32(row["CLAVE_DETALLE_COMPRA"]));
                arlDetalleCompra.Add(detalleCompra);
            }
            DetalleCompra[] arrDetalleCompra = new DetalleCompra[arlDetalleCompra.Count];
            arlDetalleCompra.CopyTo(arrDetalleCompra);
            #endregion

            return arrDetalleCompra;
        }

        #endregion

        #region Metodos trabajadores
        public Empleado[] CatalogoTrabajadores()
        {
            #region Obtener catalogo
            con.Open();
            string strConsulta = "SELECT * FROM EMPLEADOS";
            cmd = new SqlCommand(strConsulta, con);
            dapCollection = new SqlDataAdapter();
            dtCollection = new DataTable();
            dapCollection.SelectCommand = cmd;
            dapCollection.Fill(dtCollection);
            con.Close();
            #endregion

            #region crear arreglo
            ArrayList arlTrabajadores = new ArrayList();

            foreach (DataRow row in dtCollection.Rows)
            {
                Empleado trabajador = new Empleado(Convert.ToInt32(row["CLAVE_EMPLEADO"]));
                arlTrabajadores.Add(trabajador);
            }
            Empleado[] arrTRabajadores = new Empleado[arlTrabajadores.Count];
            arlTrabajadores.CopyTo(arrTRabajadores);
            #endregion
            return arrTRabajadores;

        }

        public int obtenerSiguienteTrabajador()
        {
            #region consulta
            con.Open();
            string strConsulta = "SELECT MAX(CLAVE_EMPLEADO)+1 CLAVE_EMPLEADO FROM EMPLEADOS";
            cmd = new SqlCommand(strConsulta, con);
            dapCollection = new SqlDataAdapter(cmd);
            DataTable dtResultado = new DataTable();
            dapCollection.Fill(dtResultado);
            con.Close();

            #endregion

            #region Validar que no regrese 0
            int iResultado;
            if (dtResultado.Rows[0]["CLAVE_EMPLEADO"] != DBNull.Value)
                iResultado = Convert.ToInt32(dtResultado.Rows[0]["CLAVE_EMPLEADO"]);
            else
                iResultado = 1;

            #endregion

            return iResultado;
        }
        #endregion

        #region Metodos Productos

        public Producto[] CatalogoProductos()
        {
            #region Obtener Catalogo
            con.Open();
            string strConsulta = "SELECT * FROM PRODUCTOS";
            cmd = new SqlCommand(strConsulta, con);
            dapCollection = new SqlDataAdapter();
            dtCollection = new DataTable();
            dapCollection.SelectCommand = cmd;
            dapCollection.Fill(dtCollection);
            con.Close();
            #endregion

            #region crear arreglo
            ArrayList arlProductos = new ArrayList();

            foreach (DataRow row in dtCollection.Rows)
            {
                Producto producto = new Producto(Convert.ToInt32(row["CLAVE_PRODUCTO"]));
                arlProductos.Add(producto);
            }
            Producto[] arrPRoductos = new Producto[arlProductos.Count];
            arlProductos.CopyTo(arrPRoductos);
            #endregion

            return arrPRoductos;
        }
        public int obtenerSiguienteProducto()
        {
            #region Consulta
            con.Open();
            string strConsulta = "SELECT MAX(CLAVE_PRODUCTO)+1 CLAVE_PRODUCTO FROM PRODUCTOS";
            cmd = new SqlCommand(strConsulta, con);
            dapCollection = new SqlDataAdapter(cmd);
            DataTable dtResultado = new DataTable();
            dapCollection.Fill(dtResultado);
            con.Close();

            #endregion

            #region Validar que no regrese 0
            int iResultado;
            if (dtResultado.Rows[0]["CLAVE_PRODUCTO"] != DBNull.Value)
                iResultado = Convert.ToInt32(dtResultado.Rows[0]["CLAVE_PRODUCTO"]);
            else
                iResultado = 1;
            #endregion

            return iResultado;
        }
        #endregion

        #endregion
    }
}
