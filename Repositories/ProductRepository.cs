using VisualSoftAspCoreApi.Context;
using VisualSoftAspCoreApi.Contracts;
using VisualSoftAspCoreApi.Entities;
using VisualSoftAspCoreApi.Dto;

namespace VisualSoftAspCoreApi.Repository
{
    public class ProductRepository : IProductRepository
    {
        private readonly DapperContext _context;
        public ProductRepository(DapperContext context) => _context = context;

        // Get All Products
        public async Task<IEnumerable<Product>> GetProducts()
        {
           var query = "SELECT * FROM Products";
           using (var conn= _context.CreateConnection())
           {
            var products = await conn.QueryAsync<Product>(query);
            return products.ToList();
           }
        }

        // Get Product by Id
        public async Task<Product> GetProduct(int id)
        {
           var query = "SELECT * FROM Products WHERE Id = @Id";
           using (var conn= _context.CreateConnection())
           {
            var product = await conn.QueryAsync<Product>(query, new {id});
            return product;
           }
        }

        // Create new Product
        public async Task<Product> CreateProduct(ProductCreationDto product)
        { 
            var query= " INSERT INTO Products (model, details,advancePayment, monthlyInstallment, financeDuration,imageURL) VALUES (@model,@details,@advancePayment,@monthlyInstallment, @financeDuration,@imageURL)"+
            "SELECT CAST(SCOPE_IDENTITY() AS int)";

            var parameters = new DynamicParameters();
            parameters.Add("model", product.model, DbType.string);
            parameters.Add("details", product.details, DbType.string);
            parameters.Add("advancePayment", product.advancePayment, DbType.int);
            parameters.Add("monthlyInstallment", product.monthlyInstallment, DbType.int);
            parameters.Add("financeDuration", product.financeDuration, DbType.int);
            parameters.Add("imageURL", product.imageURL, DbType.string);

            using (var conn = new _context.CreateConnection())
            {
                var id = await conn.QuerySingleAsync<int>(query, parameters);

                var CreateProduct = new Product
                {
                    Id = id,
                    model =product.model,
                    details = product.details,
                    advancePayment = product.advancePayment,
                    monthlyInstallment = product.monthlyInstallment,
                    financeDuration = product.financeDuration,
                    imageURL = product.imageURL
                };

                return CreateProduct;
            }

        }


        // Update an Existing Product
        public async Task UpdateProduct(int id, ProductUpdateDto product)
        {
            var query = "UPDATE Products SET model=@model, details=@details,advancePayment=@advancePayment, monthlyInstallment=@monthlyInstallment, financeDuration=@financeDuration,imageURL=@imageURL WHERE Id=@Id ";

            var parameters = new DynamicParameters();
            parameters.Add("Id", product.Id, DbType.int);
            parameters.Add("model", product.model, DbType.string);
            parameters.Add("details", product.details, DbType.string);
            parameters.Add("advancePayment", product.advancePayment, DbType.int);
            parameters.Add("monthlyInstallment", product.monthlyInstallment, DbType.int);
            parameters.Add("financeDuration", product.financeDuration, DbType.int);
            parameters.Add("imageURL", product.imageURL, DbType.string);

             using (var conn = new _context.CreateConnection())
            {
                await conn.ExecuteAsync(query, parameters);

            }
        }

         // Delete an Existing Product
        public async Task DeleteProduct(int id)
        {
            var query = "DELETE FROM Products WHERE Id =@Id";
            using (var conn = new _context.CreateConnection())
            {
                await conn.ExecuteAsync(query, new {id});

            }
         }

    }
}