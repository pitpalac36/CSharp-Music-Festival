using csharpMusicFestival.domain;
using csharpMusicFestival.repository;
using csharpMusicFestival.service;
using csharpMusicFestival.validator;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace csharpMusicFestival
{
    public partial class MainForm : Form
    {
        private TicketingService service;

        public MainForm()
        {
            InitializeComponent();
            customizeDatePicker();
            customizeTrackBar();
            service = new TicketingService(new ShowDbRepository(), new TicketDbRepository());
            initializeModel();
        }

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
            foreach(var each in service.GetAll())
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

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

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
                    service.BuyTickets((int)dataGridView1.SelectedRows[0].Cells["idColumn"].Value, trackBar1.Value, purchaserTextBox.Text.Trim());
                    MessageBox.Show("The sale was successful!");
                    initializeModel();
                    purchaserTextBox.Clear();
                    trackBar1.Value = 1;
                    dataGridView1.ClearSelection();
                }
                catch (InvalidPurchaseException ex)
                {
                    MessageBox.Show(ex.getMessage());
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
                List<ShowDTO> artists = service.GetArtists(dateTimePicker1.Value.ToString("yyyy-MM-dd"));
                Form modal = new ModalForm(artists);
                modal.ShowDialog(this);
                checkBox1.Checked = false;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
            Application.Exit();
        }
    }
}
