using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Buecherei.Models
{
    [Table("TblBuch")]
    public class Buch
    {
        [Key]
        [Required]
        public string Buchnummer { get; set; }
        [Required]
        [MaxLength(25)]
        public string Sachgebiet { get; set; }
        [Required]
        [MaxLength(50)]
        public string Titel { get; set; }
        [Required]
        [MaxLength(50)]
        public string Autor { get; set; }
        [Required]
        [MaxLength(50)]
        public string Ort { get; set; }
        [Required]
        public int Erscheinungsjahr { get; set; }
    }
}
