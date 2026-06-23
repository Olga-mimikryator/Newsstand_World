using Newsstand_World.Model;
using System.ComponentModel.DataAnnotations;
using Xunit;

namespace Newsstand_World.Tests.Model
{
    public class PurchaserTests
    {
        [Fact]
        public void Purchaser_WithValidData_ShouldBeValid()
        {
            // Arrange
            var purchaser = new Purchaser
            {
                Name = "Иван",
                LastName = "Иванов",
                Phone = "+7-999-123-45-67",
                BirthDate = new DateTime(1990, 5, 15)
            };

            var context = new ValidationContext(purchaser);
            var results = new List<ValidationResult>();

            // Act
            var isValid = Validator.TryValidateObject(purchaser, context, results, true);

            // Assert
            Assert.True(isValid);
            Assert.Empty(results);
        }
    }
}