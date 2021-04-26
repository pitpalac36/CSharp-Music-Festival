using csharpMusicFestival.domain;
using System.Linq;

namespace Networking
{
    class DtoUtils
    {
        public static User GetFromDto(UserDto userDto)
        {
            return new User(userDto.Name, userDto.Password);
        }

        public static UserDto GetDto(User user)
        {
            return new UserDto(user.Name, user.Password);
        }

        public static Ticket GetFromDto(TicketDto ticketDto)
        {
            return new Ticket(ticketDto.ShowId, ticketDto.PurchaserName, ticketDto.Number);
        }

        public static TicketDto GetDto(Ticket ticket)
        {
            return new TicketDto(ticket.ShowId, ticket.PurchaserName, ticket.Number);
        }

        public static Show GetFromDto(ShowDto showDto)
        {
            return new Show(showDto.Id, showDto.ArtistName, showDto.Date,
                    showDto.Location, showDto.AvailableTicketsNumber, showDto.SoldTicketsNumber);
        }

        private static Artist GetFromDto(ArtistDto dto)
        {
            return new Artist(dto.ArtistName, dto.Location, dto.Date, dto.AvailableTicketsNumber);
        }

        private static ArtistDto GetDto(Artist artist)
        {
            return new ArtistDto(artist.Name, artist.Location, artist.Date, artist.AvailableTicketsNumber);
        }

        public static ShowDto GetDto(Show show)
        {
            return new ShowDto(show.Id, show.ArtistName, show.Date,
                    show.Location, show.AvailableTicketsNumber, show.SoldTicketsNumber);
        }

        public static ArtistDto[] GetDto(Artist[] artists)
        {
            ArtistDto[] artistsDtos = new ArtistDto[artists.Count()];
            for (int i = 0; i < artists.Count(); i++)
            {
                artistsDtos[i] = GetDto(artists[i]);
            }
            return artistsDtos;
        }

        public static ShowDto[] getDto(Show[] shows)
        {
            ShowDto[] showsDtos = new ShowDto[shows.Count()];
            for (int i = 0; i < shows.Count(); i++)
            {
                showsDtos[i] = GetDto(shows[i]);
            }
            return showsDtos;
        }

        public static Show[] getFromDto(ShowDto[] dtos)
        {
            Show[] shows = new Show[dtos.Count()];
            for (int i = 0; i < dtos.Count(); i++)
            {
                shows[i] = GetFromDto(dtos[i]);
            }
            return shows;
        }

        public static Artist[] getFromDto(ArtistDto[] dtos)
        {
            Artist[] artists = new Artist[dtos.Count()];
            for (int i = 0; i < dtos.Count(); i++)
            {
                artists[i] = GetFromDto(dtos[i]);
            }
            return artists;
        }
    }
}
