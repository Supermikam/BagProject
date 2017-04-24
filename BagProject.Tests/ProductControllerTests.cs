using System.Collections.Generic;
using System.Linq;
using Moq;
using BagProject.Controllers;
using BagProject.Models;
using BagProject.Models.ViewModels;
using Xunit;

namespace BagProject.Tests
{
    class ProductControllerTests
    {
        [Fact]
        public void Can_Paginate()
        {
            // Arrange
            Mock<ProductRepository> mock = new Mock<ProductRepository>();
            mock.Setup(m => m.Products).Returns(new Product[] {
                new Product {ProductID = 1, ProductName = "P1"},
                new Product {ProductID = 2, ProductName = "P2"},
                new Product {ProductID = 3, ProductName = "P3"},
                new Product {ProductID = 4, ProductName = "P4"},
                new Product {ProductID = 5, ProductName = "P5"}
                });
            ProductController controller = new ProductController(mock.Object);
            controller.PageSize = 3;

            // Act
            int page = 2;
            ProductListViewModel result =
            controller.GetAllProduct("", page).ViewData.Model as ProductListViewModel;
            // Assert
            Product[] prodArray = result.Products.ToArray();
            Assert.True(prodArray.Length == 2);
            Assert.Equal("P4", prodArray[0].ProductName);
            Assert.Equal("P5", prodArray[1].ProductName);
        }
    }
}
