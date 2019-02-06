using RazorText.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RazorText.Services
{
    public interface IEmailService
    {
        Task SendVerificationAsync(Author author);
    }
}
