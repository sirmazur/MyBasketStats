﻿using MyBasketStats.API.Entities;

namespace MyBasketStats.API.Services.TeamServices
{
    public interface ITeamRepository
    {
        Task AddTeamToDbAsync(Team team);
        Task<Team> GetTeamByNameAsync(string name);
        Task<Team> GetTeamByIdAsync(int id);
    }
}