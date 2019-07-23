﻿using DabeaV2.Services.Interfaces;
using DabeaV2.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace DabeaV2.Web.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    public class InternTraegerComponentController : Controller
    {
        private readonly ITraegerService _traegerService;

        public InternTraegerComponentController(ITraegerService traegerService)
        {
            _traegerService = traegerService;
        }

        [Authorize]
        [HttpPost("Get_Name")]
        public async Task<TraegerNameResponseViewvModel> Get_Name([FromBody] TraegerNameRequestViewModel vm)
        {
            try
            {
                return await _traegerService.Get_Name(vm.Id);
            }
            catch (Exception ex)
            {
                throw new DabeaV2ControllerException("Serverfehler", ex);
            }
        }
    }
}