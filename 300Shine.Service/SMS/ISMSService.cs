using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _300Shine.Service.SMS
{
    public interface ISMSService
    {
        Task SendOtpSmsAsync(string toNumber, string otp);
        string GenerateOtp();
    }
}
