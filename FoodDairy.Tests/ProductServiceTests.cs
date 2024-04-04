using FluentAssertions;
using FoodDiary.Application.Services;
using FoodDiary.Domain.Interfaces;
using FoodDiary.Domain.Models;
using Moq;
using Xunit;

namespace FoodDairy.Tests
{
    public class ProductServiceTests
    {
        private Mock<IProductRepository> mockProductRepository = new Mock<IProductRepository>();
        private ProductService productService;
        

        [Fact]
        public void AddNew_ShouldAddProductBasedOnValidModel()

        {
            //Arrange
            Product product = new Product() { Id = 1, Name = "testProduct", Calories = 100, Carbs = 100, Protein = 100, Fat = 100, Weight = 100 };
            mockProductRepository.Setup(p => p.Create(It.IsAny<Product>())).Returns(product.Id);
            productService = new ProductService(mockProductRepository.Object);

            //Act
            var newProductId = productService.AddNew(product);

            //Assert
            newProductId.Should().NotBe(null);
            mockProductRepository.Verify(p => p.Create(It.IsAny<Product>()), Times.Once);
        }

        [Fact]
        public void AddNew_ShouldNotAddIfNameIsEmpty()
        {
            //Arrange
            Product product = new Product() { Id = 1, Name = "", Calories = 100, Carbs = 100, Protein = 100, Fat = 100, Weight = 100 };
            mockProductRepository.Setup(p => p.Create(It.IsAny<Product>())).Returns(product.Id);
            productService = new ProductService(mockProductRepository.Object);

            //Act
            var newProductId = productService.AddNew(product);

            //Assert
            newProductId.Should().Be(-1);

        }

        [Fact]
        public void GetAll_ShouldReturnAllProductsFromTheList()
        {
            //Arrange
            var products = new List<Product>()
            {
                new Product() { Id = 1, Name = "Jaja", Calories = 100, Carbs = 100, Protein = 100, Fat = 100, Weight = 100 },
                new Product() { Id = 2, Name = "Egg", Calories = 200, Carbs = 100, Protein = 100, Fat = 100, Weight = 100 },
                new Product() { Id = 3, Name = "Czosnek", Calories = 300, Carbs = 100, Protein = 100, Fat = 100, Weight = 100 }
            };
            mockProductRepository.Setup(p => p.GetAll()).Returns(products);
            productService = new ProductService(mockProductRepository.Object);

            //Act
            var viewProducts = productService.GetAll();

            //Assert
            viewProducts.Should().HaveCount(3);
        }

        [Fact]
        public void Edit_ShouldEditExistingProduct()
        {
            //Arrange
            Product product = new Product() { Id = 1, Name = "testProduct", Calories = 100, Carbs = 100, Protein = 100, Fat = 100 };
            int productId = 1;
            string productName = "Test name";
            double productCalories = 100;
            double productCartbs = 100;
            double productProtein = 100;
            double productFat = 100;
            mockProductRepository.Setup(p => p.Edit(It.IsAny<Product>()));
            productService = new ProductService(mockProductRepository.Object);

            //Act
            var editedProduct = productService.Edit(productId, productName, productCalories, productCartbs, productProtein, productFat);

            //Assert
            editedProduct.Id.Should().Be(productId);
            editedProduct.Protein.Should().Be(productProtein);
            editedProduct.Calories.Should().Be(productCalories);
            editedProduct.Fat.Should().Be(productFat);
            editedProduct.Carbs.Should().Be(productCartbs);
            editedProduct.Name.Should().Be(productName);
        }
    }


}