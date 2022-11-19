using FluentValidation;
using SettlementApiService.Models;
using System.Text.RegularExpressions;

namespace SettlementApiService.Validators
{
    public class SettlementBookingDataValidator : AbstractValidator<SettlementBookingModel>
    {
        public SettlementBookingDataValidator()
        {
            RuleFor(x => x.Name).NotNull().NotEmpty();
            RuleFor(x => x.BookingTime).Must(BeAValidBookingTime);
        }
        private bool BeAValidBookingTime(string bookingTime)
        {
            var regexPatern = "([0-9][0-9]):([0-9][0-9])";
            var regex = new Regex(regexPatern);

            var matches = regex.Matches(bookingTime);
            if (matches.Count() > 0)
            {
                var timeParts = bookingTime.Split(':');
                var hours = int.Parse(timeParts[0]);
                var minute = int.Parse(timeParts[1]);

                if (hours <= 23 && minute <= 60)
                {
                    return true;
                }
            }

            return false;
        }
    }
}
