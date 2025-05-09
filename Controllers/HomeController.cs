using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using InventariApp.Models;
using InventariApp.Data;
using Microsoft.EntityFrameworkCore;

namespace InventariApp.Controllers;

public class HomeController : Controller
{    
    //setting up the context
    private readonly InventoryContext _context;

    public HomeController(InventoryContext context) 
    {
        _context = context;
    }

    public IActionResult Privacy()
    {
        return View();
    }



    // GET /Home/ 
    // Shows products
    public async Task<IActionResult> Index(string searchString = "")
    {
        searchString = searchString.ToLower();
        ViewData["CurrentFilter"] = searchString;
        var products = await _context.Products.ToListAsync();
        if (!string.IsNullOrEmpty(searchString))
        {
            products = products.Where(p => p.Name.ToLower().Contains(searchString) ||
                p.Description.ToLower().Contains(searchString)).ToList();
        }
        products.ForEach(p => Debug.WriteLine(p)); // Comprovació ràpida de que s'han creat els dos items
        return View(products);
    }

    // GET: /Home/Create - Shows form to add new product
    public IActionResult Create()
    {
        return View();
    }

    // POST: /Home/Create - Form to create a new product
    [HttpPost]
    public async Task<IActionResult> Create(Product product)
    {
        if (ModelState.IsValid)
        {
            _context.Products.Add(product);
            await _context.SaveChangesAsync();
            // Back to the list 
            return RedirectToAction(nameof(Index));
        }
        return View(product);
    }

    // GET: /Home/Edit/{i} - Shows form to edit existing product
    public async Task<IActionResult> Edit(int id)
    {
        var product = await _context.Products.FindAsync(id);
        if (product == null)
        {
            return NotFound();
        }
        return View("UpdateProduct", product);
    }

    // POST: /Home/Edit/{i} - Handles form submission for editing
    [HttpPost]
    public async Task<IActionResult> Edit(int id, Product product)
    {
        if (id != product.Id)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                // Update product in database
                _context.Update(product);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Products.Any(e => e.Id == id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
        }

        //return View(product);
        // Torna a ensenyar la pàgina d'edició
        //return View("UpdateProduct", product);
        return View("UpdateProduct", product);
    }

    // GET: /Home/Delete/{i} - Shows confirmation page
    public async Task<IActionResult> Delete(int id)
    {
        var product = await _context.Products.FindAsync(id);
        if (product == null)
        {
            return NotFound();
        }
        _context.Products.Remove(product);
        await _context.SaveChangesAsync();

        //TempData["DeletedMessage"] = $"Deleted {product.Id}: {product.Name}";

        return RedirectToAction("Index");
    }








}
