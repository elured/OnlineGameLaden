using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RubiksCubeStore.Domain.Entities
{
    public class ShippingDetails
    {
            [Required(ErrorMessage = "Bitte geben Sie Ihren Namen ein")]
            public string Name { get; set; }

            [Required(ErrorMessage = "Geben Sie die erste Lieferadresse ein")]
            [Display(Name = "Erste Lieferadresse")]
            public string Line1 { get; set; }

            [Display(Name = "Zweite Lieferadresse")]
            public string Line2 { get; set; }

            [Display(Name = "Dritte Lieferadresse")]
            public string Line3 { get; set; }

            [Required(ErrorMessage = "Geben Sie Ihre Stadt ein")]
            [Display(Name = "Stadt")]
            public string City { get; set; }

            [Required(ErrorMessage = "Geben Sie Ihres Land ein")]
            [Display(Name = "Land")]
            public string Country { get; set; }

            public bool GiftWrap { get; set; }
    }
}
