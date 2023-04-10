using DTO;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using System.Net.Mail;
using System.Net;
using System.Text;
using Server.Models.UserAccount;
using Microsoft.Extensions.Options;

namespace Server.Services.Foundation.MailService
{
    public class MailService : IMailService
    {
        public readonly UserManager<User> _userManager;
        public readonly IConfiguration configuration;
        private IOptions<MailSettings> mailSeetting { get; set; }

        public MailService(UserManager<User> _userManager, IConfiguration configuration, IOptions<MailSettings> mailSeetting)
        {
            this.configuration = configuration;
            this._userManager = _userManager;
            this.mailSeetting = mailSeetting;

        }




        public async Task<MessageResultDto> SendValidationMailToClient(User user)
        {
            var message = $"Verify your account {user.Email} " + "we send Link Validation Account";
            await GenerateTokenValidationEmailAsync(user);
            //return user.UserToReponseRegistre(message);
            return new MessageResultDto
            {
                EmailAdress = user.Email,
                Message = message
            };

        }
        private async Task GenerateTokenValidationEmailAsync(User user)
        {
            var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            token = token.Replace('/', '-');
            if (!string.IsNullOrEmpty(token))
            {
                await SendEmailConfirmationEmail(user, token);
            }
        }
        private async Task SendEmailConfirmationEmail(User user, string token)
        {
            string appDomain = configuration.GetSection("Application:AppDomain").Value;
            string confirmationLink = configuration.GetSection("Application:EmailConfirmation").Value;
            string LoginPath = configuration.GetSection("Application:LoginPath").Value;
            confirmationLink = string.Format(confirmationLink, user.Id, token);
            string Link = appDomain + LoginPath + confirmationLink;

            MailRequest mailRequest = new MailRequest
            {
                ToEmail = user.Email,
                Subject = "Authentication of an account",
                PlaceHolders = new List<KeyValuePair<string, string>>()
                {
                    new KeyValuePair<string, string>("{{UserName}}", user.Email),
                    new KeyValuePair<string, string>("{{Link}}",
                        string.Format(appDomain + confirmationLink, user.Id, token))
                },
                Body = " <h3> Data Finder </h3> " +
                               "Cliquez sur le lien pour " + $"<a  href=\"{Link}\">confirmer votre inscription</a>" + "<br/>"





            };
            await SendEmail(mailRequest);

            // await SendEmail(mailRequest);
        }

        private async Task SendEmail(MailRequest userEmailOptions)
        {
            try
            {


                MailMessage mail = new MailMessage
                {
                    // To = userEmailOptions.ToEmail,


                    Subject = userEmailOptions.Subject,
                    Body = userEmailOptions.Body,
                    From = new MailAddress(mailSeetting.Value.Mail, mailSeetting.Value.DisplayName),
                    IsBodyHtml = true
                };
                mail.To.Add(userEmailOptions.ToEmail);

                NetworkCredential networkCredential = new NetworkCredential(mailSeetting.Value.Mail, mailSeetting.Value.Password);

                SmtpClient smtpClient = new SmtpClient
                {

                    Host = mailSeetting.Value.Host,
                    Port = mailSeetting.Value.Port,
                    EnableSsl = true,
                    UseDefaultCredentials = false,
                    Credentials = networkCredential,





                };

                mail.BodyEncoding = Encoding.Default;

                await smtpClient.SendMailAsync(mail);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task SendEmailNotification(MailRequest mailRequest)
        {
            try
            {
                await SendEmail(mailRequest);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

        }
    }
}
