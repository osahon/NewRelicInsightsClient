using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewRelicInsights
{
	public abstract class BaseNewRelicEvent
	{
		public BaseNewRelicEvent()
		{
			eventType = this.GetType().Name;
		}
		public abstract string eventType { get; set; }
	}
}
