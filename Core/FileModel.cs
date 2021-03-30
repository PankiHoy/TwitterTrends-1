using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Core
{
    public class FileModel
    {
       
        public int Id { get; set; }
        public string Name { get; set; }
        [Key]
        public string Path { get; set; }
    }
}
