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

        public async Task CreateSubjectLevelAsync(SubjectLevel subjectLevel)
        {
            await _context.SubjectLevel.AddAsync(subjectLevel);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateSubjectLevelAsync(SubjectLevel subjectLevel)
        {
            _context.SubjectLevel.Update(subjectLevel);
            await _context.SaveChangesAsync();
        }

        public async Task<SubjectLevel> FindSubjectLevelByIdAsync(int idSubjectLevel)
        {
            var subjectLevel =
                await _context.SubjectLevel.FirstOrDefaultAsync(sl => sl.IdSubjectLevel == idSubjectLevel);
            return subjectLevel;
        }
    }
}