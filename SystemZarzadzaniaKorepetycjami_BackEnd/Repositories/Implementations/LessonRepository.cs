﻿using Microsoft.EntityFrameworkCore;
using SystemZarzadzaniaKorepetycjami_BackEnd.DTOs;
using SystemZarzadzaniaKorepetycjami_BackEnd.Models;
using SystemZarzadzaniaKorepetycjami_BackEnd.Repositories.Interfaces;
using Task = System.Threading.Tasks.Task;

namespace SystemZarzadzaniaKorepetycjami_BackEnd.Repositories.Implementations
{
    public class LessonRepository : ILessonRepository
    {
        private readonly SZKContext _context;

        public LessonRepository(SZKContext context)
        {
            _context = context;
        }

        public async Task<bool> IsLessonConflictlessAsync(Lesson lesson)
        {
            var conflictExists = await _context.Set<Lesson>()
                .AnyAsync(l =>
                    (l.IdTeacher == lesson.IdTeacher || l.IdStudent == lesson.IdStudent) &&
                    (l.IdLessonStatus == 1 || l.IdLessonStatus == 2) &&
                    l.StartDate < lesson.StartDate.AddMinutes(lesson.DurationInMinutes) &&
                    lesson.StartDate < l.StartDate.AddMinutes(l.DurationInMinutes)
                );

            return !conflictExists;
        }

        public async Task AddLessonAsync(Lesson lesson)
        {
            await _context.Lesson.AddAsync(lesson);
            await _context.SaveChangesAsync();
        }

        public async Task<List<LessonDTO>> getAllReservedLessonsByTeacherAsync(Teacher teacher)
        {
            var lessons = await (from lesson in _context.Lesson
                join student in _context.Student on lesson.IdStudent equals student.IdStudent
                join person in _context.Person on student.IdStudent equals person.IdPerson
                join subjectLevel in _context.SubjectLevel on lesson.IdSubjectLevel equals subjectLevel.IdSubjectLevel
                join subjectCategory in _context.SubjectCategory on subjectLevel.IdSubjectCategory equals
                    subjectCategory.IdSubjectCategory
                where lesson.IdTeacher == teacher.IdTeacher && lesson.IdLessonStatus == 1
                select new LessonDTO
                {
                    LessonId = lesson.IdLesson,
                    DateTime = lesson.StartDate.ToString("yyyy-MM-dd HH:mm"),
                    SubjectCategoryName = subjectCategory.Name,
                    SubjectLevelName = subjectLevel.Name,
                    StudentName = person.Name,
                    StudentSurname = person.Surname
                }).ToListAsync();

            return lessons;
        }
    }
}