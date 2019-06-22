using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text;

namespace Klienti
{
    static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new FormaUP());
        }
    }
    /*
    public class Mesimdhenesi
    {
        public string Nofka { get; set; }
        public string HashFjalkalimi { get; set; }
        public string EmriMbiemri { get; set; }
        public string Titulli { get; set; }
        public double Paga { get; set; }
        public string EPosta { get; set; }
    }
    */
}
