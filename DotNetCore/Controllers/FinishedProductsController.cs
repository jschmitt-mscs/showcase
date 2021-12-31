using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using jschmitt2747ex1i.Data;
using jschmitt2747ex1i.Models;

namespace jschmitt2747ex1i.Controllers
{
    public class FinishedProductsController : Controller
    {
        private readonly SchedulerContext _context;

        public FinishedProductsController(SchedulerContext context)
        {
            _context = context;
        }

        [AcceptVerbs("Get", "Post")]
        public async Task<IActionResult> Index(string sortOrder, string currentFilter, string searchString, string selectCategory, int? pageNumber)
        {
            if(selectCategory == null)
            {
                selectCategory = "Beef";
            }
            var finishedProducts = _context.FinishedProducts.Where(e => e.Category.CategoryName == selectCategory);
            if (!string.IsNullOrEmpty(searchString))
            {
                finishedProducts = finishedProducts.Where(fp => fp.FinishedProductName.Contains(searchString)
                || fp.FinishedProductDescription.Contains(searchString));
            }
            if (!string.IsNullOrEmpty(currentFilter))
            {
                finishedProducts = finishedProducts.Where(fp => fp.FinishedProductName.Contains(currentFilter)
                || fp.FinishedProductDescription.Contains(currentFilter));
            }
            List<string> categories = new List<string>();
            categories = await _context.Categories.Select(c => c.CategoryName).Distinct().ToListAsync();
            List<SelectListItem> categorySelectListItems = new List<SelectListItem>();
            foreach (string cat in categories)
            {
                categorySelectListItems.Add(new SelectListItem { Text = cat, Value = cat, Selected = (cat == selectCategory) });
            }


            ViewBag.categorySelectListItems = categorySelectListItems;
            ViewData["CurrentSort"] = sortOrder;
            ViewData["NameSortParm"] = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewData["DescriptSortParm"] = String.IsNullOrEmpty(sortOrder) ? "descript_desc" : "";
            ViewData["CurrentFilter"] = searchString;
            ViewData["CategoryName"] = selectCategory;




            switch (sortOrder)
            {
                case "name_desc":
                    finishedProducts = finishedProducts.OrderByDescending(c => c.FinishedProductName);
                    break;
                case "descript_desc":
                    finishedProducts = finishedProducts.OrderByDescending(c => c.FinishedProductDescription);
                    break;
            }

            //var prods = await finishedProducts.AsNoTracking().ToListAsync();

            int pageSize = 8;
            return View(await PaginatedList<FinishedProduct>.CreateAsync(finishedProducts.AsNoTracking(), pageNumber ?? 1, pageSize));
        }

        // GET: FinishedProducts/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var finishedProduct = await _context.FinishedProducts
                .Include(c => c.ComponentProducts)
                .FirstOrDefaultAsync(m => m.FinishedProductId == id);
                
            if (finishedProduct == null)
            {
                return NotFound();
            }

            return View(finishedProduct);
        }

        // GET: FinishedProducts/Create
        public async Task<IActionResult> Create()
        {
            string selectedCategory = "Beef";
            List<Category> categories = await _context.Categories.Distinct().ToListAsync();
            List<SelectListItem> selectListItems = new List<SelectListItem>();
            foreach(Category cat in categories)
            {
                selectListItems.Add(new SelectListItem { Text = cat.CategoryName, Value = cat.CategoryId.ToString() });
            }
            ViewBag.CategorySelectList = selectListItems;

            return View();
        }

        // POST: FinishedProducts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("FinishedProductName,FinishedProductDescription,CategoryId")] FinishedProduct finishedProduct)
        {
            if (ModelState.IsValid)
            {
                _context.Add(finishedProduct);
                try
                {
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                } catch (DbUpdateException ex)
                {
                    ModelState.AddModelError("", "Unable to save changes. " +
                    "Try again, and if the problem persists " +
                    "see your system administrator.");
                }

            }
            return View(finishedProduct);
        }

        // GET: FinishedProducts/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var finishedProduct = await _context.FinishedProducts.FindAsync(id);
            if (finishedProduct == null)
            {
                return NotFound();
            }
            return View(finishedProduct);
        }

        // POST: FinishedProducts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditPost(int id)
        {
            if(id == null)
            {
                return NotFound();
            }
            var productToUpdate = await _context.FinishedProducts.FirstOrDefaultAsync(p => p.FinishedProductId == id);
            if(await TryUpdateModelAsync<FinishedProduct>(productToUpdate,
                "",
                s=> s.FinishedProductName, s=> s.FinishedProductDescription, s=> s.CategoryId))
            {
                try
                {
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                } catch(DbUpdateException ex)
                {
                    ModelState.AddModelError("", "Unable to save changes");
                }
            }
            return View(productToUpdate);
        }

        // GET: FinishedProducts/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var finishedProduct = await _context.FinishedProducts
                .FirstOrDefaultAsync(m => m.FinishedProductId == id);
            if (finishedProduct == null)
            {
                return NotFound();
            }

            return View(finishedProduct);
        }

        // POST: FinishedProducts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var finishedProduct = await _context.FinishedProducts.FindAsync(id);
            _context.FinishedProducts.Remove(finishedProduct);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FinishedProductExists(int id)
        {
            return _context.FinishedProducts.Any(e => e.FinishedProductId == id);
        }

        public async Task<ActionResult> Statistics()
        {
            //IQueryable<ComponentsViewModel> data = _context.Products
            //    .Join(
            //        _context.ComponentProducts,
            //        product => product.ProductId,
            //        ComponentProduct => ComponentProduct.ProductId,
            //        (product, componentProduct) => new ComponentsViewModel
            //        {
            //            productName = product.ProductName,
            //            productDescription = product.ProductDescription,
            //            finalProductCount = 
            //        }
            //    )

            var query = from p in _context.Products
                        join component in _context.ComponentProducts on p.ProductId equals component.ProductId
                        select new { p.ProductName, p.ProductDescription, component.ProductId } into x
                        group x by new { x.ProductName, x.ProductDescription } into g
                        select new ComponentsViewModel
                        {
                            productName = g.Key.ProductName,
                            productDescription = g.Key.ProductDescription,
                            finalProductCount = g.Count(e => e.ProductId > 0)
                        };
            return View(await query.AsNoTracking().ToListAsync());

        }
    }
}
