using Microsoft.Extensions.Logging;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Diagnostics.Service
{
    public interface IPhonebook
    {
        Task<long> Get(string name);
    }

    public class Phonebook : IPhonebook
    {
        public Phonebook(ILogger<Phonebook> logger)
        {
            this.logger = logger;
        }

        IList<string> _queryIds = new List<string>();
        private readonly object _lock = new object();
        private readonly ILogger<Phonebook> logger;

        public async Task<long> Get(string name)
        {
            string queryRef = $"{name}.{Guid.NewGuid()}";
            lock(_lock)
            {
                _queryIds.Add(queryRef);
            }
            logger.LogInformation("Query from {queryRef}", queryRef);
            await Task.Delay(1);//Some fake work

            return 100;
        }
    }
}
