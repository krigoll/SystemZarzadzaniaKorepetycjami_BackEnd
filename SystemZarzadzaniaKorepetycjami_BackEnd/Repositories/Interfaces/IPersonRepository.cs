﻿using SystemZarzadzaniaKorepetycjami_BackEnd.Models;

namespace SystemZarzadzaniaKorepetycjami_BackEnd.Repositories.Interfaces;

public interface IPersonRepository
{
    public Task<bool> AddPerson(Person person);
}