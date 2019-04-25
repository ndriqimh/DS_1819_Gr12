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
        /*
        private string pregadit(string str)
        {
            return new Regex(@"[\W\d]").Replace(str, String.Empty).ToLower();
        }
        private string formatiOrigjinal(string origjinal, string enkodim)
        {
            string shkrVogel = origjinal.ToLower(); // string lowerCase

            for (int i = 0; i < origjinal.Length; i++)
            {
                if (Beaufort.Alfabeti.IndexOf(shkrVogel[i]) != -1)
                {
                    string newLetter = (shkrVogel[i] == origjinal[i]) ?
                        enkodim[0].ToString() :
                        enkodim[0].ToString().ToUpper();

                    origjinal = origjinal.Remove(i, 1).Insert(i, newLetter);
                    enkodim = enkodim.Remove(0, 1);
                }
            }

            return origjinal;
        }
        */


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
