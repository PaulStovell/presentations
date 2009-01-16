using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Example2.WindowsFormsDropDowns
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);


            // Get the data - we'd normally get this from a 
            // database
            CarModel holden = new CarModel("Holden Commodore");
            holden.AvailableParts.Add(new CarPart("Red Paint"));
            holden.AvailableParts.Add(new CarPart("Fluffy dice"));

            CarModel ford = new CarModel("Ford Falcon");
            ford.AvailableParts.Add(new CarPart("Decent Engine"));
            ford.AvailableParts.Add(new CarPart("Comfy seats"));

            List<CarModel> cars = new List<CarModel>();
            cars.Add(holden);
            cars.Add(ford);


            // Display it
            bindingSource1.DataSource = cars;

        }
    }
}