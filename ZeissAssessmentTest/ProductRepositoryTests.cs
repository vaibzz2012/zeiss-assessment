using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Linq;
using ZeissAssessment;
using Xunit;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ZeissAssessmentTest

{
   
    public class ProductRepositoryTests
    {
        private readonly ProductDbContext _context;
        private readonly ProductRepository _repository;

        public ProductRepositoryTests()
        {
            var options = new DbContextOptionsBuilder<ProductDbContext>()
                .UseSqlServer()
                .Options;

            _context = new ProductDbContext(options);

            var mockLogger = new Mock<ILogger<ProductRepository>>();
            _repository = new ProductRepository(_context, mockLogger.Object);
        }

        [Fact]
        public void AddProduct_ShouldAddProductSuccessfully()
        {
            var product = new Product
            {
                Name = "Test Product",
                AvailableQuantity = 10,
                Price = 100m
            };

            var addedProduct = _repository.AddProduct(product);

            Assert.IsNotNull(addedProduct);
            Assert.AreEqual(product.Name, addedProduct.Name);
            Assert.AreEqual(product.AvailableQuantity, addedProduct.AvailableQuantity);
        }

        [Fact]
        public void GetProductById_ShouldReturnCorrectProduct()
        {
            var product = new Product
            {
                Name = "Test Product",
                AvailableQuantity = 10,
                Price = 100m
            };

            var addedProduct = _repository.AddProduct(product);
            var retrievedProduct = _repository.GetProductById(addedProduct.Id);

            Assert.IsNotNull(retrievedProduct);
            Assert.Equals(addedProduct.Id, retrievedProduct.Id);
        }

        [Fact]
        public void UpdateStock_ShouldDecrementStockSuccessfully()
        {
            var product = new Product
            {
                Name = "Test Product",
                AvailableQuantity = 10,
                Price = 100m
            };

            var addedProduct = _repository.AddProduct(product);
            var result = _repository.UpdateStock(addedProduct.Id, 5, true, out var errorMessage);

            Assert.IsTrue(result);
            Assert.IsNull(errorMessage);
            var updatedProduct = _repository.GetProductById(addedProduct.Id);
            Assert.Equals(5, updatedProduct.AvailableQuantity);
        }

        [Fact]
        public void UpdateStock_ShouldNotAllowDecrementBelowZero()
        {
            var product = new Product
            {
                Name = "Test Product",
                AvailableQuantity = 3,
                Price = 100m
            };

            var addedProduct = _repository.AddProduct(product);
            var result = _repository.UpdateStock(addedProduct.Id, 5, true, out var errorMessage);

            Assert.IsFalse(result);
            Assert.IsNotNull(errorMessage);
            Assert.Equals("Insufficient stock", errorMessage);
        }

        [Fact]
        public void DeleteProduct_ShouldRemoveProductSuccessfully()
        {
            var product = new Product
            {
                Name = "Test Product",
                AvailableQuantity = 10,
                Price = 100m
            };

            var addedProduct = _repository.AddProduct(product);
            _repository.DeleteProduct(addedProduct.Id);

            var deletedProduct = _repository.GetProductById(addedProduct.Id);
            Assert.IsNull(deletedProduct);
        }
    }
}
