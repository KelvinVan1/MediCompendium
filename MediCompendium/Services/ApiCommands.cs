using System.Collections.Specialized;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Web;
using MediCompendium.Models.ApiRecords;

namespace MediCompendium.Services;

public class ApiCommands {
    /// <summary>
    /// Fetches a list of medications from the OpenFDA API
    /// </summary>
    /// <returns>A list of medication NDC data. Returns an empty list if request fails</returns>
    public static async Task<List<NdcData>> FetchMedications() {
        try {
            HttpResponseMessage responseMessage = await ApiClient.Client.GetAsync($"{Constants.NdcRoute}{Constants.Limit}");
            var response = await responseMessage.Content.ReadFromJsonAsync<JsonDocument>();
            var content = response.RootElement.GetProperty("results").GetRawText();
            return JsonSerializer.Deserialize<List<NdcData>>(content) ?? new List<NdcData>();
        }
        catch(Exception err) {
            Console.WriteLine(err);
            return new List<NdcData>();
        }
    }
    
    
}