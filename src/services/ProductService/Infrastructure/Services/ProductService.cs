using AutoMapper;
using System.Web;
using Infrastructure.EFCore.Helpers;
using Newtonsoft.Json;

namespace ProductService.Services
{
    public interface IProductService
    {
        Task<Response<object>> GetProductByUrlAsync(string uri);
    }
    public class BaseProductService : IProductService
    {
        private readonly IRepository<Product> _repository;
        private readonly IMapper _mapper;

        public BaseProductService(IRepository<Product> repository, IMapper mapper) 
        { 
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<Response<object>> GetProductByUrlAsync(string uri)
        {
            var id = GetIdFromUrl(uri);

            if (id == null)
            {
                return ResponseHelper.CreateNotFoundResponse<object>("Invalid URL");
            }

            var client = new HttpClient();
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri($"https://otapi-taobao-tmall-simple.p.rapidapi.com/GetItemInfo?language=en&itemId={id}"),
                Headers =
            {
                { "X-RapidAPI-Key", "6d26cd3969msh80667aed8632b61p1d6391jsn03d54fce4d7e" },
                { "X-RapidAPI-Host", "otapi-taobao-tmall-simple.p.rapidapi.com" },
            },
            };

            using (var response = await client.SendAsync(request))
            {
                response.EnsureSuccessStatusCode();
                var body = await response.Content.ReadAsStringAsync();

                var result = JsonConvert.DeserializeObject(body);

                return ResponseHelper.CreateSuccessResponse(result);

            };
        }

        private string? GetIdFromUrl(string url)
        {
            Uri myUri = new Uri(url);
            Console.WriteLine("url: " + url);
            Console.WriteLine("myUri: " + myUri);
            string id = HttpUtility.ParseQueryString(myUri.Query).Get("id");

            return id;
        }

    }
}
