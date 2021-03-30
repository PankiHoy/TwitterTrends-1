using System.ComponentModel.DataAnnotations;

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
