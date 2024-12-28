using SystemZarzadzaniaKorepetycjami_BackEnd.Models;

namespace SystemZarzadzaniaKorepetycjami_BackEnd.Repositories.Interfaces;

public interface IMarkRepository
{
    public Task CreateAndUpdateMark(List<Mark> marks);
}