using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Pinch.Models
{
    public class Instruction
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public int SequenceOrder { get; set; }

        public int RecipeId { get; set; }
        public Recipe Recipe { get; set; }

    }
}