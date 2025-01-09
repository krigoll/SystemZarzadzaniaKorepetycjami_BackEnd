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

    public async Task DeleteSubjectCategoryAsync(int subjectId, string subjectCategoryName)
    {
        var subjectCategory =
            await _context.SubjectCategory.FirstOrDefaultAsync(sc =>
                sc.Name == subjectCategoryName && sc.IdSubject == subjectId);
        _context.SubjectCategory.Remove(subjectCategory);
        await _context.SaveChangesAsync();
    }

    public async Task<SubjectCategory> FindSubjectCategoryByIdAsync(int idSubjectCategory)
    {
        var subjectCategory =
            await _context.SubjectCategory.FirstOrDefaultAsync(sc => sc.IdSubjectCategory == idSubjectCategory);
        return subjectCategory;
    }

    public async Task<SubjectCategory> FindSubjectCategoryBySubjectIdAndSubjectCategoryNameAsync(int idSubject,
        string subjectCategoryName)
    {
        var subjectCategory =
            await _context.SubjectCategory.FirstOrDefaultAsync(sc =>
                sc.IdSubject == idSubject && sc.Name == subjectCategoryName);
        return subjectCategory;
    }

    public async Task UpdateSubjectCategoryAsync(SubjectCategory subjectCategory)
    {
        _context.SubjectCategory.Update(subjectCategory);
        await _context.SaveChangesAsync();
    }
}