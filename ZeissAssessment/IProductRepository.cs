namespace ZeissAssessment
{
    public interface IProductRepository
    {
        Product AddProduct(Product product);
        Product GetProductById(int id);
        IQueryable<Product> GetAllProducts();
        Product UpdateProduct(int id, Product updatedProduct);
        void DeleteProduct(int id);
        bool UpdateStock(int id, int quantity, bool decrement, out string errorMessage);
    }
}
