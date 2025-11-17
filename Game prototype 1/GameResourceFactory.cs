using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game_prototype_1
{
    public abstract class GameResourceFactory
    {
        public string FactoryType { get; protected set; }
        public int Level { get; private set; }

        protected GameResourceFactory(string type, int level = 1)
        {
            FactoryType = type;
            Level = level;
        }

        public virtual void Upgrade()
        {
            if (Level < 3)
            {
                Level++;
            }
        }

        protected int Scale(int baseValue)
        {
            switch (Level)
            {
                case 1: 
                    return baseValue;
                case 2: 
                    return baseValue * Config.Level2Multiplier;
                case 3: 
                    return baseValue * Config.Level3Multiplier;
                default: 
                    return baseValue;
            }
        }

        public abstract GameResource Tick();
    }

    public class TitaniumFactory : GameResourceFactory
    {
        public TitaniumFactory(int level = 1) : base(Config.TitaniumFact, level) { }
        public override GameResource Tick() 
        { 
            return new TitaniumResource(Scale(Config.TitaniumBaseProduction)); 
        }
    }

    public class WaterFactory : GameResourceFactory
    {
        public WaterFactory(int level = 1) : base(Config.WaterFact, level) { }
        public override GameResource Tick() 
        { 
            return new WaterResource(Scale(Config.WaterBaseProduction)); 
        }
    }

    public class EnergyBricksFactory : GameResourceFactory
    {
        public EnergyBricksFactory(int level = 1) : base(Config.EnergyBrickFact, level) { }
        public override GameResource Tick() 
        { 
            return new EnergyBricksResource(Scale(Config.EnergyBricksBaseProduction)); 
        }
    }

    public class FarmFactory : GameResourceFactory
    {
        public FarmFactory(int level = 1) : base(Config.FoodFact, level) { }
        public override GameResource Tick() 
        {
            return new FoodResource(Scale(Config.FoodBaseProduction)); 
        }
    }

    public class ResearchFactory : GameResourceFactory
    {
        public ResearchFactory(int level = 1) : base(Config.PopulationFact, level) { }
        public override GameResource Tick() 
        { 
            return new ResearchResource(Scale(Config.ResearchBaseProduction)); 
        }
    }
    public class PopulationFactory : GameResourceFactory
    {
        public PopulationFactory(int level = 1) : base(Config.PopulationFact, level) { }
        public override GameResource Tick()
        {
            return new HousingResource(Scale(Config.PopulatiomBaseProduction));
        }
    }
}
