using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BarCodeLab.Data;
using BarCodeLab.Models;

namespace BarCodeLab.Pages.Brands
{
    public class DetailsModel : PageModel
    {
        private readonly BarCodeLab.Data.ShoesStoreContext _context;

        public DetailsModel(BarCodeLab.Data.ShoesStoreContext context)
        {
            _context = context;
        }

        public Brand Brand { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Brand = await _context.Brand.FirstOrDefaultAsync(m => m.Id == id);

            if (Brand == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
