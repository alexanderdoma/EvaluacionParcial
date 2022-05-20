using System;
using System.Data.SqlClient;
using System.Data;
using System.Web.UI.WebControls;

namespace Tienda
{
    public partial class productos : System.Web.UI.Page
    {
        //string strConexion = "Server=(local); Database=Tienda; UserId=nombre; password=clave";
        string strConexion = "Server=DESKTOP-AFFVEDI\\SQLEXPRESS; Database=Tienda; Integrated Security=true";
        protected void Page_Load(object sender, EventArgs e)
        {
            cargarDatos();
            LlenarCategorias();
            LlenarMarcas();
        }

        void cargarDatos()
        {
            using(var conexion = new SqlConnection(strConexion))
            {
                using(var command = new SqlCommand("Select IdProducto AS Id, Nombre, Marca, " +
                    "Precio AS PrecioProducto, Foto as Imagen " +
                    "From producto", conexion))
                {
                    var ds = new DataSet();
                    var da = new SqlDataAdapter(command);
                    da.Fill(ds);

                    gvDatos.DataSource = ds;
                    gvDatos.DataBind();
                    
                    rpDatos.DataSource = ds;
                    rpDatos.DataBind();
                }
            }
        }
        void LlenarCategorias()
        {
            using (var conexion = new SqlConnection(strConexion))
            {
                using (var command = new SqlCommand("Select * From Categoria", conexion))
                {
                    conexion.Open();
                    var reader = command.ExecuteReader();
                    if (reader != null && reader.HasRows)
                    {
                        ListItem item;
                        while (reader.Read())
                        {
                            item = new ListItem();
                            item.Text = reader["Nombre"].ToString();
                            item.Value = reader["IdCategoria"].ToString();
                            ddlCategoria.Items.Add(item);
                        }

                    }
                }
            }
        }

        void LlenarMarcas()
        {
            using (var conexion = new SqlConnection(strConexion))
            {
                using (var command = new SqlCommand("Select * From Marcas", conexion))
                {
                    conexion.Open();
                    var reader = command.ExecuteReader();
                    if (reader != null && reader.HasRows)
                    {
                        ListItem item;
                        while (reader.Read())
                        {
                            item = new ListItem();
                            item.Text = reader["nom_marca"].ToString();
                            item.Value = reader["id_marca"].ToString();
                            ddlMarca.Items.Add(item);
                        }

                    }
                }
            }
        }

        protected void FiltrarMarca(object sender, EventArgs e)
        {
            string marca = ddlMarca.SelectedItem + "";

            using (var conexion = new SqlConnection(strConexion))
            {
                using (var command = new SqlCommand("Select IdProducto AS Id, Nombre, Marca, " +
                    "Precio AS PrecioProducto " +
                    "From producto " +
                    "WHERE Marca like '%"+ marca + "'", conexion))
                {
                    var ds = new DataSet();
                    var da = new SqlDataAdapter(command);
                    da.Fill(ds);

                    gvDatos.DataSource = ds;
                    gvDatos.DataBind();

                    rpDatos.DataSource = ds;
                    rpDatos.DataBind();

                }
            }
        }

        protected void FiltrarCategoria(object sender, EventArgs e)
        {

            if (ddlCategoria.SelectedIndex == 0)
            {
                cargarDatos();
            }
            else
            {
                int categoria = ddlCategoria.SelectedIndex;
                using (var conexion = new SqlConnection(strConexion))
                {
                    using (var command = new SqlCommand("Select IdProducto AS Id, Nombre, Marca, " +
                        "Precio AS PrecioProducto " +
                        "From producto " +
                        "WHERE idCategoria =" + categoria, conexion))
                    {
                        var ds = new DataSet();
                        var da = new SqlDataAdapter(command);
                        da.Fill(ds);

                        gvDatos.DataSource = ds;
                        gvDatos.DataBind();

                        rpDatos.DataSource = ds;
                        rpDatos.DataBind();

                    }
                }
            }   
        }
    }
}