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
    public partial class TestScreen : Form
    {
  
        private Panel playPanel;
        private List<Button> TileButtons = new List<Button>();
        private List<bool> Factory = new List<bool>();
        private List<string> FactoryTypes = new List<string>();
        private List<int> FactoryLevels = new List<int>();
        private ListBox ListOfBuildAction;
        // Factory selection and actions
        private ListBox FactoryTypeList;
        private string SelectedFactoryType = null;
        private const int TileSize = 100;
        private int GridCols = 5;


        private bool buildMode = false;

        public TestScreen()
        {
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
            int cy = 10;

            left.Controls.Add(
                new Label { Text = "Actions:", Location = new Point(10, cy) });
            cy += 20;
            ListOfBuildAction = new CheckedListBox 
            { 
                Location = new Point(10, cy), 
                Size = new Size(220, 80) 
            }; 
                ListOfBuildAction.Items.AddRange(Config.ListedActions.ToArray()); 
                ListOfBuildAction.SelectedIndexChanged += ListOfBuildAction_SelectedIndexChanged; 
                left.Controls.Add(ListOfBuildAction);   
                cy += 90;

            left.Controls.Add(new Label { Text = "Factory Types:", Location = new Point(10, cy) }); 
            cy += 20;

            FactoryTypeList = new ListBox 
            { 
                Location = new Point(10, cy), 
                Size = new Size(220, 120) 
            };
            FactoryTypeList.Items.AddRange(new object[] 
            { 
                "Titanium Mine", 
                "Water Pump", 
                "Energy Brick Generator", 
                "Farm", 
                "Research Lab" 
            }
            );
            left.Controls.Add(FactoryTypeList);
            cy += 130;
          
            Button btnBuildFactory = new Button
            {
                Text = "Build Factory Mode",
                Location = new Point(10, cy),
                Width = 220
            };
            left.Controls.Add(btnBuildFactory);
            cy += 40;
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
            left.Controls.Add(FactoryTypeList); cy += 140;

            left.Controls.Add(new Label 
            { 
                Text = "Resources:", 
                Location = new Point(10, cy), 
                Font = new Font("Segoe UI", 9, FontStyle.Bold) 
            }
            ); 
            cy += 22;

            LabelTitaniumCount = new Label 
            { 
                Text = "0", 
                Location = new Point(10, cy), 
                AutoSize = true 
            }; 
            left.Controls.Add(new Label 
            { 
                Text = "Titanium:", 
                Location = new Point(10, cy) 
            }
            ); 
            LabelTitaniumCount.Location = new Point(120, cy); 
            left.Controls.Add(LabelTitaniumCount); 
            cy += 22;

            LabelWaterCount = new Label 
            { 
                Text = "0", Location = new Point(120, cy), 
                AutoSize = true 
            }; 
            left.Controls.Add(new Label 
            { 
                Text = "Water:", 
                Location = new Point(10, cy) 
            }
            ); 
            LabelWaterCount.Location = new Point(120, cy); 
            left.Controls.Add(LabelWaterCount); 
            cy += 22;

            LabelEnergyBricksCount = new Label 
            { 
                Text = "0", Location = new Point(120, cy), 
                AutoSize = true 
            }; 
            left.Controls.Add(new Label 
            { 
                Text = "Energy:", 
                Location = new Point(10, cy) 
            }
            ); 
            LabelEnergyBricksCount.Location = new Point(120, cy); 
            left.Controls.Add(LabelEnergyBricksCount); 
            cy += 22;

            LabelFoodCount = new Label 
            { 
                Text = "0", 
                Location = new Point(120, cy), 
                AutoSize = true 
            }; 
            left.Controls.Add(new Label 
            { 
                Text = "Food:", 
                Location = new Point(10, cy) 
            }
            ); 
            LabelFoodCount.Location = new Point(120, cy); 
            left.Controls.Add(LabelFoodCount); 
            cy += 22;

            LabelResearchCount = new Label 
            { 
                Text = "0",
                Location = new Point(120, cy), 
                AutoSize = true 
            }; 
            left.Controls.Add(new Label 
            { 
                Text = "Research:", 
                Location = new Point(10, cy) }
            ); 
            LabelResearchCount.Location = new Point(120, cy); 
            left.Controls.Add(LabelResearchCount); 
            cy += 30;

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
            GenOfTilePicBoxs(5);

            // production timer
            ProductionTimer = new Timer { Interval = 1000 };
            ProductionTimer.Tick += ProductionTimer_Tick;
            ProductionTimer.Start();
        }

        private void CreateTiles(int cols, int rows)
        {
            TileButtons.Clear(); 
            Factory.Clear(); 
            FactoryTypes.Clear(); 
            FactoryLevels.Clear();

            Panel canvas = new Panel 
            { 
                Location = new Point(0, 0), 
                Size = new Size(cols * TileSize, rows * TileSize) 
            };
            playPanel.Controls.Clear();
            playPanel.Controls.Add(canvas);

            for (int r = 0; r < rows; r++)
            {
                for (int c = 0; c < cols; c++)
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
                            case "Titanium Mine":
                                GameResourceManager.AddFactory(new TitaniumFactory(1));
                                break;
                            case "Water Pump":
                                GameResourceManager.AddFactory(new WaterFactory(1));
                                break;
                            case "Energy Brick Generator":
                                GameResourceManager.AddFactory(new EnergyBricksFactory(1));
                                break;
                            case "Farm":
                                GameResourceManager.AddFactory(new FarmFactory(1));
                                break;
                            case "Research Lab":
                                GameResourceManager.AddFactory(new ResearchFactory(1));
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
                            case "Titanium Mine":
                                GameResourceManager.RemoveFactory(new TitaniumFactory(1));
                                break;

                            case "Water Pump":
                                GameResourceManager.RemoveFactory(new WaterFactory(1));
                                break;

                            case "Energy Brick Generator":
                                GameResourceManager.RemoveFactory(new EnergyBricksFactory(1));
                                break;

                            case "Farm":
                                GameResourceManager.RemoveFactory(new FarmFactory(1));
                                break;

                            case "Research Lab":
                                GameResourceManager.RemoveFactory(new ResearchFactory(1));
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
                case "Titanium Mine": 
                    return Color.DarkGray;

                case "Water Pump": 
                    return Color.LightBlue;

                case "Energy Brick Generator": 
                    return Color.Orange;

                case "Farm": 
                    return Color.Green;

                case "Research Lab": 
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

      
            using (MainMenu mainMenu = new MainMenu())
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
    }
}


