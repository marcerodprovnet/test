using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProvNet.Core.Model
{
    public class Translation
    {
        public Translation(String language, string text)
        {
            Language = language;
            Text = text;
        }
        public string Language { get; set; }
        public string Text { get; set; }
    }
}
