// NET 6.0 Notation
// HTTP CLIENT

using Microsoft.AspNetCore.WebUtilities;
using System.Net.Mime;
using System.Text;

string BASE_URI = "http://localhost:8080";

HttpClient client = new HttpClient();
client.BaseAddress = new Uri(BASE_URI);

//Method called & Parameters Dictionnary //call http://localhost:8080/Incr?value=49
string requestUri = QueryHelpers.AddQueryString("Incr", new Dictionary<string, string>()
{
    { "value", "49" } //Change 49 
});

var request = new HttpRequestMessage(HttpMethod.Get, requestUri);

var response = await client.SendAsync(request).ConfigureAwait(false);
response.EnsureSuccessStatusCode();

var responseBody = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
Console.WriteLine(responseBody);