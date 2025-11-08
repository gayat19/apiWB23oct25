using AutoMapper;
using FirstAPI.Contexts;
using FirstAPI.Interfaces;
using FirstAPI.Models;
using FirstAPI.Models.DTOs;
using FirstAPI.Repositories;
using FirstAPI.Services;
using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestProject1
{
    internal class ProductServiceTest
    {
        ShoppingContext28Oct25 context;
        IRepository<int, Product> repository;
        IProductService productService;

        [SetUp]
        public void Setup()
        {
            var opts = new DbContextOptionsBuilder<ShoppingContext28Oct25>()
                .UseInMemoryDatabase(databaseName: "TestDB")
                .Options;
            context = new ShoppingContext28Oct25(opts);
            Category category = new Category() { Name = "Electronics" };
            context.Categories.Add(category);
            context.SaveChanges();
            repository = new ProductRepositoryDB(context);

        }

        [Test]
        public async Task AddProductPassTest()
        {
            //Arrange
            AddProductRequest product = new AddProductRequest()
            {
                Title = "Test Product",
                Price = 100,
                Stock = 50,
                CategoryId = 1
            };
            Mock<IMapper> mockMapper = new Mock<IMapper>();
            productService = new ProductService(repository, mockMapper.Object );
            //Action
            var result = await productService.AddProduct(product);

            //Assert
            Assert.That(result.Id, Is.GreaterThan(0));
        }
        [Test]
        public async Task ListProductTest()
        {
            AddProductRequest product = new AddProductRequest()
            {
                Title = "Test Product",
                Price = 100,
                Stock = 50,
                CategoryId = 1
            };
            Mock<IMapper> mockMapper = new Mock<IMapper>();
            mockMapper.Setup(m => m.Map<ProductListResponse>(It.IsAny<Product>()))
                      .Returns((Product src) => new ProductListResponse
                      {
                          Id = src.Id,
                          Title = src.Title,
                          Price = src.Price
                      });
            productService = new ProductService(repository, mockMapper.Object);
            await productService.AddProduct(product);
            //Action
            var result = await productService.GetAllProducts();

            //Assert
            Assert.That(result.Count(), Is.GreaterThan(0));
        }
        [TearDown]
        public void TearDown()
        {
            context.Database.EnsureDeleted();
            context.Dispose();
        }
    }
}
