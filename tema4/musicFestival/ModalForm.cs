using csharpMusicFestival.domain;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace csharpMusicFestival
{
    public partial class ModalForm : Form
    {

        public ModalForm(IList<Artist> artists)
        {
            InitializeComponent();
            foreach (var each in artists)
            {
                var index = dataGridView1.Rows.Add();
                dataGridView1.Rows[index].Cells["nameColumn"].Value = each.Name;
                dataGridView1.Rows[index].Cells["dateColumn"].Value = each.Date;
                dataGridView1.Rows[index].Cells["locationColumn"].Value = each.Location;
                dataGridView1.Rows[index].Cells["availableTicketsNumberColumn"].Value = each.AvailableTicketsNumber;
                if (each.AvailableTicketsNumber == 0)
                {
                    dataGridView1.Rows[index].DefaultCellStyle.BackColor = ColorTranslator.FromHtml("#A53A3B");
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Close();
            Dispose();
        }
    }
}
