using Restaurante.Modelos;
using Restaurante.Servicios;
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
	public partial class RegComida : ContentPage
	{
        ServicioBaseDatos<Carta> bd;

        public RegComida (Carta carta)
        {
			InitializeComponent ();
            this.BindingContext = carta;
            bd = new ServicioBaseDatos<Carta>();

            if (carta.Id == 0)
                this.ToolbarItems.RemoveAt(1);
        }


        void Loading(bool mostrar)
        {
            indicator.IsEnabled = mostrar;
            indicator.IsRunning = mostrar;
        }

        async void btnRegistrar_Clicked(object sender, EventArgs e)
        {
            Loading(true);
            var carta = (Carta)this.BindingContext;


            // Obtener el valor seleccionado del Picker (ComboBox)
            if (cmbMenu.SelectedIndex >= 0)
            {
                carta.categoria = cmbMenu.SelectedItem.ToString();
            }
            else
            {
                await DisplayAlert("Error", "Debes seleccionar una categoria del menú.", "OK");
                Loading(false);
                return;
            }

            if (carta.Id > 0)
                await bd.Actualizar(carta);
            else
                await bd.Agregar(carta);

            Loading(false);
            await DisplayAlert("Correcto", "Registro realizado correctamente", "OK");
            await Navigation.PopAsync();
        }

        async void btnEliminar_Clicked(object sender, EventArgs e)
        {
            if (await DisplayAlert("Advertencia", "¿Deseas eliminar este registro?", "Si", "No"))
            {
                Loading(true);
                await bd.Eliminar(((Carta)this.BindingContext).Id);
                Loading(false);
                await DisplayAlert("Correcto", "Registro eliminado correctamente", "OK");
                await Navigation.PopAsync();
            }
        }

    }
}