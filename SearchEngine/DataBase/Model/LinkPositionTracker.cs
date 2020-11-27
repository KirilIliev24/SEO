using System.ComponentModel.DataAnnotations;

namespace SearchEngine.DataBase.Model
{
    public class LinkPositionTracker
    {
        [Key]
        public string Keywords { get; set; }

        [Key]
        public string Link { get; set; }
    }
}
