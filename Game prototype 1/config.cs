using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Immutable;
using System.ComponentModel.Design;

namespace Game_prototype_1
{
    static public class Config
    {
        // for upgrades
        public readonly static ImmutableArray<int> TitaniumUpgrades = ImmutableArray.Create(new[] { 0, 5, 50, 200 });
        public readonly static ImmutableList<string> ListedActions = ImmutableList.Create(new[] { "Factory", "Demolish", "Upgrade", });
 
            // ---- Upgrade costs (index 1 = cost for L1→L2, index 2 = cost for L2→L3) ----
            public readonly static ImmutableArray<int> TitaniumMineUpgradeCosts = ImmutableArray.Create(new[] { 0, 20, 100, 0 });
            public readonly static ImmutableArray<int> WaterPumpUpgradeCosts = ImmutableArray.Create(new[] { 0, 15, 80, 0 });
            public readonly static ImmutableArray<int> EnergyBrickGeneratorUpgradeCosts =ImmutableArray.Create(new[] { 0, 25, 120, 0 });
            public readonly static ImmutableArray<int> FarmUpgradeCosts = ImmutableArray.Create(new[] { 0, 30, 150, 0 });
            public readonly static ImmutableArray<int> ResearchLabUpgradeCosts = ImmutableArray.Create(new[] { 0, 40, 200, 0 });
            // ---- Tile layout ----
            public const int TileX = 400;
            public const int TileY = 500;
            // ---- Base production per tick ----
            public const int TitaniumBaseProduction = 2;
            public const int WaterBaseProduction = 3;
            public const int EnergyBricksBaseProduction = 4;
            public const int FoodBaseProduction = 5;
            public const int ResearchBaseProduction = 1;
            // ---- Production multipliers ----
            public const int Level2Multiplier = 2;
            public const int Level3Multiplier = 3;
            // ===== Images for the Research page
            public const string Arrow = "M:\\Visual Studio 2022\\Graphing\\Graphing\\Arrow.png";
            //------ Values for Perlin noise
            public const int TileSize = 100;

    }
}


