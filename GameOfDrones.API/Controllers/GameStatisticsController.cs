using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using GameOfDrones.API.Data.Interfaces;
using GameOfDrones.API.Dtos;
using GameOfDrones.API.Models;
using Microsoft.AspNetCore.Mvc;

namespace GameOfDrones.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GameStatisticsController : ControllerBase
    {
        private readonly IGameStatisticsRepository _repo;
        private readonly IUserRepository _user;
        private readonly IAuditRepository _audit;
        public GameStatisticsController(IGameStatisticsRepository repo, IUserRepository user, IAuditRepository audit)
        {
            _audit = audit;
            _repo = repo;
            _user = user;
        }

        [HttpGet]
        public async Task<IActionResult> GetGameStatisticsOrganizedByHighestValue()
        {
            var organizedGameStatistics = new List<CustomPackageGameStatistics>();
            try
            {
                 organizedGameStatistics = await _repo.GetGameStatisticsOrganizedByHighestValue();
            }
            catch (Exception e)
            {
                var auditTransaction = new Audit();
                auditTransaction.AAaUid = 1;
                auditTransaction.ADescription = e.Message;
                await _audit.CreateAuditTransaction(auditTransaction);
            }
            
            return Ok(organizedGameStatistics);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetIndividualGameStatistics(int id)
        {
            var individualGameStatistics = await _repo.GetIndividualGameStatistics(id);

            return Ok(individualGameStatistics);
        }

        [HttpPost]
        public async Task<IActionResult> CreateGameStatistics(CustomPackageGameStatistics customGameStatistics)
        {
            var gameStatistics = new GameStatistics();
            var auditTransaction = new Audit();
            customGameStatistics.UName = customGameStatistics.UName.ToLower().Trim();

            if (!await _user.VerifyUserExists(customGameStatistics.UName))
            {
                var user = new User();

                user.UName = customGameStatistics.UName;

                try
                {
                    await _user.CreateUser(user);
                }
                catch (Exception e)
                {
                    auditTransaction.AAaUid = 1;
                    auditTransaction.ADescription = e.Message;
                    await _audit.CreateAuditTransaction(auditTransaction);
                }

                gameStatistics.GsUUid = user.UUid;
                gameStatistics.GsScore = customGameStatistics.GsScore;

                try
                {
                    await _repo.CreateGameStatistics(gameStatistics);
                }
                catch (Exception e)
                {
                    auditTransaction.AAaUid = 1;
                    auditTransaction.ADescription = e.Message;
                    await _audit.CreateAuditTransaction(auditTransaction);
                }
                auditTransaction.AAaUid = 3;
                auditTransaction.ADescription = "Game Finished Succesfully";
                await _audit.CreateAuditTransaction(auditTransaction);
            }
            else
            {
                int userId = 0;
                try
                {
                    userId = await _user.GetUserIdByName(customGameStatistics.UName);
                }
                catch (Exception e)
                {
                    auditTransaction.AAaUid = 1;
                    auditTransaction.ADescription = e.Message;
                    await _audit.CreateAuditTransaction(auditTransaction);
                }

                if (await _repo.VerifyUserHasPlayed(userId))
                {
                    gameStatistics = await _repo.GetGameStatisticsByUserId(userId);
                    gameStatistics.GsScore = customGameStatistics.GsScore + gameStatistics.GsScore;

                    try
                    {
                        await _repo.UpdateGameStatistics(gameStatistics);
                    }
                    catch (Exception e)
                    {

                        auditTransaction.AAaUid = 1;
                        auditTransaction.ADescription = e.Message;
                        await _audit.CreateAuditTransaction(auditTransaction);
                    }
                    auditTransaction.AAaUid = 3;
                    auditTransaction.ADescription = "Game Finished Succesfully";
                    await _audit.CreateAuditTransaction(auditTransaction);
                }
                else
                {
                    gameStatistics.GsUUid = userId;
                    gameStatistics.GsScore = customGameStatistics.GsScore;

                    try
                    {
                        await _repo.CreateGameStatistics(gameStatistics);
                    }
                    catch (Exception e)
                    {
                        auditTransaction.AAaUid = 1;
                        auditTransaction.ADescription = e.Message;
                        await _audit.CreateAuditTransaction(auditTransaction);
                    }

                    auditTransaction.AAaUid = 3;
                    auditTransaction.ADescription = "Game Finished Succesfully";
                    await _audit.CreateAuditTransaction(auditTransaction);

                }

            }

            return Created($"api/GameStatistics/{gameStatistics.GsUid}", gameStatistics);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateGameStatistics(GameStatistics gameStatistics)
        {
            var gameStatisticsToUpdate = await _repo.UpdateGameStatistics(gameStatistics);

            if (gameStatisticsToUpdate != null)
            {
                return Ok(gameStatisticsToUpdate);
            }
            else
            {
                return NotFound($"Couldn't perform the update operation successfully on requested item. ({gameStatisticsToUpdate})");
            }
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteGameStatistics(GameStatistics gameStatistics)
        {
            var operation = await _repo.DeleteGameStatistics(gameStatistics);

            if (operation == true)
            {
                return Ok("The item was successfully deleted.");
            }
            else
            {
                return NotFound($"Couldn't delete audit action with the given id. (id:{gameStatistics.GsUid})");
            }
        }
    }
}