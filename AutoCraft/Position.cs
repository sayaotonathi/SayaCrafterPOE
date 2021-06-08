using CsvHelper.Configuration.Attributes;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoCraft
{
    public class Position
    {

        [Name("改造X")]
        public int relAltX { get; set; } = 100; //改造X
        [Name("改造Y")]
        public int relAltY { get; set; } = 250; //改造Y
        [Name("增幅X")]
        public int relAugX { get; set; } = 200; //增幅X
        [Name("增幅Y")]
        public int relAugY { get; set; } = 300; //增幅Y
        [Name("機會X")]
        public int relChanceX { get; set; } = 200; //機會X
        [Name("機會Y")]
        public int relChanceY { get; set; } = 250; //機會Y
        [Name("重鑄X")]
        public int relScourX { get; set; } = 150; //重鑄X
        [Name("重鑄Y")]
        public int relScourY { get; set; } = 400; //重鑄Y
        [Name("富豪X")]
        public int relRegalX { get; set; } = 360; //富豪X
        [Name("富豪Y")]
        public int relRegalY { get; set; } = 250; //富豪Y
        [Name("混沌X")]
        public int relChaosX { get; set; } = 460; //混沌X
        [Name("混沌Y")]
        public int relChaosY { get; set; } = 250; //混沌Y
        [Name("點金X")]
        public int relAlchX { get; set; } = 410; //點金X
        [Name("點金Y")]
        public int relAlchY { get; set; } = 250; //點金Y
        [Name("做裝區域X")]
        public int relCraftAreaX { get; set; } = 290; //做裝區域X
        [Name("做裝區域Y")]
        public int relCraftAreaY { get; set; } = 408; //做裝區域Y
        [Name("蛻變X")]
        public int relTransX { get; set; } = 50; //做裝區域X
        [Name("蛻變Y")]
        public int relTransY { get; set; } = 250; //做裝區域Y
        [Name("Index")]
        public int Index { get; set; } = 0;

    }
}
