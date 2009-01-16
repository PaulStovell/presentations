using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Windows.Forms.Integration;

namespace SampleApplication
{
    public partial class WindowsForm : Form
    {
        public WindowsForm()
        {
            InitializeComponent();

            System.Windows.Controls.Button wpfButton = new System.Windows.Controls.Button();
            System.Windows.Controls.TextBox wpfTextBox = new System.Windows.Controls.TextBox();
            wpfButton.Resources.MergedDictionaries.Add(System.Windows.Application.Current.Resources);
            wpfButton.Content = wpfTextBox;
            
            ElementHost elementHost = new ElementHost();
            elementHost.Child = wpfButton;
            panel1.Controls.Add(elementHost);
        }
    }
}