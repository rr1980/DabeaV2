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
        [HttpPost("Get_Person")]
        public async Task<ComponentViewModel<NamePersonComponentViewModel>> Get_Person([FromBody]RequestComponentViewModel request)
        {
            return await _namedComponentService.Get_Person(request);
        }

        [Authorize]
        [HttpPost("Get_Benutzer")]
        public async Task<ComponentViewModel<NameBenutzerComponentViewModel>> Get_Benutzer([FromBody]RequestComponentViewModel request)
        {
            return await _namedComponentService.Get_Benutzer(request);
        }
    }
}