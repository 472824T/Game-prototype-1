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
        public TitaniumResource(int value) : base("Titanium", value) { }
    }
    public class WaterResource : GameResource
    {
        public WaterResource(int value) : base("Water", value) { }
    }
    public class EnergyBricksResource : GameResource
    {
        public EnergyBricksResource(int value) : base("EnergyBricks", value) { }
    }
    public class FoodResource : GameResource
    {
        public FoodResource(int value) : base("Food", value) { }
    }
    public class ResearchResource : GameResource
    {
        public ResearchResource(int value) : base("Research", value) { }
    }

}
