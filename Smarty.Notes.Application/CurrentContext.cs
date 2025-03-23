using System.Text.Json.Nodes;
using Smarty.Notes.Domain.Interfaces;

namespace Smarty.Notes.Infrastructure
{
    public class CurrentContext : ICurrentContext
    {
        Guid? _instanceId;
        readonly static object _lock = new();

        readonly IConfiguration _configuration;
        public CurrentContext(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public Guid GetInstanceId()
        {
            if (_instanceId is not null)
            {
                return _instanceId.Value;
            }
            
            lock (_lock)
            {
                if (_instanceId is null)
                {
                    _instanceId = _configuration.GetValue<Guid?>("InstanceId");
                    if (_instanceId is null)
                    {
                        _instanceId = Guid.NewGuid();

                        var filePath = Path.Combine(AppContext.BaseDirectory, "appsettings.json");
                        var text = File.ReadAllText(filePath);
                        var node = JsonNode.Parse(text);

                        if (node != null)
                        {
                            node["InstanceId"] = _instanceId;
                            File.WriteAllText(filePath, node.ToJsonString());
                        }
                    }
                }

            }

            return _instanceId.Value;
        }

        public DateTime GetNow()
        {
            return DateTime.Now;
        }
    }
}