using cfloresS6Jueves.Models;
using Newtonsoft.Json;
using System.Text;

namespace cfloresS6Jueves.Views;

public partial class vActualizarEliminarEstudiante : ContentPage
{
    private const string Url = "https://petscan-hkbtgngtbecxf5ag.centralus-01.azurewebsites.net/api/Metricas";

    public vActualizarEliminarEstudiante()
	{
		InitializeComponent();
	}

    public vActualizarEliminarEstudiante(Metrica datos)
    {
        InitializeComponent();
        txtCodigo.Text = datos.Id.ToString();
        txtEspecie.Text = datos.Especie;
        txtNombreRaza.Text = datos.NombreRaza;
        txtOrigen.Text = datos.Origen;
    }

    private async void btnActualizar_Clicked(object sender, EventArgs e)
    {
        try
        {

            var trama = new
            {
                id = txtCodigo.Text,
                especie = txtEspecie.Text,
                nombreRaza = txtNombreRaza.Text,
                origen = txtOrigen.Text,
                usuario = Guid.NewGuid().ToString(),
                fechaModificacion = DateTime.UtcNow.ToString("yyyy-MM-ddTHH:mm:ss.fffZ"),
                estado = true
            };

            string json = JsonConvert.SerializeObject(trama);

            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add("Accept", "application/json");

                var content = new StringContent(json, Encoding.UTF8, "application/json");

                HttpResponseMessage response = await client.PutAsync($"{Url}/{txtCodigo.Text}", content);

                if (response.IsSuccessStatusCode)
                {
                    await DisplayAlert("Éxito", "Estudiante actualizado correctamente", "Cerrar");
                    await Navigation.PushAsync(new vCrud());
                }
                else
                {
                    string errorMessage = await response.Content.ReadAsStringAsync();
                    _ = DisplayAlert("Error", $"No se pudo actualizar el estudiante: {errorMessage}", "Cerrar");
                }
            }
        }
        catch (Exception ex)
        {
            _ = DisplayAlert("Alerta", ex.Message, "Cerrar");
        }
    }

    private async void btnEliminar_Clicked(object sender, EventArgs e)
    {
        try
        {
            bool isConfirmed = await DisplayAlert("Confirmar", "¿Estás seguro de eliminar este estudiante?", "Sí", "No");

            if (isConfirmed)
            {
                using (HttpClient client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Add("Accept", "application/json");


                    HttpResponseMessage response = await client.DeleteAsync($"{Url}/{txtCodigo.Text}");

                    if (response.IsSuccessStatusCode)
                    {
                        if (response.StatusCode == System.Net.HttpStatusCode.NoContent || response.StatusCode == System.Net.HttpStatusCode.OK)
                        {
                            await DisplayAlert("Éxito", "Estudiante eliminado correctamente", "Cerrar");
                            await Navigation.PushAsync(new vCrud());
                        }
                        else
                        {
                            await DisplayAlert("Error", "No se pudo eliminar el estudiante. Código de estado: " + response.StatusCode, "Cerrar");
                        }
                    }
                    else
                    {
                        string errorMessage = await response.Content.ReadAsStringAsync();
                        await DisplayAlert("Error", $"No se pudo eliminar el estudiante: {errorMessage}", "Cerrar");
                    }
                }
            }
        }
        catch (Exception ex)
        {
            await DisplayAlert("Alerta", ex.Message, "Cerrar");
        }
    }

}