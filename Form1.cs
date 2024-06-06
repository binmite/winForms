using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace винформы
{
    public struct Manufacturer
    {
        public int ManufacturerId;
        public string Name;
        public string Country;
    }
    public struct Seller
    {
        public int SellerId;
        public string Name;
        public string City;
    }
    public struct Availability
    {
        public int AvailabilityId;
        public int DeviceId;
        public int SellerId;
        public int Amount;
        public int Price;
    }
    public struct Device
    {
        public int DeviceId;
        public int ManufacturerId;
        public string Model;
        public string Type;
        public bool HdSupport;
        public string StorageCapacity;
        public string Proportions;
        public string CPU;
    }


    public partial class Form1 : Form
    {
        public DataTable dt;
        public DataView dv;
        public static List<Manufacturer> manufacturers = new List<Manufacturer>();
        public static List<Seller> sellers = new List<Seller>();
        public static List<Availability> availabilities = new List<Availability>();
        public static List<Device> devices = new List<Device>();
        public static List<string> filterConstarinsManufacturers = new List<string>() { "None", "Sony", "Microsoft", "Valve", "Nintendo" };
        static void getManufacturersData()
        {
            Manufacturer tempManufacturer = new Manufacturer();
            using (StreamReader sr = new StreamReader(@"manufacturer.txt"))
            {
                while (!sr.EndOfStream)
                {
                    string[] line = sr.ReadLine().Split('|').ToArray();
                    tempManufacturer.ManufacturerId = int.Parse(line[0]);
                    tempManufacturer.Name = line[1];
                    tempManufacturer.Country = line[2];
                    manufacturers.Add(tempManufacturer);
                }
            }
        }

        static void getSellersData()
        {
            Seller tempSeller = new Seller();
            using (StreamReader sr = new StreamReader(@"Seller.txt"))

            {
                while (!sr.EndOfStream)
                {
                    string[] line = sr.ReadLine().Split('|').ToArray();
                    tempSeller.SellerId = int.Parse(line[0]);
                    tempSeller.Name = line[1];
                    tempSeller.City = line[2];
                    sellers.Add(tempSeller);
                }
            }
        }

        static void getAvailabilitiesData()
        {
            Availability tempAvailability = new Availability();
            using (StreamReader sr = new StreamReader(@"Availability.txt"))

            {
                while (!sr.EndOfStream)
                {
                    string[] line = sr.ReadLine().Split('|').ToArray();
                    tempAvailability.AvailabilityId = int.Parse(line[0]);
                    tempAvailability.DeviceId = int.Parse(line[1]);
                    tempAvailability.SellerId = int.Parse(line[2]);
                    tempAvailability.Amount = int.Parse(line[3]);
                    tempAvailability.Price = int.Parse(line[4]);
                    availabilities.Add(tempAvailability);
                }
            }
        }

        static void getDevicesData()
        {
            Device tempDevice = new Device();
            using (StreamReader sr = new StreamReader(@"device.txt"))

            {
                while (!sr.EndOfStream)
                {
                    string[] line = sr.ReadLine().Split('|').ToArray();
                    tempDevice.DeviceId = int.Parse(line[0]);
                    tempDevice.ManufacturerId = int.Parse(line[1]);
                    tempDevice.Model = line[2];
                    tempDevice.Type = line[3];
                    tempDevice.HdSupport = bool.Parse(line[4]);
                    tempDevice.StorageCapacity = line[5];
                    tempDevice.CPU = line[6];
                    tempDevice.Proportions = line[7];
                    devices.Add(tempDevice);
                }
            }
        }

        static void getAllData()
        {
            getManufacturersData();
            getSellersData();
            getAvailabilitiesData();
            getDevicesData();
        }

        public Form1()
        {
            dataGridView1 = new DataGridView();
            getAllData();//чтение данных из файлов
            InitializeComponent();
            Controls.Add(dataGridView1);
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells;
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dt = new DataTable();
            dt.Columns.Add("Name");
            dt.Columns.Add("Manufacturer");
            dt.Columns.Add("Type");
            dt.Columns.Add("HdSupport");
            dt.Columns.Add("StorageCapacity");
            dt.Columns.Add("Proportions");//добавление колонок в таблицу
            fillDataTable(devices, manufacturers);//заполнение таблицы
            dv = new DataView(dt);
            dataGridView1.DataSource = dv;
            comboBox1.DataSource = filterConstarinsManufacturers;
        }

        public void fillDataTable(List<Device> devices, List<Manufacturer> manufacturers)
        {
            Manufacturer tempManufacturer;
            foreach (var device in devices)
            {
                tempManufacturer = manufacturers.Find(x => x.ManufacturerId == device.ManufacturerId);
                dt.Rows.Add(device.Model, tempManufacturer.Name, device.Type, device.HdSupport, device.StorageCapacity, device.CPU.ToString());
            }//добавлени естрок в таблицу
        }


        private void Form1_Load(object sender, EventArgs e)
        {

        }


        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            string data = dataGridView1.SelectedRows[0].Cells[0].Value.ToString();
            Device ClickedDevice = devices.Find(x => x.Model == dataGridView1.SelectedRows[0].Cells[0].Value.ToString());
            Form2 form2 = new Form2(sellers, ClickedDevice, availabilities);
            form2.Owner = this;
            form2.Show();

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedItem = comboBox1.SelectedItem.ToString();
            if (dv != null)
            {
                if (selectedItem != "None")
                {
                    dv.RowFilter = string.Format("[Manufacturer] Like '%{0}%'", selectedItem);
                    dataGridView1.DataSource = dv;
                }
                else
                {
                    dataGridView1.DataSource = dt;
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Данная курсовая работа является каталогом игровых консолей");
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            dataGridView1.DataSource = dt;
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {

            dv.RowFilter = string.Format("[Type] Like '%{0}%'", "телевизионная");
            dataGridView1.DataSource = dv;
        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            dv.RowFilter = string.Format("[Type] Like '%{0}%'", "портативная");
            dataGridView1.DataSource = dv;
        }
        private void radioButton4_CheckedChanged(object sender, EventArgs e)
        {
            dv.RowFilter = string.Format("[Type] Like '%{0}%'", "универсальная");
            dataGridView1.DataSource = dv;
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
