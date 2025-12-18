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
            static public int PlayerTitaniumValue;
            static public int PlayerWaterValue;
            static public int PlayerEnergyBricksValue;
            static public int PlayerFoodValue;
            static public int PlayerPopulationValue;
            static public int PlayerResearchValue;
            static public int AIPlayerCount = 0;    
            static public Dictionary<int, AIPlayer> AIplayerDict;
            static GameResourceManager()
            {
                factories = new List<GameResourceFactory>();
                PlayerTitaniumValue = Config.TitaniumStartingValue; 
                PlayerWaterValue = Config.WaterStartingValue; 
                PlayerEnergyBricksValue = Config.EnergyBricksStartingValue; 
                PlayerFoodValue = Config.FoodStartingValue; 
                PlayerPopulationValue = Config.PopulationStartingValue;
                PlayerResearchValue = Config.ResearchStartingValue;
                AIplayerDict = new Dictionary<int, AIPlayer>(); 
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
                            PlayerTitaniumValue += res.Value; 
                            break;
                        case Config.WaterName: 
                            PlayerWaterValue += res.Value; 
                            break;
                        case Config.EnergyBricksName: 
                            PlayerEnergyBricksValue += res.Value; 
                            break;
                        case Config.FoodName: 
                            PlayerFoodValue += res.Value; 
                            break;
                        case Config.PopulationName:
                            PlayerPopulationValue += res.Value;
                            break;
                        case Config.ResearchName: 
                            PlayerResearchValue += res.Value; 
                            break;
                    }
                }
                OnGameStateChanged();
            }
            static public void AiPlayers()
            {
                AIPlayer ai1 = new AIPlayer("Ai"+AIPlayerCount.ToString());
                AIPlayerCount++ ;
                AIplayerDict.Add(AIPlayerCount, ai1);
                foreach(AIPlayer Ai in AIplayerDict.Values)
                { 
                    Ai.DisplayInfo();
            }

            }
            static public void AddFactory(GameResourceFactory Afact) 
            {
                factories.Add(Afact);
            }
            static public void RemoveFactory(GameResourceFactory Dfact)
            {
                factories.Remove(Dfact);
            }
            static public void UpgradeFactory(GameResourceFactory Ufact) 
            {
               
                  Ufact.Upgrade();
              
                
            }
            static public int GetResourceAmount(string name)
            {
                switch (name)
                {
                    case Config.TitaniumName: 
                        return PlayerTitaniumValue;
                       
                    case Config.WaterName: 
                        return PlayerWaterValue;

                    case Config.EnergyBricksName:
                        return PlayerEnergyBricksValue;

                    case Config.FoodName: 
                        return PlayerFoodValue;

                    case Config.PopulationName:
                        return PlayerPopulationValue;

                    case Config.ResearchName: 
                        return PlayerResearchValue;

                    default: 
                        return 0;
                }
            }
            static public void DeductResource(string name, int amount)
            {
                switch (name)
                {
                    case Config.TitaniumName: 
                        PlayerTitaniumValue -= amount; 
                        break;

                    case Config.WaterName:
                        PlayerWaterValue -= amount; 
                        break;

                    case Config.EnergyBricksName: 
                        PlayerEnergyBricksValue -= amount; 
                        break;

                    case Config.FoodName: 
                        PlayerFoodValue -= amount; 
                        break;
                    case Config.PopulationName:
                        PlayerPopulationValue -= amount;                        
                        break;

                    case Config.ResearchName: 
                        PlayerResearchValue -= amount; 
                        break;
                }
                OnGameStateChanged();
            }
            static public void AddResource(string name, int amount)
            {
                switch (name)
                {
                    case Config.TitaniumName:
                        PlayerTitaniumValue += amount;
                        break;

                    case Config.WaterName:
                        PlayerWaterValue += amount;
                        break;

                    case Config.EnergyBricksName:
                        PlayerEnergyBricksValue += amount;
                        break;

                    case Config.FoodName:
                        PlayerFoodValue += amount;
                        break;

                    case Config.PopulationName:
                        PlayerPopulationValue += amount;
                        break;

                    case Config.ResearchName:
                        PlayerResearchValue += amount;
                        break;
                }
                OnGameStateChanged();
            }
            static public void ResetAll()
            {   
                factories.Clear();
                PlayerTitaniumValue = 0; 
                PlayerWaterValue = 0; 
                PlayerEnergyBricksValue = 0; 
                PlayerFoodValue = 0; 
                PlayerPopulationValue = 0;
                PlayerResearchValue = 0;
                OnGameStateChanged();
            }
        }
    }

