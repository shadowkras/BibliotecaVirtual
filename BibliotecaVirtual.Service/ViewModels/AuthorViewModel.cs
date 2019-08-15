using Newtonsoft.Json;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BibliotecaVirtual.Service.ViewModels
{
    public class AuthorViewModel
    {
        [Display(Name="Autor")]
        public int AuthorId { get; set; }

        [Display(Name = "Nome")]
        public string Name { get; set; }

        [JsonIgnore]
        public virtual ICollection<BookViewModel> Books { get; set; }
    }
}