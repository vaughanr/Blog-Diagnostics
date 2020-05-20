using System.Threading.Tasks;
using Diagnostics.Service;
using Microsoft.AspNetCore.Mvc;

namespace Diagnostics.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PhonebookController : ControllerBase
    {
        private readonly IPhonebook phoneBook;

        public PhonebookController(IPhonebook phoneBook)
        {
            this.phoneBook = phoneBook;
        }

        [HttpGet]
        public async Task<IActionResult> Get(string name)
        {
            long number = await phoneBook.Get(name);

            return Ok(number);
        }
    }
}