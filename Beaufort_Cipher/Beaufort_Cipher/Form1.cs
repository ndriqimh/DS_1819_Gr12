using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Beaufort_Cipher
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            Beaufort.Alfabeti = "abcdefghijklmnopqrstuvwxyz";
        }
       


        private void EncodeButton_Click(object sender, EventArgs e)
        {
            string hyrja = pregadit(txtPlaintexti.Text);
            string celesi = pregadit(txtCelesi.Text);

            txtCiphertexti.Text = formatiOrigjinal(
                txtPlaintexti.Text,
                Beaufort.Encode(hyrja, celesi, false)
            );
        }

        private void DecodeButton_Click(object sender, EventArgs e)
        {
            string dalja = pregadit(txtCiphertexti.Text);
            string celesi = pregadit(txtCelesi.Text);

            txtPlaintexti.Text = formatiOrigjinal(
                txtCiphertexti.Text,
                Beaufort.Encode(dalja, celesi, true)
            );
        }
    }
}
