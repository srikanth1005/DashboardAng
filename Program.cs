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
            List<string> storenames = new List<string>();
            storenames.Add("Root");
            storenames.Add("AuthRoot");
            storenames.Add("CA");

            string BaseFullTableConetent = string.Empty;
            foreach (var data in storenames)
            {
                //X509Store store = new X509Store(StoreName.AuthRoot, StoreLocation.CurrentUser);
                X509Store store = new X509Store(data, StoreLocation.CurrentUser);
                try
                {
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
                   
                    string result = generateHtmlTable(_certdetails,data.ToUpper());
                    if (!string.IsNullOrEmpty(result))
                    {
                        BaseFullTableConetent += result;
                    }
                }
                catch(Exception ex)
                {
                    store.Close();
                    var msg = ex.Message.ToString();
                }
            }
            if (!string.IsNullOrEmpty(BaseFullTableConetent))
            {
                bool mailresult = sendMail(BaseFullTableConetent);
            }

        }

        public static string generateHtmlTable(List<ceretdetails> _certdetails,string storeName)
        {
            string tablebody = string.Empty;
            string BaseTableConetent = string.Empty;
            int iterator = 1;
            foreach (var certs in _certdetails)
            {
                if ((Convert.ToDateTime(certs.ExpirationDate) - DateTime.Now.Date).TotalDays <= 30)
                {
                    tablebody +=  String.Format(@"<tr class={3}><td>{0}</td><td>{1}</td><td class={4}>{2}</td></tr>", certs.FriendlyName, certs.thumbPrintId, certs.ExpirationDate,(iterator%2==0)? @"""tr_even""": @"""tr_odd""",@"""td_last""");
                    iterator++;
                }
                
            }

            if (!string.IsNullOrEmpty(tablebody))
            {
                BaseTableConetent = string.Format(@"
                                      <div class=""divHeader"">
                                           Store Name - {0}
                                       </div>
                                      <table class=""fl-table"">
                                      <thead>
                                          <tr>
                                              <th width=""35%""> Certificate Name </th>
                                              <th width=""45%""> ThumbPrint Id </th>
                                              <th width=""20%"" class=""td_last""> Expire Date </th>
                                          </tr>
                                      </thead>
                                      <tbody>
                                      {1}
                                     </tbody>
                                  </table>", storeName ,tablebody);
            }
            return BaseTableConetent;
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
                                        border: 1px solid #1e80d5e3;
                                        border-collapse: collapse;
                                        width: 90%;
                                        max-width: 100%;
                                        white-space: nowrap;
                                        background-color: white;
                                        margin-left: 5% !important;
                                    }
                                
                                        .fl-table td, .fl-table th {
                                            text-align: center;
                                            padding: 8px;
                                        }
                                
                                        .fl-table td {
                                            border-right: 3px solid #f8f9fa;;
                                            font-size: 15px;
                                        }
                                
                                        .fl-table thead th {
                                            border-right: 3px solid #f8f9fa;
                                            color: #ffffff;
                                            background: #1e80d5e3;
                                        }
                                        
                                        table tr th:first-child {
                                            border-left: 0;
                                        }

                                        table tr th:last-child {
                                            border-right: 0;
                                        }
                                        .td_last {
                                            border-right: 1px solid #1e80d5e3 !important;
                                        }
                                        .tr_even {
                                            background: #d4dee7;
                                        }  
                               
                                        .divHeader {
                                          margin-top: 25px;
                                          font-size: 21px;
                                          margin-bottom: 10px;
                                          text-align: center;
                                          text-decoration: underline;
                                        }
                                        .table-wrapper {
                                            width: 90%;
                                            text-align: center;
                                            margin-left: 54px;
                                        }
 
                                        .Main {
                                            width: 100%;
                                            border: 1px solid #eaeaea; /*1px solid #004080;*/
                                            padding-bottom: 69px;
                                        }
                                        .GreetingDiv
                                        {
                                            margin-left: 5%;
                                            margin-top: 24px;
                                        }
                                        .header {
                                            height: 9vh !important;
                                            border-bottom: 1px solid;
                                            width: 90%;
                                            margin-left: 5%;
                                        }

                                        .header h1 {
                                            padding-top: 17px;
                                            margin-left: 78%;
                                        }
                                        .divRegards {
                                            border-bottom: 1px solid;
                                            padding-bottom: 3%;
                                        }
                                </style>
                              </style>
                              </head>
                              <body>
                                <div class=""Main"">
                                <div class=""header""><h1>NPC</h1></div>
                                <div class=""GreetingDiv"">
                                    <span>Dear XXXXXXX,</span>
                                    <br/>
                                    <span>Greetings from NPC support team.</span>
                                    <br />
                                    <span> The below certificates are going to expire withing 30 days.Please take a necessary action.</span>
                                    <br/>
                                </div>
                              <div class=""table-wrapper"">
                                " + TableBody+
                              "</div>"
                               +@"<div class=""GreetingDiv divRegards"">"
                                +"Regards,<br/>"
                                +"Team NPC"
                                +"</div>"
                                + "</div>"
                              + "</body>"
                              +"</html>";
                MailMessage message = new MailMessage();
                SmtpClient smtp = new SmtpClient();
                message.From = new MailAddress("Fromemail");
                message.To.Add(new MailAddress("Toemail"));
                message.Subject = "Test";
                message.IsBodyHtml = true; //to make message body as html  
                message.Body = MailBody;
                smtp.Port = 587;
                smtp.Host = "smtp.gmail.com"; //for gmail host  
                smtp.EnableSsl = true;
                smtp.UseDefaultCredentials = false;
                smtp.Credentials = new NetworkCredential("emailid", "pwd");
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
