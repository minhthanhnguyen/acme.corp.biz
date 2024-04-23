using Core.CQRS;
using Core.Entities;
using Core.Repositories;

namespace Infrastructure.Services
{
    public class AcmeCorpBizService : IAcmeCorpBizService
    {
        private readonly IGenericRepository<Customer, int> _customerRepository;
        private readonly IGenericRepository<Address, int> _addressRepository;
        private readonly IGenericRepository<Product, int> _productRepository;
        private readonly IGenericRepository<Order, int> _orderRepository;
        private readonly IGenericRepository<OrderDetail, OrderDetailKey> _orderDetailRepository;

        public AcmeCorpBizService(
            IGenericRepository<Customer, int> customerRepository,
            IGenericRepository<Address, int> addressRepository,
            IGenericRepository<Product, int> productRepository,
            IGenericRepository<Order, int> orderRepository,
            IGenericRepository<OrderDetail, OrderDetailKey> orderDetailRepository)
        {
            _customerRepository = customerRepository;
            _addressRepository = addressRepository;
            _productRepository = productRepository;
            _orderRepository = orderRepository;
            _orderDetailRepository = orderDetailRepository;
        }

        public async Task<Customer?> GetCustomerAsync(GetEntityByIdRequest getCustomerRequest)
        {
            Customer? customer = await _customerRepository.GetSingleAsync(getCustomerRequest.Id);

            if (customer != null)
            {
                IAsyncEnumerable<Address> addresses = _addressRepository.Fetch(x => x.CustomerId == customer.Id);

                if (addresses != null)
                {
                    foreach (Address address in addresses.ToBlockingEnumerable())
                    {
                        if (address != null && !customer.Addresses.Any(x => x.Id == address.Id))
                        {
                            customer.Addresses.Add(address);
                        }
                    }
                }

                IAsyncEnumerable<Order> orders = _orderRepository.Fetch(x => x.CustomerId == customer.Id);

                if (orders != null)
                {
                    foreach(Order order in orders.ToBlockingEnumerable())
                    {
                        if (order != null && !customer.Orders.Any(x => x.Id == order.Id))
                        {
                            customer.Orders.Add(order);
                        }
                    }
                }
            }

            return customer;
        }

        public async Task<int> CreateCustomerAsync(CreateCustomerRequest createCustomerRequest)
        {
            var newCustomer = new Customer
            {
                FirstName = createCustomerRequest.FirstName,
                LastName = createCustomerRequest.LastName,    
                Email = createCustomerRequest.Email,
            };

            if (createCustomerRequest.Addresses.Any())
            {
                foreach (AddressRequest address in createCustomerRequest.Addresses)
                {
                    newCustomer.Addresses.Add(new Address
                    {
                        AddressType = address.AddressType,
                        Address1 = address.Address1,
                        Address2 = address.Address2,
                        City = address.City,
                        State = address.State,
                        ZipCode = address.ZipCode,
                    });
                }
            }

            await _customerRepository.AddAsync(newCustomer);

            return newCustomer.Id;
        }

        public async Task<Product?> GetProductAsync(GetEntityByIdRequest getProductRequest)
        {
            Product? product = await _productRepository.GetSingleAsync(getProductRequest.Id);

            return product;
        }

        public async Task<int> CreateProductAsync(CreateProductRequest createProductRequest)
        {
            var newProduct = new Product
            {
                Name = createProductRequest.Name,
                UnitPrice = createProductRequest.UnitPrice,
                UnitsInStock = createProductRequest.UnitsInStock,
            };

            await _productRepository.AddAsync(newProduct);

            return newProduct.Id;
        }

        public async Task<Order?> GetOrderAsync(GetEntityByIdRequest getOrderRequest)
        {
            Order? order = await _orderRepository.GetSingleAsync(getOrderRequest.Id);

            if (order != null)
            {
                IAsyncEnumerable<OrderDetail> orderDetails = _orderDetailRepository.Fetch(x => x.OrderId == order.Id);

                if (orderDetails != null)
                {
                    foreach (OrderDetail orderDetail in orderDetails.ToBlockingEnumerable())
                    {
                        if (orderDetail != null)
                        {
                            order.OrderDetails.Add(orderDetail);
                        }
                    }
                }
            }

            return order;
        }

        public async Task<int> CreateOrderAsync(CreateOrderRequest createOrderRequest)
        {
            Order newOrder = new Order
            {
                OrderDate = System.DateTime.Now.ToUniversalTime(),
                CustomerId = createOrderRequest.CustomerId,
            };

            await _orderRepository.AddAsync(newOrder);

            var orderDetails = new List<OrderDetail>();

            foreach(OrderLineRequest orderLine in createOrderRequest.OrderLines)
            {
                orderDetails.Add(new OrderDetail
                {
                    OrderId = newOrder.Id,
                    ProductId = orderLine.ProductId,
                    Quantity = orderLine.Quantity,
                });
            }

            await _orderDetailRepository.AddRangeAsync(orderDetails);

            return newOrder.Id;
        }        
    }
}
