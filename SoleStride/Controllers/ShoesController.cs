
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SoleStride.Models;

public class ShoesController : Controller
{
    private readonly SoleStrideDbContext _context;

    public ShoesController(SoleStrideDbContext context)
    {
        _context = context;
    }

    // GET: SHOESS
    public async Task<IActionResult> Index()    
    {
        return View(await _context.Shoes.ToListAsync());
    }

    // GET: SHOESS/Details/5
    public async Task<IActionResult> Details(System.Guid? productid)
    {
        if (productid == null)
        {
            return NotFound();
        }

        var shoes = await _context.Shoes
            .FirstOrDefaultAsync(m => m.ProductId == productid);
        if (shoes == null)
        {
            return NotFound();
        }

        return View(shoes);
    }

    // GET: SHOESS/Create
    public IActionResult Create()
    {
        return View();
    }

    // POST: SHOESS/Create
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("ProductId,ShoesName,SkuId,Category,CategoryId,ShoesGender,ShoesSize,Material,Description,Price,SalePercentage")] Shoes shoes)
    {
        if (ModelState.IsValid)
        {
            _context.Add(shoes);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        return View(shoes);
    }

    // GET: SHOESS/Edit/5
    public async Task<IActionResult> Edit(System.Guid? productid)
    {
        if (productid == null)
        {
            return NotFound();
        }

        var shoes = await _context.Shoes.FindAsync(productid);
        if (shoes == null)
        {
            return NotFound();
        }
        return View(shoes);
    }

    // POST: SHOESS/Edit/5
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(System.Guid? productid, [Bind("ProductId,ShoesName,SkuId,Category,CategoryId,ShoesGender,ShoesSize,Material,Description,Price,SalePercentage")] Shoes shoes)
    {
        if (productid != shoes.ProductId)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                _context.Update(shoes);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ShoesExists(shoes.ProductId))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return RedirectToAction(nameof(Index));
        }
        return View(shoes);
    }

    // GET: SHOESS/Delete/5
    public async Task<IActionResult> Delete(System.Guid? productid)
    {
        if (productid == null)
        {
            return NotFound();
        }

        var shoes = await _context.Shoes
            .FirstOrDefaultAsync(m => m.ProductId == productid);
        if (shoes == null)
        {
            return NotFound();
        }

        return View(shoes);
    }

    // POST: SHOESS/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(System.Guid? productid)
    {
        var shoes = await _context.Shoes.FindAsync(productid);
        if (shoes != null)
        {
            _context.Shoes.Remove(shoes);
        }

        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    private bool ShoesExists(System.Guid? productid)
    {
        return _context.Shoes.Any(e => e.ProductId == productid);
    }
}
