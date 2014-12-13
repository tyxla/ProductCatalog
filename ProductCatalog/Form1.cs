using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace ProductCatalog
{
    public partial class Form1 : Form
    {
        //Имена на файловете
        public const string productsFile = "products.dat";
        public const string manufacturersFile = "manufacturers.dat";
        FileStream fs;
        BinaryFormatter bf = new BinaryFormatter();

        //Глобални масиви с продукти и производители
        public ArrayList products = new ArrayList();
        public ArrayList manufacturers = new ArrayList();

        //Сортираща функция
        public void sortProducts(string order, string criteria)
        {
            ArrayList temp = new ArrayList();
            ArrayList tempProducts = new ArrayList();
            foreach (Product p in products)
            {
                //Критерий за сортиране
                switch (criteria)
                {
                    case "Name":
                        temp.Add(p.Name);
                    break;
                    case "Category":
                        temp.Add(p.Category.ToString());
                    break;
                    case "Manufacturer":
                        temp.Add(p.Manufacturer);
                    break;
                    case "Price":
                        temp.Add(p.Price);
                    break;
                    case "Quantity":
                        temp.Add(p.Quantity);
                    break;
                }
            }
            
            temp.Sort(); //Същинско сортиране

            //Пълнене на временен масив със сортирани обекти
            for(int i = 0; i < temp.Count; i++) 
            {

                foreach (Product p in products)
                {
                    if (criteria == "Name")
                    {
                        if (p.Name == (string)temp[i])
                        {
                            if (order == "DESC")
                            {
                                tempProducts.Add(p);
                            }
                            else
                            {
                                tempProducts.Insert(0, p);
                            }
                        }
                    }
                    else if (criteria == "Category")
                    {
                        if (p.Category.ToString() == (string)temp[i])
                        {
                            if (order == "DESC")
                            {
                                tempProducts.Add(p);
                            }
                            else
                            {
                                tempProducts.Insert(0, p);
                            }
                        }
                    }
                    else if (criteria == "Manufacturer")
                    {
                        if (p.Manufacturer == (string)temp[i])
                        {
                            if (order == "DESC")
                            {
                                tempProducts.Add(p);
                            }
                            else
                            {
                                tempProducts.Insert(0, p);
                            }
                        }
                    }
                    else if (criteria == "Price")
                    {
                        if (p.Price == double.Parse(temp[i].ToString()))
                        {
                            if (order == "DESC")
                            {
                                tempProducts.Add(p);
                            }
                            else
                            {
                                tempProducts.Insert(0, p);
                            }
                        }
                    }
                    else if (criteria == "Quantity")
                    {
                        if (p.Quantity == int.Parse(temp[i].ToString()))
                        {
                            if (order == "DESC")
                            {
                                tempProducts.Add(p);
                            }
                            else
                            {
                                tempProducts.Insert(0, p);
                            }
                        }
                    } 
                }
            }
            dataGridView1.DataSource = tempProducts;
        }

        //Функция, изчисляваща средната цена на продукт
        public string calculateAveragePrice()
        {
            double total_price = 0;
            foreach (Product p in products)
            {
                total_price += p.Price;
            }
            return string.Format("{0:0.00}", (total_price / products.Count));
        }

        //Функция, извеждаща статистика за продуктите и производителите
        public void calculateStatistics()
        {
            label6.Text = products.Count.ToString();
            label7.Text = manufacturers.Count.ToString();
            double subtotal = 0;
            foreach (Product p in products)
            {
                subtotal += p.TotalValue;
            }
            label8.Text = string.Format("{0:0.00}",subtotal);
            label9.Text = calculateAveragePrice();
        }

        //Функция, добавяща по 2 примерни продукта и производители
        public void addInitialData()
        {
            Manufacturer m = new Manufacturer();
            m.Country = "Япония";
            m.Email = "office@sony.net";
            m.Name = "Sony";
            m.Phone = "81-3-6748-2111";
            m.Website = "http://sony.net/";
            manufacturers.Add(m);

            Manufacturer n = new Manufacturer();
            n.Country = "Япония";
            n.Email = "office@panasonic.com";
            n.Name = "Panasonic";
            n.Phone = "88-1-1234-8845";
            n.Website = "http://panasonic.com/";
            manufacturers.Add(n);

            Product p = new Product();
            p.Category = categories.Other;
            p.Description = "2.2GHz, 4GB RAM, 500GB HD, 512MB GFX";
            p.Manufacturer = m.Name;
            p.Name = "VAIO 1500";
            p.Price = 1200.50;
            p.Quantity = 3;
            p.TotalValue = p.Price * p.Quantity;
            products.Add(p);

            Product q = new Product();
            q.Category = categories.Monitors;
            q.Manufacturer = n.Name;
            q.Name = "GSCH7560";
            q.Price = 138.00;
            q.Quantity = 7;
            q.TotalValue = q.Price * q.Quantity;
            products.Add(q);
        }

        //Конструктор на главната форма
        public Form1()
        {
            InitializeComponent();

            addInitialData();

            //Избор на източник на данни за таблиците
            dataGridView1.DataSource = products;
            dataGridView2.DataSource = manufacturers;

            calculateStatistics();

        }

        //Главно меню -> Изход
        private void изходToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        //Главно меню -> Добавяне/редактиране/изтриване -> Продукт
        private void добавянеНаПродуктToolStripMenuItem_Click(object sender, EventArgs e)
        {          
            ArrayList tempProducts = new ArrayList();
            ArrayList tempManufacturers = new ArrayList();
            tempProducts.AddRange(products);
            tempManufacturers.AddRange(manufacturers);
            EditProduct addProductForm = new EditProduct(tempProducts, tempManufacturers);
            
            //Свързване на производителите с продуктите
            foreach (Manufacturer m in manufacturers)
            {
                addProductForm.comboBox2.Items.Add(m.Name);
            }
            addProductForm.ShowDialog();
            
            products.Clear();
            ArrayList tempProducts2 = new ArrayList();
            //Проверка за некоректно въведени продукти
            foreach (Product p in tempProducts)
            {
                if (!(p.Name == null || p.Price == 0))
                {
                    p.TotalValue = p.Price * p.Quantity;
                    tempProducts2.Add(p);
                }
            }
            products.InsertRange(0, tempProducts2);
            dataGridView1.DataSource = tempProducts2;
            calculateStatistics();
        }
        //Главно меню -> Добавяне/редактиране/изтриване -> производител
        private void добавянеНаПроизводителToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ArrayList tempManufacturers = new ArrayList();
            tempManufacturers.AddRange(manufacturers);
            EditManufacturer addManufacturerForm = new EditManufacturer(tempManufacturers);
            addManufacturerForm.ShowDialog();

            ArrayList tempManufacturers2 = new ArrayList();

            //Проверка за некоректно въведени производители
            foreach (Manufacturer m in tempManufacturers)
            {
                if(m.Name != null) {
                    tempManufacturers2.Add(m);
                }
            }

            manufacturers.Clear();
            manufacturers.InsertRange(0, tempManufacturers2);
            dataGridView2.DataSource = tempManufacturers2;
            calculateStatistics();
        }

        //Главно меню -> Сортиране -> По име -> В низходящ ред
        private void вНизходящРедToolStripMenuItem_Click(object sender, EventArgs e)
        {
            sortProducts("ASC", "Name");
        }

        //Главно меню -> Сортиране -> По име -> Във възходящ ред
        private void въвВъзходящРедToolStripMenuItem_Click(object sender, EventArgs e)
        {
            sortProducts("DESC", "Name");
        }

        //Главно меню -> Сортиране -> По категория -> В низходящ ред
        private void вНизходящРедToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            sortProducts("ASC", "Category");
        }

        //Главно меню -> Сортиране -> По категория -> Във възходящ ред
        private void въвВъзходящРедToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            sortProducts("DESC", "Category");
        }

        //Главно меню -> Сортиране -> По производител -> В низходящ ред
        private void вНизходящРедToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            sortProducts("ASC", "Manufacturer");
        }

        //Главно меню -> Сортиране -> По производител -> Във възходящ ред
        private void въвВъзходящРедToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            sortProducts("DESC", "Manufacturer");
        }

        //Главно меню -> Сортиране -> По цена -> В низходящ ред
        private void вНизходящРедToolStripMenuItem3_Click(object sender, EventArgs e)
        {
            sortProducts("ASC", "Price");
        }

        //Главно меню -> Сортиране -> По цена -> Във възходящ ред
        private void въвВъзходящРедToolStripMenuItem3_Click(object sender, EventArgs e)
        {
            sortProducts("DESC", "Price");
        }

        //Главно меню -> Сортиране -> По количество -> В низходящ ред
        private void вНизходящРедToolStripMenuItem4_Click(object sender, EventArgs e)
        {
            sortProducts("ASC", "Quantity");
        }

        //Главно меню -> Сортиране -> По количество -> Във възходящ ред
        private void въвВъзходящРедToolStripMenuItem4_Click(object sender, EventArgs e)
        {
            sortProducts("DESC", "Quantity");
        }

        //Главно меню -> Търсене на продукти
        private void търсенеToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Search searchForm = new Search(products);
            searchForm.ShowDialog();

            ArrayList tempProducts = new ArrayList();
            //Обхождане и проверка за изпълнени критерии
            foreach (Product p in products)
            {
                if (searchForm.textBox1.Text.Trim() == p.Name)
                {
                    if (searchForm.comboBox1.SelectedItem.ToString() == p.Category.ToString())
                    {
                        if (searchForm.comboBox2.SelectedItem.ToString() == p.Manufacturer.ToString())
                        {
                            //Добавяне на продуктите, изпълняващи условията
                            tempProducts.Add(p);
                        }
                    }
                }
            }
            if (tempProducts.Count >= 1)
            {
                dataGridView1.DataSource = tempProducts;
            }

        }

        //Филтриране по цена
        private void filterButton_Click(object sender, EventArgs e)
        {
            ArrayList tempProducts = new ArrayList();
            ArrayList sourceProducts = new ArrayList();
            sourceProducts = (ArrayList)dataGridView1.DataSource;
            //Проверка за валидни входни данни
            if ((filterMin.Text == "" || filterMax.Text == "") || (double.Parse(filterMin.Text) > double.Parse(filterMax.Text)))
            {
                MessageBox.Show("Моля, въведете коректни ценови граници за филтриране!");
            }
            else
            {
                //Обхождане и филтриране на продуктите чрез подадения критерии
                foreach (Product p in sourceProducts)
                {
                    if (double.Parse(filterMin.Text) <= p.Price && double.Parse(filterMax.Text) >= p.Price)
                    {
                        tempProducts.Add(p);
                    }
                }
                dataGridView1.DataSource = tempProducts;
            }
        }

        //Изчистване на филтъра/резултатите от търсенето
        private void filterClear_Click(object sender, EventArgs e)
        {
            dataGridView1.DataSource = products;
        }

        //Филтриране по количество
        private void button2_Click(object sender, EventArgs e)
        {
            ArrayList tempProducts = new ArrayList();
            ArrayList sourceProducts = new ArrayList();
            sourceProducts = (ArrayList)dataGridView1.DataSource;
            //Точно количество
            if (quantExact.Checked == true)
            {
                //Обхождане и филтриране на продуктите по зададения критерии
                foreach (Product p in sourceProducts)
                {
                    if (p.Quantity == quantExactField.Value)
                    {
                        tempProducts.Add(p);
                    }
                }
                dataGridView1.DataSource = tempProducts;
            }
            //Количествени граници
            else
            {
                //Проверка за некоректно въведени входни данни
                if ((quantMin.Text == "" || quantMax.Text == "") || (int.Parse(quantMin.Text) > int.Parse(quantMax.Text)))
                {
                    MessageBox.Show("Моля, въведете коректни ценови граници за филтриране!");
                }
                else
                {
                    //Обхождане и филтриране на продуктите по зададения критерии
                    foreach (Product p in sourceProducts)
                    {
                        if (p.Quantity >= int.Parse(quantMin.Text) && p.Quantity <= int.Parse(quantMax.Text))
                        {
                            tempProducts.Add(p);
                        }
                    }
                    dataGridView1.DataSource = tempProducts;
                }
            }
        }
        //Главно меню -> Помощ -> За програмата
        private void заToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AboutProgram aboutP = new AboutProgram();
            aboutP.ShowDialog();
        }

        //Главно меню -> Помощ -> За създателите
        private void заСъздателитеToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AboutCreators aboutC = new AboutCreators();
            aboutC.ShowDialog();
        }

        //Главно меню -> Файл -> Запазване
        private void запазванеToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                //Запазване на данните за продуктите и производителите в съответните файлове
                fs = new FileStream(productsFile, FileMode.Create);
                bf.Serialize(fs, products);
                fs = new FileStream(manufacturersFile, FileMode.Create);
                bf.Serialize(fs, manufacturers);
            }
            catch{
                MessageBox.Show("Грешка при запис!");
            }
            finally
            {
                fs.Close();
            }
        }

        private void отварянеToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                //Отваряне на файла с продуктите
                if (File.Exists(productsFile))
                {
                    fs = new FileStream(productsFile, FileMode.Open);
                    products = (ArrayList)bf.Deserialize(fs);
                    fs.Close();     
                    dataGridView1.DataSource = products;
                }

                //Отваряне на файла с производителите
                if (File.Exists(manufacturersFile))
                {
                    fs = new FileStream(manufacturersFile, FileMode.Open);
                    manufacturers = (ArrayList)bf.Deserialize(fs);
                    fs.Close();
                    dataGridView2.DataSource = manufacturers;
                } 
            }
            catch {
                MessageBox.Show("Грешка при отваряне!");
            }
        }
    }
}
