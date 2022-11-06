﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using MVC_01.Models;

namespace MVC_01.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly AppDbContext _context;

        public HomeController(ILogger<HomeController> logger,AppDbContext context)
        {
            _logger = logger;
            _context = context;
        }
        public string HiHome() => "Xin chao hi home";
        public IActionResult Index()
        {
            var products = _context.Products.Include(p => p.Author)
            .Include(p =>p.Photos)
            .Include(p=>  p.ProductCategoryProducts)
            .ThenInclude(p => p.Category).AsQueryable().OrderByDescending(p => p.DateUpdated).AsQueryable();
            products = products.OrderByDescending(p => p.DateUpdated).Take(4);

            var posts = _context.Posts.Include(p => p.Author).Include(p => p.PostCategories).ThenInclude(p => p.Category).
                OrderByDescending(p => p.DateUpdated).AsQueryable();
            posts = posts.OrderByDescending(p=>p.DateUpdated).Take(3);

            ViewBag.products = products;
            ViewBag.posts = posts;
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
