using System;
using System.Net.Sockets;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading;
using csharpMusicFestival.validator;
using Google.Protobuf;
using Services;

namespace Proto
{
    public class ProtoWorker : IObserver
    {
		private IService server;
		private TcpClient connection;
		private NetworkStream stream;
		private IFormatter formatter;
		private volatile bool connected;

		public ProtoWorker(IService server, TcpClient connection)
		{
			this.server = server;
			this.connection = connection;
			try
			{
				stream = connection.GetStream();
				formatter = new BinaryFormatter();
				connected = true;
			}
			catch (Exception e)
			{
				Console.WriteLine(e.StackTrace);
			}
		}
		
		public virtual void run()
		{
			while(connected)
			{
				try
				{
					Request request = Request.Parser.ParseDelimitedFrom(stream);
					Response response =handleRequest(request);
					if (response!=null)
					{
						sendResponse(response);
					}
				}
				catch (Exception e)
				{
					Console.WriteLine(e.StackTrace);
				}
				
				try
				{
					Thread.Sleep(1000);
				}
				catch (Exception e)
				{
					Console.WriteLine(e.StackTrace);
				}
			}
			try
			{
				stream.Close();
				connection.Close();
			}
			catch (Exception e)
			{
				Console.WriteLine("Error "+e);
			}
		}

		private void sendResponse(Response response)
		{
			Console.WriteLine("sending response "+response);
			lock (stream)
			{
				response.WriteDelimitedTo(stream);
				stream.Flush();
			}

		}

		private Response handleRequest(Request request)
		{
			Response response = null;
			Request.Types.Type reqType = request.Type;
			switch (reqType)
			{
				case Request.Types.Type.Login:
				{
					Console.WriteLine("Login request ...");
					csharpMusicFestival.domain.User user = ProtoUtils.GetUser(request);
					try
					{
						lock (server)
						{
							server.Login(user, this);
						}

						return ProtoUtils.createOkResponse();
					}
					catch (Error e)
					{
						connected = false;
						return ProtoUtils.createErrorResponse(e.Message);
					}
				}

				case Request.Types.Type.Logout:
				{
					Console.WriteLine("Logout request");
					csharpMusicFestival.domain.User user = ProtoUtils.GetUser(request);
					try
					{
						lock (server)
						{
							server.Logout(user, this);
						}

						connected = false;
						return ProtoUtils.createOkResponse();

					}
					catch (Error e)
					{
						return ProtoUtils.createErrorResponse(e.Message);
					}
				}

				case Request.Types.Type.GetShows:
				{
					Console.WriteLine("Get shows request");
					try
					{
						csharpMusicFestival.domain.Show[] shows = server.GetAll();
						return ProtoUtils.CreateGetShowsResponse(shows);
					}
					catch (Error e)
					{
						return ProtoUtils.createErrorResponse(e.Message);
					}
				}

				case Request.Types.Type.GetArtistsByDate:
				{
					Console.WriteLine("Get artists by date request");
					String date = request.Date;
					try
					{
						csharpMusicFestival.domain.Artist[] artists = server.GetArtists(date);
						return ProtoUtils.CreateGetArtistsByDateResponse(artists);
					}
					catch (Error e)
					{
						return ProtoUtils.createErrorResponse(e.Message);
					}
				}

				case Request.Types.Type.BuyTicket:
				{
					Console.WriteLine("Buy ticket request");
					csharpMusicFestival.domain.Ticket ticket = ProtoUtils.GetTicket(request);
					try
					{
						lock (server)
						{
							server.SellTickets(ticket);
						}

						return ProtoUtils.createOkResponse();
					}
					catch (InvalidPurchaseException e)
					{
						return ProtoUtils.createErrorResponse(e.Message);
					}
					catch (Error e)
					{
						return ProtoUtils.createErrorResponse(e.Message);
					}
				}
					
			}
			return response;
		}

		public void TicketSold(csharpMusicFestival.domain.Ticket ticket)
		{
			Console.WriteLine("Ticket sold  " + ticket);
			try
			{
				sendResponse(ProtoUtils.CreateBuyTicketResponse(ticket));
			}
			catch (Exception e)
			{
				throw new Error("Sending error: " + e);
			}
		}
    }
}