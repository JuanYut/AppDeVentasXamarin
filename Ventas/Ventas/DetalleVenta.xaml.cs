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
	public partial class DetalleVenta : ContentPage
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

        async private void EliminaVenta(object sender, EventArgs e)
        {
            string folder = System.Environment.GetFolderPath(System.Environment.SpecialFolder.LocalApplicationData);
            string rutaDb = System.IO.Path.Combine(folder, "MiNegocio1.db");
            var db = new SQLiteConnection(rutaDb);

            int MiId = int.Parse(Id.Text);

            var respuesta = await DisplayAlert("Alerta!!", "¿Esta seguro de que lo desea eliminar?", "Si", "No");

            if (respuesta == true)
            {
                db.Delete<Ventas>(MiId);
                await Application.Current.MainPage.Navigation.PopAsync();
            }
        }
        private void ActualizarRegistro(object sender, EventArgs e)
        {
            string folder = System.Environment.GetFolderPath(System.Environment.SpecialFolder.LocalApplicationData);
            string rutaDb = System.IO.Path.Combine(folder, "MiNegocio1.db");
            var db = new SQLiteConnection(rutaDb);

            int MiId = int.Parse(Id.Text);
            //DisplayAlert("ID", "" + MiId, "Ok");

            var registro = new Ventas
            {
                Id = MiId,
                Fecha = fecha.ToString(),
                Cliente = cliente.Text,
                Producto = producto.Text,
                Pagado = pagado.Text
            };

            db.Table<Ventas>();

            db.Update(registro);
            // DisplayAlert("Anuncio", "Registro Actualizado!!", "ok");

            Application.Current.MainPage.Navigation.PopAsync();
        }

        public DetalleVenta ()
		{
			InitializeComponent ();
		}
	}
}