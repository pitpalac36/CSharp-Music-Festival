using csharpMusicFestival.repository;
using System;
using System.Collections;
using System.Runtime.Remoting;
using System.Runtime.Remoting.Channels;
using System.Runtime.Remoting.Channels.Tcp;

namespace Server
{
    class StartServer
    {
        public static void Main(string[] args)
        {
            log4net.Config.XmlConfigurator.Configure();

            BinaryServerFormatterSinkProvider serverProv = new BinaryServerFormatterSinkProvider();
            serverProv.TypeFilterLevel = System.Runtime.Serialization.Formatters.TypeFilterLevel.Full;
            BinaryClientFormatterSinkProvider clientProv = new BinaryClientFormatterSinkProvider();
            IDictionary props = new Hashtable();

            props["port"] = 55555;
            TcpChannel channel = new TcpChannel(props, clientProv, serverProv);
            ChannelServices.RegisterChannel(channel, false);

            IUserRepository userRepo = new UserDbRepository();
            ITicketRepository ticketRepo = new TicketDbRepository();
            IShowRepository showRepo = new ShowDbRepository();
            var server = new Service(userRepo, ticketRepo, showRepo);
            RemotingServices.Marshal(server, "MusicFestival");
            //RemotingConfiguration.RegisterWellKnownServiceType(typeof(Service), "MusicFestival", WellKnownObjectMode.Singleton);

            // the server will keep running until keypress.
            Console.WriteLine("Server started ...");
            Console.WriteLine("Press <enter> to exit...");
            Console.ReadLine();
        }
    }
}
