using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using SuperHero.Domain.Entities;
using SuperHero.Domain.Services;

namespace SuperHero.Infrastructure.Services
{
    public class AuditEventService : IAuditEventService
    {
        private readonly string _auditEventUrl;

        public AuditEventService(IConfiguration configuration)
        {
            _auditEventUrl = configuration.GetSection("AppConfig").GetSection("AuditEventUrl").Value;
        }

        public async Task Subscribe(AuditEvent auditEvent)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(_auditEventUrl);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.TryAddWithoutValidation("Content-Type", "application/json");

                var values = new Dictionary<string, string>
                {
                    { "entity", auditEvent.Entity },
                    { "entityId", auditEvent.EntityId.ToString() },
                    { "username", auditEvent.Username },
                    { "action", auditEvent.Action.ToString() },
                };

                StringContent content = new StringContent(JsonConvert.SerializeObject(values), Encoding.UTF8, "application/json");

                var response = await client.PostAsync("api/auditevent/", content);
            }
        }
    }
}
