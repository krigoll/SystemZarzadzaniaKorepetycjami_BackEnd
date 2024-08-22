namespace SystemZarzadzaniaKorepetycjami_BackEnd.Enums;

public enum SingUpToLessonStaus
{
    OK,
    INVALID_EMAIL,
    INVALID_SUBJECT,
    INVALID_LESSON,
    CONFLICT_LESSON,
    DATABASE_ERROR
}