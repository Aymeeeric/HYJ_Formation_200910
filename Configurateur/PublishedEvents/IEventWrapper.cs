using System;

namespace Configurateur
{
    public interface IEventWrapper
    {
        IEvent Event { get; set; }

        String GetId();
    }
}