﻿using DTO;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using System.Net.Mail;
using System.Net;
using System.Text;
using Server.Models.UserAccount;
using Microsoft.Extensions.Options;
using static Server.Utility.Utility;

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
        public async Task SendEmailResetPasswordUserAccount(User user)
        {
            var token = await _userManager.GeneratePasswordResetTokenAsync(user);
            token = token.Replace('/', '-');
            if (!string.IsNullOrEmpty(token))
            {
                await SendEmailResetPassword(user, token);
            }
        }
        private async Task SendEmailResetPassword(User user, string token)
        {
            string appDomain = configuration.GetSection("ApplicationResetPassword:AppDomain").Value;
            string confirmationLink = configuration.GetSection("ApplicationResetPassword:DataConfirmation").Value;
            string LoginPath = configuration.GetSection("ApplicationResetPassword:ResetPasswordPath").Value;
            confirmationLink = string.Format(confirmationLink, EncryptGuid(Guid.Parse(user.Id)).Replace("/","-"), token);
            string Link = appDomain + LoginPath + confirmationLink;

            MailRequest mailRequest = new MailRequest
            {
                ToEmail = user.Email,
                Subject = "Reset Password Your account",
                PlaceHolders = new List<KeyValuePair<string, string>>()
                {
                    new KeyValuePair<string, string>("{{UserName}}", user.Email),
                    new KeyValuePair<string, string>("{{Link}}",
                        string.Format(appDomain + confirmationLink,user.Id, token))
                },
                Body = " <h3> Dawi.Dz </h3> " +
                               "Click here " + $"<a  href=\"{Link}\"> to reset your password</a>" + "<br/>"





            };
            await SendEmail(mailRequest);
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
                Body = " <h3> Dawi.dz </h3> " +
                               "Click on this link " + $"<a  href=\"{Link}\">to confirm the account</a>" + "<br/>"





            };
            await SendEmail(mailRequest);
        }

        private async Task SendEmail(MailRequest userOptions)
        {
            try
            {


                MailMessage mail = new MailMessage
                {
                    // To = userEmailOptions.ToEmail,


                    Subject = userOptions.Subject,
                    Body = userOptions.Body,
                    From = new MailAddress(mailSeetting.Value.Mail, mailSeetting.Value.DisplayName),
                    IsBodyHtml = true
                };
                mail.To.Add(userOptions.ToEmail);

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
