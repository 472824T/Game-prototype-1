using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

    namespace Game_prototype_1
    {
         static public class GameResourceManager
        {
            public static event EventHandler GameStateChanged;
            static public  PlanetScreen MainMenu;
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
                TitaniumValue = Config.TitaniumStartingValue; 
                WaterValue = Config.WaterStartingValue; 
                EnergyBricksValue = Config.EnergyBricksStartingValue; 
                FoodValue = Config.FoodStartingValue; 
                PopulationValue = Config.PopulationStartingValue;
                ResearchValue = Config.ResearchStartingValue;
            }
            private static void OnGameStateChanged()
            {
                if (GameStateChanged != null)
                {
                    GameStateChanged(null, EventArgs.Empty);
                }
            GameStateChanged?.Invoke(null, EventArgs.Empty);
            }
            static public void Tick()
            {
                foreach (GameResourceFactory fact in factories)
                {
                    GameResource res = fact.Tick();
                    switch (res.Name)
                    {
                        case Config.TitaniumName: 
                            TitaniumValue += res.Value; 
                            break;
                        case Config.WaterName: 
                            WaterValue += res.Value; 
                            break;
                        case Config.EnergyBricksName: 
                            EnergyBricksValue += res.Value; 
                            break;
                        case Config.FoodName: 
                            FoodValue += res.Value; 
                            break;
                        case Config.PopulationName:
                            PopulationValue += res.Value;
                            break;
                        case Config.ResearchName: 
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
                    case Config.TitaniumName: 
                        return TitaniumValue;
                       
                    case Config.WaterName: 
                        return WaterValue;

                    case Config.EnergyBricksName:
                        return EnergyBricksValue;

                    case Config.FoodName: 
                        return FoodValue;

                    case Config.PopulationName:
                        return PopulationValue;

                    case Config.ResearchName: 
                        return ResearchValue;

                    default: 
                        return 0;
                }
            }
            static public void DeductResource(string name, int amount)
            {
                switch (name)
                {
                    case Config.TitaniumName: 
                        TitaniumValue -= amount; 
                        break;

                    case Config.WaterName:
                        WaterValue -= amount; 
                        break;

                    case Config.EnergyBricksName: 
                        EnergyBricksValue -= amount; 
                        break;

                    case Config.FoodName: 
                        FoodValue -= amount; 
                        break;
                    case Config.PopulationName:
                        PopulationValue -= amount;                        
                        break;

                    case Config.ResearchName: 
                        ResearchValue -= amount; 
                        break;
                }
                OnGameStateChanged();
            }
            static public void AddResource(string name, int amount)
            {
                switch (name)
                {
                    case Config.TitaniumName:
                        TitaniumValue += amount;
                        break;

                    case Config.WaterName:
                        WaterValue += amount;
                        break;

                    case Config.EnergyBricksName:
                        EnergyBricksValue += amount;
                        break;

                    case Config.FoodName:
                        FoodValue += amount;
                        break;

                    case Config.PopulationName:
                        PopulationValue += amount;
                        break;

                    case Config.ResearchName:
                        ResearchValue += amount;
                        break;
                }
                OnGameStateChanged();
            }
            static public void ResetAll()
            {   
                factories.Clear();
                TitaniumValue = 0; 
                WaterValue = 0; 
                EnergyBricksValue = 0; 
                FoodValue = 0; 
                PopulationValue = 0;
                ResearchValue = 0;
                OnGameStateChanged();
            }
        }
    }

