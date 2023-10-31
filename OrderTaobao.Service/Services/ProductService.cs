using BaseSource.Dto;
using BaseSource.Model;

namespace BaseSource.BackendAPI.Services
{
    public interface IProductService
    {
        Task<Response<List<Product>>> Get();
        Task<Response<Product>> GetById(string id);
        Task<Response<bool>> Add(Product product, string user);
        Task<Response<bool>> Update(string id, string user, Product request);
        Task<Response<bool>> Enable(string id, string user, bool enable);
        Task<Response<bool>> Erase(string id, string user);
    }

    public class ProductService : IProductService
    {
        private readonly IRepository<Product> _productRepo;
        public ProductService(IRepository<Product> productRepo)
        {
            _productRepo = productRepo;
        }

        public async Task<Response<List<Product>>> Get()
        {
            var products = await _productRepo.ReadAllAsync();

            if (products is null)
                return ResponseHelper.CreateErrorResponse<List<Product>>(404, "Products not found!");

            products = products.Where(product => product.Enable).ToList();

            return ResponseHelper.CreateSuccessResponse(products);
        }

        public async Task<Response<Product>> GetById(string id)
        {
            Product product = await _productRepo.ReadByIdAsync(id);

            if (product is null)
                return ResponseHelper.CreateErrorResponse<Product>(404,"Product not found!");

            return ResponseHelper.CreateSuccessResponse(product);
        }

        public async Task<Response<bool>> Add(Product product,string user)
        {
            await _productRepo.AddAsync(product, user);

            return ResponseHelper.CreateCreatedResponse(true);
        }

        public async Task<Response<bool>> Update(string id, string user,Product request)
        {
            Product product = await _productRepo.ReadByIdAsync(id);

            if (product is null)
                return ResponseHelper.CreateErrorResponse<bool>(404, "Product not found!");

            product = request;

            await _productRepo.UpdateAsync(product, user);

            return ResponseHelper.CreateSuccessResponse(true);
        }

        public async Task<Response<bool>> Enable(string id, string user, bool enable)
        {
            Product product = await _productRepo.ReadByIdAsync(id);

            if (product is null)
                return ResponseHelper.CreateErrorResponse<bool>(404, "Product not found!");

            product.Enable = enable;

            await _productRepo.UpdateAsync(product, user);

            return ResponseHelper.CreateSuccessResponse(true);
        }

        public async Task<Response<bool>> Erase(string id, string user)
        {
            Product product = await _productRepo.ReadByIdAsync(id);

            if (product is null)
                return ResponseHelper.CreateErrorResponse<bool>(404, "Product not found!");

            await _productRepo.DeleteAsync(product, user);

            return ResponseHelper.CreateSuccessResponse(true);
        }
    }
}
