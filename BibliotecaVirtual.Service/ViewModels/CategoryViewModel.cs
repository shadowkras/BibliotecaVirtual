using BibliotecaVirtual.Data.Interfaces;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BibliotecaVirtual.Application.ViewModels
{
    public class CategoryViewModel : IAutoMappleable
    {
        [Display(Name="G�nero")]
        public int CategoryId { get; set; }

        [Display(Name = "Descri��o")]
        public string Description { get; set; }

        [Display(Name = "Sobre")]
        public string AboutUrl { get; set; }

        [JsonIgnore]
        public virtual ICollection<BookCategoryViewModel> Books { get; set; }
    }
}