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
    public class TestController : Controller
    {
        private readonly ITestService _testService;

        public TestController(ITestService testService)
        {
            _testService = testService;
        }

        [HttpPost("Test1")]
        public async Task<Test1ResponseViewvModel> Test1()
        {
            try
            {
                return await _testService.Test1();
            }
            catch (Exception ex)
            {
                throw new DabeaV2ControllerException("Serverfehler", ex);
            }
        }
    }
}
