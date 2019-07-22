using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DabeaV2.Services.Interfaces;
using DabeaV2.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DabeaV2.Web.Controllers.Components
{
    [Route("api/[controller]")]
    public class NameComponentController : Controller
    {
        private readonly INamedComponentService _namedComponentService;
        public NameComponentController(INamedComponentService namedComponentService)
        {
            _namedComponentService = namedComponentService;
        }

        [Authorize]
        [HttpPost("Get")]
        public async Task<ComponentViewModel<NameComponentViewModel>> Get([FromBody]ComponentViewModel<NameComponentViewModel> request)
        {
            return await _namedComponentService.Get(request);
        }
    }
}