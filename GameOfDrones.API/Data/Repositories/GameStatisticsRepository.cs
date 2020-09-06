using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GameOfDrones.API.Data.Interfaces;
using GameOfDrones.API.Dtos;
using GameOfDrones.API.Models;
using Microsoft.EntityFrameworkCore;

namespace GameOfDrones.API.Data.Repositories
{
    public class GameStatisticsRepository : IGameStatisticsRepository
    {
        private readonly DataContext _context;
        public GameStatisticsRepository(DataContext context)
        {
            _context = context;

        }

        public async Task<List<CustomPackageGameStatistics>> GetGameStatisticsOrganizedByHighestValue()
        {
            var gameStatistics = await _context.GameStatistics
            .Join(
                _context.User,
                individualGameStatistics => individualGameStatistics.GsUUid,
                user => user.UUid,
                (individualGameStatistics, user) => new CustomPackageGameStatistics{
                    GsUid = individualGameStatistics.GsUid,
                    GsUUid = individualGameStatistics.GsUUid,
                    GsScore = individualGameStatistics.GsScore,
                    UName = user.UName 
                }
            )
            .OrderByDescending(gs => gs.GsScore)
            .ToListAsync();

            return gameStatistics;
        }

        public async Task<CustomPackageGameStatistics> GetIndividualGameStatistics(int id)
        {
            var individualGameStatistics = await _context.GameStatistics
            .Join(
                _context.User,
                individualGameStatistic => individualGameStatistic.GsUUid,
                user => user.UUid,
                (individualGameStatistic, user) => new CustomPackageGameStatistics{
                    GsUid = individualGameStatistic.GsUid,
                    GsUUid = individualGameStatistic.GsUUid,
                    GsScore = individualGameStatistic.GsScore,
                    UName = user.UName 
                }
            )
            .FirstOrDefaultAsync(gs=> gs.GsUid == id);

            return individualGameStatistics;
        }


        public async Task<GameStatistics> CreateGameStatistics(GameStatistics gameStatistics)
        {
            var result = await _context.GameStatistics.AddAsync(gameStatistics);
            await _context.SaveChangesAsync();
            return gameStatistics;
        }


        public async Task<GameStatistics> UpdateGameStatistics(GameStatistics gameStatistics)
        {
            var gameStatisticsInDB = await _context.GameStatistics.FirstOrDefaultAsync(x => x.GsUid == gameStatistics.GsUid);

            if (gameStatisticsInDB != null)
            {
                gameStatisticsInDB.GsScore = gameStatistics.GsScore;

                _context.GameStatistics.Update(gameStatisticsInDB);
                await _context.SaveChangesAsync();
            }

            return gameStatisticsInDB;
        }
        public async Task<bool> DeleteGameStatistics(GameStatistics gameStatistics)
        {
            var deletionSuccess = false;

            var gameStatisticsInDB = await _context.GameStatistics.FirstOrDefaultAsync(x => x.GsUid == gameStatistics.GsUid);

            if (gameStatisticsInDB != null)
            {
                _context.GameStatistics.Remove(gameStatisticsInDB);
                await _context.SaveChangesAsync();
                deletionSuccess = true;
            }

            return deletionSuccess;
        }

        public async Task<GameStatistics> GetGameStatisticsByUserId(int id)
        {
            var gameStatisticsInDB = await _context.GameStatistics.FirstOrDefaultAsync(x => x.GsUUid == id);
            return gameStatisticsInDB;
        }

        public async Task<bool> VerifyUserHasPlayed(int userId)
        {
            if (await _context.GameStatistics.AnyAsync(x=> x.GsUUid == userId)) 
            {
                return true;
            }
            else 
            {
                return false;
            }
        }
    }
}