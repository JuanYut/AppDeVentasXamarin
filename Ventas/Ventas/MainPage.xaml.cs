using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ventas;
using Xamarin.Forms;

namespace AppBaseLocal
{
    public partial class MainPage : ContentPage
    {
        public class News
        {
            [PrimaryKey]
            public int Id { get; set; }
            public string Title { get; set; }
            [Ignore]
            public string Body { get; set; }
            public string ImageName { get; set; }
        }

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

        public void AbrirBase()
        {
            string folder = System.Environment.GetFolderPath(System.Environment.SpecialFolder.LocalApplicationData);
            string rutaDb = System.IO.Path.Combine(folder, "MiNegocio1.db");
            // DisplayAlert("Ruta de la base de datos", rutaDb, "ok");
            // Crea la base de datos si no existe, y crea una conexión
            var db = new SQLiteConnection(rutaDb);
            db.CreateTable<Ventas>();
            var todoslosproductos = db.Table<Ventas>().ToList();

            lst.ItemsSource = null;
            lst.ItemsSource = todoslosproductos;
        }

        // Clase de ayuda para visualizar las operaciones
        private void TestDb()
        {
            string folder = System.Environment.GetFolderPath(System.Environment.SpecialFolder.LocalApplicationData);
            string rutaDb = System.IO.Path.Combine(folder, "notiXamarinDb.db");
            // DisplayAlert("Ruta de la base de datos", rutaDb, "ok");
            // Crea la base de datos si no existe, y crea una conexión
            var db = new SQLiteConnection(rutaDb);

            // Crea la tabla si no existe
            db.CreateTable<News>();
            
            db.DeleteAll<News>();

            var news1 = new News
            {
                Id = 1,
                Title = "titulo 1",
                ImageName = "image name 1",
                Body = "body 1"
            };

            var news2 = new News
            {
                Id = 5,
                Title = "Creando apps multiplataforma con Xamarin",
                ImageName = "image name 2",
                Body = "body 2"
            };

            var news3 = new News
            {
                Id = 8,
                Title = "Usar Xamarin con c# o desarrollar con Java?",
                ImageName = "image name 3",
                Body = "body 3"
            };

            // Inserta elementos en la tabla (uno a uno)
            db.Insert(news1);
            db.Insert(news2);
            db.Insert(news3);
            var news_xmarin = db.Table<News>().Where(x => x.Title.Contains("Xamarin")).ToList();

            string s = "";
            foreach (var x in news_xmarin)
            {
                s = s + x.Title + "\n";
            }

            DisplayAlert("Los datos son:", s, "ok");

            // Reemplaza el elemento si no existe
            news1.Title = "nuevo titulo";
            db.InsertOrReplace(news1);

            // Si queremos insertar varios elementos a la vez
            //db.InsertAll(new List<News>() { news1, news2, news3 });

            // Obtener una noticia por su Id
            var news1_fromDb = db.Get<News>(news1.Id);

            // Obtener todas las noticias
            var news_todas = db.Table<News>().ToList();

            // Obtener un listado de noticias
            // var news_xmarin = db.Table<News>().Where(x => x.Title.Contains("Xamarin")).ToList();

            var cantidadDeNoticias = db.Table<News>().Count();

            // Borra el elemento de Id 1 de la tabla
            db.Delete<News>(1);

            // Borrar todos los elementos de la tabla
            db.DeleteAll<News>();

            // Borrar la tabla
            db.DropTable<News>();

        }

        private void MainPage_Appearing(object sender, EventArgs e)
        {
            AbrirBase();
        }

        public MainPage()
        {
            InitializeComponent();

            AbrirBase();
            lst.IsRefreshing = false;
            this.Appearing += MainPage_Appearing;
        }

        // Botones de Arriba ----------------------------------------------------------------------------------------------------------- //
        async private void MenuItem1_Clicked(object sender, EventArgs e)
        {
            var ventaPage = new NuevaVenta();
            await Navigation.PushAsync(ventaPage);
            AbrirBase();
        }
        // ----------------------------------------------------------------------------------------------------------------------------- //

        // Botones de Abajo ----------------------------------------------------------------------------------------------------------- //
        async private void BotonVentas(object sender, EventArgs e)
        {
            var detailPage = new AgregarProducto();
            await Navigation.PushAsync(detailPage);
            AbrirBase();
        }

        async private void BotonGraficas(object sender, EventArgs e)
        {
            var detailPage = new AgregarProducto();
            await Navigation.PushAsync(detailPage);
            AbrirBase();
        }

        async private void BotonOpciones(object sender, EventArgs e)
        {
            var detailPage = new AgregarProducto();
            await Navigation.PushAsync(detailPage);
            AbrirBase();
        }
        // ----------------------------------------------------------------------------------------------------------------------------- //

        private async Task ItemSeleccionado(object sender, ItemTappedEventArgs e)
        {
            var elemento = e.Item as Ventas;
            var detailPageVenta = new DetalleVenta();
            detailPageVenta.BindingContext = elemento;
            await Navigation.PushAsync(detailPageVenta);
        }

        private void MenuItem_Clicked(object sender, EventArgs e)
        {

        }

        private void MenuItem_Clicked_1(object sender, EventArgs e)
        {

        }
    }
}
