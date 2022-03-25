using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Buecherei.Models
{
    [Table("TblAusleihe")]
    public class Ausleihe
    {
        [Key]
        [Required]
        public int Zaehler { get; set; }
        [Required]
        public string Buchnummer { get; set; }
        [Required]
        public int Ausweisnummer { get; set; }
        [Required]
        public DateTime Ausleihdatum { get; set; }
        public DateTime? Retourdatum { get; set; }

        [ForeignKey("Buchnummer")]
        public virtual Buch Buch { get; set; }
        [ForeignKey("Ausweisnummer")]
        public virtual SchuelerIn SchülerIn { get; set; }
    }
}
