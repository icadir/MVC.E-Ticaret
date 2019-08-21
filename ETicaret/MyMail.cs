using System.Net.Mail;

namespace ETicaret
{
    public class MyMail
    {
        #region - Şifre -
        private const string password = "asdas";
        #endregion
        public string ToMail { get; private set; }
        public string Subject { get; private set; }
        public string Body { get; private set; }

        /// <inheritdoc />
        public MyMail(string _toMail, string _subject, string _body)
        {
            this.ToMail = _toMail;
            this.Subject = _subject;
            this.Body = _body;
        }

        public void SendMail()
        {
            var mail = new MailMessage()
            {
                From = new MailAddress("deneme@gmail.com", "İsmail ÇADIR")
            };
            mail.To.Add(this.ToMail);
            mail.Subject = this.Subject;
            mail.Body = this.Body;

            var client = new SmtpClient()
            {
                Port = 587,
                Host = "smtp.gmail.com",
                EnableSsl = true
            };

            client.Credentials = new System.Net.NetworkCredential("deneme@gmail.com", password);
            client.Send(mail);
        }

    }
}