﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QuanLyThuVien.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuanLyThuVien.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class SearchController : Controller
    {
        private readonly dbLibraryContext _context;
        public SearchController(dbLibraryContext context)
        {
            _context = context;
        }
        // GET: Search/FindProduct
        [HttpPost]
        public IActionResult FindProduct(string keyword)
        {
            List<Products> ls = new List<Products>();
            if (string.IsNullOrEmpty(keyword) || keyword.Length < 1)
            {
                return PartialView("ListProductsSearchPartial", null);
            }
            ls = _context.Products
                                        .AsNoTracking()
                                        .Include(a => a.Cat)
                                        .Where(x => x.ProductName.Contains(keyword))
                                        .OrderByDescending(x => x.ProductName)
                                        .Take(10)
                                        .ToList();
            if (ls == null)
            {
                return PartialView("ListProductsSearchPartial", null);
            }
            else
            {
                return PartialView("ListProductsSearchPatial", ls);
            }
        }
/*        public IActionResult Index()
        {
            return View();
        }   */
    }
}
