using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Button;

namespace винформы
{
    public partial class Form2 : Form
    {
        public DataTable dt;
        public DataView dv;
        public Form2(List<Seller> sellers, Device device, List<Availability>availabilities)
        {
            dataGridView1 = new DataGridView();
            List<Availability> thisDeviceAvailabilities = filterAvailabilities(availabilities,device);
            InitializeComponent();
            
            Controls.Add(dataGridView1);
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells;
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dt = new DataTable();
            dt.Columns.Add("Seller");
            dt.Columns.Add("City");
            dt.Columns.Add("Price");
            dt.Columns.Add("Amount");
           
            fillAvailabilityData( sellers, thisDeviceAvailabilities,dt);
            dv = new DataView(dt);
            dataGridView1.DataSource = dv;
        }
        
        public void fillAvailabilityData(List<Seller> sellers,List<Availability> availabilities,DataTable dt)
        {
            Seller tempSeller;
           
            foreach (Availability availability in availabilities)
            {

                tempSeller = sellers.Find(x=>x.SellerId == availability.SellerId);
                dt.Rows.Add(tempSeller.Name, tempSeller.City, availability.Amount, availability.Price);
            }
        }
        
        public List<Availability> filterAvailabilities(List<Availability> availabilities,Device device)
        {
            return availabilities.FindAll(x=>x.DeviceId==device.DeviceId);
        }

        private void Form2_Load(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            int price = int.Parse(dataGridView1.SelectedRows[0].Cells[2].Value.ToString());
            int amount = int.Parse(dataGridView1.SelectedRows[0].Cells[3].Value.ToString());
            Form3 form3 = new Form3(price, amount);
            form3.Owner = this;
            form3.Show();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            dv.RowFilter = string.Format("[City] Like '%{0}%'", "Минск");
            dataGridView1.DataSource = dv;
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            dv.RowFilter = string.Format("[City] Like '%{0}%'", "Могилёв");
            dataGridView1.DataSource = dv;
        }

        private void checkBox3_CheckedChanged_1(object sender, EventArgs e)
        {
            dv.RowFilter = string.Format("[City] Like '%{0}%'", "Витебск");
            dataGridView1.DataSource = dv;
        }
        
        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
        }
    }
}
