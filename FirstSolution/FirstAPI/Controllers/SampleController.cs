using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FirstAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SampleController : ControllerBase
    {
       List<string> items = new List<string> { "Item1", "Item2", "Item3" };

        [HttpGet]
        public IEnumerable<string> GetItems()
        {
            return items;
        }

        [HttpPost]
        public string AddItem([FromBody] string newItem)
        {
            items.Add(newItem);
            return newItem;
        }
    }
}
