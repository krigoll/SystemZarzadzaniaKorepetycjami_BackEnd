using Microsoft.EntityFrameworkCore;
using SystemZarzadzaniaKorepetycjami_BackEnd.DTOs;
using SystemZarzadzaniaKorepetycjami_BackEnd.Models;
using SystemZarzadzaniaKorepetycjami_BackEnd.Repositories.Interfaces;

namespace SystemZarzadzaniaKorepetycjami_BackEnd.Repositories.Implementations;

public class AssignmentRepository : IAssignmentRepository
{
    private readonly SZKContext _context;

    public AssignmentRepository(SZKContext context)
    {
        _context = context;
    }

    public async Task CreateAssignmentAsync(Assignment newAssigment)
    {
        await _context.Assignment.AddAsync(newAssigment);
        await _context.SaveChangesAsync();
    }

    public async Task<Assignment> GetAssignmentByIdAsync(int assignmentId)
    {
        return await _context.Assignment.FirstOrDefaultAsync(a => a.IdAssignment == assignmentId);
    }

    public async Task DeleteAssignmentAsync(Assignment assigment)
    {
        _context.Assignment.Remove(assigment);
        await _context.SaveChangesAsync();
    }

    public async Task<List<AssignmentDTO>> GetAssignmentsByTestIdAsync(int idTest)
    {
        var assignments = await (
            from assigment in _context.Assignment
            where
                assigment.IdTest == idTest
            select new AssignmentDTO
            {
                IdAssignment = assigment.IdAssignment,
                Content = assigment.Content,
                Answer = assigment.Answer
            }).ToListAsync();
        return assignments;
    }
}