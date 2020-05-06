using Diagnostics.EventSources;
using Diagnostics.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Diagnostics.Service
{
    public class PersonService : IPersonService
    {
        private readonly PersonEventSource personEventSource;

        public PersonService(PersonEventSource personEventSource)
        {
            this.personEventSource = personEventSource;
        }
        public async Task<Person> GetAsync(string name)
        {
            await Task.Delay(TimeSpan.FromSeconds(1));
            personEventSource.Created(name);
            return new Person { Name = name, Age = 18 };
        }
    }

    public interface IPersonService
    {
        Task<Person> GetAsync(string name);
    }
}
