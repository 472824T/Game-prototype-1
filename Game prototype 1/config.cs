using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Immutable;
using System.ComponentModel.Design;

namespace Game_prototype_1
{
    public class Config
    {
        // for upgrades
        public readonly static ImmutableArray<int> TitaniumUpgrades = ImmutableArray.Create(new[] { 0, 5, 50, 200 });
        public readonly static ImmutableList<string> ListedActions = ImmutableList.Create(new[] { "Factory", "Demolish", "Upgrade", });
        // Testin tiles placment 
        public const int TileX = 400;
        public const int TileY = 500;
        // Base amount for each of the resources stuff that they have 
        //Titanium
        public const int TitaniumBaseAmount = 100;
        public const int TitaniumNumOfStand1s = 0;
        public const int TitaniumNumOfStand2s = 0;
        public const int TitaniumNumOfStand3s = 0;
        //EnergyBricks
        public const int EnergyBricksBaseAmount = 100;
        public const int EnergyBricksNumOfStand1s = 0;
        public const int EnergyBricksNumOfStand2s = 0;
        public const int EnergyBricksNumOfStand3s = 0;

        // Stand orgnisation for how much they produce  
        // Titanium 
        public const int StandTitanium1 = 2;
        public const int StandTitanium2 = 4;
        public const int StandTitanium3 = 8;

    }
}
