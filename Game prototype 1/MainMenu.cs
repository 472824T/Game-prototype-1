using Game_prototype_1.Game_prototype_1;
using Graphing;
using System;
using System.Collections.Generic;


using System.ComponentModel;
using System.Diagnostics;
using System.Diagnostics.Eventing.Reader;
using System.Drawing;

using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Cryptography.X509Certificates;
using System.Windows.Forms;



namespace Game_prototype_1
{
    public partial class MainMenu : Form
    {
    
        private Panel playPanel;
        private List<Button> TileButtons = new List<Button>();
        private List<bool> Factory = new List<bool>();
        private List<string> FactoryTypes = new List<string>();
        private List<int> FactoryLevels = new List<int>();
        private ListBox ListOfBuildAction;
        private Label LabelTitaniumCount = new Label();
        private Label LabelWaterCount = new Label();
        private Label LabelEnergyBricksCount = new Label();
        private Label LabelFoodCount = new Label();
        private Label LabelPopulationCount = new Label();
        private Label LabelResearchCount = new Label();


        // Factory selection and actions
        private ListBox FactoryTypeList;
        private string SelectedFactoryType = null;
        private const int TileSize = 100;
        private int GridCollums = 5;


        private bool buildMode = false;

        public MainMenu()
        {
            GameResourceManager.GameStateChanged += GameManager_GameStateChanged;
            InitializeComponent();
            BuildUI();
        }
       
        private void BuildUI()
        {
            Text = "Game - Prototype";
            ClientSize = new Size(1200, 800);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;

           
            Panel left = new Panel 
            { 
                 Location = new Point(10, 10), 
                 Size = new Size(260, ClientSize.Height - 20), 
                 BorderStyle = BorderStyle.FixedSingle, 
                 AutoScroll = true 
            };
            Controls.Add(left);
            int CollumY= 10;

            left.Controls.Add(
                new Label { Text = "Actions:", Location = new Point(10, CollumY) });
            CollumY+= 20;
            ListOfBuildAction = new CheckedListBox 
            { 
                Location = new Point(10, CollumY), 
                Size = new Size(220, 80) 
            }; 
                ListOfBuildAction.Items.AddRange(Config.ListedActions.ToArray()); 
                ListOfBuildAction.SelectedIndexChanged += ListOfBuildAction_SelectedIndexChanged; 
                left.Controls.Add(ListOfBuildAction);   
                CollumY+= 90;

            left.Controls.Add(new Label { Text = "Factory Types:", Location = new Point(10, CollumY) }); 
            CollumY+= 20;

            FactoryTypeList = new ListBox 
            { 
                Location = new Point(10, CollumY), 
                Size = new Size(220, 120) 
            };
            FactoryTypeList.Items.AddRange(new object[] 
            { 
                Config.TitaniumFact, 
                Config.WaterFact, 
                Config.EnergyBrickFact, 
                Config.FoodFact, 
                Config.PopulationFact 
            }
            );
            left.Controls.Add(FactoryTypeList);
            CollumY+= 130;
          
            Button btnBuildFactory = new Button
            {
                Text = "Build Factory Mode",
                Location = new Point(10, CollumY),
                Width = 220
            };
            left.Controls.Add(btnBuildFactory);
            CollumY+= 40;
            btnBuildFactory.Click += (s, e) =>
            {
                if (FactoryTypeList.SelectedIndex >= 0)
                    SelectedFactoryType = FactoryTypeList.SelectedItem.ToString();
            };
            btnBuildFactory.Click += (s, e) =>
            { 
                buildMode = !buildMode;
                btnBuildFactory.Text = buildMode ? "Exit Build Mode" : "Build Factory Mode";
            if (FactoryTypeList.SelectedIndex >= 0)
                SelectedFactoryType = FactoryTypeList.SelectedItem.ToString();
            };
            

            FactoryTypeList.SelectedIndexChanged += FactoryTypeList_SelectedIndexChanged;
            FactoryTypeList.Visible = true; 
            left.Controls.Add(FactoryTypeList); CollumY+= 140;

            left.Controls.Add(new Label 
            { 
                Text = "Resources:", 
                Location = new Point(10, CollumY), 
                Font = new Font("Segoe UI", 9, FontStyle.Bold) 
            }
            ); 
            CollumY+= 22;

            LabelTitaniumCount = new Label 
            { 
                Text = "0", 
                Location = new Point(10, CollumY), 
                AutoSize = true 
            }; 
            left.Controls.Add(new Label 
            { 
                Text = "Titanium:", 
                Location = new Point(10, CollumY) 
            }
            ); 
            LabelTitaniumCount.Location = new Point(120, CollumY); 
            left.Controls.Add(LabelTitaniumCount); 
            CollumY+= 22;

            LabelWaterCount = new Label 
            { 
                Text = "0", Location = new Point(120, CollumY), 
                AutoSize = true 
            }; 
            left.Controls.Add(new Label 
            { 
                Text = "Water:", 
                Location = new Point(10, CollumY) 
            }
            ); 
            LabelWaterCount.Location = new Point(120, CollumY); 
            left.Controls.Add(LabelWaterCount); 
            CollumY+= 22;

            LabelEnergyBricksCount = new Label 
            { 
                Text = "0", Location = new Point(120, CollumY), 
                AutoSize = true 
            }; 
            left.Controls.Add(new Label 
            { 
                Text = "Energy:", 
                Location = new Point(10, CollumY) 
            }
            ); 
            LabelEnergyBricksCount.Location = new Point(120, CollumY); 
            left.Controls.Add(LabelEnergyBricksCount); 
            CollumY+= 22;

            LabelFoodCount = new Label 
            { 
                Text = "0", 
                Location = new Point(120, CollumY), 
                AutoSize = true 
            }; 
            left.Controls.Add(new Label 
            { 
                Text = "Food:", 
                Location = new Point(10, CollumY) 
            }
            ); 
            LabelFoodCount.Location = new Point(120, CollumY); 
            left.Controls.Add(LabelFoodCount); 
            CollumY+= 22;

            LabelPopulationCount = new Label
            {
                Text = "0",
                Location = new Point(120, CollumY),
                AutoSize = true
            };
            left.Controls.Add(new Label
            {
                Text = "Population:",
                Location = new Point(10, CollumY)
            }
            );
            LabelPopulationCount.Location = new Point(120, CollumY);
            left.Controls.Add(LabelPopulationCount);
            CollumY += 22;


            LabelResearchCount = new Label 
            { 
                Text = "0",
                Location = new Point(120, CollumY), 
                AutoSize = true 
            }; 
            left.Controls.Add(new Label 
            { 
                Text = "Research:", 
                Location = new Point(10, CollumY) }
            ); 
            LabelResearchCount.Location = new Point(120, CollumY); 
            left.Controls.Add(LabelResearchCount); 
            CollumY+= 30;

            playPanel = new Panel 
            { 
                Location = new Point(280, 10), 
                Size = new Size(ClientSize.Width - 300, 
                ClientSize.Height - 20), 
                BorderStyle = BorderStyle.FixedSingle, 
                AutoScroll = true 
            };
            Controls.Add(playPanel);

            
            CreateTiles(5, 2); 
            GenOfTilePicBoxs(8);

            // production timer
            ProductionTimer = new Timer { Interval = 1000 };
            ProductionTimer.Tick += ProductionTimer_Tick;
            ProductionTimer.Start();
        }

