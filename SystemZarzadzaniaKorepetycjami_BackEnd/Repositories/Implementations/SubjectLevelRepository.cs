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

        public async Task<int> GetSubjectLevelIdBySubjectCategoryNameAndSubjectNameAsync(
            string subjectLevelName, string subjectCategoryName, string subjectName)
        {
            var subjectLevel = await _context.SubjectLevel
                .Include(sl => sl.IdSubjectCategoryNavigation)
                .ThenInclude(sc => sc.IdSubjectNavigation)
                .FirstOrDefaultAsync(sl =>
                    sl.Name == subjectLevelName &&
                    sl.IdSubjectCategoryNavigation.Name == subjectCategoryName &&
                    sl.IdSubjectCategoryNavigation.IdSubjectNavigation.Name == subjectName);

            return subjectLevel.IdSubjectLevel;
        }
    }
}