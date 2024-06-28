using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiaryManager
{
    public class Entry
    {
        public string Date { get; set; }
        public string Content { get; set; }

        public override string ToString()
        {
            return $"\n{Date}:\n{Content}";
        }
    }
}
