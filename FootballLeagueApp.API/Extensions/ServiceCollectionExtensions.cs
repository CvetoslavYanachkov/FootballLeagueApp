using FluentValidation;
using FootballLeagueApp.Common.Interfaces;
using FootballLeagueApp.DataAccess.Contexts;
using FootballLeagueApp.DataAccess.DbServices;
using FootballLeagueApp.DataAccess.Interfaces;
using FootballLeagueApp.DataAccess.Repositories;
using FootballLeagueApp.Domain.Interfaces;
using FootballLeagueApp.Domain.Models.Requests.Match;
using FootballLeagueApp.Domain.Models.Requests.Team;
using FootballLeagueApp.Domain.Services;
using FootballLeagueApp.Domain.Settings;
using FootballLeagueApp.Domain.Validators;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace FootballLeagueApp.API.Extensions
{
    /// <summary>
    /// 
    /// </summary>
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        /// <exception cref="ArgumentNullException"></exception>
        public static void ResolveDependencies(this IServiceCollection services, IConfiguration configuration)
        {
            string appConString = configuration.GetConnectionString("DefaultConnection");

            services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(appConString ?? throw new ArgumentNullException(appConString)));

           
            services.AddScoped<ITeamService, TeamService>();
            services.AddScoped<ITeamRepository, teamRepository>();

            services.AddScoped<IMatchService, MatchService>();
            services.AddScoped<IMatchRepository, MatchRepository>();

            services.AddScoped<IRankingService, RankingService>();
            services.AddScoped<IRankingRepository, RankingRepository>();

            services.AddScoped<IDefaultErrorCodeProvider, ErrorResponseSettings>();

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="services"></param>
        public static void ConfigureFluentValidators(this IServiceCollection services)
        {
            ValidatorOptions.Global.DefaultRuleLevelCascadeMode = CascadeMode.Stop;

            services.AddScoped<IValidator<CreateTeamRequest>, CreateTeamRequestValidator>();
            services.AddScoped<IValidator<UpdateTeamRequest>, UpdateTeamRequestValidator>();
            services.AddScoped<IValidator<CreateMatchRequest>, CreateMatchRequestValidator>();
            services.AddScoped<IValidator<UpdateMatchRequest>, UpdateMatchRequestValidator>();
        }
    }
}
