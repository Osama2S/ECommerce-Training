﻿using API.Errors;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("error/{code}")]
    [ApiExplorerSettings(IgnoreApi =true)]
    public class ErrorController : ControllerBase
    {
        public IActionResult Error(int code)
        {
            return new ObjectResult(new APIResponse(code));
        } 
    }
}
