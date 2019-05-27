using System;
using System.Net;
using System.Net.Mail;
using Microsoft.Win32;
using Prism.Mvvm;

namespace EmailTest.Models
{
    public class EmailSending:BindableBase
    {
//        private string Email { get; }
//        private string Password { get; }
//        private int Port { get; }
        public string Result { get; set; }
        public string FilePath { get; set; }
        public string SendResult { get; set; }
        public bool CheckConnection(string smtpServer, string email, string password, string port)
        {
            try
            {
                var client = new SmtpClient(smtpServer,int.Parse(port));
                client.Credentials = new NetworkCredential(email,password);
                client.EnableSsl = true;
                client.SendMailAsync(new MailMessage(email, email));
                Result = Properties.Resources.PositiveResult;
                RaisePropertyChanged("ConnectionResult");
                return true;
            }
            catch (Exception)
            {
                Result = Properties.Resources.NegativeResult;
                RaisePropertyChanged("ConnectionResult");
                return false;
            }
        }
        public bool SendEmail(string smtpServer, string email,string password,string port, string to, string message, string attachmentPath)
        {
            try
            {
                var myEmail = new MailAddress(email);
                var recipient = new MailAddress(to);
                var emailMessage = new MailMessage(myEmail,recipient);
                if(!string.IsNullOrEmpty(attachmentPath))
                emailMessage.Attachments.Add(new Attachment(attachmentPath));
                emailMessage.Body = message;
                emailMessage.Subject = "test";
                var client = new SmtpClient(smtpServer, int.Parse(port));
                client.Credentials = new NetworkCredential(email, password);
                client.EnableSsl = true;
                try
                {
                    client.SendMailAsync(emailMessage).GetAwaiter().OnCompleted(delegate
                    {
                        SendResult = Properties.Resources.SendPositiveResult;
                        RaisePropertyChanged("SendResult");
                    });
                    SendResult = Properties.Resources.Waiting;
                    RaisePropertyChanged("SendResult");
                    return true;
                }
                catch (SmtpException)
                {
                    SendResult = Properties.Resources.SendNegativeResult;
                    RaisePropertyChanged("SendResult");
                    return false;
                }
            }
            catch (Exception)
            {
                SendResult = Properties.Resources.SendNegativeResult;
                RaisePropertyChanged("SendResult");
                return false;
            }
        }

        public void ChooseFile()
        {
            try
            {
                OpenFileDialog OPF = new OpenFileDialog();
                OPF.ShowDialog();
                FilePath = OPF.FileName;
                RaisePropertyChanged("FilePath");
            }
            catch (Exception)
            {

            }
        }
    }
}
