using System.Net;
using System.Text;
using System.Text.Json;

namespace MVM.LojaVirtual.MVC.Services.Common;

public abstract class Service
{
    protected T DeserializarObjeto<T>(string content)
    {
        return JsonSerializer.Deserialize<T>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true }) 
               ?? throw new ArgumentNullException("Erro ao deserializar o objeto");
    }

    protected StringContent SerializarObjeto(object data)
    {
        return new StringContent(JsonSerializer.Serialize(data), Encoding.UTF8, "application/json");
    }
    
    protected bool TratarErrorRequest(HttpResponseMessage response)
    {
        switch ((int)response.StatusCode)
        {
            case 400: return false;
            case 401:
            case 403:
            case 404:
            case 500: throw new HttpRequestException("Error no servidor", new Exception(), HttpStatusCode.InternalServerError);
        }

        response.EnsureSuccessStatusCode();
        return true;
    }
}