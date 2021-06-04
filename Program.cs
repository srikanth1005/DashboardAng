using System;
using System.Security.Cryptography.X509Certificates;
using System.Collections.Generic;
using System.IO;
using System.Net.Mail;
using System.Net;

namespace CertificateCrypto
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            //X509Store store = new X509Store(StoreName.AuthRoot, StoreLocation.CurrentUser);
            X509Store store = new X509Store("AuthRoot", StoreLocation.CurrentUser);
            store.Open(OpenFlags.ReadOnly | OpenFlags.OpenExistingOnly);
            List<ceretdetails> _certdetails = new List<ceretdetails>();
            foreach (X509Certificate2 mCert in store.Certificates)
            {
                _certdetails.Add(new ceretdetails()
                {
                    FriendlyName = mCert.FriendlyName,
                    thumbPrintId = mCert.Thumbprint,
                    ExpirationDate = mCert.GetExpirationDateString()
                });
            }
            store.Close();
            //TextWriter tw = new StreamWriter("SavedList.txt");
            //TextWriter twEx = new StreamWriter("ExpiredTokenList.txt");
            string BaseTableConetent = null;
            foreach (var certs in _certdetails)
            {
                if ((Convert.ToDateTime(certs.ExpirationDate) - DateTime.Now.Date).TotalDays < 30)
                {
                    BaseTableConetent = BaseTableConetent+ String.Format("<tr><td>{0}</td><td>{1}</td><td>{2}</td></tr>", certs.FriendlyName, certs.thumbPrintId, certs.ExpirationDate);
                    //string myLine1 = $"Thumbprint ids : {certs.thumbPrintId} , Expire date {certs.ExpirationDate} , Friendly Name {certs.FriendlyName}";
                    //twEx.WriteLine(myLine1);
                }
                //else
                //{
                //    string myLine = $"Thumbprint ids : {certs.thumbPrintId} , Expire date {certs.ExpirationDate} , Friendly Name {certs.FriendlyName}";
                //    tw.WriteLine(myLine);
                //}
            }

            if (!string.IsNullOrEmpty(BaseTableConetent))
            {
                BaseTableConetent = "<tbody>" + BaseTableConetent + "</tbody>";
                var result = sendMail(BaseTableConetent);
            }
            //tw.Close();
            //twEx.Close();
        }

        public static bool sendMail(string TableBody)
        {
            try
            {
                var MailBody = @"<html>
                              <head>
                              <style>
                                  body {
                                      font-family: 'Times New Roman';
                                      -webkit-font-smoothing: antialiased;
                                  }
                                  .fl-table {
                                      border-radius: 5px;
                                      font-size: 16px;
                                      font-weight: normal;
                                      border: none;
                                      border-collapse: collapse;
                                      width: 100%;
                                      max-width: 100%;
                                      white-space: nowrap;
                                      background-color: white;
                                  }
                                 .fl-table td, .fl-table th {
                                     text-align: center;
                                     padding: 8px;
                                 }
                                 .fl-table td {
                                     border-right: 3px solid #f8f8f8;
                                     font-size: 15px;
                                 }
                                 .fl-table thead th {
                                     border-right: 3px solid #f8f8f8;
                                     color: #ffffff;
                                     background: #1e80d5e3;
                                 }
                                 .fl-table tr:nth-child(even) {
                                     background: #bebbbb !important;
                                 }
                                 .fl-table tr:nth-child(odd) {
                                     border-bottom: 3px solid #efefef;
                                 }
                              </style>
                              </head>
                              <body>
                              <div class=""table-wrapper"">
                                  <table class=""fl-table"">
                                      <thead>
                                          <tr>
                                              <th>Certificate Name</th>
                                              <th>ThumbPrint Id</th>
                                              <th>Expire Date</th>
                                          </tr>
                                      </thead>
                                      " + TableBody+
                                  "</table>"
                              +"</div>"
                              +"</body>"
                              +"</html>";
                MailMessage message = new MailMessage();
                SmtpClient smtp = new SmtpClient();
                message.From = new MailAddress("Fromemail");
                message.To.Add(new MailAddress("toemailid"));
                message.Subject = "Test";
                message.IsBodyHtml = true; //to make message body as html  
                message.Body = MailBody;
                smtp.Port = 587;
                smtp.Host = "smtp.gmail.com"; //for gmail host  
                smtp.EnableSsl = true;
                smtp.UseDefaultCredentials = false;
                smtp.Credentials = new NetworkCredential("emailid", "password");
                smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                smtp.Send(message);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }

    public class ceretdetails
    {
        public string FriendlyName { get; set; }
        public string thumbPrintId { get; set; }
        public string ExpirationDate { get; set; }
    }
}