        private void CreateTiles(int Collums, int rows)
        {
            TileButtons.Clear(); 
            Factory.Clear(); 
            FactoryTypes.Clear(); 
            FactoryLevels.Clear();

            Panel canvas = new Panel 
            { 
                Location = new Point(0, 0), 
                Size = new Size(Collums * TileSize, rows * TileSize) 
            };
            playPanel.Controls.Clear();
            playPanel.Controls.Add(canvas);

            for (int r = 0; r < rows; r++)
            {
                for (int c = 0; c < Collums; c++)
                {
                    Button b = new Button 
                    { 
                        Location = new Point(c * TileSize, r * TileSize), 
                        Size = new Size(TileSize - 2, TileSize - 2), 
                        BackColor = Color.Yellow, 
                        Text = "Empty", 
                        ForeColor = Color.Black, 
                        Tag = (c).ToString() 
                    };

                    b.Click += TileButton_Click;
                    canvas.Controls.Add(b);
                    TileButtons.Add(b);
                    Factory.Add(false);
                    FactoryTypes.Add(null);
                    FactoryLevels.Add(0);
                }
            }
        }

        private void GenOfTilePicBoxs(int WantedNum = 0)
        {
         
            CreateTiles(WantedNum, 1);
        }
        private void GameManager_GameStateChanged(object sender, EventArgs e)
        {
            UpdateDisplay();
        }
        private void UpdateDisplay()
        {
            LabelTitaniumCount.Text = GameResourceManager.TitaniumValue.ToString();
            LabelWaterCount.Text = GameResourceManager.WaterValue.ToString();
            LabelEnergyBricksCount.Text = GameResourceManager.EnergyBricksValue.ToString();
            LabelFoodCount.Text = GameResourceManager.FoodValue.ToString();
            LabelPopulationCount.Text = GameResourceManager.PopulationValue.ToString();
            LabelResearchCount.Text = GameResourceManager.ResearchValue.ToString();

        }
        private void TileButton_Click(object sender, EventArgs e)
        {
            {
                Button b = sender as Button;
                if (b == null) return;
                int index = TileButtons.IndexOf(b);
                if (index < 0) return;
                if (ListOfBuildAction.SelectedIndex > -1)
                {
                    string action = Config.ListedActions[ListOfBuildAction.SelectedIndex];
                    if (action == "Factory")
                    {
                    if (SelectedFactoryType == null)
                        {
                            MessageBox.Show("Select a factory type first from the list.");
                            return;
                        }
                  // Use unified SelectedFactoryType (was previously mismatched variable)
                   Factory[index] = true;
                        FactoryTypes[index] = SelectedFactoryType;
                        FactoryLevels[index] = 1;
                        b.BackColor = GetFactoryColor(SelectedFactoryType);
                        b.Text = SelectedFactoryType + " L1";


                        switch (SelectedFactoryType)
                        {
                            case Config.TitaniumFact:
                                GameResourceManager.AddFactory(new TitaniumFactory(1));
                                break;
                            case Config.WaterFact:
                                GameResourceManager.AddFactory(new WaterFactory(1));
                                break;
                            case Config.EnergyBrickFact:
                                GameResourceManager.AddFactory(new EnergyBricksFactory(1));
                                break;
                            case Config.FoodFact:
                                GameResourceManager.AddFactory(new FarmFactory(1));
                                break;
                            case Config.PopulationFact:
                                GameResourceManager.AddFactory(new PopulationFactory(1));
                                break;
                            
                        }


                        SelectedFactoryType = null;
                        FactoryTypeList.ClearSelected();
                    }
                    else if (action == "Demolish")
                    {
                        Factory[index] = false;
                        FactoryTypes[index] = null;
                        FactoryLevels[index] = 0;
                        b.BackColor = Color.Yellow;
                        b.Text = "Empty";


                        switch (FactoryTypes[index])
                        {
                            case Config.TitaniumFact:
                                GameResourceManager.RemoveFactory(new TitaniumFactory(1));
                                break;

                            case Config.WaterFact:
                                GameResourceManager.RemoveFactory(new WaterFactory(1));
                                break;

                            case Config.EnergyBrickFact:
                                GameResourceManager.RemoveFactory(new EnergyBricksFactory(1));
                                break;

                            case Config.FoodFact:
                                GameResourceManager.RemoveFactory(new FarmFactory(1));
                                break;

                            case Config.PopulationFact:
                                GameResourceManager.RemoveFactory(new PopulationFactory(1));
                                break;
                        }
                    }
                    else if (action == "Upgrade")
                    {
                        if (FactoryTypes[index] == null)
                        {
                            MessageBox.Show("No factory here to upgrade!");
                            return;
                        }
                        int currentLevel = FactoryLevels[index];
                        if (currentLevel >= 3)
                        {
                            MessageBox.Show("Already max level");
                            return;
                        }

                        int cost = 0;
                        switch (FactoryTypes[index])
                        {
                            case Config.TitaniumFact:
                                cost = Config.TitaniumMineUpgradeCosts[currentLevel];
                                break;
                            case Config.WaterFact:
                                cost = Config.WaterPumpUpgradeCosts[currentLevel];
                                break;
                            case Config.EnergyBrickFact:
                                cost = Config.EnergyBrickGeneratorUpgradeCosts[currentLevel];
                                break;
                            case Config.FoodFact:
                                cost = Config.FarmUpgradeCosts[currentLevel];
                                break;
                            case Config.PopulationFact:
                                cost = Config.ResearchLabUpgradeCosts[currentLevel];
                                break;
                        }

                        int have = GameResourceManager.GetResourceAmount("Titanium");
                        if (have < cost)
                        {
                            MessageBox.Show("Not enough Titanium. Need " + cost);
                            return;
                        }

                        GameResourceManager.DeductResource("Titanium", cost);
                        FactoryLevels[index] = currentLevel + 1;
                        b.Text = FactoryTypes[index] + " L" + FactoryLevels[index];
                        GameResourceManager.UpgradeFactory(index);
                    }
                }
                else
                {
                    MessageBox.Show("Select an action first (Factory / Demolish / Upgrade).");
                } 
            } 
        }
        

