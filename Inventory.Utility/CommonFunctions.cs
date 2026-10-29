using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Inventory.Utility
{
    public class CommonFunctions
    {
        public static string GenerateNumber(int number)
        {
            return number.ToString().PadLeft(4, '0');
        }
        public static int GetTransactionNumber(string? number)
        {
            if (number == null) return 0;
            var SplitNumber = number.Substring(number.Length - 5);
            return Convert.ToInt32(SplitNumber);
        }
        public static string GenerateDocumentNumber(string prefix, int sequenceNumber)
        {
            DateTime today = DateTime.Today;

            // Determine Financial Year (April 1st to March 31st standard)
            int financialYear = (today.Month <= 3) ? today.Year - 1 : today.Year;

            // Formats the sequence to be zero-padded to 5 digits (e.g., 1234 -> 01234)
            return $"{prefix}-{financialYear}-{++sequenceNumber:D5}";
        }
        public static string GenerateCalendarDocNumber(string prefix, int sequenceNumber)
        {
            return $"{prefix}-{DateTime.Today.Year}-{sequenceNumber:D5}";
        }
        public static int GenerateVerificationCode()
        {
            int number = RandomNumberGenerator.GetInt32(10000, 100000);
            return number;
        }

        public static int GenerateTransactionNumber()
        {
            int number = RandomNumberGenerator.GetInt32(1, 1000);
            return number;
        }

        public static string GetYear()
        {
            return DateTime.Now.Year.ToString();
        }
        public static string GetMonth()
        {
            return DateTime.Now.Month.ToString();
        }
        public static string GetDay()
        {
            return DateTime.Now.Day.ToString();
        }

        public static string GetVerificationCodeTemplate(string ApplicationName, string code)
        {
            string htmlBody = @"<!DOCTYPE html>
                <html>
                  <head>
                    <meta charset=""utf-8"">
                    <meta name=""viewport"" content=""width=device-width, initial-scale=1.0"" />
                    <title>{{ApplicationName}} Verification</title>
                  </head>
                  <body style=""margin:0;padding:0;background:#f4f6f8;font-family: 'Segoe UI', Roboto, Arial, sans-serif;"">
                    <table width=""100%"" cellpadding=""0"" cellspacing=""0"" border=""0"">
                      <tr>
                        <td align=""center"" style=""padding:30px 10px;"">
                          <table width=""600"" cellpadding=""0"" cellspacing=""0"" border=""0"" style=""background:#ffffff;border-radius:8px;overflow:hidden;"">
                            <tr>
                              <td style=""padding:24px 32px 16px 32px;"">
                                <h1 style=""margin:0;font-size:20px;color:#111;"">{{ApplicationName}}</h1>
                                <p style=""margin:8px 0 20px 0;color:#555;"">Verify your account with the following code:</p>

                                <div style=""display:inline-block;padding:16px 20px;border-radius:6px;background:#f1f5f9;font-size:24px;font-weight:700;letter-spacing:2px;"">
                                  {{code}}
                                </div>

                                <p style=""color:#777;font-size:13px;margin-top:18px;"">
                                  This code expires in 10 minutes. If you didn't request this, please ignore this message.
                                </p>
                              </td>
                            </tr>
                            <tr>
                              <td style=""background:#fafafa;padding:12px 32px;text-align:center;color:#999;font-size:12px;"">
                                © 2025 {{ApplicationName}}. All rights reserved.
                              </td>
                            </tr>
                          </table>
                        </td>
                      </tr>
                    </table>
                  </body>
                </html>
                ";
            htmlBody = htmlBody.Replace("{{ApplicationName}}", ApplicationName).Replace("{{code}}", code);
            return htmlBody;
        }
    }
}
