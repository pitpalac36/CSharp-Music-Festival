using musicFestival;
using Services;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using Model.domain;

namespace csharpMusicFestival
{
    public partial class MainForm : Form
    {
        private ClientController controller;

        public MainForm(ClientController controller)
        {
            InitializeComponent();
            this.controller = controller;
            customizeDatePicker();
            customizeTrackBar();
            initializeModel();
            controller.updateEvent += userUpdate;
        }


        public void userUpdate(object sender, UserEventArgs e)
        {
            if (e.UserEventType == UserEvent.TicketSold)
            {
                Ticket ticket = (Ticket)e.Data;
                Console.WriteLine("[MainForm] ticket sold " + ticket);
                dataGridView1.BeginInvoke(new UpdateGridCallback(this.updateGridView), new object[] { dataGridView1 });
            }
        }

        private void updateGridView(DataGridView list)
        {
            int index = 0;
            IList<Show> data = controller.GetShows();
            foreach (var each in data)
            {
                list.Rows[index].Cells["noAvailableTicketsColumn"].Value = each.AvailableTicketsNumber;
                list.Rows[index].Cells["noSoldTicketsColumn"].Value = each.SoldTicketsNumber;
                if (each.AvailableTicketsNumber == 0)
                {
                    list.Rows[index].DefaultCellStyle.BackColor = ColorTranslator.FromHtml("#A53A3B");
                }
                index++;
            }
        }

        public delegate void UpdateGridCallback(DataGridView list);

        private void customizeDatePicker()
        {
            dateTimePicker1.Format = DateTimePickerFormat.Custom;
            dateTimePicker1.CustomFormat = "yyyy-MM-dd";
        }

        private void customizeTrackBar()
        {
            trackBar1.Minimum = 1;
            trackBar1.Maximum = 30;
            trackBar1.TickFrequency = 1;
            trackBar1.TickStyle = TickStyle.BottomRight;
            label3.Text += "1";
        }

        private void initializeModel()
        {
            dataGridView1.Rows.Clear();
            foreach(var each in controller.GetShows())
            {
                var index = dataGridView1.Rows.Add();
                dataGridView1.Rows[index].Cells["idColumn"].Value = each.Id;
                dataGridView1.Rows[index].Cells["artistNameColumn"].Value = each.ArtistName;
                dataGridView1.Rows[index].Cells["dateColumn"].Value = each.Date;
                dataGridView1.Rows[index].Cells["locationColumn"].Value = each.Location;
                dataGridView1.Rows[index].Cells["noAvailableTicketsColumn"].Value = each.AvailableTicketsNumber;
                dataGridView1.Rows[index].Cells["noSoldTicketsColumn"].Value = each.SoldTicketsNumber;
                if (each.AvailableTicketsNumber == 0)
                {
                    dataGridView1.Rows[index].DefaultCellStyle.BackColor = ColorTranslator.FromHtml("#A53A3B");
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count != 1)
            {
                MessageBox.Show("Please select a show!");
                return;
            }
            else if (purchaserTextBox.Text.Trim() == "")
            {
                MessageBox.Show("Please introduce a name!");
            }
            else
            {
                try
                {
                    Ticket ticket = new Ticket((int)dataGridView1.SelectedRows[0].Cells["idColumn"].Value, purchaserTextBox.Text.Trim(), trackBar1.Value);
                    controller.SellTicket(ticket);
                    MessageBox.Show("The sale was successful!");
                    purchaserTextBox.Clear();
                    trackBar1.Value = 1;
                    dataGridView1.ClearSelection();
                }
                catch (Error)
                {
                    MessageBox.Show("Cannot buy tickets!!");
                    purchaserTextBox.Clear();
                    trackBar1.Value = 1;
                }
            }
        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            label3.Text = "Tickets: " + trackBar1.Value;
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                IList<Artist> artists = controller.GetArtists(dateTimePicker1.Value.ToString("yyyy-MM-dd"));
                Form modal = new ModalForm(artists);
                modal.ShowDialog(this);
                checkBox1.Checked = false;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            controller.Logout();
            controller.updateEvent -= userUpdate;
            Application.Exit();
        }
    }
}
