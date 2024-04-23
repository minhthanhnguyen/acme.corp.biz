using Core.Entities;
using Infrastructure.EF;

namespace Infrastructure.Repositories
{
    public class ProductRepository : GenericRepository<Product, System.Int32, AcmeCorpBizDbContext>
    {
        public ProductRepository(AcmeCorpBizDbContext context) : base(context)
        {
        }
    }
}
