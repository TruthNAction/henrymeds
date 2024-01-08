using FluentValidation;

namespace Scheduling.Application.Features.Schedules.Commands.SubmitSchedule
{
    public class UpdateScheduleCommandValidator : AbstractValidator<SubmitScheduleCommand>
    {
        public UpdateScheduleCommandValidator()
        {
            RuleFor(c => c.ProviderId)
                .NotNull().WithMessage("{ProviderId} is required.");

            RuleFor(c => c.StartDateTime)
                .NotNull().WithMessage("{StartDateTime} is required.")
                .Must(startDate => IsValidTime(startDate)).WithMessage("{StartDateTime} must be at a 15 minute mark.");
        }

        private bool IsValidTime(DateTime dateTime)
        {
            int minute = dateTime.Minute;
            return minute == 0 || minute == 15 || minute == 30 || minute == 45;
        }
    }
}