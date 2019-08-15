using BibliotecaVirtual.Service.Resources;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BibliotecaVirtual.Service.ViewModels
{
    public class BookViewModel
    {
        [Display(Name="Livro")]
        public int BookId { get; set; }

        [Display(Name = "T�tulo")]
        [Required(ErrorMessageResourceType = typeof(Criticas), ErrorMessageResourceName = nameof(Criticas.Campo_Requerido))]
        public string Title { get; set; }

        [Display(Name = "Descri��o")]
        [Required(ErrorMessageResourceType = typeof(Criticas), ErrorMessageResourceName = nameof(Criticas.Campo_Requerido))]
        public string Description { get; set; }

        [Display(Name = "Sinopse")]
        [Required(ErrorMessageResourceType = typeof(Criticas), ErrorMessageResourceName = nameof(Criticas.Campo_Requerido))]
        public string Sinopsis { get; set; }

        [Display(Name = "P�ginas")]
        public int Pages { get; set; }

        [Display(Name = "Data de publica��o")]
        [Required(ErrorMessageResourceType = typeof(Criticas), ErrorMessageResourceName = nameof(Criticas.Campo_Requerido))]
        public DateTime PublishDate { get; set; }

        [Display(Name = "Capa")]
        public string CoverImageBase64 { get; set; }

        [Display(Name = "Autor(a)")]
        [Required(ErrorMessageResourceType = typeof(Criticas), ErrorMessageResourceName = nameof(Criticas.Campo_Requerido))]
        public int AuthorId { get; set; }

        [Display(Name = "Editora")]
        [Required(ErrorMessageResourceType = typeof(Criticas), ErrorMessageResourceName = nameof(Criticas.Campo_Requerido))]
        public int PublisherId { get; set; }

        [Display(Name = "G�neros")]
        [Required(ErrorMessageResourceType = typeof(Criticas), ErrorMessageResourceName = nameof(Criticas.Campo_Requerido))]
        [JsonIgnore]
        public ICollection<BookCategoryViewModel> Categories { get; set; }

        [Display(Name = "Autor")]
        [JsonIgnore]
        public AuthorViewModel Author { get; set; }

        [Display(Name = "Editora")]
        [JsonIgnore]
        public PublisherViewModel Publisher { get; set; }
    }
}