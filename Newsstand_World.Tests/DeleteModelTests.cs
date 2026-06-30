using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Newsstand_World.Data;
using Newsstand_World.Model;
using Newsstand_World.Pages.ProductType;
using Xunit;

namespace Newsstand_World.Tests.Pages.ProductType
{
    public class DeleteModelTests
    {
        private ApplicationDbContext GetDbContext()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            return new ApplicationDbContext(options);
        }

        [Fact]
        public async Task OnGet_WithValidId_ShouldReturnPage()
        {
            // Arrange
            var context = GetDbContext();
            var productType = new Newsstand_World.Model.ProductType { Name = "Журналы" };
            context.ProductTypes.Add(productType);
            await context.SaveChangesAsync();

            var pageModel = new DeleteModel(context);

            // Act
            var result = await pageModel.OnGetAsync(productType.Id);

            // Assert
            Assert.IsType<PageResult>(result);
            Assert.NotNull(pageModel.ProductType);
            Assert.Equal("Журналы", pageModel.ProductType.Name);
        }

        [Fact]
        public async Task OnGet_WithInvalidId_ShouldReturnNotFound()
        {
            // Arrange
            var context = GetDbContext();
            var pageModel = new DeleteModel(context);

            // Act
            var result = await pageModel.OnGetAsync(999);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }
    }
}