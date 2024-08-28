using SystemZarzadzaniaKorepetycjami_BackEnd.DTOs;
using SystemZarzadzaniaKorepetycjami_BackEnd.Enums;

namespace SystemZarzadzaniaKorepetycjami_BackEnd.Services.Interfaces;

public interface ISingUpToLessonService
{
    public Task<SingUpToLessonStaus> SingUpToLessonAsync(SingUpToLessonDTO singUpToLessonDTO);
}