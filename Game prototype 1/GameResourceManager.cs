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
            static private List<GameResourceFactory> factories;

            static private int TitaniumValue;
            static private int WaterValue;
            static private int EnergyBricksValue;
            static private int FoodValue;
            static private int ResearchValue;

            static private Label TitaniumLabel;
            static private Label WaterLabel;
            static private Label EnergyBricksLabel;
            static private Label FoodLabel;
            static private Label ResearchLabel;

            static GameResourceManager()
            {
                factories = new List<GameResourceFactory>();
                TitaniumValue = 0; 
                WaterValue = 0; 
                EnergyBricksValue = 0; 
                FoodValue = 0; 
                ResearchValue = 0;
               
                UpdateLabels();
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
                        case "Research": 
                            ResearchValue += res.Value; 
                            break;
                    }
                }
                UpdateLabels();
            }

            static private void UpdateLabels()
            {
                if (TitaniumLabel != null)
                {
                    TitaniumLabel.Text = TitaniumValue.ToString();
                }
                if (WaterLabel != null)
                { 
                    WaterLabel.Text = WaterValue.ToString(); 
                }
                if (EnergyBricksLabel != null)
                {
                    EnergyBricksLabel.Text = EnergyBricksValue.ToString(); 
                }
                if (FoodLabel != null)
                { 
                    FoodLabel.Text = FoodValue.ToString(); 
                }
                if (ResearchLabel != null)
                {
                    ResearchLabel.Text = ResearchValue.ToString();
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
                UpdateLabels();
            }

            static public void ResetAll()
            {
                TitaniumValue = 0; 
                WaterValue = 0; 
                EnergyBricksValue = 0; 
                FoodValue = 0; 
                ResearchValue = 0;
                UpdateLabels();
            }
        }
    }
}
