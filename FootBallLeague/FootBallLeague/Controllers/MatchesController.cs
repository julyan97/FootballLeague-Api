using FootBallLeague.Extenssions;
using FootBallLeague.Models;
using FootBallLeague.Models.DTOs;
using FootBallLeague.Repositories;
using FootBallLeague.Repositories.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FootBallLeague.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class MatchesController : ControllerBase
    {
        private readonly IRepositoriesContext repositoriesContext;

        public MatchesController(IRepositoriesContext repositoriesContext)
        {
            this.repositoriesContext = repositoriesContext;
        }

        /// <summary>
        /// Retrieves a collection with all Match objects
        /// </summary>
        /// <response code="200">FootBall Matches successfuly retrived</response>
        /// <response code="404">No Matches have been found</response>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var result = await repositoriesContext.MatchRepository.FindAllAsync();
            return Ok(result
                .Include(x => x.HomeTeam)
                .Include(x => x.AwayTeam)
                .ToList());
        }

        /// <summary>
        /// Creates a Team object from Dto.
        /// Dto format should be as follows:
        /// Team1: string, Team2: string, MatchScore: string
        /// example: Levski, CSKA, 3-0
        /// </summary>
        /// <param name="team">Match to create</param>
        /// <response code="201">the requested Match has been created</response>
        /// <response code="400">We were unable to create the requested Match</response>
        [HttpPost]
        public async Task<IActionResult> Post(MatchDto match)
        {
            try
            {
                var result = await repositoriesContext.MatchRepository
                    .InsertMatchEntityAndUpdateTeamPointsAsync(match);
                await repositoriesContext.SaveAsync();

                return StatusCode(201, result);//Created
            }
            catch(ArgumentException e)
            {
                return BadRequest(e);
            }
        }



        /// <summary>
        /// Update an existing Match object
        /// </summary>
        /// <param name="id">Id of an existing Match to update</param>
        /// <param name="match">Dto with required info to update</param>
        /// <response code="200">Match successfully updated</response>
        /// <response code="400">Cannot find Match with the given id</response>
        [HttpPut]
        public async Task<IActionResult> Put(Guid matchId, MatchDto match)
        {
            try
            {
                var result = await repositoriesContext.MatchRepository
                    .UpdateMatchEntityAndUpdateTeamPointsAsync(matchId, match);
                await repositoriesContext.SaveAsync();

                return Ok(result);
            }
            catch(ArgumentException e)
            {
                return NotFound(e);
            }
        }

        /// <summary>
        /// Deletes an existing Match object
        /// </summary>
        /// <param name="id">Id of a Match to Delete</param>
        /// <response code="200">Match successfully deleted</response>
        /// <response code="400">We were unable to delete the requested Match</response>
        [HttpDelete]
        public async Task<IActionResult> Delete(Guid matchId)
        {
            try
            {
                await repositoriesContext.MatchRepository.DeleteEntityAndUpdatePointsAsync(matchId);
                await repositoriesContext.SaveAsync();

                return Ok();
            }
            catch (ArgumentException e)
            {
                return BadRequest(e);
            }


        }

    }
}
