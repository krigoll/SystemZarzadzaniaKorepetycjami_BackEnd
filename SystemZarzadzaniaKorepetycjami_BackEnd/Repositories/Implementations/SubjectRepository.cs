using SystemZarzadzaniaKorepetycjami_BackEnd.Repositories.Interfaces;
using SystemZarzadzaniaKorepetycjami_BackEnd.Models;
using Microsoft.EntityFrameworkCore;

namespace SystemZarzadzaniaKorepetycjami_BackEnd.Repositories.Implementations
{
    public class SubjectRepository : ISubjectRepository
    {

        private readonly SZKContext _context;

        public SubjectRepository(SZKContext context)
        {
            _context = context;
        }

        public async Task<List<string>> GetAllFullSubjects()
        {
            var result = await _context.Subject
                                .Include(s => s.SubjectCategory)
                                .ThenInclude(sc => sc.SubjectLevel)
                                .SelectMany(s => s.SubjectCategory, (s, sc) => new { s.Name, SubjectCategory = sc })
                                .SelectMany(sc => sc.SubjectCategory.SubjectLevel, (sc, sl) => new { Subject = sc.Name, Category = sc.SubjectCategory.Name, Level = sl.Name })
                                .ToListAsync();

             var formattedResult = result.Select(x => $"{x.Subject}, {x.Category}, {x.Level}")
                                        .ToList();

            return formattedResult; 
        }
    }
}
