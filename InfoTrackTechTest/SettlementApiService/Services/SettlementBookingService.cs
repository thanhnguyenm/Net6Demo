using FluentValidation;
using Microsoft.EntityFrameworkCore;
using SettlementApiService.Db;
using SettlementApiService.Enitties;
using SettlementApiService.Exceptions;
using SettlementApiService.Models;
using System.Text.RegularExpressions;

namespace SettlementApiService.Services
{
    public interface ISettlementService
    {
        Task<SettlementBookingModelResult> CreateSettlementBookingAsync(SettlementBookingModel model, CancellationToken cancellationToken);
    }

    public class SettlementBookingService : ISettlementService
    {
        private readonly IValidator<SettlementBookingModel> validator;
        private readonly SettlementDbContext context;

        private const int MAX_SIMULTANEOUS_SETTLEMENT = 4;

        public SettlementBookingService(IValidator<SettlementBookingModel> _validator,
            SettlementDbContext _context)
        {
            validator = _validator;
            context = _context;
        }

        public async Task<SettlementBookingModelResult> CreateSettlementBookingAsync(SettlementBookingModel model, CancellationToken cancellationToken)
        {
            if (!validator.Validate(model).IsValid) throw new BadRequestException();

            var timeParts = model.BookingTime.Split(':');
            var hours = int.Parse(timeParts[0]);
            var minute = int.Parse(timeParts[1]);

            var bookingDateTimeFrom = DateTime.UtcNow.Date.AddHours(hours).AddMinutes(minute);
            var bookingDateTimeTo = bookingDateTimeFrom.AddHours(1);
            var noSameBookingBoth = await context.SettlementBookings.CountAsync(x => x.BookingTimeFrom == bookingDateTimeFrom && bookingDateTimeFrom == x.BookingTimeTo, cancellationToken);
            
            var bookingsInFrom = await context.SettlementBookings.Where(x => x.BookingTimeFrom < bookingDateTimeFrom && bookingDateTimeFrom < x.BookingTimeTo).ToListAsync(cancellationToken);
            var noSameBookingFrom = bookingsInFrom.Count();
            var minTimeFrom = noSameBookingFrom > 0 ? bookingsInFrom.Min(x => x.BookingTimeTo) : (DateTime?)null;

            var bookingsInTo = await context.SettlementBookings.Where(x => x.BookingTimeFrom < bookingDateTimeTo && bookingDateTimeTo < x.BookingTimeTo).ToListAsync(cancellationToken);
            var noSameBookingTo = bookingsInTo.Count();
            var maxTimeTo = noSameBookingTo > 0 ? bookingsInTo.Max(x => x.BookingTimeFrom) : (DateTime?)null;

            var noSameBooking = noSameBookingFrom + noSameBookingBoth;
            if(minTimeFrom.HasValue && maxTimeTo.HasValue)
            {
                noSameBooking += bookingsInTo.Where(x => bookingDateTimeFrom < x.BookingTimeFrom && x.BookingTimeFrom < minTimeFrom).Count();
            }

            if (noSameBooking >= MAX_SIMULTANEOUS_SETTLEMENT) throw new ConflictException();

            var newBooking = new SettlementBooking
            {
                SettlementBookingId = Guid.NewGuid(),
                Name = model.Name,
                BookingTimeFrom = bookingDateTimeFrom,
                BookingTimeTo = bookingDateTimeTo
            };
            await context.SettlementBookings.AddAsync(newBooking, cancellationToken);
            await context.SaveChangesAsync(cancellationToken);

            return new SettlementBookingModelResult { BookingId = newBooking.SettlementBookingId };
        }
    }
}
