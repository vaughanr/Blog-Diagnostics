using Diagnostics.EventSources;
using Diagnostics.Model;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Diagnostics.Service
{
    public class PersonService : IPersonService
    {
        private readonly PersonEventSource personEventSource;
        private readonly ConcurrentBag<Person> uniquePeople = new ConcurrentBag<Person>();

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

        public async Task SaveAsync(Person person)
        {
            await Task.Delay(TimeSpan.FromSeconds(1));

            //.Equals and .GetHashCode aren't implemented so this won't work
            //as expected. The compare on object reference which is never
            //going to match.
            uniquePeople.Add(person);
        }
    }

    public interface IPersonService
    {
        Task<Person> GetAsync(string name);
        Task SaveAsync(Person person);
    }
}
