namespace MediCompendium.Services;

public class ApiClient {
    public static HttpClient Client { get; } = new HttpClient();
}