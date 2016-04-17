using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Authority.DomainModel;
using Authority.EntityFramework;
using Authority.Tests.Common;
using Authority.UnitOfWork.Products;
using Moq;
using Xunit;

namespace Authority.Tests.Products
{
    public sealed class GetProductDetailsTests
    {
        [Fact]
        public async Task GetDetails()
        {
            List<Product> products = new List<Product>
            {
                new Product
                {
                    IsActive = true,
                    IsPublic = true,
                    Name = "TestProduct",
                    OwnerId = Guid.Empty,
                    Policies = new List<Policy>
                    {
                        new Policy
                        {
                            Name = "TestPolicy",
                            Claims = new List<Claim>
                            {
                                new Claim
                                {
                                    Issuer = "issuer",
                                    Type = "type",
                                    Value = "value"
                                }
                            }
                        }
                    }
                }
            };

            Mock<IAuthorityContext> mockContext = EntityFrameworkTest.CreateMockContext();
            mockContext.Setup(m => m.Products)
                .Returns(EntityFrameworkTest.CreateDbSetWithData(products));
            mockContext.Setup(m => m.Policies)
                .Returns(EntityFrameworkTest.CreateDbSetWithData(products.First().Policies));

            GetProductDetails operation = new GetProductDetails(mockContext.Object);

            Product result = await operation.GetDetails(Guid.Empty, products.First().Id);

            Assert.NotNull(result);
        }
    }
}
