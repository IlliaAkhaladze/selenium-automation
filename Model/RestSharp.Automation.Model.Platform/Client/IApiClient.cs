﻿using System.Threading.Tasks;

namespace RestSharp.Automation.Model.Platform.Client
{
	public interface IApiClient
	{
		Task<T1> ExecutePostAsync<T1, T2>(string uri, T2 body, string accessToken = null)
			where T1 : class
			where T2 : class;

		Task<ClientResponse> ExecutePostAsync<T1>(string uri, T1 body, string accessToken = null)
			where T1 : class;

		Task<T1> ExecuteGetAsync<T1>(string uri, string accessToken = null)
			where T1 : class;
	}
}
