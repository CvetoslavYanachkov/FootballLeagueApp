using FluentValidation;
using FootballLeagueApp.Common.Interfaces;
using FootballLeagueApp.Common.Validation;
using FootballLeagueApp.Domain.Models.Requests.Match;

namespace FootballLeagueApp.Domain.Validators
{
    public class UpdateMatchRequestValidator : BaseFluentValidator<UpdateMatchRequest>
    {
        public UpdateMatchRequestValidator(IDefaultErrorCodeProvider defaultErrorCodeProvider) : base(defaultErrorCodeProvider)
        {
            RuleFor(x => x.HomeTeamId)
              .NotEmpty()
              .WithMessage("HomeTeamId is required.")
              .GreaterThan(0)
              .WithMessage("HomeTeamId must be greater than 0.");

            RuleFor(x => x.AwayTeamId)
              .NotEmpty()
              .WithMessage("AwayTeamId is required.")
              .GreaterThan(0)
              .WithMessage("AwayTeamId must be greater than 0.");

            RuleFor(x => x)
               .Must(x => x.HomeTeamId != x.AwayTeamId)
               .WithMessage("HomeTeamId and AwayTeamId must not be the same.");

        }
    }
}
