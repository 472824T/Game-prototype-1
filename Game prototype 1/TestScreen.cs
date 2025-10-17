using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Diagnostics.Eventing.Reader;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Security.Cryptography.X509Certificates;
using System.Windows.Forms;

namespace Game_prototype_1
{
    public partial class TestScreen : Form
    {
      
            GameResourceManager resourceManager;
            List<Button> TileButtons = new List<Button>();
            List<bool> Factory = new List<bool>();
        List<string> FactoryTypes = new List<string>();
         List<int> FactoryLevels = new List<int>();
        void GenOfTilePicBoxs(int WantedNum = 0)
            {
                int X = 0;
                for (int i = 0; i < WantedNum; i++)
                {
                    TileButtons.Add(new Button());
                    TileButtons[i].Name = i.ToString();
                    TileButtons[i].Location = new Point(Config.TileX + X, Config.TileY);
                    TileButtons[i].Size = new Size(200, 200);
                    TileButtons[i].TabIndex = 1;
                    TileButtons[i].BackColor = Color.Yellow;
                    TileButtons[i].Text = "Standard1";
                    TileButtons[i].Enabled = true;
                    TileButtons[i].Click += new EventHandler(MyClickEvent);
                    this.Controls.Add(TileButtons[i]);
                    X += 200;
                    Factory.Add(false);
                }
            }
            void newgame(bool x = false)
            {
                if (x)
                {
                    TCSelectionButton.Show();
                    WCSelectionButton.Show();
                    TLButton.Show();
                    TTSelectionButton.Show();
                    Title_label.Hide();
                    ContinueButton.Hide();
                    New_Game_button.Hide();
                    LoadGameButton.Hide();
                    OptionsButton.Hide();
                }
                else
                {
                    TCSelectionButton.Hide();
                    WCSelectionButton.Hide();
                    TLButton.Hide();
                    TTSelectionButton.Hide();
                    Title_label.Hide();
                    ContinueButton.Hide();
                    New_Game_button.Hide();
                    LoadGameButton.Hide();
                    OptionsButton.Hide();
                    ListOfBuildAction.Show();
                    LabelTitaniumCount.Show();
                    LabelWaterCount.Show();
                    LabelEnergyBricksCount.Show();
                    LabelFoodCount.Show();
                    LabelResearchCount.Show();
                    LabelTextTitanium.Show();
                    LabelTextEnergyBricks.Show();
                    LabelTextFood.Show();
                    LabelTextWater.Show();
                    LabelTextResearchPoints.Show();

                    ListedBoxLoader();
                    resourceManager = new GameResourceManager(
                        LabelTitaniumCount,
                        LabelWaterCount,
                        LabelEnergyBricksCount,
                        LabelFoodCount,
                        LabelResearchCount
                    );
                }
            }
            public TestScreen()
            {
                InitializeComponent();
            }
            void ListedBoxLoader()
            {
                foreach (string I in Config.ListedActions)
                    ListOfBuildAction.Items.Add(I);
            }
            private void ContinueButton_Click(object sender, EventArgs e)
            {
                PrototypeErrorMessage.Show();
            }
            private void New_Game_button_Click(object sender, EventArgs e)
            {
                PrototypeErrorMessage.Hide();
                newgame();
                GenOfTilePicBoxs(5);
            }
            void TileChecker(int IndexOfButton)
            {
                if (ListOfBuildAction.SelectedIndex > -1)
                {
                    switch (Config.ListedActions[ListOfBuildAction.SelectedIndex])
                    {
                        case "Factory":
                            TileButtons[IndexOfButton].BackColor = Color.Green;
                            break;
                        case "Demolish":
                            TileButtons[IndexOfButton].BackColor = Color.Yellow;
                            Factory[IndexOfButton] = false;
                            break;
                    case "Upgrade":
                        if (FactoryTypes[IndexOfButton] != null && FactoryLevels[IndexOfButton] > 0)
                        {
                            int currentLevel = FactoryLevels[IndexOfButton];
                            if (currentLevel < 3)
                            {
                                int cost = 0;
                                switch (FactoryTypes[IndexOfButton])
                                {
                                    case "Titanium Mine":
                                        cost = Config.TitaniumMineUpgradeCosts[currentLevel];
                                        break;
                                    case "Water Pump":
                                        cost = Config.WaterPumpUpgradeCosts[currentLevel];
                                        break;
                                    case "Energy Brick Generator":
                                        cost = Config.EnergyBrickGeneratorUpgradeCosts[currentLevel];
                                        break;
                                    case "Farm":
                                        cost = Config.FarmUpgradeCosts[currentLevel];
                                        break;
                                    case "Research Lab":
                                        cost = Config.ResearchLabUpgradeCosts[currentLevel];
                                        break;
                                }
                                int currentTitanium = resourceManager.GetResourceAmount("Titanium");
                                if (currentTitanium >= cost)
                                {
                                    resourceManager.DeductResource("Titanium", cost);
                                    FactoryLevels[IndexOfButton] = currentLevel + 1;
                                    TileButtons[IndexOfButton].Text =
                                        FactoryTypes[IndexOfButton] + " L" + FactoryLevels[IndexOfButton];
                                    resourceManager.UpgradeFactory(IndexOfButton);
                                }
                                else
                                {
                                    MessageBox.Show("Not enough Titanium to upgrade! Need " + cost + ".");
                                }
                            }
                            else
                            {
                                MessageBox.Show(FactoryTypes[IndexOfButton] + " is already max level!");
                            }
                        }
                        else
                        {
                            MessageBox.Show("No factory here to upgrade!");
                        }
                        break;
                }
                }
                else
                {
                    MessageBox.Show("Please select what you would like to build (Factory for now).");
                }
            }
            int ButtonFinder(string NameOfButton)
            {
                for (int i = 0; i < TileButtons.Count; i++)
                    if (TileButtons[i].Name == NameOfButton)
                        return i;
                return -1;
            }
            void FactoryChecker(int IndexOfButton)
            {
                if (TileButtons[IndexOfButton].BackColor == Color.Green)
                {
                    Factory[IndexOfButton] = true;
                    resourceManager.AddFactory(new TitaniumResourceFactory(1));
                    resourceManager.AddFactory(new WaterResourceFactory(1));
                    resourceManager.AddFactory(new EnergyBricksResourceFactory(1));
                    resourceManager.AddFactory(new FoodResourceFactory(1));
                    resourceManager.AddFactory(new ResearchResourceFactory(1));
                }
            }
            private void MyClickEvent(object sender, EventArgs e)
            {
                int index = ButtonFinder(((Button)sender).Name);
                TileChecker(index);
                FactoryChecker(index);
            }
            private void ProductionTimer_Tick(object sender, EventArgs e)
            {
                resourceManager.Tick();
            }
        }
    }
