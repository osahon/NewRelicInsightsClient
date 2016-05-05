using System;
using System.Configuration;
using Newtonsoft.Json;
using RestSharp;

namespace NewRelicInsights
{
	public class Client
	{
		private static string NEW_RELIC_API_KEY => ConfigurationManager.AppSettings["NewRelicInsightsApiKey"];
		private static string NEW_RELIC_ACCOUNT_NUMBER => ConfigurationManager.AppSettings["NewRelicInsightsAccountNumber"];
		private static string NEW_RELIC_BASE_URL => ConfigurationManager.AppSettings["NewRelicInsightsBaseUrl"];
		private static string NEW_RELIC_EVENTS_PATH => ConfigurationManager.AppSettings["NewRelicInsightsEventsPath"];

		public static void SendEvent(BaseNewRelicEvent newRelicEvent)
		{

			var client = new RestClient($"{NEW_RELIC_BASE_URL}{NEW_RELIC_ACCOUNT_NUMBER}{NEW_RELIC_EVENTS_PATH}");
			var request = new RestRequest(Method.POST);
			request.AddHeader("Accept", "application/json");
			request.AddHeader("Content-Type", "application/json");
			request.AddHeader("X-Insert-Key", NEW_RELIC_API_KEY);
			request.AddParameter("application/json", JsonConvert.SerializeObject(newRelicEvent), ParameterType.RequestBody);
			var handler = client.ExecuteAsync(request, r =>
			{
				try
				{
					if (r.ResponseStatus != ResponseStatus.Completed)
					{
						throw new Exception(
							$"Error: An error occured sending event to New Relic Insights. Request returned with a response  {r}");
					}
				}
				catch (Exception ex)
				{
					throw new Exception("Error Sending Event to New Relic Insights", ex);
				}

			});
		}
	}
}
