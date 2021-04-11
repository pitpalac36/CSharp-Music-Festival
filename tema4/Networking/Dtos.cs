using System;

namespace Networking
{
	[Serializable]
	public class UserDto
	{
		private string name;
		private string password;

		public UserDto(string name, string password)
		{
			this.name = name;
			this.password = password;
		}

		public virtual string Name
		{
			get
			{
				return name;
			}
			set
			{
				name = value;
			}
		}


		public virtual string Password
		{
			get
			{
				return password;
			}
		}
	}


	[Serializable]
	public class TicketDto
	{
		private int showId;
		private string purchaserName;
		private int number;

		public TicketDto(int showId, string purchaserName, int number)
		{
			this.showId = showId;
			this.purchaserName = purchaserName;
			this.number = number;
		}

		public virtual int ShowId
		{
			get
			{
				return showId;
			}
			set
			{
				showId = value;
			}
		}


		public virtual string PurchaserName
		{
			get
			{
				return purchaserName;
			}
			set
			{
				purchaserName = value;
			}
		}

		public virtual int Number
		{
			get
			{
				return number;
			}
			set
			{
				number = value;
			}
		}
	}


	[Serializable]
	public class ShowDto
	{
		private int id;
		private string artistName;
		private string date;
		private string location;
		private int availableTicketsNumber;
		private int soldTicketsNumber;

		public ShowDto(int id, string artistName, string date, string location, int availableTicketsNumber, int soldTicketsNumber)
		{
			this.id = id;
			this.artistName = artistName;
			this.date = date;
			this.location = location;
			this.availableTicketsNumber = availableTicketsNumber;
			this.soldTicketsNumber = soldTicketsNumber;
		}

		public virtual int Id
		{
			get
			{
				return id;
			}
			set
			{
				id = value;
			}
		}


		public virtual string ArtistName
		{
			get
			{
				return artistName;
			}
			set
			{
				artistName = value;
			}
		}

		public virtual string Date
		{
			get
			{
				return date;
			}
			set
			{
				date = value;
			}
		}

		public virtual string Location
		{
			get
			{
				return location;
			}
			set
			{
				location = value;
			}
		}

		public virtual int AvailableTicketsNumber
		{
			get
			{
				return availableTicketsNumber;
			}
			set
			{
				availableTicketsNumber = value;
			}
		}

		public virtual int SoldTicketsNumber
		{
			get
			{
				return soldTicketsNumber;
			}
			set
			{
				soldTicketsNumber = value;
			}
		}
	}


	[Serializable]
	public class ArtistDto
	{
		private string artistName;
		private string date;
		private string location;
		private int availableTicketsNumber;

		public ArtistDto(string artistName, string date, string location, int availableTicketsNumber)
		{
			this.artistName = artistName;
			this.date = date;
			this.location = location;
			this.availableTicketsNumber = availableTicketsNumber;
		}

		public virtual string ArtistName
		{
			get
			{
				return artistName;
			}
			set
			{
				artistName = value;
			}
		}

		public virtual string Date
		{
			get
			{
				return date;
			}
			set
			{
				date = value;
			}
		}

		public virtual string Location
		{
			get
			{
				return location;
			}
			set
			{
				location = value;
			}
		}

		public virtual int AvailableTicketsNumber
		{
			get
			{
				return availableTicketsNumber;
			}
			set
			{
				availableTicketsNumber = value;
			}
		}
	}
}