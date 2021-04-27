import domain.Artist;
import domain.Show;
import domain.Ticket;
import domain.User;
import protobuffprotocol.Protobufs;
import java.io.*;
import java.net.Socket;
import java.util.concurrent.BlockingQueue;
import java.util.concurrent.LinkedBlockingQueue;

public class ProtoProxy implements IService {
    private String host;
    private int port;

    private IObserver client;

    private InputStream input;
    private OutputStream output;
    private Socket connection;

    private BlockingQueue<Protobufs.Response> qresponses;
    private volatile boolean finished;

    public ProtoProxy(String host, int port) {
        this.host = host;
        this.port = port;
        qresponses=new LinkedBlockingQueue<Protobufs.Response>();
    }

    public void login(User user, IObserver client) throws java.lang.Error {
        initializeConnection();
        sendRequest(ProtoUtils.createLoginRequest(user));
        Protobufs.Response response = readResponse();
        if (response.getType() == Protobufs.Response.Type.Ok){
            this.client=client;
            return;
        }
        if (response.getType()==Protobufs.Response.Type.Error){
            String errorText= ProtoUtils.getError(response);
            closeConnection();
            throw new java.lang.Error(errorText);
        }
    }

    public void logout(User user, IObserver client) throws java.lang.Error {
        sendRequest(ProtoUtils.createLogoutRequest(user));
        Protobufs.Response response=readResponse();
        closeConnection();
        if (response.getType()==Protobufs.Response.Type.Error){
            String errorText= ProtoUtils.getError(response);
            throw new java.lang.Error(errorText);
        }
    }

    @Override
    public Show[] getAll() throws java.lang.Error {
        sendRequest(ProtoUtils.getShowsRequest());
        Protobufs.Response response = readResponse();
        if (response.getType()==Protobufs.Response.Type.Error){
            String errorText= ProtoUtils.getError(response);
            throw new java.lang.Error(errorText);
        }
        return ProtoUtils.getShows(response);
    }

    @Override
    public Artist[] getArtists(String date) throws java.lang.Error {
        sendRequest(ProtoUtils.getArtistsByDateRequest(date));
        Protobufs.Response response = readResponse();
        if (response.getType()==Protobufs.Response.Type.Error){
            String errorText= ProtoUtils.getError(response);
            throw new java.lang.Error(errorText);
        }
        return ProtoUtils.getArtists(response);
    }

    @Override
    public void sellTickets(Ticket ticket) throws java.lang.Error {
        sendRequest(ProtoUtils.buyTicketRequest(ticket));
        Protobufs.Response response = readResponse();
        if (response.getType()==Protobufs.Response.Type.Error){
            String errorText= ProtoUtils.getError(response);
            throw new java.lang.Error(errorText);
        }
    }


    private void closeConnection() {
        finished=true;
        try {
            input.close();
            output.close();
            connection.close();
            client=null;
        } catch (IOException e) {
            e.printStackTrace();
        }

    }

    private void sendRequest(Protobufs.Request request) throws java.lang.Error {
        try {
            System.out.println("Sending request ..."+request);
            //request.writeTo(output);
            request.writeDelimitedTo(output);
            output.flush();
            System.out.println("Request sent.");
        } catch (IOException e) {
            throw new java.lang.Error("Error sending object "+e);
        }

    }

    private Protobufs.Response readResponse() throws java.lang.Error {
        Protobufs.Response response=null;
        try{
            response=qresponses.take();

        } catch (InterruptedException e) {
            e.printStackTrace();
        }
        return response;
    }
    private void initializeConnection() throws java.lang.Error {
        try {
            connection=new Socket(host,port);
            output=connection.getOutputStream();
            //output.flush();
            input=connection.getInputStream();     //new ObjectInputStream(connection.getInputStream());
            finished=false;
            startReader();
        } catch (IOException e) {
            e.printStackTrace();
        }
    }
    private void startReader(){
        Thread tw=new Thread(new ReaderThread());
        tw.start();
    }


    private void handleUpdate(Protobufs.Response updateResponse) {
        if (updateResponse.getType() == Protobufs.Response.Type.BuyTicket) {
            Ticket ticket = ProtoUtils.getTicket(updateResponse);
            System.out.println("HANDLE UPDATE " + ticket);
            try {
                client.ticketSold(ticket);
            } catch (java.lang.Error | Error e) {
                e.printStackTrace();
            }
        }
    }

    private class ReaderThread implements Runnable{
        public void run() {
            while(!finished){
                try {
                    Protobufs.Response response=Protobufs.Response.parseDelimitedFrom(input);
                    System.out.println("response received "+response);

                    if (isUpdateResponse(response.getType())){
                        handleUpdate(response);
                    }else{
                        try {
                            qresponses.put(response);
                        } catch (InterruptedException e) {
                            e.printStackTrace();
                        }
                    }
                } catch (IOException e) {
                    System.out.println("Reading error "+e);
                }
            }
        }
    }

    private boolean isUpdateResponse(Protobufs.Response.Type type){
        return type == Protobufs.Response.Type.BuyTicket;
    }
}
