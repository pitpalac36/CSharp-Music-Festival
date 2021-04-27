import utils.AbstractConcurrentServer;
import java.net.Socket;

public class ObjectConcurrentServer extends AbstractConcurrentServer {
    private IService server;
    public ObjectConcurrentServer(int port, IService server) {
        super(port);
        this.server = server;
    }

    @Override
    protected Thread createWorker(Socket client) {
        ProtoWorker worker=new ProtoWorker(server, client);
        Thread tw =new Thread(worker);
        return tw;
    }
}
