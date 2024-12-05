namespace SystemZarzadzaniaKorepetycjami_BackEnd.Enums;

public enum SingUpToLessonStatus
{
    OK,
    INVALID_EMAIL,
    INVALID_SUBJECT,
    INVALID_LESSON,
    CONFLICT_LESSON,
    DATABASE_ERROR
}