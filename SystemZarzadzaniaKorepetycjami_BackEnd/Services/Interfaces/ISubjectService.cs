﻿using SystemZarzadzaniaKorepetycjami_BackEnd.DTOs;

namespace SystemZarzadzaniaKorepetycjami_BackEnd.Services.Interfaces
{
    public interface ISubjectService
    {
        public Task<List<SubjectDTO>> getAllSubjects();
    }
}
