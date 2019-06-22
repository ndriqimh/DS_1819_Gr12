using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using System.Net.Sockets;
using System.IO;
using System.Threading;
using System.Text.RegularExpressions;
using System.IO;
using System.Security.Cryptography;
using System.Xml;
using System.Security.Cryptography.Xml;
using System.Xml.Linq;

namespace Klienti
{
    public partial class FormaUP : Form
    {


        public static Socket soketKlienti;

        public FormaUP()
        {
            InitializeComponent();
            Lidhja();
        }

        public void Lidhja()
        {

            String IPAdresa = "127.0.0.1";
            Int32 Porti = 1234;

            IPEndPoint ip = new IPEndPoint(IPAddress.Parse(IPAdresa), Porti);
            soketKlienti = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            try
            {
                soketKlienti.Connect(ip);
                txtPergjigjiaServeri.AppendText("Jeni lidhur në IP-në: " + IPAdresa + " dhe Portin: " + Porti + "\n");
                txtPergjigjiaServeri.AppendText("\n" + Merr());
            }
            catch (SocketException se)
            {
                txtPergjigjiaServeri.AppendText("Nuk jeni lidhur me serverin. Ju lutem provoni përsëri!");
                txtPergjigjiaServeri.AppendText("\n" + se.ToString());
                return;
            }

            


        }

        private void Dergo(string teksti)
        {
            try
            {
                soketKlienti.Send(Encoding.UTF8.GetBytes(teksti));
            }
            catch (SocketException se)
            {
                MessageBox.Show(se.Message.ToString());
            }
        }

        public string Merr()
        {
            byte[] data = new byte[1024];
            int receivedDataLength = soketKlienti.Receive(data);
            string stringEdhena = Encoding.UTF8.GetString(data, 0, receivedDataLength);
            return stringEdhena;
            
            
        }
        

        private void BtnRegjistrohu_Click(object sender, EventArgs e)
        {
            string txtEmriMbiemriShifruar = DESShifrimi(txtEmriMbiemri.Text);
            Dergo(txtEmriMbiemri.Text);
            string txtTitulliShifruar = DESShifrimi(txtTitulli.Text);
            Dergo(txtTitulli.Text);
            string txtPagaShifruar = DESShifrimi(txtPaga.Text);
            Dergo(txtPaga.Text);
            string txtEPostaShifruar = DESShifrimi(txtEPosta.Text);
            Dergo(txtEPosta.Text);
            string txtNofkaShifruar = DESShifrimi(txtNofka.Text);
            Dergo(txtNofka.Text);
            string txtFjalkalimiShifruar = DESShifrimi(txtFjalkalimi.Text);
            String salt = KrijoSalt(10);
            string hashFjalkalimi = GjeneroSHA256Hashin(txtFjalkalimi.Text, salt);
            
            Dergo(hashFjalkalimi);
        }
    

