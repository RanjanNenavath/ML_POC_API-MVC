using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FrontEnd_MVC_UI.Services
{
    public interface IQueueReciverServices
    {
        Task<string> recivemessage();
    }
}
