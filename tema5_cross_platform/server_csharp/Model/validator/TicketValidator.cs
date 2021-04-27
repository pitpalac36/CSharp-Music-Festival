using csharpMusicFestival.domain;
using System.Text;

namespace csharpMusicFestival.validator
{
    class TicketValidator : IValidator<Ticket>
    {
        public void validate(Ticket elem)
        {
            StringBuilder errors = new StringBuilder("");
            if (elem.PurchaserName.Equals(""))
                errors.Append("\nPurchaser name cannot be empty!!\n");
            if (elem.Number < 1)
                errors.Append("Number of purchased tickets must be at least 1!!\n");
            if (!errors.ToString().Equals(""))
                throw new ValidationException(errors.ToString());
        }
    }
}