using Core.Entities;
using Infrastructure.EF;

namespace Infrastructure.Repositories
{
    public class AddressRepository : GenericRepository<Address, System.Int32, AcmeCorpBizDbContext>
    {
        public AddressRepository(AcmeCorpBizDbContext context) : base(context)
        {
        }
    }
}
