using System.ComponentModel.DataAnnotations;

namespace Jynx.Database.Entities
{
    public class Tag
    {
        [Key]
        public string Name { get; set; }

        public string Content { get; set; }
    }
}
