using Ewallet.Models.emailModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Ewallet.Commons
{
    public interface IMailService
    {
        Task SendMailAsync(MailRequest mailRequest);

    }
}
