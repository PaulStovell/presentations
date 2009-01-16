using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        DateTime d;
        public Form1()
        {
            InitializeComponent();

            // AddHandler(Me.Load, AddressOf Form1_Load)

            d = DateTime.Now;
            this.Load += new EventHandler(Form1_Load);
        }

        void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
