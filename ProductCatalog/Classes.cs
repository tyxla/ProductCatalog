using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProductCatalog
{
    [Serializable]
    class Product
    {
        private string name;

        public string Name
        {
            get { return name; }
            set { name = value; }
        }
        private categories category;

        public categories Category
        {
            get { return category; }
            set { category = value; }
        }
        private string manufacturer;

        public string Manufacturer
        {
            get { return manufacturer; }
            set { manufacturer = value; }
        }
        private string description;

        public string Description
        {
            get { return description; }
            set { description = value; }
        }
        private double price;

        public double Price
        {
            get { return price; }
            set { price = value; }
        }
        private int quantity;

        public int Quantity
        {
            get { return quantity; }
            set { quantity = value; }
        }
        private double totalValue;

        public double TotalValue
        {
            get { return totalValue; }
            set { totalValue = value; }
        }
    }

    public enum categories
    {
        DVD,
        CD,
        VideoCards,
        Keyboards,
        Mice,
        Monitors,
        LAN,
        WLAN,
        RAM,
        Mainboards,
        CPU,
        Multimedia,
        Fan,
        SoundCards,
        Accessories,
        Other,
    }

    [Serializable]
    public class Manufacturer
    {
        private string name;

        public string Name
        {
            get { return name; }
            set { name = value; }
        }
        private string country;

        public string Country
        {
            get { return country; }
            set { country = value; }
        }
        private string phone;

        public string Phone
        {
            get { return phone; }
            set { phone = value; }
        }
        private string website;

        public string Website
        {
            get { return website; }
            set { website = value; }
        }

        private string email;

        public string Email
        {
            get { return email; }
            set { email = value; }
        }
    }
}
