using AppBaseLocal;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Ventas
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class NuevaVenta : ContentPage
	{

        public class Ventas
        {
            [PrimaryKey]
            [AutoIncrement]
            public int Id { get; set; }
            public string Fecha { get; set; }
            public string Cliente { get; set; }
            public string Producto { get; set; }
            public string Pagado { get; set; }
        }

        private void AgregarVentas()
		{
            string folder = System.Environment.GetFolderPath(System.Environment.SpecialFolder.LocalApplicationData);
            string rutaDb = System.IO.Path.Combine(folder, "MiNegocio1.db");
            // DisplayAlert("Ruta de la base de datos", rutaDb, "ok");
            // Crea la base de datos si no existe, y crea una conexión
            var db = new SQLiteConnection(rutaDb);

            // Crea la tabla si no existe

            db.CreateTable<Ventas>();

            var registro = new Ventas
            {
                //DateTime.Now.ToString("dd/MM/yyyy")
                //DateTimePicker1.Value.ToString("dd/MM/yyyy");
                Fecha = fecha.Date.ToString(),
                Cliente = cliente.Text,
                Producto = producto.Text,
                Pagado = pagado.Text
            };

            db.Insert(registro);
            DisplayAlert("Agregar", "La venta fue realizada con exito!", "ok");
        }

        // Botones Cliente ------------------------------------------------------------------------------------------------------------- //
        async private void BotonCliente(object sender, EventArgs e)
        {
            var clientePage = new ListaCliente();
            await Navigation.PushAsync(clientePage);
        }
        // Botones Producto ----------------------------------------------------------------------------------------------------------- //
        async private void BotonListaProducto(object sender, EventArgs e)
        {
            var listaProductoPage = new ListaProducto();
            await Navigation.PushAsync(listaProductoPage);
        }
        // --------------------------------------------------------------------------------------------------------------------------- //

        public NuevaVenta ()
        {
            InitializeComponent();
        }

        private void MenuItem1_Clicked(object sender, EventArgs e)
        {
            AgregarVentas();
            Application.Current.MainPage.Navigation.PopAsync();
        }

    }
}