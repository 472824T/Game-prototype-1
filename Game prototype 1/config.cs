using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Immutable;

namespace Game_prototype_1
{
    internal static class config
    {
        
        public readonly static ImmutableArray<int> TitaniumUpgrades = ImmutableArray.Create(new[] { 0, 5, 50, 200 });
    }
}
