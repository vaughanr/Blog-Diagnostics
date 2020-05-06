using System;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Diagnostics.EventSources
{
    [EventSource(Name = PersonEventSource.SourceName)]
    public class PersonEventSource : EventSource
    {
        readonly EventCounter steveCreatedCounter;

        int steveCreatedCount = 0;

        readonly EventCounter notSteveCreatedCounter;

        int notSteveCreatedCount = 0;

        public PersonEventSource()
        {
            steveCreatedCounter = new EventCounter("Steve created", this);
            notSteveCreatedCounter = new EventCounter("Not Steve created", this);
        }

        const string SourceName = "Diagnostics.Person";
        [Event(1, Level= EventLevel.Informational)]
        public void Created(string name)
        {
            if (name.Contains("Steve", StringComparison.InvariantCultureIgnoreCase))
            {
                steveCreatedCounter.WriteMetric(Interlocked.Increment(ref steveCreatedCount));
            }
            else
            {
                notSteveCreatedCounter.WriteMetric(Interlocked.Increment(ref notSteveCreatedCount));
            }
        }
    }
}
