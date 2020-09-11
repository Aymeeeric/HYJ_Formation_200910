using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Configurateur
{
    public class EventStore
    {
        public List<IEventWrapper> Events { get; set; } = new List<IEventWrapper>();

        private readonly string fileName = @"C:\Temp\EventStore.json";

        public async Task SaveAsync(List<IEventWrapper> wrappers)
        {
            Events.AddRange(wrappers);
            using (var writer = File.AppendText(fileName))
            {
                foreach (var @event in Events)
                {
                    var jsonSerializer = new JsonSerializerSettings
                    {
                        ReferenceLoopHandling = ReferenceLoopHandling.Serialize,
                        TypeNameHandling = TypeNameHandling.All,
                        TypeNameAssemblyFormatHandling = TypeNameAssemblyFormatHandling.Simple,
                        ObjectCreationHandling = ObjectCreationHandling.Replace
                    };
                    var json = JsonConvert.SerializeObject(@event, jsonSerializer);
                    await writer.WriteLineAsync(json);
                }
            }
        }

        public List<IEvent> GetAllEventsForId(string id)
        {
            return Events.
                Where(wrp => wrp.GetId().Equals(id))
                .Select(wrp => wrp.Event)
                .ToList();
        }
    }
}