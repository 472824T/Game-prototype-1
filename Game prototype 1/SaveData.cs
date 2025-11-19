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
        public class MapSaveData
        {
            public int Columns { get; set; }
            public int Rows { get; set; }
            public int Seed { get; set; }
            public float NoiseScale { get; set; }
            public List<TileInfo> Tiles { get; set; }
        }
        public class ResourceSaveData
        {
           
            public int TitaniumValue { get; set; }
            public int WaterValue { get; set; }
            public int EnergyBricksValue { get; set; }
            public int FoodValue { get; set; }
            public int PopulationValue { get; set; }
            public int ResearchValue { get; set; }
        }

    }
}
