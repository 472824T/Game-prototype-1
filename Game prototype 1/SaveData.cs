using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Game_prototype_1.PerlinGen;

namespace Game_prototype_1
{
    internal class SaveData
    {
        public class FullGameSaveData
        {
            public int Columns { get; set; }
            public int Rows { get; set; }
            public int Seed { get; set; }
            public float NoiseScale { get; set; }
            public List<TileInfo> Tiles { get; set; }
            public int PlayerTitaniumValue { get; set; }
            public int PlayerWaterValue { get; set; }
            public int PlayerEnergyBricksValue { get; set; }
            public int PlayerFoodValue { get; set; }
            public int PlayerPopulationValue { get; set; }
            public int PlayerResearchValue { get; set; }
        }
    }
}
