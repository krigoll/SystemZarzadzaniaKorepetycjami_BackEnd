using Microsoft.EntityFrameworkCore;
using SystemZarzadzaniaKorepetycjami_BackEnd.Models;

public class SubjectCategoryRepository : ISubjectCategoryRepository
{
    private readonly SZKContext _context;

    public SubjectCategoryRepository(SZKContext context)
    {
        _context = context;
    }

    public async Task CreateSubjectCategoryAsync(SubjectCategory subjectCategory)
    {
        await _context.SubjectCategory.AddAsync(subjectCategory);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateSubjectCategoryAsync(SubjectCategory subjectCategory)
    {
        _context.SubjectCategory.Update(subjectCategory);
        await _context.SaveChangesAsync();
    }

    public async Task<SubjectCategory> FindSubjectCategoryByIdAsync(int idSubjectCategory)
    {
        var subjectCategory =
            await _context.SubjectCategory.FirstOrDefaultAsync(sc => sc.IdSubjectCategory == idSubjectCategory);
        return subjectCategory;
    }
}