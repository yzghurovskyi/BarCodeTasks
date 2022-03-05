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
    public class IndexModel : PageModel
    {
        private readonly BarCodeLab.Data.ShoesStoreContext _context;

        public IndexModel(BarCodeLab.Data.ShoesStoreContext context)
        {
            _context = context;
        }

        public IList<Brand> Brand { get;set; }

        public async Task OnGetAsync()
        {
            Brand = await _context.Brand.ToListAsync();
        }
    }
}
