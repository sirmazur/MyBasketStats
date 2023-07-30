﻿using MyBasketStats.API.Entities;
using MyBasketStats.API.Models;
using MyBasketStats.API.Services.Basic;

namespace MyBasketStats.API.Services.SeasonServices
{
    public interface ISeasonRepository
    {
        Task AddSeasonToDbAsync(Season season);
        Task CreateSeasonalStatsheetsAsync(Season season);
    }
}