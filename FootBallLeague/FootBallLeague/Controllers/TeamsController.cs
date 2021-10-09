using FootBallLeague.Data;
using FootBallLeague.Models;
using FootBallLeague.Models.DTOs;
using FootBallLeague.Repositories;
using FootBallLeague.Repositories.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
        public IActionResult Get()
        {
            try
            {

                return Ok(repositoriesContext.TeamRepository
                    .FindAll()
                    .OrderBy(x => x.Ranking)
                    .ToList());
            }
            catch (ArgumentException e)
            {
                return NotFound(e.Message);
            }
        }  

        /// <summary>
        /// Creates a Team object
        /// </summary>
        /// <param name="team">Team to create</param>
        /// <response code="201">the requested Team has been created</response>
        /// <response code="400">We were unable to create the requested Team</response>
        [HttpPost]
        public IActionResult Post(TeamDto team)
        {
            try
            {
                var toCreate = team.CreateTeam();
                repositoriesContext.TeamRepository.Create(toCreate);
                repositoriesContext.Save();
                return StatusCode(201);//Created
            }
            catch (ArgumentException e)
            {
                return BadRequest(e);
            }
        }

        /// <summary>
        /// Update an existing Team object
        /// </summary>
        /// <param name="id">Id of an existing Team object</param>
        /// <param name="team">Dto object to retrive and update the data of an existing Team</param>
        /// <response code="200">Team successfully updated</response>
        /// <response code="400">Cannot find Teams with the given parameters</response>
        [HttpPut]
        public IActionResult Put(Guid id, TeamDto team)
        {
            try
            {
                var toUpdate = repositoriesContext.TeamRepository.FindByCondition(x => x.Id == id).First();
                toUpdate.Update(team);
                repositoriesContext.TeamRepository.Update(toUpdate);
                repositoriesContext.Save();
                return Ok();
            }
            catch(ArgumentException e)
            {
                return BadRequest(e);
            }
        }

        /// <summary>
        /// Deletes an existing Team object
        /// </summary>
        /// <param name="team">Based team to delete an existing Team</param>
        /// <response code="200">Team successfully deleted</response>
        /// <response code="400">We were unable to delete the requested Team</response>
        [HttpDelete]
        public IActionResult Delete(Guid id)
        {
            try
            {
                var toDelete = repositoriesContext.TeamRepository.FindByCondition(x => x.Id == id).First();
                repositoriesContext.TeamRepository.Delete(toDelete);
                repositoriesContext.Save();
                return Ok();
            }
            catch(ArgumentException e)
            {
                return BadRequest(e);
            }
        }

    }
}
