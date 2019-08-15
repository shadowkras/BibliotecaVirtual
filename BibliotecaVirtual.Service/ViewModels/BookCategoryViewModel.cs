using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BibliotecaVirtual.Service.ViewModels
{
    public class BookCategoryViewModel
    {
        [Display(Name = "Livro")]
        public int BookId { get; set; }

        [Display(Name = "G�nero")]
        public int CategoryId { get; set; }        
    }
}