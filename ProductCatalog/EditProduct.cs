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
    public partial class EditProduct : Form
    {
        public EditProduct(ArrayList products, ArrayList manufacturers)
        {
            InitializeComponent();
            bindingSource1.DataSource = products;
        }
    }
}
