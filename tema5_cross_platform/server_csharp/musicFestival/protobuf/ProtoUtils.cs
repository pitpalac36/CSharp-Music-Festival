namespace Proto
{
    public class ProtoUtils {

    public static Request createLoginRequest(User user)
    {
        Proto.User userDTO = new Proto.User {Username = user.Username, Password = user.Password};
        Request request = new Request {Type = Request.Types.Type.Login, User = userDTO};
        return request;
    }

    public static Request createLogoutRequest(csharpMusicFestival.domain.User user)
    {
        Proto.User userDTO = new Proto.User {Username = user.Name, Password = user.Password};
        Request request = new Request{Type = Request.Types.Type.Logout, User = userDTO};
        return request;
    }

    public static Response createGetShowsResponse(Show[] shows){
        Response response = new Response { 
            Type = Response.Types.Type.GetShows
        };
        foreach (var show in shows)
        {
            Proto.Show showDTO = new Proto.Show
            {
                Id = show.Id,
                ArtistName = show.ArtistName,
                AvailableTicketsNumber = show.AvailableTicketsNumber,
                Date = show.Date,
                Location = show.Location,
                SoldTicketsNumber = show.SoldTicketsNumber
            };
            response.Shows.Add(showDTO);
        }
        return response;
    }

    public static Response createGetArtistsByDateResponse(Artist[] artists){
        Response response = new Response { 
            Type = Response.Types.Type.GetArtistsByDate
        };
        foreach (var artist in artists)
        {
            Proto.Artist artistDTO = new Proto.Artist
            {
                Name = artist.Name,
                AvailableTicketsNumber = artist.AvailableTicketsNumber,
                Date = artist.Date,
                Location = artist.Location,
            };
            response.Artist.Add(artistDTO);
        }
        return response;
    }

    public static Response createOkResponse()
    {
        Response response = new Response{ Type=Response.Types.Type.Ok};
        return response;
    }

    public static Response createErrorResponse(string text)
    {
        Response response = new Response{
            Type=Response.Types.Type.Error, Error=text};
        return response;
    }

    public static string GetError(Response response){
        string errorMessage = response.Error;
        return errorMessage;
    }

    public static csharpMusicFestival.domain.User GetUser(Response response){
        csharpMusicFestival.domain.User user = new csharpMusicFestival.domain.User(response.User.Username, response.User.Password);
        return user;
    }

    public static csharpMusicFestival.domain.User GetUser(Request request)
    {
        var user = new csharpMusicFestival.domain.User {Name = request.User.Username, Password = request.User.Password};
        return user;
    }

    public static string GetDate(Request request)
    {
        return request.Date;
    }

    public static Ticket GetTicket(Response response)
    {
        Ticket ticket = new Ticket
        {
            ShowId = response.Ticket.ShowId,
            Number = response.Ticket.Number,
            PurchaserName = response.Ticket.PurchaserName
        };
        return ticket;
    }

    public static csharpMusicFestival.domain.Ticket GetTicket(Request request)
    {
        var ticket = new csharpMusicFestival.domain.Ticket
        {
            ShowId = request.Ticket.ShowId,
            Number = request.Ticket.Number,
            PurchaserName = request.Ticket.PurchaserName
        };
        return ticket;
    }

    public static Show[] GetShows(Response response) {
        Show[] shows = new Show[response.Shows.Count];
        for(int i = 0; i < response.Shows.Count;i++)
        {
            Show show = new Show
            {
                Id = response.Shows[i].Id,
                Location = response.Shows[i].Location,
                Date = response.Shows[i].Date,
                ArtistName = response.Shows[i].ArtistName,
                AvailableTicketsNumber = response.Shows[i].AvailableTicketsNumber,
                SoldTicketsNumber = response.Shows[i].SoldTicketsNumber
            };
            shows[i] = show;
        }
        return shows;
    }

    public static Artist[] GetArtists(Response response) {
        Artist[] artists = new Artist[response.Artist.Count];
        for(int i = 0; i < response.Artist.Count;i++)
        {
            Artist artist = new Artist
            {
                Location = response.Shows[i].Location,
                Date = response.Shows[i].Date,
                Name = response.Shows[i].ArtistName,
                AvailableTicketsNumber = response.Shows[i].AvailableTicketsNumber,
            };
            artists[i] = artist;
        }
        return artists;
    }

    public static Response CreateGetShowsResponse(csharpMusicFestival.domain.Show[] shows)
    {
        Response response = new Response { 
            Type=Response.Types.Type.GetShows
        };
        foreach (csharpMusicFestival.domain.Show show in shows)
        {
            Proto.Show showDTO = new Proto.Show
            {
                Id = show.Id,
                Location = show.Location,
                Date = show.Date,
                ArtistName = show.ArtistName,
                AvailableTicketsNumber = show.AvailableTicketsNumber,
                SoldTicketsNumber = show.SoldTicketsNumber
            };
            response.Shows.Add(showDTO);
        }

        return response;
    }

    public static Response CreateGetArtistsByDateResponse(csharpMusicFestival.domain.Artist[] artists)
    {
        Response response = new Response { 
            Type=Response.Types.Type.GetArtistsByDate
        };
        foreach (csharpMusicFestival.domain.Artist artist in artists)
        {
            Proto.Artist artistDTO = new Proto.Artist
            {
                Location = artist.Location,
                Date = artist.Date,
                Name = artist.Name,
                AvailableTicketsNumber = artist.AvailableTicketsNumber,
            };
            response.Artist.Add(artistDTO);
        }

        return response;
    }

    public static Response CreateBuyTicketResponse(csharpMusicFestival.domain.Ticket ticket)
    {
        var ticketDTO = new Proto.Ticket
        {
            ShowId = ticket.ShowId,
            Number = ticket.Number,
            PurchaserName = ticket.PurchaserName
        };

        Response response = new Response { Type = Response.Types.Type.BuyTicket, Ticket = ticketDTO};
        return response;
    }
    }
}