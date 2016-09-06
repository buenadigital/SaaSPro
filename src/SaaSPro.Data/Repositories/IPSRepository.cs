using SaaSPro.Domain;

namespace SaaSPro.Data.Repositories
{
	public class IPSRepository : EFRespository<IPSEntry>, IIPSRepository
	{
		public IPSRepository(EFDbContext dbContext)
			: base(dbContext)
		{
		}
	}
}
