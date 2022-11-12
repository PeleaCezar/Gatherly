using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Gatherly.Presentation.Abstractions
{
    [ApiController]
    public abstract class ApiController : ControllerBase
    {
        protected readonly ISender Sender;
        public ApiController(ISender sender)
        {
            Sender = sender;
        }
    }
}
