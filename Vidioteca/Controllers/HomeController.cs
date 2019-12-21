﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Vidioteca.Models;

namespace Vidioteca.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public async Task<IActionResult> Index()
        {
           return View();
        }
    }
}
