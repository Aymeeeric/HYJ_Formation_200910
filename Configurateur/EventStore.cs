using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Configurateur
{
    public class EventStore :IEventStore
    {
        public List<IEventWrapper> Events { get; set; } = new List<IEventWrapper>();

        private readonly string fileName = @"C:\Temp\EventStore.json";
        
        private readonly JsonSerializerSettings _jsonSerializer = new JsonSerializerSettings
        {
            ReferenceLoopHandling = ReferenceLoopHandling.Serialize,
            TypeNameHandling = TypeNameHandling.All,
            TypeNameAssemblyFormatHandling = TypeNameAssemblyFormatHandling.Simple,
            ObjectCreationHandling = ObjectCreationHandling.Replace
        };

        public EventStore()
        {
            var history = LoadEvents();

            Events.Clear();
            Events.AddRange(history);
        }

        public void SaveEvents(List<IEventWrapper> wrappers)
        {
            // TODO : check if event contains an eent with seq. number...
            Events.AddRange(wrappers);
            
            using (var writer = File.AppendText(fileName))
            {
                foreach (var @event in Events)
                {
                    var json = JsonConvert.SerializeObject(@event, _jsonSerializer);
                    writer.WriteLineAsync(json).Wait();
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

        private List<IEventWrapper> LoadEvents()
        {
            List<IEventWrapper> wrappers = new List<IEventWrapper>();

            string[] lines = File.ReadAllLines(fileName);
            wrappers.AddRange(
                lines
                    .Select(line => (IEventWrapper)JsonConvert.DeserializeObject(line, _jsonSerializer))
                    .ToList());

            return wrappers;
        }
    }
}