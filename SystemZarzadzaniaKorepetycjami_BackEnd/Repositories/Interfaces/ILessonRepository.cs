﻿using SystemZarzadzaniaKorepetycjami_BackEnd.DTOs;
using SystemZarzadzaniaKorepetycjami_BackEnd.Models;
using Task = System.Threading.Tasks.Task;

namespace SystemZarzadzaniaKorepetycjami_BackEnd.Repositories.Interfaces
{
    public interface ILessonRepository
    {
        public Task<bool> IsLessonConflictlessAsync(Lesson lesson);
        public Task AddLessonAsync(Lesson lesson);
        public Task<List<LessonDTO>> getAllReservedLessonsByTeacherAsync(Teacher teacher);
    }
}