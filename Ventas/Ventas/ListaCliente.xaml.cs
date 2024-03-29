﻿using AppBaseLocal;
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
	public partial class ListaCliente : ContentPage
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

        public class Clientes
        {
            [PrimaryKey]
            [AutoIncrement]
            public int Id { get; set; }
            public string Nombre { get; set; }
            public string Telefono { get; set; }
            public string Correo { get; set; }
            public string Comentarios { get; set; }
            public string Foto { get; set; }
        }

        public void AbrirBase()
        {
            string folder = System.Environment.GetFolderPath(System.Environment.SpecialFolder.LocalApplicationData);
            string rutaDb = System.IO.Path.Combine(folder, "MiNegocio1.db");
            // DisplayAlert("Ruta de la base de datos", rutaDb, "ok");
            // Crea la base de datos si no existe, y crea una conexión
            var db = new SQLiteConnection(rutaDb);
            db.CreateTable<Clientes>();
            var todoslosclientes = db.Table<Clientes>().ToList();
            lst.ItemsSource = null;
            lst.ItemsSource = todoslosclientes;
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

        // Botones de Arriba ----------------------------------------------------------------------------------------------------------- //
        async private void BotonAgregarCliente(object sender, EventArgs e)
        {
            var clientePage = new AgregarCliente();
            await Navigation.PushAsync(clientePage);
            AbrirBase();
        }

        private async Task ItemSeleccionado(object sender, ItemTappedEventArgs e)
        {
            var elemento = e.Item as Clientes;
            var detailPageCliente = new DetalleCliente();
            detailPageCliente.BindingContext = elemento;
            await Navigation.PushAsync(detailPageCliente);

            //await Application.Current.MainPage.Navigation.PopAsync();
        }

        public ListaCliente ()
		{
            InitializeComponent();

            AbrirBase();
            lst.IsRefreshing = false;
            this.Appearing += MainPage_Appearing;
        }
	}
}