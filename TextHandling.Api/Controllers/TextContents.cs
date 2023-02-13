using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TextHandling.Api.Services.Contracts;

namespace TextHandling.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TextContents : ControllerBase
    {
        private readonly IDataHandler _dataHandler;
        public TextContents(IDataHandler handler)
        {
            _dataHandler = handler;
        }


    }
}
