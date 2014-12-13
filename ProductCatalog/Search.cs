using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace ProductCatalog
{
    public partial class Search : Form
    {
        public Search(ArrayList products)
        {
            InitializeComponent();
            foreach (Product p in products)
            {
                //Добавяне на категориите и продуктите в dropdown списъците
                comboBox1.Items.Add(p.Category);
                comboBox2.Items.Add(p.Manufacturer);
            }
            //Избиране на нулевия индекс в dropdown списъците
            comboBox1.SelectedIndex = 0;
            comboBox2.SelectedIndex = 0;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
