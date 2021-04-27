using CsvHelper.Configuration.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoCraft
{
    public class Affix
    {
        [Name("詞綴")]
        public string AffixName { get; set; }
        [Name("下限")]
        public string AffixMin { get; set; } = "0";
        [Name("上限")]
        public string AffixMax { get; set; } = "0";
        [Name("選取")]
        public bool IsSelected { get; set; } = false;
    }
}
