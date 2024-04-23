using Core.CQRS;
using Core.Entities;
using Core.Repositories;
using Infrastructure.EF;
using Infrastructure.Repositories;
using Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Tests.UnitTests
{
    [TestClass]
    public class UnitTests
    {
        private ServiceProvider _serviceProvider;

        [TestInitialize]
        public void Setup()
        {
            var services = new ServiceCollection();

            // Using In-Memory database for testing
            services.AddDbContext<AcmeCorpBizDbContext>(options => options.UseInMemoryDatabase("TestDb"));

            services.AddScoped<IGenericRepository<Customer, int>, CustomerRepository>();
            services.AddScoped<IGenericRepository<Address, int>, AddressRepository>();
            services.AddScoped<IGenericRepository<Product, int>, ProductRepository>();
            services.AddScoped<IGenericRepository<Order, int>, OrderRepository>();
            services.AddScoped<IGenericRepository<OrderDetail, OrderDetailKey>, OrderDetailRepository>();

            services.AddScoped<IAcmeCorpBizService, AcmeCorpBizService>();

            _serviceProvider = services.BuildServiceProvider();
        }

        [TestCleanup]
        public void Cleanup()
        {
            var dbContext = _serviceProvider.GetService<AcmeCorpBizDbContext>();

            dbContext.Database.EnsureDeleted();
        }

        [TestMethod]
        public async Task AddCustomer_Should_Add_Customer()
        {
            // Arrange
            using (var scope = _serviceProvider.CreateScope())
            {
                var scopedServices = scope.ServiceProvider;
                IAcmeCorpBizService bizService = scopedServices.GetRequiredService<IAcmeCorpBizService>();
                AcmeCorpBizDbContext dbContext = scopedServices.GetRequiredService<AcmeCorpBizDbContext>();

                var createCustomerRequest = new CreateCustomerRequest
                {
                    FirstName = "Minh",
                    LastName = "Nguyen",
                    Email = "minh@test.com",
                };

                // Act
                await bizService.CreateCustomerAsync(createCustomerRequest);

                // Assert
                Customer? addedCustomer = await dbContext.Customers.Where(x => x.FirstName == createCustomerRequest.FirstName &&
                                                                                          x.LastName == createCustomerRequest.LastName &&
                                                                                          x.Email == createCustomerRequest.Email).FirstOrDefaultAsync();

                Assert.IsNotNull(addedCustomer);
                Assert.AreEqual(addedCustomer.FirstName, createCustomerRequest.FirstName);
                Assert.AreEqual(addedCustomer.LastName, createCustomerRequest.LastName);
                Assert.AreEqual(addedCustomer.Email, createCustomerRequest.Email);
            }
        }

        [TestMethod]
        public async Task AddProduct_Should_Add_Product()
        {
            // Arrange
            using (var scope = _serviceProvider.CreateScope())
            {
                var scopedServices = scope.ServiceProvider;
                IAcmeCorpBizService bizService = scopedServices.GetRequiredService<IAcmeCorpBizService>();
                AcmeCorpBizDbContext dbContext = scopedServices.GetRequiredService<AcmeCorpBizDbContext>();

                var createProductRequest = new CreateProductRequest
                {
                    Name = "Test Product",
                    UnitPrice = 100m,
                    UnitsInStock = 100,
                };

                // Act
                await bizService.CreateProductAsync(createProductRequest);

                // Assert
                Product? addedProduct = await dbContext.Products.Where(x => x.Name == createProductRequest.Name &&
                                                                                      x.UnitPrice == createProductRequest.UnitPrice &&
                                                                                      x.UnitsInStock == createProductRequest.UnitsInStock).FirstOrDefaultAsync();

                Assert.IsNotNull(addedProduct);
                Assert.AreEqual(addedProduct.Name, createProductRequest.Name);
                Assert.AreEqual(addedProduct.UnitPrice, createProductRequest.UnitPrice);
                Assert.AreEqual(addedProduct.UnitsInStock, createProductRequest.UnitsInStock);
            }
        }

        [TestMethod]
        public async Task AddOrder_Should_AddOrder()
        {
            // Arrange
            using (var scope = _serviceProvider.CreateScope())
            {
                var scopedServices = scope.ServiceProvider;
                IAcmeCorpBizService bizService = scopedServices.GetRequiredService<IAcmeCorpBizService>();
                AcmeCorpBizDbContext dbContext = scopedServices.GetRequiredService<AcmeCorpBizDbContext>();

                var createCustomerRequest = new CreateCustomerRequest
                {
                    FirstName = "Minh",
                    LastName = "Nguyen",
                    Email = "minh@test.com",
                };

                // Act
                await bizService.CreateCustomerAsync(createCustomerRequest);

                // Assert
                Customer? addedCustomer = await dbContext.Customers.Where(x => x.FirstName == createCustomerRequest.FirstName &&
                                                                                          x.LastName == createCustomerRequest.LastName &&
                                                                                          x.Email == createCustomerRequest.Email).FirstOrDefaultAsync();

                Assert.IsNotNull(addedCustomer);
                Assert.AreEqual(addedCustomer.FirstName, createCustomerRequest.FirstName);
                Assert.AreEqual(addedCustomer.LastName, createCustomerRequest.LastName);
                Assert.AreEqual(addedCustomer.Email, createCustomerRequest.Email);

                var createProductRequest = new CreateProductRequest
                {
                    Name = "Test Product",
                    UnitPrice = 100m,
                    UnitsInStock = 100,
                };

                // Act
                await bizService.CreateProductAsync(createProductRequest);

                // Assert
                Product? addedProduct = await dbContext.Products.Where(x => x.Name == createProductRequest.Name &&
                                                                                      x.UnitPrice == createProductRequest.UnitPrice &&
                                                                                      x.UnitsInStock == createProductRequest.UnitsInStock).FirstOrDefaultAsync();

                Assert.IsNotNull(addedProduct);
                Assert.AreEqual(addedProduct.Name, createProductRequest.Name);
                Assert.AreEqual(addedProduct.UnitPrice, createProductRequest.UnitPrice);
                Assert.AreEqual(addedProduct.UnitsInStock, createProductRequest.UnitsInStock);

                var createOrderRequest = new CreateOrderRequest
                {
                    CustomerId = addedCustomer.Id,
                    OrderLines = new List<OrderLineRequest>
                    {
                        new OrderLineRequest
                        {
                            ProductId = addedProduct.Id,
                            Quantity = 1
                        }
                    }
                };

                int orderId = await bizService.CreateOrderAsync(createOrderRequest);

                Order? order = await bizService.GetOrderAsync(new GetEntityByIdRequest { Id = orderId });
                Assert.IsNotNull(order);
                Assert.AreEqual(createOrderRequest.OrderLines.Count, order.OrderDetails.Count);
                Assert.AreEqual(createOrderRequest.OrderLines[0].ProductId, order.OrderDetails[0].ProductId);
            }
        }
    }
}