using FluentValidation;
using FootballLeagueApp.Common.Interfaces;
using FootballLeagueApp.Common.Validation;
using FootballLeagueApp.Domain.Models.Requests.Team;

namespace FootballLeagueApp.Domain.Validators
{
    public class UpdateTeamRequestValidator :  BaseFluentValidator<UpdateTeamRequest>
    {
        public UpdateTeamRequestValidator(IDefaultErrorCodeProvider defaultErrorCodeProvider) : base(defaultErrorCodeProvider)
        {
            RuleFor(x => x.Id)
                .NotEmpty()
                .WithMessage("Id is required.")
                .GreaterThan(0)
                .WithMessage("Id must be greater than 0.");
            RuleFor(x => x.Name)
                .NotEmpty()
                .WithMessage("Name is required.")
                .MaximumLength(100)
                .WithMessage("Name must not exceed 100 characters.");

        }
    }
}

