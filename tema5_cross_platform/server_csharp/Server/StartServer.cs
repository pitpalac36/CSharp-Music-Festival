using csharpMusicFestival.repository;
using Networking;
using ServerTemplate;
using Services;
using System;
using System.Net.Sockets;
using System.Threading;
using Proto;

namespace Server
{
    class StartServer
    {
        public static void Main(string[] args)
        {

            log4net.Config.XmlConfigurator.Configure();

            IUserRepository userRepo = new UserDbRepository();
            ITicketRepository ticketRepo = new TicketDbRepository();
            IShowRepository showRepo = new ShowDbRepository();
            IService serviceImpl = new Service(userRepo, ticketRepo, showRepo);

            Console.WriteLine("userRepo size : " + userRepo.FindAll().Count);

            //SerialServer server = new SerialServer("127.0.0.1", 55555, serviceImpl);
            ProtoServer server = new ProtoServer("127.0.0.1", 55555, serviceImpl);
            server.Start();
            Console.WriteLine("Server started ...");

        }
    }

    public class SerialServer : ConcurrentServer
    {
        private IService server;
        private ClientWorker worker;
        public SerialServer(string host, int port, IService server) : base(host, port)
        {
            this.server = server;
            Console.WriteLine("SerialServer...");
        }
        protected override Thread createWorker(TcpClient client)
        {
            worker = new ClientWorker(server, client);
            return new Thread(new ThreadStart(worker.run));
        }
    }
    
    public class ProtoServer : ConcurrentServer
    {
        private IService server;
        private ProtoWorker worker;
        public ProtoServer(string host, int port, IService server)
            : base(host, port)
        {
            this.server = server;
            Console.WriteLine("ProtoChatServer...");
        }
        protected override Thread createWorker(TcpClient client)
        {
            worker = new ProtoWorker(server, client);
            return new Thread(new ThreadStart(worker.run));
        }
    }
}
