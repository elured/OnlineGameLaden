﻿using RubiksCubeStore.Domain.Abstract;
using RubiksCubeStore.Domain.Entities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace RubiksCubeStore.Domain.Concrete
{
public class EmailSettings
    {
        public string MailToAddress = "orders@example.com";
        public string MailFromAddress = "cubetore@example.com";
        public bool UseSsl = true;
        public string Username = "MySmtpUsername";
        public string Password = "MySmtpPassword";
        public string ServerName = "smtp.example.com";
        public int ServerPort = 587;
        public bool WriteAsFile = true;
        public string FileLocation = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + @"\EmailTests\";
    }

    public class EmailOrderProcessor : IOrderProcessor
    {
        private EmailSettings emailSettings;

        public EmailOrderProcessor(EmailSettings settings)
        {
            emailSettings = settings;
        }

        public void ProcessOrder(Cart cart, ShippingDetails shippingInfo)
        {
            using (var smtpClient = new SmtpClient())
            {
                smtpClient.EnableSsl = emailSettings.UseSsl;
                smtpClient.Host = emailSettings.ServerName;
                smtpClient.Port = emailSettings.ServerPort;
                smtpClient.UseDefaultCredentials = false;
                smtpClient.Credentials
                    = new NetworkCredential(emailSettings.Username, emailSettings.Password);

                if (emailSettings.WriteAsFile)
                {
                    smtpClient.DeliveryMethod
                        = SmtpDeliveryMethod.SpecifiedPickupDirectory;
                    smtpClient.PickupDirectoryLocation = emailSettings.FileLocation;
                    smtpClient.EnableSsl = false;
                }

                StringBuilder body = new StringBuilder()
                    .AppendLine("Neue Bestellung wurde berabeitet")
                    .AppendLine("---")
                    .AppendLine("Waren:");

                foreach (var line in cart.Lines)
                {
                    var subtotal = line.Cube.Price * line.Quantity;
                    body.AppendFormat("{0} x {1} (Gesamt: {2:c}", 
                        line.Quantity, line.Cube.Name, subtotal);
                }

                body.AppendFormat("Gesamtbetrag: {0:c}", cart.ComputeTotalValue())
                    .AppendLine("---")
                    .AppendLine("Lieferung:")
                    .AppendLine(shippingInfo.Name)
                    .AppendLine(shippingInfo.Line1)
                    .AppendLine(shippingInfo.Line2 ?? "")
                    .AppendLine(shippingInfo.Line3 ?? "")
                    .AppendLine(shippingInfo.City)
                    .AppendLine(shippingInfo.Country)
                    .AppendLine("---")
                    .AppendFormat("Geschenkverpackung Verwenden: {0}",
                        shippingInfo.GiftWrap ? "Ja" : "Nein");

                MailMessage mailMessage = new MailMessage(
                                       emailSettings.MailFromAddress,	// Absender
                                       emailSettings.MailToAddress,		// 
                                       "Neue Bestellung wurde gesendet!",		// Thema
                                       body.ToString()); 				// Mailkörper

                if (emailSettings.WriteAsFile)
                {
                    mailMessage.BodyEncoding = Encoding.UTF8;
                }

                var dir = smtpClient.PickupDirectoryLocation;
                if (!System.IO.Directory.Exists(dir))
                {
                    Directory.CreateDirectory(dir);
                }

                try
                {
                smtpClient.Send(mailMessage);

                }
                catch (Exception ex)
                {

                    throw;
                }

            }
        }
    }
}