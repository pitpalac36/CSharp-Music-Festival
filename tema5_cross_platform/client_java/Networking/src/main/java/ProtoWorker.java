import domain.Artist;
import domain.Show;
import domain.Ticket;
import domain.User;
import objectprotocol.ErrorResponse;
import objectprotocol.GetArtistsByDateRequest;
import protobuffprotocol.Protobufs;
import validator.InvalidPurchaseException;
import java.io.*;
import java.net.Socket;


public class ProtoWorker implements Runnable, IObserver {
    private IService server;
    private Socket connection;
    private InputStream input;
    private OutputStream output;
    private volatile boolean connected;

    public ProtoWorker(IService server, Socket connection) {
        this.server = server;
        this.connection = connection;
        try{
            output=connection.getOutputStream() ;//new ObjectOutputStream(connection.getOutputStream());
            input=connection.getInputStream(); //new ObjectInputStream(connection.getInputStream());
            connected=true;
        } catch (IOException e) {
            e.printStackTrace();
        }
    }

    public void run() {
        while(connected){
            try {
                // Object request=input.readObject();
                System.out.println("Waiting requests ...");
                Protobufs.Request request = Protobufs.Request.parseDelimitedFrom(input);
                System.out.println("Request received: "+request);
                Protobufs.Response response = (Protobufs.Response) handleRequest(request);
                if (response!=null){
                    sendResponse(response);
                }
            } catch (IOException e) {
                e.printStackTrace();
            }
            try {
                Thread.sleep(1000);
            } catch (InterruptedException e) {
                e.printStackTrace();
            }
        }
        try {
            input.close();
            output.close();
            connection.close();
        } catch (IOException e) {
            System.out.println("Error "+e);
        }
    }

    private Serializable handleRequest(Protobufs.Request request) {
        Protobufs.Response response = null;
        switch (request.getType()) {
            case Login: {
                System.out.println("Login request ...");
                User user = ProtoUtils.getUser(request);
                try {
                    server.login(user, this);
                    return ProtoUtils.createOkResponse();
                } catch (Error e) {
                    connected=false;
                    return ProtoUtils.createErrorResponse(e.getMessage());
                }
            }
            case Logout:{
                System.out.println("Logout request");
                User user=ProtoUtils.getUser(request);
                try {
                    server.logout(user, this);
                    connected=false;
                    return ProtoUtils.createOkResponse();

                } catch (Error e) {
                    return ProtoUtils.createErrorResponse(e.getMessage());
                }
            }
            case GetShows: {
                System.out.println("Get shows request");
                try {
                    Show[] shows = server.getAll();
                    return ProtoUtils.createGetShowsResponse(shows);
                } catch (Error e) {
                    return ProtoUtils.createErrorResponse(e.getMessage());
                }
            }
            case GetArtistsByDate: {
                System.out.println("Get artists by date request");
                String date = ProtoUtils.getDate(request);
                try {
                    Artist[] artists = server.getArtists(date);
                    return ProtoUtils.createGetArtistsByDateResponse(artists);
                } catch (Error e) {
                    return ProtoUtils.createErrorResponse(e.getMessage());
                }
            }

            case BuyTicket: {
                System.out.println("Buy ticket request");
                Ticket ticket = ProtoUtils.getTicket(request);
                try {
                    server.sellTickets(ticket);
                    return ProtoUtils.createOkResponse();
                } catch (Error | InvalidPurchaseException e) {
                    return ProtoUtils.createErrorResponse(e.getMessage());
                }
            }
        }

        return response;
    }

    private void sendResponse(Protobufs.Response response) throws IOException{
        System.out.println("sending response "+response);
        response.writeDelimitedTo(output);
        //output.writeObject(response);
        output.flush();
    }

    @Override
    public void ticketSold(Ticket ticket) throws Error {
        System.out.println("Ticket sold  "+ ticket);
        try {
            sendResponse(ProtoUtils.createTicketSoldResponse(ticket));
        } catch (IOException e) {
            throw new Error("Sending error: "+e);
        }
    }
}
