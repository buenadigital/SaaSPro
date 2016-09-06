using SaaSPro.Domain;

namespace SaaSPro.Data.Repositories
{
    public class NoteRepository : EFRespository<Note>, INoteRepository
    {
        public NoteRepository(EFDbContext dbContext)
            : base(dbContext)
        {

        }
    }
}
