using Core.Entities;
using Infrastructure.EF;

namespace Infrastructure.Repositories
{
    public class CustomerRepository : GenericRepository<Customer, System.Int32, AcmeCorpBizDbContext>
    {
        public CustomerRepository(AcmeCorpBizDbContext context) : base(context)
        {
        }
    }
}
