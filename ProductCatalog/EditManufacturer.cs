﻿using System;
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
    public partial class EditManufacturer : Form
    {
        public EditManufacturer(ArrayList manufacturers)
        {
            InitializeComponent();
            bindingSource1.DataSource = manufacturers;
        }
    }
}
