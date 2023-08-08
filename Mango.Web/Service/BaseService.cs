using Mango.Web.Models;
using Mango.Web.Service.IService;
using Newtonsoft.Json;
using System.Net;
using static Mango.Web.Utility.SD;

namespace Mango.Web.Service
{
    public class BaseService : IBaseService
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public BaseService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory; 
        }

        public async Task<ResponseDTO?> SendAsync(RequestDTO requestDTO)
        {
            try
            {
                HttpClient client = _httpClientFactory.CreateClient("MangoAPI");

                HttpRequestMessage message = new();
                message.Headers.Add("Content-Type", "application/json");
                message.Headers.Add("Accept", "application/json");

                //token

                message.RequestUri = new Uri(requestDTO.Url);
                if (requestDTO.Data != null)
                {
                    message.Content = new StringContent(JsonConvert.SerializeObject(requestDTO.Data));
                }

                HttpResponseMessage? apiResponse = null;
                switch (requestDTO.ApiType)
                {
                    case ApiType.POST:
                        message.Method = HttpMethod.Post;
                        break;
                    case ApiType.PUT:
                        message.Method = HttpMethod.Put;
                        break;
                    case ApiType.DELETE:
                        message.Method = HttpMethod.Delete;
                        break;
                    default:
                        message.Method = HttpMethod.Get;
                        break;
                }

                apiResponse = await client.SendAsync(message);

                switch (apiResponse.StatusCode)
                {
                    case HttpStatusCode.NotFound:
                        return new ResponseDTO() { IsScucess = false, Message = "Not Found" };
                    case HttpStatusCode.Forbidden:
                        return new ResponseDTO() { IsScucess = false, Message = "Access Denied" };
                    case HttpStatusCode.Unauthorized:
                        return new ResponseDTO() { IsScucess = false, Message = "Unathorized" };
                    case HttpStatusCode.InternalServerError:
                        return new ResponseDTO() { IsScucess = false, Message = "Internal Server Error" };
                    default:
                        var apiContent = await apiResponse.Content.ReadAsStringAsync();
                        var apiResponseDTO = JsonConvert.DeserializeObject<ResponseDTO>(apiContent);
                        return apiResponseDTO;
                }

            }catch(Exception ex)
            {
                var dto = new ResponseDTO()
                {
                    Message = ex.Message.ToString(),
                    IsScucess = false
                };
                return dto;
            }
        }
    }
}