        private void BtnFutu_Click(object sender, EventArgs e)
        {
            string txtEmriMbiemriShifruar = DESShifrimi(txtEmriMbiemri.Text);
            txtEmriMbiemri.Text = " ";
            Dergo(txtEmriMbiemri.Text);
            string txtTitulliShifruar = DESShifrimi(txtTitulli.Text);
            txtTitulli.Text = " ";
            Dergo(txtTitulli.Text);
            string txtPagaShifruar = DESShifrimi(txtPaga.Text);
            txtPaga.Text = " ";
            Dergo(txtPaga.Text);
            string txtEPostaShifruar = DESShifrimi(txtEPosta.Text);
            txtEPosta.Text = " ";
            Dergo(txtEPosta.Text);
            string txtNofkaShifruar = DESShifrimi(txtNofka.Text);
            Dergo(txtNofkaHyrja.Text);
            string txtFjalkalimiShifruar = DESShifrimi(txtFjalkalimi.Text);
            String salt = KrijoSalt(10);
            string hashFjalkalimi = GjeneroSHA256Hashin(txtFjalkalimiHyrja.Text, salt);
            Dergo(hashFjalkalimi);
            
            XmlDocument objXml = new XmlDocument();
            RSACryptoServiceProvider objRsa = new RSACryptoServiceProvider();
            //importimi i qelsit publik per te verifikuar nenshkrimin
            
            StreamReader sr = new StreamReader(@"/Users/Ndriqim/Desktop/Detyra2_Edi/Serveri/Serveri/bin/Debug/celesiPublik.xml");
            string strXmlParametrat = sr.ReadToEnd();
            sr.Close();

            objRsa.FromXmlString(strXmlParametrat);

            objXml.Load(@"/Users/Ndriqim/Desktop/Detyra2_Edi/Serveri/Serveri/bin/Debug/xmldb_nenshkruar.xml");
            SignedXml objSignedXml = new SignedXml(objXml);

            XmlNodeList signatureNodes = objXml.GetElementsByTagName("Signature");
            XmlElement nenshkrimi = (XmlElement)signatureNodes[0];

            // e lexon
            objSignedXml.LoadXml(nenshkrimi);

            if (objSignedXml.CheckSignature() == true)
            {
                // ne vend te console.writeline e bon pergjigja.appentText per me te dal te ai textboxi
                txtPergjigjiaServeri.AppendText("Nenshkrimi valid!");
                // nese eshte valid duhet me i shfaq te dhenat qka ka ne xml file
                objXml.Load(@"/Users/Ndriqim/Desktop/Detyra2_Edi/Serveri/Serveri/bin/Debug/xmldb_nenshkruar.xml");
                //txtPergjigjiaServeri.AppendText(objXml.ToString());

            }
            else
            {
                txtPergjigjiaServeri.AppendText("Nenshkrimi jo valid!");
            }
            
        }

        public static String KrijoSalt(int madhesia)
        {

            var nrRastesishem = new System.Security.Cryptography.RNGCryptoServiceProvider();
            var baferi = new byte[madhesia];
            nrRastesishem.GetBytes(baferi);
            return Convert.ToBase64String(baferi);
        }
        public String GjeneroSHA256Hashin(String hyrja, String salt)
        {
            byte[] bytes = System.Text.Encoding.UTF8.GetBytes(hyrja + salt);
            System.Security.Cryptography.SHA256Managed sha256hashstring =
                new System.Security.Cryptography.SHA256Managed();
            byte[] hash = sha256hashstring.ComputeHash(bytes);

            return Convert.ToBase64String(hash);
        }

        private string DESShifrimi(string tekstiShifrues)
        {
            DESCryptoServiceProvider FjalaDES =
                new DESCryptoServiceProvider();
            FjalaDES.Key = Encoding.UTF8.GetBytes("12345678");
            FjalaDES.IV = Encoding.UTF8.GetBytes("12345678");
            FjalaDES.Padding = PaddingMode.Zeros;
            FjalaDES.Mode = CipherMode.CBC;

            byte[] bytePlaintext =
                Encoding.UTF8.GetBytes(tekstiShifrues);
            MemoryStream ms = new MemoryStream();

            CryptoStream cs = new CryptoStream(ms,
                                FjalaDES.CreateEncryptor(),
                                CryptoStreamMode.Write);
            cs.Write(bytePlaintext, 0, bytePlaintext.Length);
            cs.Close();

            byte[] byteCiphertexti = ms.ToArray();
            return Convert.ToBase64String(byteCiphertexti);
           // return Encoding.UTF8.GetString(byteCiphertexti);

        }

        private string DESDeshifrimi(string tekstiPerDeshifrim)
        {
            DESCryptoServiceProvider FjalaDES = new DESCryptoServiceProvider();
            FjalaDES.Key = Encoding.UTF8.GetBytes("12345678");
            FjalaDES.IV = Encoding.UTF8.GetBytes("12345678");
            FjalaDES.Padding = PaddingMode.Zeros;
            FjalaDES.Mode = CipherMode.CBC;

            byte[] byteCiphertexti = Convert.FromBase64String(tekstiPerDeshifrim);
            MemoryStream ms = new MemoryStream(byteCiphertexti);

            byte[] byteTextiDeshifruar = new byte[ms.Length];
            CryptoStream cs =
                new CryptoStream(ms,
                        FjalaDES.CreateDecryptor(),
                        CryptoStreamMode.Read);
            cs.Read(byteTextiDeshifruar, 0, byteTextiDeshifruar.Length);
            cs.Close();

            return Encoding.UTF8.GetString(byteTextiDeshifruar);
        }

       
    }
}
