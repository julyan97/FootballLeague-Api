﻿using FootBallLeague.Extenssions;
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
        public IActionResult Get()
        {
            try
            {
                return Ok(repositoriesContext.MatchRepository
                    .FindAll()
                    .Include(x => x.PlayedTeams)
                    .ToList());
            }
            catch (ArgumentException e)
            {
                return NotFound(e);
            }
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
        public IActionResult Post(MatchDto match)
        {
            try
            {
                var teams = repositoriesContext.TeamRepository
                    .FindByCondition(x => x.TeamName == match.Team1 || x.TeamName == match.Team2)
                    .ToArray();

                repositoriesContext.MatchRepository.CreateMatchFromDto(match, teams);
                repositoriesContext.Save();

                return StatusCode(201);//Created
            }
            catch
            {
                return NotFound();
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
        public IActionResult Put(Guid id, MatchDto match)
        {

            try
            {
                var toUpdate = repositoriesContext.MatchRepository
                    .FindByCondition(x => x.Id == id)
                    .Include(x => x.PlayedTeams)
                    .First();

                
                var team1 = toUpdate.PlayedTeams.First();
                var team2 = toUpdate.PlayedTeams.Last();

                if (match.MatchScore == toUpdate.MatchScore)
                {
                    team1.Update(match.Team1, team1.Ranking);
                    team2.Update(match.Team2, team2.Ranking);
                }
                else
                {

                    team1.UpdateGameOutcome(team2, toUpdate.MatchScore, match.MatchScore);
                }
                toUpdate.MatchScore = match.MatchScore;
                repositoriesContext.Save();
                return Ok();

            }
            catch
            {
                return NotFound();
            }
        }

        /// <summary>
        /// Deletes an existing Match object
        /// </summary>
        /// <param name="id">Id of a Match to Delete</param>
        /// <response code="200">Match successfully deleted</response>
        /// <response code="400">We were unable to delete the requested Match</response>
        [HttpDelete]
        public IActionResult Delete(Guid id)
        {
            try
            {
                var toDelete = repositoriesContext.MatchRepository
                    .FindByCondition(x => x.Id == id)
                    .First();

                repositoriesContext.MatchRepository.Delete(toDelete);
                repositoriesContext.Save();
                return Ok();
            }
            catch (ArgumentException e)
            {
                return BadRequest(e);
            }


        }

    }
}