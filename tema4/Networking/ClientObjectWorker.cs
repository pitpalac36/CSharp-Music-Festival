using csharpMusicFestival.domain;
using csharpMusicFestival.validator;
using Services;
using System;
using System.Net.Sockets;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading;

namespace Networking
{
	public class ClientWorker : IObserver
	{
		private IService server;
		private TcpClient connection;
		private NetworkStream stream;
		private IFormatter formatter;
		private volatile bool connected;

		public ClientWorker(IService server, TcpClient connection)
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

		private void sendResponse(Response response)
		{
			Console.WriteLine("sending response " + response);
			formatter.Serialize(stream, response);
			stream.Flush();

		}

		private Response handleRequest(Request request)
		{
			Response response = null;
			if (request is LoginRequest)
			{
				Console.WriteLine("Login request ...");
				LoginRequest logReq = (LoginRequest)request;
				UserDto udto = logReq.User;
				User user = DtoUtils.GetFromDto(udto);
				try
				{
					lock (server)
					{
						server.Login(user, this);
					}
					return new OkResponse();
				}
				catch (Error e)
				{
					connected = false;
					return new ErrorResponse(e.Message);
				}
			}
			if (request is LogoutRequest)
			{
				Console.WriteLine("Logout request");
				LogoutRequest logReq = (LogoutRequest)request;
				UserDto udto = logReq.User;
				User user = DtoUtils.GetFromDto(udto);
				try
				{
					lock (server)
					{
						server.Logout(user, this);
					}
					connected = false;
					return new OkResponse();
				}
				catch (Error e)
				{
					return new ErrorResponse(e.Message);
				}
			}

			if (request is GetShowsRequest)
			{
				Console.WriteLine("Get shows request");
				GetShowsRequest getShowsRequest = (GetShowsRequest)request;
				try
				{
					Show[] shows = server.GetAll();
					ShowDto[] showDtos = DtoUtils.getDto(shows);
					return new GetShowsResponse(showDtos);
				}
				catch (Error e)
				{
					return new ErrorResponse(e.Message);
				}
			}

			if (request is GetArtistsByDateRequest)
			{
				Console.WriteLine("Get artists by date request");
				GetArtistsByDateRequest getArtistsByDateRequest = (GetArtistsByDateRequest)request;
				String date = getArtistsByDateRequest.Date;
				try
				{
					Artist[] artists = server.GetArtists(date);
					ArtistDto[] artistDtos = DtoUtils.GetDto(artists);
					return new GetArtistsByDateResponse(artistDtos);
				}
				catch (Error e)
				{
					return new ErrorResponse(e.Message);
				}
			}

			if (request is BuyTicketRequest)
			{
				Console.WriteLine("Buy ticket request");
				BuyTicketRequest buyTicketRequest = (BuyTicketRequest)request;
				TicketDto ticketDto = buyTicketRequest.TicketDto;
				Ticket ticket = DtoUtils.GetFromDto(ticketDto);
				try
				{
					lock(server)
                    {
						server.SellTickets(ticket);
					}
					return new OkResponse();
				}
				catch (InvalidPurchaseException e)
				{
					return new ErrorResponse(e.Message);
				}
				catch (Error e)
				{
					return new ErrorResponse(e.Message);
				}
			}
			return response;
		}

		public virtual void run()
		{
			while (connected)
			{
				try
				{
					object request = formatter.Deserialize(stream);
					object response = handleRequest((Request)request);
					if (response != null)
					{
						sendResponse((Response)response);
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
				Console.WriteLine("Error " + e);
			}
		}

		public void TicketSold(Ticket ticket)
		{
			TicketDto dto = DtoUtils.GetDto(ticket);
			Console.WriteLine("Ticket sold  " + ticket);
			try
			{
				sendResponse(new BuyTicketResponse(dto));
			}
			catch (Exception e)
			{
				throw new Error("Sending error: " + e);
			}
		}
	}
}