        private void ListOfBuildAction_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }

        private void FactoryTypeList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (FactoryTypeList.SelectedIndex >= 0)
            {
                SelectedFactoryType = FactoryTypeList.SelectedItem.ToString();
            }
        }

        private Color GetFactoryColor(string factoryType)
        {
            switch (factoryType)
            {
                case Config.TitaniumFact: 
                    return Color.DarkGray;

                case Config.WaterFact: 
                    return Color.LightBlue;

                case Config.EnergyBrickFact: 
                    return Color.Orange;

                case Config.FoodFact: 
                    return Color.Green;

                case Config.PopulationFact: 
                    return Color.MediumPurple;

                default: 
                    return Color.White;
            }
        }

        private void ProductionTimer_Tick(object sender, EventArgs e)
        {
            GameResourceManager.Tick();
        }
        private void ContinueButton_Click(object sender, EventArgs e)
        {
            PrototypeErrorMessage.Show();
        }
     
            


            private void New_Game_button_Click(object sender, EventArgs e)
        {   PrototypeErrorMessage.Hide();
      
            this.Hide();

      
            using (MapDesigner mainMenu = new MapDesigner())
            {
                mainMenu.ShowDialog();
            }

 
            this.Show();
        }

        private void TestScreen_Load(object sender, EventArgs e)
        {

        }

        private void LoadGameButton_Click(object sender, EventArgs e)
        {
            using (Research research = new Research())
            {
                research.ShowDialog();
            }

        }
        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            // Unsubscribe from the event when form closes
            GameResourceManager.GameStateChanged -= GameManager_GameStateChanged;
            base.OnFormClosing(e);
        }
    }
}


