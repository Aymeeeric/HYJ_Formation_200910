using System;
using System.Collections.Generic;
using System.Text;

namespace Configurateur
{
    public interface IEventWrapper
    {
        IEvent Event { get; set; }
    }
}
