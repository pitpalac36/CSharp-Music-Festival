import domain.Artist;
import domain.Show;
import domain.Ticket;
import domain.User;
import protobuffprotocol.Protobufs;

public class ProtoUtils {

    public static Protobufs.Request createLoginRequest(User user){
        Protobufs.User userDTO = Protobufs.User.newBuilder().setUsername(user.getUsername()).setPassword(user.getPassword()).build();
        return Protobufs.Request.newBuilder().setType(Protobufs.Request.Type.Login)
                .setUser(userDTO).build();
    }

    public static Protobufs.Request createLogoutRequest(User user){
        Protobufs.User userDTO = Protobufs.User.newBuilder().setUsername(user.getUsername()).setPassword(user.getPassword()).build();
        return Protobufs.Request.newBuilder().setType(Protobufs.Request.Type.Logout)
                .setUser(userDTO).build();
    }

    public static Protobufs.Response createGetShowsResponse(Show[] shows){
        Protobufs.Response.Builder response = Protobufs.Response.newBuilder()
                .setType(Protobufs.Response.Type.GetShows);
        for (Show show : shows){
            Protobufs.Show showDTO = Protobufs.Show.newBuilder()
                    .setId(show.getId())
                    .setArtistName(show.getArtistName())
                    .setAvailableTicketsNumber(show.getAvailableTicketsNumber())
                    .setDate(show.getDate())
                    .setLocation(show.getLocation())
                    .setSoldTicketsNumber(show.getSoldTicketsNumber())
                    .build();
            response.addShows(showDTO);
        }
        return response.build();
    }

    public static Protobufs.Response createGetArtistsByDateResponse(Artist[] artists){
        Protobufs.Response.Builder response = Protobufs.Response.newBuilder()
                .setType(Protobufs.Response.Type.GetArtistsByDate);
        for (Artist artist : artists){
            Protobufs.Artist artistDTO = Protobufs.Artist.newBuilder()
                    .setName(artist.getName())
                    .setAvailableTicketsNumber(artist.getAvailableTicketsNumber())
                    .setDate(artist.getDate())
                    .setLocation(artist.getLocation())
                    .build();
            response.addArtist(artistDTO);
        }
        return response.build();
    }

    public static Protobufs.Response createOkResponse(){
        return Protobufs.Response.newBuilder()
                .setType(Protobufs.Response.Type.Ok).build();
    }

    public static Protobufs.Response createErrorResponse(String text){
        return Protobufs.Response.newBuilder()
                .setType(Protobufs.Response.Type.Error)
                .setError(text).build();
    }

    public static String getError(Protobufs.Response response){
        return response.getError();
    }

    public static User getUser(Protobufs.Response response){
        User user=new User();
        user.setUsername(response.getUser().getUsername());
        return user;
    }

    public static User getUser(Protobufs.Request request){
        User user=new User();
        user.setUsername(request.getUser().getUsername());
        user.setPassword(request.getUser().getPassword());
        return user;
    }

    public static String getDate(Protobufs.Request request) {
        return request.getDate();
    }

    public static Ticket getTicket(Protobufs.Response response) {
        Ticket ticket = new Ticket();
        ticket.setShowId(response.getTicket().getShowId());
        ticket.setNumber(response.getTicket().getNumber());
        ticket.setPurchaserName(response.getTicket().getPurchaserName());
        return ticket;
    }

    public static Ticket getTicket(Protobufs.Request request) {
        Ticket ticket = new Ticket();
        ticket.setShowId(request.getTicket().getShowId());
        ticket.setNumber(request.getTicket().getNumber());
        ticket.setPurchaserName(request.getTicket().getPurchaserName());
        return ticket;
    }

    public static Show[] getShows(Protobufs.Response response) {
        Show[] shows = new Show[response.getShowsCount()];
        for(int i = 0; i < response.getShowsCount();i++){
            Protobufs.Show showDTO = response.getShows(i);
            Show show = new Show();
            show.setId(showDTO.getId());
            show.setLocation(showDTO.getLocation());
            show.setDate(showDTO.getDate());
            show.setArtistName(showDTO.getArtistName());
            show.setAvailableTicketsNumber(showDTO.getAvailableTicketsNumber());
            show.setSoldTicketsNumber(showDTO.getSoldTicketsNumber());
            shows[i] = show;
        }
        return shows;
    }

    public static Artist[] getArtists(Protobufs.Response response) {
        Artist[] artists = new Artist[response.getArtistCount()];
        for(int i = 0; i < response.getArtistCount();i++){
            Protobufs.Artist artistDTO = response.getArtist(i);
            Artist artist = new Artist();
            artist.setName(artistDTO.getName());
            artist.setAvailableTicketsNumber(artistDTO.getAvailableTicketsNumber());
            artist.setDate(artistDTO.getDate());
            artist.setLocation(artistDTO.getLocation());
            artists[i] = artist;
        }
        return artists;
    }

    public static Protobufs.Request getShowsRequest() {
        return Protobufs.Request.newBuilder().setType(Protobufs.Request.Type.GetShows).build();
    }

    public static Protobufs.Request getArtistsByDateRequest(String date) {
        return Protobufs.Request.newBuilder().setType(Protobufs.Request.Type.GetArtistsByDate).setDate(date).build();
    }

    public static Protobufs.Request buyTicketRequest(Ticket ticket) {
        Protobufs.Ticket ticketDTO = Protobufs.Ticket.newBuilder()
                .setShowId(ticket.getShowId())
                .setPurchaserName(ticket.getPurchaserName())
                .setNumber(ticket.getNumber())
                .build();
        return Protobufs.Request.newBuilder().setType(Protobufs.Request.Type.BuyTicket).setTicket(ticketDTO).build();
    }

    public static Protobufs.Response createTicketSoldResponse(Ticket ticket) {
        Protobufs.Ticket ticketDTO = Protobufs.Ticket.newBuilder()
                .setShowId(ticket.getShowId())
                .setPurchaserName(ticket.getPurchaserName())
                .setNumber(ticket.getNumber())
                .build();

        return Protobufs.Response.newBuilder()
                .setType(Protobufs.Response.Type.BuyTicket)
                .setTicket(ticketDTO).build();
    }
}
