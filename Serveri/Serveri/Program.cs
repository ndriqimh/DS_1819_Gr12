using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.IO;
using System.Collections;
using System.Security.Cryptography;
using System.Xml.Linq;
using System.Xml;
using System.Security.Cryptography.Xml;

namespace Serveri
{
    class Program
    {
        public static Socket server;
        static void Main(string[] args)
        {


            int porti = 1234;
            IPEndPoint ip = new IPEndPoint(IPAddress.Any, porti);
            Socket soketiMireseardhes = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            soketiMireseardhes.Bind(ip);
            soketiMireseardhes.Listen(10);

            Console.WriteLine("=========================================================");
            Console.WriteLine("Universiteti i Prishtinës 'Hasan Prishtina', njësia FIEK");
            Console.WriteLine("       Projekt nga lënda 'Siguria e të dhënave'");
            Console.WriteLine("=======================================================");
            Console.WriteLine("    Serveri është i gatshëm të lidhet me një klient!");


            server = soketiMireseardhes.Accept();
            IPEndPoint clientep = (IPEndPoint)server.RemoteEndPoint;
            Console.WriteLine("    Serveri u lidh me hostin: {0} në portin {1}", clientep.Address, clientep.Port);
            Dergo("\n\n Jeni lidhur me serverin");



            string[] fjala = new string[6];
            for (int i = 0; i < 6; i++)
            {
                byte[] edhenaP = new byte[1024];
                int gjatesia = server.Receive(edhenaP);
                string eardhura = Encoding.UTF8.GetString(edhenaP, 0, gjatesia);
                fjala[i] = eardhura;          //DESDeshifrimi(eardhura);
                //Console.WriteLine(fjala[i]);
            }


            XmlDocument objXml = new XmlDocument();
            //nese nuk egziston db e krijon
            if (File.Exists("xmldb.xml") == false)
            {
                XmlTextWriter xmlTw = new XmlTextWriter("xmldb.xml", Encoding.UTF8);
                xmlTw.WriteStartElement("mesimdhenesit");
                xmlTw.Close();
            }



            // nese egziston e ben load
            objXml.Load("xmldb.xml");

            XmlElement rootNode = objXml.DocumentElement;
            // i krijon ne xmldb kto 
            XmlElement MesimidhenesiNode = objXml.CreateElement("Mesimdhenesi");
            XmlElement EmriMbiemriNode = objXml.CreateElement("EmriMbiemri");
            XmlElement TitulliNode = objXml.CreateElement("Titulli");
            XmlElement PagaNode = objXml.CreateElement("Paga");
            XmlElement EPostaNode = objXml.CreateElement("EPosta");
            XmlElement NofkaNode = objXml.CreateElement("Nofka");
            XmlElement HashFjalkalimiNode = objXml.CreateElement("HashFjalkalimi");

            XmlElement shiko = objXml.SelectSingleNode(@"/mesimdhenesit/Mesimdhenesi/Nofka") as XmlElement;

            if (fjala[0] == " " && shiko.InnerText == fjala[4])
            {
                Console.WriteLine("Perdouruesi ekziston.Jeni kyqur me sukses");
                NenshkruajFajllin();
                
            }
            else if (fjala[0] == " " && shiko.InnerText != fjala[4])
            {

                Dergo("Nuk jeni futur me sukses");
                Console.WriteLine("Nuk jeni futur me sukses");
            }

            else if (fjala[0] != " " && shiko.InnerText == fjala[4])
            {

                Dergo("Nuk jeni regjistruar me sukses");
                Console.WriteLine("Nuk jeni regjistruar me sukses");
            }

            else if (fjala[0] != " " && shiko.InnerText != fjala[4])
            {
                //Dergo("Jeni regjistruar me sukses");

                EmriMbiemriNode.InnerText = fjala[0];
                TitulliNode.InnerText = fjala[1];
                PagaNode.InnerText = fjala[2];
                EPostaNode.InnerText = fjala[3];
                NofkaNode.InnerText = fjala[4];
                HashFjalkalimiNode.InnerText = fjala[5];


                //Futja e krejt te dhenave me 1 tag <mesimdhenesi> ne db qe i perban ato
                MesimidhenesiNode.AppendChild(EmriMbiemriNode);
                MesimidhenesiNode.AppendChild(TitulliNode);
                MesimidhenesiNode.AppendChild(PagaNode);
                MesimidhenesiNode.AppendChild(EPostaNode);
                MesimidhenesiNode.AppendChild(NofkaNode);
                MesimidhenesiNode.AppendChild(HashFjalkalimiNode);


                // futja ne "rrenjen kryesore te db" e krejt te dhenave
                rootNode.AppendChild(MesimidhenesiNode);
                // Ruajtja ne databaze
                objXml.Save("xmldb.xml");
                Console.WriteLine("Jeni regjistruar me sukses");
                Dergo("Jeni regjistruar me sukses");


            }
            

        }

        
        

        private static string DESShifrimi(string tekstiShifrues)
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
            //return Encoding.UTF8.GetString(byteCiphertexti);

        }
        private static string DESDeshifrimi(string tekstiPerDeshifrim)
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


        public static void Dergo(string teksti)
        {
            string fjala = teksti; //DESShifrimi(teksti);
            byte[] edhenaD = new byte[1024];
            edhenaD = Encoding.UTF8.GetBytes(fjala);
            server.Send(edhenaD, edhenaD.Length, SocketFlags.None);
        }
        public static void NenshkruajFajllin()
        {
            // krijimi dhe nenshkrimi duhet te futet ne Server
            // shiko se duhet me exportu public key te Rsa me e qit si file
            //  e nuk mujta me gjet 
            XmlDocument objXml = new XmlDocument();
            RSACryptoServiceProvider objRsa = new RSACryptoServiceProvider();


            // merr db 
            objXml.Load("xmldb.xml");

            SignedXml objSignedXml = new SignedXml(objXml); ;

            Reference referenca = new Reference();
            referenca.Uri = "";

            XmlDsigEnvelopedSignatureTransform transform = new XmlDsigEnvelopedSignatureTransform();

            referenca.AddTransform(transform);
            objSignedXml.AddReference(referenca);

            KeyInfo ki = new KeyInfo();
            ki.AddClause(new RSAKeyValue(objRsa));

            objSignedXml.KeyInfo = ki;

            //exportimi i qelsit
            string strXmlParametrat = objRsa.ToXmlString(true); // true eshte per celes privat dhe publik 2t
            StreamWriter sw = new StreamWriter("celesiPublik.xml");
            sw.Write(strXmlParametrat);
            sw.Close();

            objSignedXml.SigningKey = objRsa;


            objSignedXml.ComputeSignature();


            XmlElement signatureNode = objSignedXml.GetXml();

            XmlElement rootNode = objXml.DocumentElement;
            rootNode.AppendChild(signatureNode);
            // e nenshkruan xmldb dhe e run ne 1 fallj tjeter
            objXml.Save("xmldb_nenshkruar.xml");
        }

    }
   
}