using Core.Entities;
using Infrastructure.EF;

namespace Infrastructure.Repositories
{
    public class OrderDetailRepository : GenericRepository<OrderDetail, OrderDetailKey, AcmeCorpBizDbContext>
    {
        public OrderDetailRepository(AcmeCorpBizDbContext context) : base(context)
        {
        }
    }
}
