using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
namespace Game_prototype_1
{
    public class GameResourceManager
    {
        private List<GameResourceFactory> factories;
        private int TitaniumValue;
        private int WaterValue;
        private int EnergyBricksValue;
        private int FoodValue;
        private int ResearchValue;
        private Label TitaniumLabel;
        private Label WaterLabel;
        private Label EnergyBricksLabel;
        private Label FoodLabel;
        private Label ResearchLabel;
        public GameResourceManager(Label titaniumLabel, Label waterLabel,
            Label energyLabel, Label foodLabel, Label researchLabel)
        {
            factories = new List<GameResourceFactory>();
            TitaniumValue = 0;
            WaterValue = 0;
            EnergyBricksValue = 0;
            FoodValue = 0;
            ResearchValue = 0;
            TitaniumLabel = titaniumLabel;
            WaterLabel = waterLabel;
            EnergyBricksLabel = energyLabel;
            FoodLabel = foodLabel;
            ResearchLabel = researchLabel;
        }
        public void Tick()
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
            TitaniumLabel.Text = TitaniumValue.ToString();
            WaterLabel.Text = WaterValue.ToString();
            EnergyBricksLabel.Text = EnergyBricksValue.ToString();
            FoodLabel.Text = FoodValue.ToString();
            ResearchLabel.Text = ResearchValue.ToString();
        }
        public void AddFactory(GameResourceFactory fact)
        {
            factories.Add(fact);
        }
        public int GetResourceAmount(string name)
        {
            switch (name)
            {
                case "Titanium": return TitaniumValue;
                case "Water": return WaterValue;
                case "EnergyBricks": return EnergyBricksValue;
                case "Food": return FoodValue;
                case "Research": return ResearchValue;
                default: return 0;
            }
        }
        public void DeductResource(string name, int amount)
        {
            switch (name)
            {
                case "Titanium": TitaniumValue -= amount; break;
                case "Water": WaterValue -= amount; break;
                case "EnergyBricks": EnergyBricksValue -= amount; break;
                case "Food": FoodValue -= amount; break;
                case "Research": ResearchValue -= amount; break;
            }
        }
    }
}