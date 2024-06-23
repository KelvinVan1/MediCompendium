using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using MediCompendium.Models.ApiRecords;

namespace MediCompendium.Services;

public class ApiCommands {
    
    public static async void FetchMedications()  {
        HttpResponseMessage responseMessage = await ApiClient.Client.GetAsync(Constants.NdcRoute);
        var response = await responseMessage.Content.ReadFromJsonAsync<JsonDocument>();
        var content = response.RootElement.GetProperty("results").GetRawText();
        var data = JsonSerializer.Deserialize<List<NdcData>>(content);
    }
}