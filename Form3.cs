using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace винформы
{
    public partial class Form3 : Form
    {
        public int Price;
        public Form3(int price,int amount)
        {
            InitializeComponent();
            Price=price;
        }

        private void Form3_Load(object sender, EventArgs e)
        {
           
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int amount;
            if(textBox1.Text!=null&& textBox2.Text!=null && textBox3.Text!=null && int.TryParse(textBox3.Text, out amount) && amount>0)
            {
                MessageBox.Show("Заказ успешно оформлен! Сумма к к оплате: "+ amount * Price);
            }
            else
            {
                MessageBox.Show("ОШИБКА ОФОРМЛЕНИЯ ЗАКАЗА:Некоторые данные указаны неверно либо не указаны");
            }
        }
    }
}
