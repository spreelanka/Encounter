using System;
using System.Net.Http;

namespace EncounterMobile.Services
{
	public abstract class BaseOpen5eService: BaseApiService
	{
        public BaseOpen5eService(HttpMessageHandler messageHandler) : base(messageHandler)
        {
        }

        protected override string BaseUri => "https://api.open5e.com";	

    }
}

