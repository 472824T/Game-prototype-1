using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game_prototype_1
{
     public class GameResource
    {
        public string Name { get; private set; }
        public int Value { get; set; }
        public GameResource(string name, int value)
        {
            Name = name;
            Value = value;
        }
    }
    public class TitaniumResource : GameResource
    {
        public TitaniumResource(int value) : base(Config.TitaniumName, value) { }
    }
    public class WaterResource : GameResource
    {
        public WaterResource(int value) : base(Config.WaterName, value) { }
    }
    public class EnergyBricksResource : GameResource
    {
        public EnergyBricksResource(int value) : base(Config.EnergyBricksName, value) { }
    }
    public class FoodResource : GameResource
    {
        public FoodResource(int value) : base(Config.FoodName, value) { }
    }
    public class ResearchResource : GameResource
    {
        public ResearchResource(int value) : base(Config.ResearchName, value) { }
    }
    public class PopulationResource : GameResource
    {
        public PopulationResource(int value) : base(Config.PopulationName, value) { }
    }

}
