using System.Collections.Specialized;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Web;
using MediCompendium.Models;
using MediCompendium.Models.ApiRecords;

namespace MediCompendium.Services;

public class ApiCommands {
    public static async Task<List<NdcData>> FetchMedications(int skipCount) {
        try {
            string reqUri = $"{Constants.NdcRoute}search=finished:true&{Constants.Limit}&skip={skipCount}";
            HttpResponseMessage responseMessage = await ApiClient.Client.GetAsync(reqUri);
            var response = await responseMessage.Content.ReadFromJsonAsync<JsonDocument>();
            var content = response.RootElement.GetProperty("results").GetRawText();
            return JsonSerializer.Deserialize<List<NdcData>>(content) ?? new List<NdcData>();
        }
        catch(Exception err) {
            Console.WriteLine(err);
            return new List<NdcData>();
        }
    }
    
    public static async Task<List<NdcData>> SearchMedication(string medicationName, int skipCount) {
        try {
            string reqUri = $"{Constants.NdcRoute}search=brand_name:\"{medicationName}\"finished:True&{Constants.Limit}&skip={skipCount}";
            HttpResponseMessage responseMessage = await ApiClient.Client.GetAsync(reqUri);
            var response = await responseMessage.Content.ReadFromJsonAsync<JsonDocument>();
            var content = response.RootElement.GetProperty("results").GetRawText();
            return JsonSerializer.Deserialize<List<NdcData>>(content) ?? new List<NdcData>();
        }
        catch(Exception err) {
            Console.WriteLine(err);
            return new List<NdcData>();
        }
    }

    public static async Task<NdcData> FetchMedicationNdc(string productNdc) {
        try {
            var reqUri = $"{Constants.NdcRoute}search=product_ndc:\"{productNdc}\"";
            var responseMessage = await ApiClient.Client.GetAsync(reqUri);
            var response = await responseMessage.Content.ReadFromJsonAsync<JsonDocument>();
            var content = response.RootElement.GetProperty("results").GetRawText();
            var result = JsonSerializer.Deserialize<List<NdcData>>(content);
            return result != null ? result[0] : new NdcData();
        }
        catch (Exception err) {
            Console.WriteLine(err);
            return new NdcData();
        } 
    }
    public static async Task<LabelData> FetchMedicationLabel(string productNdc) {
        try {
            var reqUri = $"{Constants.LabelRoute}search=openfda.product_ndc:\"{productNdc}\"";
            var responseMessage = await ApiClient.Client.GetAsync(reqUri);
            var response = await responseMessage.Content.ReadFromJsonAsync<JsonDocument>();
            var content = response.RootElement.GetProperty("results").GetRawText();
            var result = JsonSerializer.Deserialize<List<LabelData>>(content);
            return result != null ? result[0] : new LabelData();
        }
        catch (Exception err) {
            Console.WriteLine(err);
            return new LabelData();
        } 
    }
}