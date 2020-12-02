using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SearchEngine.Model
{
    public class MeaningfulText
    {
        public string Text { get; set; }
        public string Keyword { get; set; }
        public int KeywordsInText { get; set; }
        public int KeywordsInMetaTags { get; set; }
    }
}
