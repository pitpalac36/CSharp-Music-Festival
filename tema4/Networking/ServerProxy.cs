using csharpMusicFestival.domain;
using csharpMusicFestival.validator;
using Services;
using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading;

namespace Networking
{
	public class ServerProxy : IService
	{
		private string host;
		private int port;
		private IObserver client;
		private NetworkStream stream;
		private IFormatter formatter;
		private TcpClient connection;
		private Queue<Response> responses;
		private volatile bool finished;
		private EventWaitHandle _waitHandle;

		public ServerProxy(string host, int port)
		{
			this.host = host;
			this.port = port;
			responses = new Queue<Response>();
		}

		public virtual void Login(User user, IObserver client)
		{
			initializeConnection();
			UserDto udto = DtoUtils.GetDto(user);
			sendRequest(new LoginRequest(udto));
			Response response = readResponse();
			if (response is OkResponse)
			{
				this.client = client;
				return;
			}
			if (response is ErrorResponse)
			{
				ErrorResponse err = (ErrorResponse)response;
				closeConnection();
				throw new Error(err.Message);
			}
		}

		public virtual void Logout(User user, IObserver client)
		{
			UserDto udto = DtoUtils.GetDto(user);
			sendRequest(new LogoutRequest(udto));
			Response response = readResponse();
			closeConnection();
			if (response is ErrorResponse)
			{
				ErrorResponse err = (ErrorResponse)response;
				throw new Error(err.Message);
			}
		}

		public Show[] GetAll()
		{
			sendRequest(new GetShowsRequest());
			Response response = readResponse();
			if (response is ErrorResponse)
			{
				ErrorResponse error = (ErrorResponse)response;
				throw new Error(error.Message);
			}
			GetShowsResponse resp = (GetShowsResponse)response;
			ShowDto[] showsDto = resp.Shows;
			return DtoUtils.getFromDto(showsDto);
		}


		public Artist[] GetArtists(string date)
		{
			sendRequest(new GetArtistsByDateRequest(date));
			Response response = readResponse();
			if (response is ErrorResponse)
			{
				ErrorResponse error = (ErrorResponse)response;
				throw new Error(error.Message);
			}
			GetArtistsByDateResponse resp = (GetArtistsByDateResponse)response;
			ArtistDto[] artistDtos = resp.Artists;
			return DtoUtils.getFromDto(artistDtos);
		}

		public void SellTickets(Ticket ticket)
		{
			TicketDto dto = DtoUtils.GetDto(ticket);
			sendRequest(new BuyTicketRequest(dto));
			Response response = readResponse();
			if (response is ErrorResponse)
			{
				ErrorResponse err = (ErrorResponse)response;
				throw new Error(err.Message);
			}
		}

		private void sendRequest(Request request)
		{
			try
			{
				formatter.Serialize(stream, request);
				stream.Flush();
			}
			catch (Exception e)
			{
				throw new Error("Error sending object " + e);
			}

		}

		private Response readResponse()
		{
			Response response = null;
			try
			{
				_waitHandle.WaitOne();
				lock (responses)
				{
					//Monitor.Wait(responses); 
					response = responses.Dequeue();
				}
			}
			catch (Exception e)
			{
				Console.WriteLine(e.StackTrace);
			}
			return response;
		}

		private void initializeConnection()
		{
			try
			{
				connection = new TcpClient(host, port);
				stream = connection.GetStream();
				formatter = new BinaryFormatter();
				finished = false;
				_waitHandle = new AutoResetEvent(false);
				startReader();
			}
			catch (Exception e)
			{
				Console.WriteLine(e.StackTrace);
			}
		}

		private void closeConnection()
		{
			finished = true;
			try
			{
				stream.Close();
				connection.Close();
				_waitHandle.Close();
				client = null;
			}
			catch (Exception e)
			{
				Console.WriteLine(e.StackTrace);
			}
		}

		private void startReader()
		{
			Thread tw = new Thread(run);
			tw.Start();
		}


		private void handleUpdate(UpdateResponse update)
		{
			if (update is BuyTicketResponse)
			{
				BuyTicketResponse response = (BuyTicketResponse)update;
				Ticket ticket = DtoUtils.GetFromDto(response.Ticket);
				Console.WriteLine("HANDLE UPDATE " + ticket);
				client.TicketSold(ticket);
			}
		}

		public virtual void run()
		{
			while (!finished)
			{
				try
				{
					object response = formatter.Deserialize(stream);
					Console.WriteLine("response received " + response);
					if (response is UpdateResponse)
					{
						handleUpdate((UpdateResponse)response);
					}
					else
					{
						lock (responses)
						{
							responses.Enqueue((Response)response);
						}
						_waitHandle.Set();
					}
				}
				catch (Exception e)
				{
					Console.WriteLine("Reading error " + e);
				}

			}
		}
	}
}
