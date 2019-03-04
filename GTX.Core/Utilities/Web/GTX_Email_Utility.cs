/*
 **************************************************************
 * Author: Irfansjah
 * Email: irfansjah@gmail.com
 * Created: 07/14/2018
 *
 * Permission is hereby granted, free of charge, to any person
 * obtaining a copy of this software and associated documentation
 * files (the "Software"), to deal in the Software without
 * restriction, including without limitation the rights to use,
 * copy, modify, merge, publish, distribute, sublicense, and/or sell
 * copies of the Software, and to permit persons to whom the
 * Software is furnished to do so, subject to the following
 * conditions:
 * 
 * The above copyright notice and this permission notice shall be
 * included in all copies or substantial portions of the Software.
 * 
 * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND,
 * EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES
 * OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND
 * NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT
 * HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY,
 * WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING
 * FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR
 * OTHER DEALINGS IN THE SOFTWARE.
 **************************************************************  
*/
using System.Net.Mail;
using System.Linq;
using System.Configuration;

namespace System.Web
{
    public static class GTX_Email_Utility
    {
        public class GTX_EmailAddress
        {
            public GTX_EmailAddress(string emailAddress, string friendlyName)
            {
                EmailAddress = emailAddress;
                FriendlyName = friendlyName;
            }
            public string EmailAddress { get; set; }
            public string FriendlyName { get; set; }

            public static implicit operator MailAddress(GTX_EmailAddress address)
            {
                if (string.IsNullOrEmpty(address.FriendlyName))
                    return new MailAddress(address.EmailAddress);
                else 
                    return new MailAddress(address.EmailAddress, address.FriendlyName);
            }
        }

        public enum MailBodyType
        {
            Text,
            Html
        }

        public static string SendEmail(
            GTX_EmailAddress mail_from,
            GTX_EmailAddress mail_to,
            MailBodyType bodyType,
            string subject,
            string mailbody,
            Collections.Generic.List<GTX_EmailAddress> bcc = null,
            Collections.Generic.List<GTX_EmailAddress> cc = null,
            Collections.Generic.List<string> attachment = null
        )
        {
            try
            {
                using (SmtpClient smtpClient = new SmtpClient(ConfigurationManager.AppSettings["MAIL_SERVER_HOST"], Convert.ToInt32(ConfigurationManager.AppSettings["MAIL_SERVER_PORT"])))
                {
                    MailAddress mfrom = mail_from;
                    MailAddress mto = mail_to;
                    MailMessage mail = new MailMessage(mfrom, mto)
                    {
                        Body = mailbody,
                        Subject = subject
                    };
                    if (bcc.IsNotNull())
                    {
                        if (bcc.Count > 0)
                        {
                            foreach (var item in bcc.Select(u => new MailAddress(u.EmailAddress, u.FriendlyName)).ToArray())
                            {
                                mail.Bcc.Add(item);
                            }
                        }
                    }
                    if (cc.IsNotNull())
                    {
                        if (cc.Count > 0)
                        {
                            foreach (var item in cc.Select(u => new MailAddress(u.EmailAddress, u.FriendlyName)).ToArray())
                            {
                                mail.CC.Add(item);
                            }
                        }
                    }
                    if (attachment.IsNotNull())
                    {
                        if (attachment.Count > 0)
                        {
                            foreach (var item in attachment)
                            {
                                mail.Attachments.Add(new Attachment(item));
                            }
                        }
                    }

                    mail.IsBodyHtml = bodyType.Equals(MailBodyType.Html);
                    try
                    {
                        smtpClient.Credentials = new Net.NetworkCredential(ConfigurationManager.AppSettings["MAIL_SERVER_CREDENTIAL_MAIL"], ConfigurationManager.AppSettings["MAIL_SERVER_CREDENTIAL_PWD"]);
                        smtpClient.Send(mail);

                        return "OK";
                    }
                    catch (Exception error)
                    {
                        return error.Message;
                    }
                }

            }
            catch (Exception error)
            {
                return error.Message;
            }
        }
    }
}
