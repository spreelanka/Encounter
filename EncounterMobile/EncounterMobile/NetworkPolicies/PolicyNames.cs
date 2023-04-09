using System;
using Polly;
using Polly.CircuitBreaker;

namespace EncounterMobile.NetworkPolicies
{
	public static class PolicyNames
	{
		public const string DefaultPolicy = "DefaultPolicy";
    }
}

