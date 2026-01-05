using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Immutable;
using System.ComponentModel.Design;
using System.Runtime.Remoting.Metadata.W3cXsd2001;
using System.Drawing;

namespace Game_prototype_1
{
    static public class Config
    {
            // ---- Actions listed in the Planet screen ----
            public readonly static ImmutableList<string> ListedActions = ImmutableList.Create(new[] { "Build", "Demolish", "Upgrade", });
            // ---- Upgrade costs (index 1 = cost for L1→L2, index 2 = cost for L2→L3) ----
            public readonly static ImmutableArray<int> TitaniumMineUpgradeCosts = ImmutableArray.Create(new[] { 0, 20, 100, 0, 0 });
            public readonly static ImmutableArray<int> WaterPumpUpgradeCosts = ImmutableArray.Create(new[] { 0, 15, 80, 0 });
            public readonly static ImmutableArray<int> EnergyBrickGeneratorUpgradeCosts =ImmutableArray.Create(new[] { 0, 25, 120, 0 });
            public readonly static ImmutableArray<int> FarmUpgradeCosts = ImmutableArray.Create(new[] { 0, 30, 150, 0 });
            public readonly static ImmutableArray<int> ResearchLabUpgradeCosts = ImmutableArray.Create(new[] { 0, 40, 200, 0 });
            public readonly static ImmutableArray<int> HousingUpgradeCosts = ImmutableArray.Create(new[] { 0, 10, 50, 0 });
            // ---- Building costs ----
            public readonly static ImmutableArray<int> TitaniumMineBuildingCosts = ImmutableArray.Create(new[] { 100, 100, 5, 0, 0, 0 });
            public readonly static ImmutableArray<int> WaterPumpBuildingCosts = ImmutableArray.Create(new[] { 300, 200, 10, 0, 0, 0 });
            public readonly static ImmutableArray<int> EnergyBrickGeneratorBuildingCosts = ImmutableArray.Create(new[] { 200, 400, 20, 300, 12, 0 });
            public readonly static ImmutableArray<int> FarmBuildingCosts = ImmutableArray.Create(new[] { 200, 500, 10, 100, 4, 0 });
            public readonly static ImmutableArray<int> ResearchLabBuildingCosts = ImmutableArray.Create(new[] { 200, 400, 20, 300, 4 , 0 });
            public readonly static ImmutableArray<int> HousingBuildingCosts = ImmutableArray.Create(new[] { 200, 400, 20, 300, 4 , 0 });
            // ---- Tile layout ----
            public const int TileX = 400;
            public const int TileY = 500;
            // ---- Base production per tick ----
            public const int TitaniumBaseProduction = 2;
            public const int WaterBaseProduction = 3;
            public const int EnergyBricksBaseProduction = 4;
            public const int FoodBaseProduction = 5;
            public const int ResearchBaseProduction = 1;
            public const int PopulatiomBaseProduction = 1;
            // ---- Production multipliers ----
            public const int Level2Multiplier = 2;
            public const int Level3Multiplier = 3;
            // ---- Images for the Research page ----
            public const string Arrow = "M:\\Visual Studio 2022\\Graphing\\Graphing\\Arrow.png";
            // ---- Values for Perlin noise ----
            public const int TileSize = 100;
            public const string JSONFilter = "JSON Map (*.json)|*.json";
            // ---- Names of Resource producers ----
            public const string TitaniumFact = "Titanium Mine";
            public const string WaterFact = "Water Pump";
            public const string EnergyBrickFact = "Energy Brick Generator";
            public const string FoodFact = "Farm";
            public const string PopulationFact = "Housing";
            // ---- Names of Resource as strings and a list of them ----
            public enum TileType { Ocean, GrassLands, Forest, Desert, Mountains }
            public enum ResourceType { Titanium, Water, EnergyBricks, Food, Population, Research }
            public const string TitaniumName = "Titanium";
            public const string WaterName = "Water";
            public const string EnergyBricksName = "EnergyBricks";
            public const string FoodName = "Food";
            public const string PopulationName = "Population";
            public const string ResearchName = "Research";
            public readonly static  ImmutableList<string> ResourceNames = ImmutableList.Create(new[] { TitaniumName, WaterName, EnergyBricksName, FoodName, PopulationName, ResearchName });
            public const int TitaniumStartingValue = 1000;
            public const int WaterStartingValue = 1000;
            public const int EnergyBricksStartingValue = 1000; 
            public const int FoodStartingValue = 1000;
            public const int PopulationStartingValue = 50;
            public const int ResearchStartingValue = 0;
            public static readonly Color Blue = Color.FromArgb(68, 138, 255);
            public static readonly Color LightGreen = Color.FromArgb(120, 200, 80);
            public static readonly Color DarkGreen = Color.FromArgb(34, 139, 34);
            public static readonly Color Yellow = Color.FromArgb(194, 178, 128);
            public static readonly Color Brown = Color.FromArgb(120, 120, 120);
    }
}


