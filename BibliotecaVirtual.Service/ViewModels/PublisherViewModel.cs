using Newtonsoft.Json;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BibliotecaVirtual.Service.ViewModels
{
    public class PublisherViewModel
    {
        [Display(Name="Editora")]
        public int PublisherId { get; set; }

        [Display(Name = "Nome")]
        public string Name { get; set; }

        [JsonIgnore]
        public virtual ICollection<BookViewModel> Books { get; set; }

        /// <summary>
        /// Construtor da entidade Publisher, necessário para o EntityFramework.
        /// </summary>
        protected PublisherViewModel()
        { }
    }
}