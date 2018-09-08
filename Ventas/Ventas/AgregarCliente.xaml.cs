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
	public partial class AgregarCliente : ContentPage
	{

        public class Clientes
        {
            [PrimaryKey]
            [AutoIncrement]
            public int Id { get; set; }
            public string Nombre { get; set; }
            public int Telefono { get; set; }
            public string Correo { get; set; }
            public string Comentarios { get; set; }
            public string Foto { get; set; }
        }

        private void AgregarClientes()
        {
            string folder = System.Environment.GetFolderPath(System.Environment.SpecialFolder.LocalApplicationData);
            string rutaDb = System.IO.Path.Combine(folder, "MiNegocio1.db");
            // DisplayAlert("Ruta de la base de datos", rutaDb, "ok");
            // Crea la base de datos si no existe, y crea una conexión
            var db = new SQLiteConnection(rutaDb);

            // Crea la tabla si no existe

            db.CreateTable<Clientes>();

            var registro = new Clientes
            {
                Nombre = nombre.Text,
                Telefono = int.Parse(telefono.Text),
                Correo = correo.Text,
                Comentarios = comentarios.Text,
                Foto = foto.Text
            };

            db.Insert(registro);
            DisplayAlert("Agregar", "El cliente fue agregado con exito!", "ok");

        }

        public AgregarCliente ()
		{
			InitializeComponent ();
		}

        private void MenuItem1_Clicked(object sender, EventArgs e)
        {
            AgregarClientes();
            Application.Current.MainPage.Navigation.PopAsync();
        }
    }
}