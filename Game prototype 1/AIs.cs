using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Game_prototype_1

{

    public static class AIManager
    {
        private static int aiCount = 0;
        private static Random rng = new Random();
        public static void SpawnAndRun(List<Button> tileButtons)
        {
            aiCount++;
            var player = new AIPlayer($"AI_{aiCount}");
            player.TakeTurn(tileButtons);
        }
    }
    public class AIPlayer
    {
        public string Name { get; }
        private Random rng = new Random();
        public AIPlayer(string name)
        {
            Name = name;
        }
        public void DisplayInfo()
        {
            MessageBox.Show($"AI Name:{Name} ");
        }
        public void TakeTurn(List<Button> tileButtons)
        {
            if (tileButtons == null || tileButtons.Count == 0) 
                return;
            List<PerlinGen.TileInfo> tiles = tileButtons.Select(b => b.Tag).OfType<PerlinGen.TileInfo>().ToList();

            if (!tiles.Any())
                return;

            string factoryToBuild = ChooseFactoryType();

            if (factoryToBuild == null) 
                return;

            List<PerlinGen.TileInfo> candidates = tiles.Where(t => !t.HasFactory && IsTileSuitableForFactory(t, factoryToBuild)).ToList();

            if (!candidates.Any()) 
                return;

            var best = candidates

        .OrderByDescending(t => ScoreTileForFactory(t, factoryToBuild))

        .ThenBy(_ => rng.Next())

        .FirstOrDefault();

            if (best == null)
                return;

            if (!CanAffordFactory(factoryToBuild))
                return;

            DeductFactoryCosts(factoryToBuild);
            GameResourceFactory factoryInstance = CreateFactoryInstance(factoryToBuild);

            if (factoryInstance == null) 
                return;

            best.HasFactory = true;
            best.FactoryType = factoryToBuild;
            best.Level = 1;
            best.Factory = factoryInstance;
            GameResourceManager.AddFactory(factoryInstance);

            Button button = tileButtons.FirstOrDefault(b => b.Tag == best);

            if (button != null)

            {
                button.BackColor = GetFactoryColor(factoryToBuild);
                button.Text = $"{best.FactoryType} L1";
            }
        }
        private string ChooseFactoryType()
        {
            Dictionary<string, int> resources = new Dictionary<string, int>
            {
                { Config.TitaniumName, GameResourceManager.GetResourceAmount(Config.TitaniumName) },
                { Config.WaterName, GameResourceManager.GetResourceAmount(Config.WaterName) },
                { Config.EnergyBricksName, GameResourceManager.GetResourceAmount(Config.EnergyBricksName) },
                { Config.FoodName, GameResourceManager.GetResourceAmount(Config.FoodName) },
                { Config.PopulationName, GameResourceManager.GetResourceAmount(Config.PopulationName) }
            };
            string lowest = resources.OrderBy(kv => kv.Value).First().Key;

            switch (lowest)
            {
                case string s when s == Config.TitaniumName:
                    return Config.TitaniumFact;

                case string s2 when s2 == Config.WaterName:
                    return Config.WaterFact;

                case string s3 when s3 == Config.EnergyBricksName:
                    return Config.EnergyBrickFact;

                case string s4 when s4 == Config.FoodName:
                    return Config.FoodFact;

                case string s5 when s5 == Config.PopulationName:
                    return Config.PopulationFact;

                default:
                    return Config.TitaniumFact;
            }
        }
        private bool IsTileSuitableForFactory(PerlinGen.TileInfo tile, string factoryType)
        {         
            switch (factoryType)
            {
                case string s when s == Config.TitaniumFact:

                    // Prefer Mountains, allow Grasslands/Desert 

                    return tile.Type == Config.TileType.Mountains || tile.Type == Config.TileType.GrassLands || tile.Type == Config.TileType.Desert;

                case string s2 when s2 == Config.WaterFact:

                    // Prefer Ocean or GrassLands 

                    return tile.Type == Config.TileType.Ocean || tile.Type == Config.TileType.GrassLands || tile.Type == Config.TileType.Forest;

                case string s3 when s3 == Config.EnergyBrickFact:

                    // Prefer Desert/GrassLands 

                    return tile.Type == Config.TileType.Desert || tile.Type == Config.TileType.GrassLands;

                case string s4 when s4 == Config.FoodFact:

                    // Prefer GrassLands/Forest 

                    return tile.Type == Config.TileType.GrassLands || tile.Type == Config.TileType.Forest;

                case string s5 when s5 == Config.PopulationFact:

                    // Prefer non-ocean 

                    return tile.Type != Config.TileType.Ocean;

                default:

                    return false;
            }
        }
        
        private bool CanAffordFactory(string factoryType)
        {
            int i = 0;
            ImmutableArray<int> costs;

            switch (factoryType)
            {
                case string s when s == Config.TitaniumFact:
                    costs = Config.TitaniumMineBuildingCosts;
                    break;

                case string s2 when s2 == Config.WaterFact:
                    costs = Config.WaterPumpBuildingCosts;
                    break;

                case string s3 when s3 == Config.EnergyBrickFact:
                    costs = Config.EnergyBrickGeneratorBuildingCosts;
                    break;

                case string s4 when s4 == Config.FoodFact:
                    costs = Config.FarmBuildingCosts;
                    break;

                case string s5 when s5 == Config.PopulationFact:
                    costs = Config.HousingBuildingCosts;
                    break;

                default:
                    return false;
            }
            foreach (int cost in costs)
            {
                int have = GameResourceManager.GetResourceAmount(Config.ResourceNames[i]);
                if (have < cost) return false;
                i++;
            }
            return true;
        }
        private void DeductFactoryCosts(string factoryType)
        {
            int i = 0;
            ImmutableArray<int> costs;
            switch (factoryType)
            {
                case string s when s == Config.TitaniumFact:
                    costs = Config.TitaniumMineBuildingCosts;
                    break;

                case string s2 when s2 == Config.WaterFact:
                    costs = Config.WaterPumpBuildingCosts;
                    break;

                case string s3 when s3 == Config.EnergyBrickFact:
                    costs = Config.EnergyBrickGeneratorBuildingCosts;
                    break;

                case string s4 when s4 == Config.FoodFact:
                    costs = Config.FarmBuildingCosts;
                    break;

                case string s5 when s5 == Config.PopulationFact:
                    costs = Config.HousingBuildingCosts;
                    break;

                default:
                    return;

            }

            foreach (int cost in costs)
            {
                GameResourceManager.DeductResource(Config.ResourceNames[i], cost);
                i++;
            }
        }



        private GameResourceFactory CreateFactoryInstance(string factoryType)
        {
            switch (factoryType)
            {
                case string s when s == Config.TitaniumFact:
                    return new TitaniumFactory(1);

                case string s2 when s2 == Config.WaterFact:
                    return new WaterFactory(1);

                case string s3 when s3 == Config.EnergyBrickFact:
                    return new EnergyBricksFactory(1);

                case string s4 when s4 == Config.FoodFact:
                    return new FarmFactory(1);

                case string s5 when s5 == Config.PopulationFact:
                    return new PopulationFactory(1);

                default:
                    return null;

            }

        }



        private System.Drawing.Color GetFactoryColor(string factoryType)
        {
            switch (factoryType)
            {
                case string s when s == Config.TitaniumFact:
                    return System.Drawing.Color.DarkGray;

                case string s2 when s2 == Config.WaterFact:
                    return System.Drawing.Color.LightBlue;

                case string s3 when s3 == Config.EnergyBrickFact:
                    return System.Drawing.Color.Orange;

                case string s4 when s4 == Config.FoodFact:
                    return System.Drawing.Color.Green;

                case string s5 when s5 == Config.PopulationFact:
                    return System.Drawing.Color.MediumPurple;

                default:
                    return System.Drawing.Color.White;

            }

        }

    }

}