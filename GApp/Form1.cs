using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GApp
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string[] primeiraSilaba = { "Grey", "Gil", "Jil", "Jon", "Jose", "Ger", "Ged", "Jid", "Jud", "Jod", "Je", "Jeff" };
            string[] segundaSilaba = { "son", "berto", "nilson", "aldo", "ilson", "ielton", "iedson", "eir", "an", "erson" };

            string nomeDoDia = "";
            do {
                Random rnd = new Random();
                nomeDoDia = primeiraSilaba[rnd.Next(primeiraSilaba.Length - 1)] + segundaSilaba[rnd.Next(segundaSilaba.Length - 1)];
            } while (nomeDoDia == "Geraldo");

            SmtpClient client = new SmtpClient("smtp-mail.outlook.com");

            client.Port = 587;
            client.DeliveryMethod = SmtpDeliveryMethod.Network;
            client.UseDefaultCredentials = false;
            System.Net.NetworkCredential credentials = new NetworkCredential("vm.ribeiro@outlook.com", "vmr#civ@27");
            client.EnableSsl = true;
            client.Credentials = credentials;

            IList<String> emails = new List<String> {"matheusfransoni@hotmail.com", "erikabatistapaz@gmail.com",
                "vm.ribeiro@outlook.com",
                "math.cardoso97@outlook.com", "arthurkota@hotmail.com", "caue_mattoss@hotmail.com", "henriquebs98@gmail.com"
            };

            try
            {
                foreach (var email in emails)
                {
                    var mail = new MailMessage("vm.ribeiro@outlook.com".Trim(), email.Trim());
                    mail.Subject = "Nome Diário do Geraldo";
                    mail.IsBodyHtml = true;

                    Attachment inlineLogo = new Attachment(@"C:\Users\re034791\Downloads\GitHub\logo-vdp.png");
                    mail.Attachments.Add(inlineLogo);
                    string contentID = "Image";
                    inlineLogo.ContentId = contentID;

                    //To make the image display as inline and not as attachment

                    inlineLogo.ContentDisposition.Inline = true;
                    inlineLogo.ContentDisposition.DispositionType = DispositionTypeNames.Inline;

                    string message = "" +
                        "<head>" +
                        "<style>" +
                        "   .red{ color:red }" +
                        "</style>" +
                        "</head>" +
                        "<body>" +
                        "<div style='color: navy'>Prezados(as), Bom dia." +
                        "</br><br>  É com grande satisfação que venho no dia de hoje " +
                        DateTime.Now.ToShortDateString() + " às " + DateTime.Now.ToShortTimeString() +
                        " informar aos senhores(as) da nova nomeação de nosso nobre colega, <i>Geralt, the Unnamed.</i><br>" +
                        "<br>  Apresento-lhes o seu nome diário como: <b><i class='red'>" + nomeDoDia + "</i></b><br>" +
                        "<br>  É com deleite que me despeço, na esperança de cumprir esta valoroza missão no dia que se segue." +
                        "<br><br> Agradeço desde já a vossa atenção," +
                        "<br><br> Sistema do Geraldo. </div>" +
                        "<img src=\"cid:" + contentID + "\" alt='programador'>" +
                        "</body>";

                    mail.Body = message;
                    client.Send(mail);
                }
                MessageBox.Show("Enviado!");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw ex;
            }
        }
    }
}
