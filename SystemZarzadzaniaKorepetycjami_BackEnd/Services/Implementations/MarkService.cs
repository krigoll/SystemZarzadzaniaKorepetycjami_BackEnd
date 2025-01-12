using SystemZarzadzaniaKorepetycjami_BackEnd.DTOs;
using SystemZarzadzaniaKorepetycjami_BackEnd.Enums;
using SystemZarzadzaniaKorepetycjami_BackEnd.Models;
using SystemZarzadzaniaKorepetycjami_BackEnd.Repositories.Interfaces;
using SystemZarzadzaniaKorepetycjami_BackEnd.Services.Interfaces;

namespace SystemZarzadzaniaKorepetycjami_BackEnd.Services.Implementations;

public class MarkService : IMarkService
{
    private readonly IMarkRepository _markRepository;

    public MarkService(IMarkRepository markRepository)
    {
        _markRepository = markRepository;
    }

    public async Task<MarkStatus> CreateAndUpdateMark(List<MarkDTO> marks)
    {
        try
        {
            var makr = new List<Mark>();

            foreach (var m in marks)
                makr.Add(new Mark(m.Description, m.Value, m.IdStudentAnswer));


            await _markRepository.CreateAndUpdateMark(makr);

            return MarkStatus.OK;
        }
        catch (ArgumentException e)
        {
            Console.WriteLine(e);
            return MarkStatus.INVALID_MARK;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return MarkStatus.SERVER_ERROR;
        }
    }
}