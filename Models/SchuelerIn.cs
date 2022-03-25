using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Buecherei.Models
{
    [Table("TblSchuelerIn")]
    public class SchuelerIn
    {
        [Key]
        [Required]
        public int Ausweisnummer { get; set; }
        [Required]
        [MaxLength(50)]
        public string Nachname { get; set; }
        [Required]
        [MaxLength(50)]
        public string Vorname { get; set; }
    }
}
