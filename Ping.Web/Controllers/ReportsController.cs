﻿using Inertia.NET.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace Ping.Web.Controllers;

public class ReportsController : Controller
{
    public IActionResult Index()
    {
        return this.InertiaRender("Reports/Index");
    }
}