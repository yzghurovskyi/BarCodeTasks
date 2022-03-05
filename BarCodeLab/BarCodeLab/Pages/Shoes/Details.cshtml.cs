namespace BarCodeLab.Pages.Shoes
{
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.RazorPages;
    using Microsoft.EntityFrameworkCore;
    using BarCodeLab.Models;
    using BarCode;
    using System.IO;
    using System.Net.Mime;
    using System.Text;
    using System.Linq;

    public class DetailsModel : PageModel
    {
        private readonly BarCodeLab.Data.ShoesStoreContext _context;

        public DetailsModel(BarCodeLab.Data.ShoesStoreContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Shoes Shoes { get; set; }
        [BindProperty]
        public int Id { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            Id = id.Value;

            Shoes = await _context.Shoes
                .Include(s => s.Manufacturer)
                .Include(s => s.Brand)
                .AsNoTracking().FirstOrDefaultAsync(m => m.Id == id);

            if (Shoes == null)
            {
                return Redirect("~/NotFound");
            }
            return Page();
        }

        public ActionResult OnPostDownloadBarCode()
        {
            var builder = new StringBuilder();
            builder.Append(Shoes.Id.ToString().PadLeft(3, '0'));
            builder.Append(Shoes.BrandId.ToString().PadLeft(3, '0'));
            builder.Append(Shoes.ManufacturerId.ToString().PadLeft(3, '0'));
            
            var parsedDigits = builder.ToString().Select(digit => int.Parse(digit.ToString())).ToList();
            var encoder = new BarCodeEncoder();
            var bitmap = encoder.Encode(parsedDigits);

            var memoryStream = new MemoryStream();
            bitmap.Save(memoryStream, System.Drawing.Imaging.ImageFormat.Jpeg);
            memoryStream.Seek(0, SeekOrigin.Begin);
            return File(memoryStream, MediaTypeNames.Image.Jpeg, $"{Shoes.Name}.png");
        }
    }
}
