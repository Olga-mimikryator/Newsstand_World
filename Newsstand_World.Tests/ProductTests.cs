using Newsstand_World.Model;
using System.ComponentModel.DataAnnotations;
using Xunit;

namespace Newsstand_World.Tests.Model
{
    public class ProductTests
    {
        [Fact]
        public void Product_WithValidData_ShouldBeValid()
        {
            // Arrange
            var product = new Product
            {
                Name = "Научный журнал",
                TypeID = 1,
                PublisherID = 1,
                Price = 99.99,
                Quantity = 10
            };

            var context = new ValidationContext(product);
            var results = new List<ValidationResult>();

            // Act
            var isValid = Validator.TryValidateObject(product, context, results, true);

            // Assert
            Assert.True(isValid);
            Assert.Empty(results);
        }
    }
}