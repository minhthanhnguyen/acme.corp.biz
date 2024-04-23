using Core.Entities;
using Infrastructure.EF;

namespace Infrastructure.Repositories
{
    public class OrderRepository : GenericRepository<Order, System.Int32, AcmeCorpBizDbContext>
    {
        public OrderRepository(AcmeCorpBizDbContext context) : base(context)
        {
        }
    }
}
