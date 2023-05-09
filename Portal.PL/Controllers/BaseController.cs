using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Portal.Domains.Enums;

namespace Portal.PL.Controllers
{    
    public class BaseController : Controller
    {
        private IMediator mediator;
        protected IMediator _mediator => mediator ??= HttpContext.RequestServices.GetService<IMediator>()!;
    }
}
