using FootBallLeague.Data;
using FootBallLeague.Extenssions;
using FootBallLeague.Models;
using FootBallLeague.Models.DTOs;
using FootBallLeague.Repositories.Interfaces;
using FootBallLeague.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace FootBallLeague.Repositories
{
    public class MatchRepository : Repository<Match, Guid>, IMatchRepository
    {
        private readonly FootballLeageDbContext db;
        private readonly ITeamRepository teamRepository;
        private readonly IPointsService pointsService;

        public MatchRepository(
            FootballLeageDbContext db,
            ITeamRepository teamRepository,
            IPointsService pointsService)
        : base(db)
        {
            this.db = db;
            this.teamRepository = teamRepository;
            this.pointsService = pointsService;
        }


        public async Task<Match> InsertMatchEntityAndUpdateTeamPointsAsync(MatchDto matchDto)
        {
            var homeTeamEntity = await teamRepository.TryFindEntityByIdAsync(matchDto.HomeTeamId, false);
            var awayTeamEntity = await teamRepository.TryFindEntityByIdAsync(matchDto.AwayTeamId, false);

            var matchEntity = new Match
            {
                HomeTeam = homeTeamEntity,
                AwayTeam = awayTeamEntity,
                GoalsScoredByHomeTeam = matchDto.GoalsScoredByHomeTeam,
                GoalsScoredByAwayTeam = matchDto.GoalsScoredByAwayTeam
            };
            await this.CreateAsync(matchEntity);

            UpdateTeamPointsBasedOnMatch(matchEntity, homeTeamEntity, awayTeamEntity, PointsAction.Increment);

            return matchEntity;
        }
        public async Task<Match> UpdateMatchEntityAndUpdateTeamPointsAsync(Guid matchId, MatchDto match)
        {
            var existingMatchEntity = await TryFindEntityByIdAsync(matchId, true);
            var homeTeamEntity = await teamRepository.TryFindEntityByIdAsync(existingMatchEntity.HomeTeamId, false);
            var awayTeamEntity = await teamRepository.TryFindEntityByIdAsync(existingMatchEntity.AwayTeamId, false);

            existingMatchEntity.HomeTeam = homeTeamEntity;
            existingMatchEntity.AwayTeam = awayTeamEntity;

            UpdateTeamPointsBasedOnMatch(existingMatchEntity, existingMatchEntity.HomeTeam, existingMatchEntity.AwayTeam, PointsAction.Rollback);

            var newMatchEntity = new Match
            {
                Id = matchId,
                HomeTeamId = match.HomeTeamId,
                AwayTeamId = match.AwayTeamId,
                GoalsScoredByHomeTeam = match.GoalsScoredByHomeTeam,
                GoalsScoredByAwayTeam = match.GoalsScoredByAwayTeam
            };

            var newHomeTeam = existingMatchEntity.HomeTeamId != newMatchEntity.HomeTeamId
                ? await teamRepository.TryFindEntityByIdAsync(newMatchEntity.HomeTeamId, false)
                : existingMatchEntity.HomeTeam;

            var newAwayTeam = existingMatchEntity.AwayTeamId != newMatchEntity.AwayTeamId
                ? await teamRepository.TryFindEntityByIdAsync(newMatchEntity.AwayTeamId, false)
                : existingMatchEntity.AwayTeam;

            UpdateTeamPointsBasedOnMatch(newMatchEntity, newHomeTeam, newAwayTeam, PointsAction.Increment);
            await UpdateAsync(newMatchEntity);

            return newMatchEntity;
        }

        public async Task DeleteEntityAndUpdatePointsAsync(Guid matchId)
        {
            var matchEntity = await TryFindEntityByIdAsync(matchId, false);
            var homeTeamEntity = await teamRepository.TryFindEntityByIdAsync(matchEntity.HomeTeamId, true);
            var awayTeamEntity = await teamRepository.TryFindEntityByIdAsync(matchEntity.AwayTeamId, true);

            matchEntity.HomeTeam = homeTeamEntity;
            matchEntity.AwayTeam = awayTeamEntity;
            UpdateTeamPointsBasedOnMatch(matchEntity, matchEntity.HomeTeam, matchEntity.AwayTeam, PointsAction.Rollback);

            await DeleteAsync(matchEntity);
        }
        //        {
        //  "homeTeamId": "bff6fb70-726a-44d2-1575-08d98c2d66ed",
        //  "awayTeamId": "82bb55be-4d40-49a9-1576-08d98c2d66ed",
        //  "goalsScoredByHomeTeam": 0,
        //  "goalsScoredByAwayTeam": 3
        //}
        private void UpdateTeamPointsBasedOnMatch(Match match, Team homeTeam, Team awayTeam, PointsAction action)
        {
            (int homeTeamPoints, int awayTeamPoints) = pointsService.CalculatePoints(match);
            homeTeamPoints = action == PointsAction.Increment ? homeTeamPoints : -homeTeamPoints;
            awayTeamPoints = action == PointsAction.Increment ? awayTeamPoints : -awayTeamPoints;

            homeTeam.Points += homeTeamPoints;
            awayTeam.Points += awayTeamPoints;
        }

        private enum PointsAction
        {
            Increment,
            Rollback
        }
    }
}
