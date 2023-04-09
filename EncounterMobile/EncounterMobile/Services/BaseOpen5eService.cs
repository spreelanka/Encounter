using System;
using System.Net.Http;
using Polly.Registry;

namespace EncounterMobile.Services
{
	public abstract class BaseOpen5eService: BaseApiService
	{
        public BaseOpen5eService(HttpMessageHandler messageHandler, IReadOnlyPolicyRegistry<string> policyRegistry) : base(messageHandler, policyRegistry)
        {
        }

        protected override string BaseUri => "https://api.open5e.com";	

    }
}

