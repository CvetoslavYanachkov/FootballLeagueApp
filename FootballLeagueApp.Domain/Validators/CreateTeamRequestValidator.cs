using FluentValidation;
using FootballLeagueApp.Common.Interfaces;
using FootballLeagueApp.Common.Validation;
using FootballLeagueApp.Domain.Models.Requests.Team;

namespace FootballLeagueApp.Domain.Validators
{
    public class CreateTeamRequestValidator : BaseFluentValidator<CreateTeamRequest>
    {
        public CreateTeamRequestValidator(IDefaultErrorCodeProvider defaultErrorCodeProvider) : base(defaultErrorCodeProvider)
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .NotNull()
                .WithMessage("Name is required.")
                .MaximumLength(100)
                .WithMessage("Name must not exceed 100 characters.");

        }
    }

}
