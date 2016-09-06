using SaaSPro.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SaaSPro.Data.Repositories
{
    public class ReferenceListItemRepository : EFRespository<ReferenceListItem>, IReferenceListItemRepository
    {
        public ReferenceListItemRepository(EFDbContext dbContext)
            : base(dbContext)
        {

        }
    }
}
