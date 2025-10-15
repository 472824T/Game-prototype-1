using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game_prototype_1
{
    public class GameResourceFactory
    {
        public string FactoryType { get; set; }
        protected int Level { get; set; }
        public GameResourceFactory(string type, int level = 1)
        {
            FactoryType = type;
            Level = level;
        }
        public virtual GameResource Tick()
        {
            return new GameResource("None", 0);
        }
    }
    public class TitaniumResourceFactory : GameResourceFactory
    {
        public TitaniumResourceFactory(int level = 1) : base("Titanium", level) { }
        public override GameResource Tick()
        {
            int amount;
            switch (Level)
            {
                case 1: 
                    amount = Config.StandTitanium1; 
                    break;
                case 2: 
                    amount = Config.StandTitanium2; 
                    break;
                case 3: 
                    amount = Config.StandTitanium3;
                    break;
                default: 
                    amount = 0;
                    break;
            }
            return new TitaniumResource(amount);
        }
    }
    public class WaterResourceFactory : GameResourceFactory
    {
        public WaterResourceFactory(int level = 1) : base("Water", level) { }
        public override GameResource Tick()
        {
            return new WaterResource(2 * Level);
        }
    }
    public class EnergyBricksResourceFactory : GameResourceFactory
    {
        public EnergyBricksResourceFactory(int level = 1) : base("EnergyBricks", level) { }
        public override GameResource Tick()
        {
            return new EnergyBricksResource(3 * Level);
        }
    }
    public class FoodResourceFactory : GameResourceFactory
    {
        public FoodResourceFactory(int level = 1) : base("Food", level) { }
        public override GameResource Tick()
        {
            return new FoodResource(4 * Level);
        }
    }
    public class ResearchResourceFactory : GameResourceFactory
    {
        public ResearchResourceFactory(int level = 1) : base("Research", level) { }
        public override GameResource Tick()
        {
            return new ResearchResource(1 * Level);
        }
    }
}
