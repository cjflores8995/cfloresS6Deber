
using cfloresS6Jueves.Models;
using Newtonsoft.Json;
using System.Collections.ObjectModel;

namespace cfloresS6Jueves.Views;

public partial class vCrud : ContentPage
{

	private const string Url = "https://petscan-hkbtgngtbecxf5ag.centralus-01.azurewebsites.net/api/Metricas";
	private readonly HttpClient cliente = new HttpClient();
	private ObservableCollection<Metrica> elementos;

	public vCrud()
	{
		InitializeComponent();
		Obtener();
	}

	public async void Obtener()
	{
		var content = await cliente.GetStringAsync(Url);
		List<Metrica> mostrarMetricas = JsonConvert.DeserializeObject<List<Metrica>>(content);
		elementos = new ObservableCollection<Metrica>(mostrarMetricas);
        listadoMetricas.ItemsSource = elementos;


    }

    private void btnNuevo_Clicked(object sender, EventArgs e)
    {
		Navigation.PushAsync(new AgregarEstudiante());
    }

    private void listadoMetricas_ItemSelected(object sender, SelectedItemChangedEventArgs e)
    {
		var metrica = (Metrica)e.SelectedItem;
		Navigation.PushAsync(new vActualizarEliminarEstudiante(metrica));
    }
}