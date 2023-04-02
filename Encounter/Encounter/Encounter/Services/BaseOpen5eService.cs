using System;
namespace Encounter.Services
{
	public abstract class BaseOpen5eService: BaseApiService
	{
        protected override string BaseUri => "https://api.open5e.com";	

    }
}

