using Microsoft.AspNetCore.Mvc.Rendering;

namespace net_il_mio_fotoalbum.Models
{
    public class ImageFormModel
    {
        public Image Image { get; set; }

        public IFormFile? ImageFormFile { get; set; }

        // Serve per gestire "Select" che seleziona istanze multiple nelle viste (Multiple per la relazione N a N)
        public List<SelectListItem>? Category { get; set; }
        public List<string>? SelectedCategoryId { get; set; }
    }
}
