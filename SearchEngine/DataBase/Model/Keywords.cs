using System.ComponentModel.DataAnnotations;

namespace SearchEngine.DataBase.Model
{
    public class Keywords
    {
        [Key]
        public string Keyword { get; set; }
    }
}
