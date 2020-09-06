using System.Collections.Generic;
using System.Threading.Tasks;
using GameOfDrones.API.Dtos;
using GameOfDrones.API.Models;

namespace GameOfDrones.API.Data.Interfaces
{
    public interface IGameStatisticsRepository
    {
        Task<List<CustomPackageGameStatistics>> GetGameStatisticsOrganizedByHighestValue();
        Task<CustomPackageGameStatistics> GetIndividualGameStatistics(int id);

        Task<GameStatistics> GetGameStatisticsByUserId(int id);
        Task<GameStatistics> CreateGameStatistics(GameStatistics gameStatistics);
        Task<GameStatistics> UpdateGameStatistics(GameStatistics gameStatistics);
        Task<bool> DeleteGameStatistics(GameStatistics gameStatistics);

        Task<bool> VerifyUserHasPlayed(int userId);
    }
}