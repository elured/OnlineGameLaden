using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace OnlineGameLaden.Domain.Entities
{
    public class Game
    {
        [HiddenInput(DisplayValue = false)]
        public int GameId { get; set; }

        [Display(Name = "Name")]
        [Required(ErrorMessage = "Geben Sie bitte Produktname")]
        public string Name { get; set; }

        [DataType(DataType.MultilineText)]
        [Display(Name = "Beschreibung")]
        [Required(ErrorMessage = "Geben Sie bitte Produktbeschreibung")]
        public string Description { get; set; }

        [Display(Name = "Kategorie")]
        [Required(ErrorMessage = "Geben Sie bitte Produktkategorie")]
        public string Category { get; set; }

        [Display(Name = "Preise (Euro)")]
        [Required]
        [Range(0.01, double.MaxValue, ErrorMessage = "Bitte geben Sie einen positiven Wert für den Preis ein.")]
        public decimal Price { get; set; }
    }
}
