using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Pinch.Models
{
    public class Upload
    {
        public int Id { get; set; }

        public int RecipeId { get; set; }

        public string FileName { get; set; }

        public string FileDataType { get; set; }

        public string FileData { get; set; }
    }
}