using FootBallLeague.Data;
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
    public class TeamsController : ControllerBase
    {
        private readonly IRepositoriesContext repositoriesContext;

        public TeamsController(IRepositoriesContext repositoriesContext)
        {
            this.repositoriesContext = repositoriesContext;
        }
        /// <summary>
        /// Retrieves a collection with all Team objects
        /// </summary>
        /// <response code="200">Teams successfuly retrived</response>
        /// <response code="404">No Teams have been found</response>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var result = await repositoriesContext.TeamRepository.FindAllAsync();
            return Ok(result
                .Include(x=>x.PlayedMatchesAsHomeTeam)
                .Include(x=>x.PlayedMatchesAsAwayTeam)
                .ToList());
        }

        /// <summary>
        /// Creates a Team object
        /// </summary>
        /// <param name="teamDto">Team to create</param>
        /// <response code="201">the requested Team has been created</response>
        /// <response code="400">We were unable to create the requested Team</response>
        [HttpPost]
        public async Task<IActionResult> Post(TeamDto teamDto)
        {
                var result = new Team
                {
                    Name = teamDto.TeamName,
                    Points = teamDto.Points
                };
                await repositoriesContext.TeamRepository.CreateAsync(result);
                await repositoriesContext.SaveAsync();
                return StatusCode(201);//Created
        }

        /// <summary>
        /// Update an existing Team object
        /// </summary>
        /// <param name="id">Id of an existing Team object</param>
        /// <param name="teamDto">Dto object to retrive and update the data of an existing Team</param>
        /// <response code="200">Team successfully updated</response>
        /// <response code="400">Cannot find Teams with the given parameters</response>
        [HttpPut]
        public async Task<IActionResult> Put(Guid id, TeamDto team)
        {
            try
            {
                var result = await repositoriesContext.TeamRepository.UpdateTeamAsync(id, team);
                await repositoriesContext.SaveAsync();
                return Ok(result);
            }
            catch (ArgumentException e)
            {
                return BadRequest(e);
            }
        }

        /// <summary>
        /// Deletes an existing Team object
        /// </summary>
        /// <param name="teamDto">Based team to delete an existing Team</param>
        /// <response code="200">Team successfully deleted</response>
        /// <response code="400">We were unable to delete the requested Team</response>
        [HttpDelete]
        public async Task<IActionResult> Delete(Guid id)
        {
            try
            {
                await repositoriesContext.TeamRepository.DeleteTeamAsync(id);
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
