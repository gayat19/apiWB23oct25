using FirstAPI.Contexts;
using FirstAPI.Interfaces;
using FirstAPI.Models;
using FirstAPI.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace TestProject1
{
    public class ProductRepoTest
    {
        ShoppingContext28Oct25 context;
        [SetUp]
        public void Setup()
        {
            var opts = new DbContextOptionsBuilder<ShoppingContext28Oct25>()
                .UseInMemoryDatabase(databaseName: "TestDB")
                .Options;
            context = new ShoppingContext28Oct25(opts);
        }

        [Test]
        public async Task AddProductPassTest()
        {
            //Arrange
            Category category = new Category() { Name = "Electronics" };
            context.Categories.Add(category);
            context.SaveChanges();
            IRepository<int,Product> repository = new ProductRepositoryDB(context);
            Product product = new Product()
            {
                Title = "Test Product",
                Price = 100,
                IsDiscontinued = false,
                Stock = 50,
                CategoryId = category.Id
            };
            //Action
            product = await repository.Add(product);

            //Assert
            Assert.That(product.Id, Is.GreaterThan(0));
        }
        [Test]
        public async Task AddProductFailTest()
        {
            IRepository<int, Product> repository = new ProductRepositoryDB(context);
            Product product = new Product()
            {
                Title = null,
                Price = 100,
                IsDiscontinued = false,
                Stock = 50,
                CategoryId = 10 // Non-existent category
            };
            //Action
            product = await repository.Add(product);

            //Assert
            Assert.That(product.Id, Is.GreaterThan(0));
        }
            [TearDown]
        public void TearDown()
        {
            context.Database.EnsureDeleted();
            context.Dispose();
        }
    }
}