using Newtonsoft.Json;
using System.Net;
using System.Text;

namespace cfloresS6Jueves.Views;

public partial class AgregarEstudiante : ContentPage
{
    private const string Url = "https://petscan-hkbtgngtbecxf5ag.centralus-01.azurewebsites.net/api/Metricas";

    public AgregarEstudiante()
	{
		InitializeComponent();
	}

    private async void btnAgrega_Clicked(object sender, EventArgs e)
    {
        try
        {
            var trama = new
            {
                id = Guid.NewGuid().ToString(), 
                especie = txtEspecie.Text,
                nombreRaza = txtNombreRaza.Text,
                origen = txtOrigen.Text,
                usuario = Guid.NewGuid().ToString(),
                fechaRegistro = DateTime.UtcNow.ToString("yyyy-MM-ddTHH:mm:ss.fffZ"),
                fechaModificacion = DateTime.UtcNow.ToString("yyyy-MM-ddTHH:mm:ss.fffZ"),
                estado = true
            };

            string json = JsonConvert.SerializeObject(trama);

            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add("Accept", "application/json");

                var content = new StringContent(json, Encoding.UTF8, "application/json");

                HttpResponseMessage response = await client.PostAsync(Url, content);

                if (response.IsSuccessStatusCode)
                {
                    await Navigation.PushAsync(new vCrud());
                }
                else
                {
                    string errorMessage = await response.Content.ReadAsStringAsync();
                    _ = DisplayAlert("Error", $"No se pudo agregar el elemento: {errorMessage}", "Cerrar");
                }
            }
        }
        catch (Exception ex)
        {
            _ = DisplayAlert("Alerta", ex.Message, "Cerrar");
        }
    }
}

/*
  [
{
  "id": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
  "especie": "string",
  "nombreRaza": "string",
  "origen": "string",
  "usuario": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
  "fechaRegistro": "2025-05-20T23:25:31.851Z",
  "fechaModificacion": "2025-05-20T23:25:31.851Z",
  "estado": true
}
]
  */