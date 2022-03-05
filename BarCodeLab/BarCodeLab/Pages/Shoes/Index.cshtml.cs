namespace BarCodeLab.Pages.Shoes
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc.RazorPages;
    using Microsoft.EntityFrameworkCore;
    using BarCodeLab.Models;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using System.Drawing;
    using BarCode;

    public class IndexModel : PageModel
    {
        private readonly BarCodeLab.Data.ShoesStoreContext _context;

        public IndexModel(BarCodeLab.Data.ShoesStoreContext context)
        {
            _context = context;
        }

        [BindProperty]
        public IFormFile UploadedFile { get; set; }
        public IList<Shoes> Shoes { get;set; }

        public async Task OnGetAsync()
        {
            Shoes = await _context.Shoes.ToListAsync();
        }

        public IActionResult OnPostUpload()
        {
            var stream = UploadedFile.OpenReadStream();

            var bitmap = new Bitmap(stream);
            var encoder = new BarCodeEncoder();

            var (digits, _) = encoder.Decode(bitmap);
            var id = int.Parse(new string($"{digits[0]}{digits[1]}{digits[2]}").ToString());

            return Redirect($"~/Shoes/Details?id={id}");
        }
    }
}
