using Microsoft.EntityFrameworkCore;
using SystemZarzadzaniaKorepetycjami_BackEnd.Models;
using SystemZarzadzaniaKorepetycjami_BackEnd.Repositories.Interfaces;

namespace SystemZarzadzaniaKorepetycjami_BackEnd.Repositories.Implementations
{
    public class SubjectLevelRepository : ISubjectLevelRepository
    {
        private readonly SZKContext _context;

        public SubjectLevelRepository(SZKContext context)
        {
            _context = context;
        }

        public async Task<bool> IsSubjectLevelExistsBySubjectLevelIdAsync(int subjectLevelId)
        {
            var subjectLevel = await _context.SubjectLevel.FirstOrDefaultAsync(s => s.IdSubjectLevel == subjectLevelId);
            return subjectLevel == null;
        }
    }
}