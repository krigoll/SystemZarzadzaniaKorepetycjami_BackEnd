using SystemZarzadzaniaKorepetycjami_BackEnd.Models;
using SystemZarzadzaniaKorepetycjami_BackEnd.Repositories.Interfaces;

namespace SystemZarzadzaniaKorepetycjami_BackEnd.Repositories.Implementations
{
    public class LessonRepository : ILessonRepository
    {
        private readonly SZKContext _context;

        public LessonRepository(SZKContext context)
        {
            _context = context;
        }

    }
}
