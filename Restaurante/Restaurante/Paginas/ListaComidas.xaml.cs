using Restaurante.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Restaurante.Paginas
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ListaComidas : ContentPage
	{
		public ListaComidas (Sede sede)
		{
            InitializeComponent();

            this.BindingContext = sede;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            Loading(true);
            var sede = (Sede)this.BindingContext;
            if (sede != null)
            {
                lsvComidas.ItemsSource = null;
                lsvComidas.ItemsSource = sede.Carta;
            }
            Loading(false);
        }

        void Loading(bool mostrar)
        {
            indicator.IsEnabled = mostrar;
            indicator.IsRunning = mostrar;
        }

        private async void lsvComidas_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            try
            {
                var dato1 = (Carta)e.SelectedItem;
                await Navigation.PushAsync(new RegComida(dato1));
                lsvComidas.SelectedItem = null;
            }
            catch (Exception ex)
            {
            }
        }

        public async void btnAgregar_Clicked(object sender, EventArgs e)
        {
            var sede = (Sede)this.BindingContext;
            await Navigation.PushAsync(new RegComida(new Carta() { Sede = sede }));
        }
    }
}