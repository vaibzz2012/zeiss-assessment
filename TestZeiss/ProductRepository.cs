using System.Collections.Concurrent;

namespace ZeissAssessment
{
    public class ProductRepository : IProductRepository
    {
        private readonly ProductDbContext _context;
        private readonly ILogger<ProductRepository> _logger;
        private static ConcurrentDictionary<int, bool> GeneratedIds = new ConcurrentDictionary<int, bool>();

        public ProductRepository(ProductDbContext context, ILogger<ProductRepository> logger)
        {
            _context = context;
            Console.WriteLine("ProductRepository initialized.");
            _logger = logger;
        }

        public Product AddProduct(Product product)
        {
            try
            {
                product.Id = 0;//GenerateUniqueId();
                Console.WriteLine($"Adding product with ID: {product.Id}");
                _context.Products.Add(product);
                _context.SaveChanges();
                return product;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while adding a product.");
                throw;
            }
           
        }

        public Product GetProductById(int id)
        {
            try
            {
                Console.WriteLine($"Fetching product with ID: {id}");
                return _context.Products.Find(id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while fetching a product by ID.");
                throw;
            }
        }

        public IQueryable<Product> GetAllProducts()
        {
            try
            {
                Console.WriteLine("Fetching all products.");
                return _context.Products;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while fetching all products.");
                throw;
            }
        }

        public Product UpdateProduct(int id, Product updatedProduct)
        {
            
            try
            {
                Console.WriteLine($"Updating product with ID: {id}");
                var product = _context.Products.Find(id);
                if (product == null) return null;

                product.Name = updatedProduct.Name;
                product.AvailableQuantity = updatedProduct.AvailableQuantity;
                product.Price = updatedProduct.Price;

                _context.SaveChanges();
                return product;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while updating a product.");
                throw;
            }
        }

        public void DeleteProduct(int id)
        {
            
            try
            {
                Console.WriteLine($"Deleting product with ID: {id}");
                var product = _context.Products.Find(id);
                if (product == null) return;

                _context.Products.Remove(product);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while deleting a product.");
                throw;
            }
        }

        public bool UpdateStock(int id, int quantity, bool decrement, out string errorMessage)
        {
           
            try
            {
                Console.WriteLine($"Updating stock for product ID: {id}, Quantity: {quantity}, Decrement: {decrement}");
                var product = _context.Products.Find(id);
                if (product == null) 
                {
                    errorMessage = "Product not Found.";
                    return false; 
                }
                if (decrement)
                {
                    if (!ProductValidation.ValidateStockDecrement(product, quantity, out errorMessage))
                    {
                        return false;
                    }
                    product.AvailableQuantity -= quantity;
                }
                else
                {
                    if (!ProductValidation.ValidateStockIncrement(quantity, out errorMessage))
                    {
                        return false;
                    }
                    product.AvailableQuantity += quantity;
                }

                _context.SaveChanges();
                errorMessage = null;
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while updating stock.");
                errorMessage = "An error occurred while updating stock.";
                return false;
            }
        }

        private int GenerateUniqueId()
        {
            int id;
            do
            {
                id = new System.Random().Next(100000, 999999);
            } while (!GeneratedIds.TryAdd(id, true));

            Console.WriteLine($"Generated unique ID: {id}");
            return id;
        }
    }
}
