using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
namespace Game_prototype_1
{
    using System.Collections.Generic;
    using System.Windows.Forms;

    namespace Game_prototype_1
    {
         static public class GameResourceManager
        {
            public static event EventHandler GameStateChanged;
            static public  MainMenu MainMenu;
            static private List<GameResourceFactory> factories;
            static public int TitaniumValue;
            static public int WaterValue;
            static public int EnergyBricksValue;
            static public int FoodValue;
            static public int PopulationValue;
            static public int ResearchValue;

            static GameResourceManager()
            {
                factories = new List<GameResourceFactory>();
                TitaniumValue = 0; 
                WaterValue = 0; 
                EnergyBricksValue = 0; 
                FoodValue = 0; 
                ResearchValue = 0;
               
               
            }
            private static void OnGameStateChanged()
            {
                if (GameStateChanged != null)
                {
                    GameStateChanged(null, EventArgs.Empty);
                }
            }
            static public void Tick()
            {
                foreach (GameResourceFactory fact in factories)
                {
                    GameResource res = fact.Tick();
                    switch (res.Name)
                    {
                        case "Titanium": 
                            TitaniumValue += res.Value; 
                            break;
                        case "Water": 
                            WaterValue += res.Value; 
                            break;
                        case "EnergyBricks": 
                            EnergyBricksValue += res.Value; 
                            break;
                        case "Food": 
                            FoodValue += res.Value; 
                            break;
                        case "Population":
                            PopulationValue += res.Value;
                            break;
                        case "Research": 
                            ResearchValue += res.Value; 
                            break;
                    }
                    
                }
                OnGameStateChanged();
            }


            static public void AddFactory(GameResourceFactory Afact) 
            {
                factories.Add(Afact);
            }
            static public void RemoveFactory(GameResourceFactory Dfact)
            {
                factories.Remove(Dfact);
            }
            static public void UpgradeFactory(int index) 
            {
                if (index >= 0 && index < factories.Count)
                {
                    factories[index].Upgrade();
                }
            }

            static public int GetResourceAmount(string name)
            {
                switch (name)
                {
                    case "Titanium": 
                        return TitaniumValue;
                       
                    case "Water": 
                        return WaterValue;

                    case "EnergyBricks":
                        return EnergyBricksValue;

                    case "Food": 
                        return FoodValue;

                    case "Research": 
                        return ResearchValue;

                    default: 
                        return 0;
                }
            }

            static public void DeductResource(string name, int amount)
            {
                switch (name)
                {
                    case "Titanium": 
                        TitaniumValue -= amount; 

                        break;

                    case "Water":
                        WaterValue -= amount; 

                        break;

                    case "EnergyBricks": 
                        EnergyBricksValue -= amount; 

                        break;

                    case "Food": 
                        FoodValue -= amount; 

                        break;

                    case "Research": 
                        ResearchValue -= amount; 

                        break;
                }
                OnGameStateChanged();
            }

            static public void ResetAll()
            {
                TitaniumValue = 0; 
                WaterValue = 0; 
                EnergyBricksValue = 0; 
                FoodValue = 0; 
                ResearchValue = 0;
                OnGameStateChanged();
            }
        }
    }
}
