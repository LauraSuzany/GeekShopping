using System.Net.Http.Headers;
using System.Text.Json;
namespace GeekShopping.web.Ultils
{
    public static class HttpClientExtensions
    {
        private static MediaTypeHeaderValue contentType = new MediaTypeHeaderValue("application/json");

        /// <summary>
        /// Com a response do Médoto que faz a requisição para Api ele verifica se o status é sucess caso seja ele retorna a 
        /// string json com os dados do banco e transforma em um objeto.
        /// </summary>
        /// <typeparam name="T">Objeto genérico para recebero a strong Json</typeparam>
        /// <param name="responseMessage"> A resposta da comunicação entre servidor e a API.WEB</param>
        /// <returns>Objeto json convertido para class do tipo Genérico T</returns>
        /// <exception cref="ApplicationException"></exception>
        public static async Task<T> ReadContentAs<T>(this HttpResponseMessage responseMessage)
        {
            if (!responseMessage.IsSuccessStatusCode) throw new ApplicationException($"Something went wrong calling API: {responseMessage.ReasonPhrase}", null);
            string dataAsString = await responseMessage.Content.ReadAsStringAsync().ConfigureAwait(false);

            T? t = JsonSerializer.Deserialize<T>(dataAsString, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            return t;
        }
        public static Task<HttpResponseMessage> PostAsJson<T>(this  HttpClient httpClient, string url, T data)
        {
            var dataAsString = JsonSerializer.Serialize(data);
            var content = new StringContent(dataAsString);
            content.Headers.ContentType = contentType;
            return httpClient.PostAsJson(url, data);
        }
        public static Task<HttpResponseMessage> PutAsJson<T>(this  HttpClient httpClient, string url, T data)
        {
            var dataAsString = JsonSerializer.Serialize(data);
            var content = new StringContent(dataAsString);
            content.Headers.ContentType = contentType;
            return httpClient.PutAsJson(url, data);
        }
    }
}
