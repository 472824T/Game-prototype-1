using Game_prototype_1.Game_prototype_1;
using Graphing;
using Newtonsoft.Json;
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
using System.Windows.Forms.VisualStyles;
using System.IO;
using ContentAlignment = System.Drawing.ContentAlignment;
namespace Game_prototype_1
{
    public partial class PlanetScreen : Form
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
       
        private int GridCollums = 5;
        private int Collums = 5;
        private int rows = 4;
        private float noiseScale = 10f;
        private int seed = 0;

        private bool buildMode = false;
        
        public PlanetScreen()
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

            left.Controls.Add(new Label
            {
                Text = "Actions:", 
                Location = new Point(10, CollumY) 
            }
            );
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

            left.Controls.Add(new Label 
            { 
                Text = "Factory Types:", 
                Location = new Point(10, CollumY) 
            }
            ); 
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
            CollumY+= 30;
            
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
                Text = "0",
                Location = new Point(120, CollumY), 
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

            Button btnSave = new Button
            {
                Text = "Save Map",
                Location = new Point(10, CollumY),
                Width = 220
            };
            btnSave.Click += ButtonSaveClick;
            left.Controls.Add(btnSave);
            CollumY += 40;

            Button btnLoad = new Button 
            { 
                Text = "Load Map", 
                Location = new Point(10, CollumY), 
                Width = 220 
            };
            btnLoad.Click += ButtonLoadClick;
            left.Controls.Add(btnLoad);
            CollumY += 40;
            Label legendTitle = new Label
            {
                Text = "Legend",
                Location = new Point(10, CollumY),
                Font = new Font(FontFamily.GenericSansSerif, 10, FontStyle.Bold)
            };
            left.Controls.Add(legendTitle);
            CollumY += 24;


            foreach (PerlinGen.TileType t in Enum.GetValues(typeof(PerlinGen.TileType)))
            {
                Label l = new Label
                {
                    Text = t.ToString(),
                    Location = new Point(10, CollumY),
                    AutoSize = true
                };
                Panel p = new Panel
                {
                    BackColor = TileColor(t),
                    Location = new Point(150, CollumY + 3),
                    Size = new Size(25, 20)
                };
                left.Controls.Add(l);
                left.Controls.Add(p);
                CollumY += 25;
            }
            playPanel = new Panel 
            { 
                Location = new Point(280, 10), 
                Size = new Size(ClientSize.Width - 300, 
                ClientSize.Height - 20), 
                BorderStyle = BorderStyle.FixedSingle, 
                AutoScroll = true 
            };
            Controls.Add(playPanel);
            // production timer
            ProductionTimer = new Timer { Interval = 1000 };
            ProductionTimer.Tick += ProductionTimer_Tick;
            ProductionTimer.Start();
        }

        private void MakeLabelSmall(Panel parent, string text, int y)
        {
            Label label = new Label
            {
                Text = text,
                Location = new Point(10, y),
                AutoSize = true
            };
            parent.Controls.Add(label);
        }
        private Color TileColor(PerlinGen.TileType t)
        {
            switch (t)
            {
                case PerlinGen.TileType.Ocean:
                    return Color.FromArgb(68, 138, 255);

                case PerlinGen.TileType.GrassLands:
                    return Color.FromArgb(120, 200, 80);

                case PerlinGen.TileType.Forest:
                    return Color.FromArgb(34, 139, 34);

                case PerlinGen.TileType.Desert:
                    return Color.FromArgb(194, 178, 128);

                case PerlinGen.TileType.Mountains:
                    return Color.FromArgb(120, 120, 120);

                default:
                    return Color.White;
            }
        }
        private void ButtonSaveClick(object sender, EventArgs e)
        {
            using (SaveFileDialog savefiledialog = new SaveFileDialog { Filter = Config.JSONFilter })
            {
                if (savefiledialog.ShowDialog() != DialogResult.OK)
                {
                    return;
                }
                List<PerlinGen.TileInfo> tiles = new List<PerlinGen.TileInfo>();
                foreach (Button b in TileButtons)

                    if (b.Tag is PerlinGen.TileInfo t)
                    {
                        tiles.Add(t);
                    }
                PerlinGen.MapSaveData map = new PerlinGen.MapSaveData
                {
                    Columns = Collums,
                    Rows = rows,
                    Seed = seed,
                    NoiseScale = noiseScale,
                    Tiles = tiles
                };
                File.WriteAllText(savefiledialog.FileName, JsonConvert.SerializeObject(map, Formatting.Indented));
                MessageBox.Show("Map saved successfully!");
            }
        }
        private void ButtonLoadClick(object sender, EventArgs e)
        {
            using (OpenFileDialog ofd = new OpenFileDialog { Filter = Config.JSONFilter })
            {
                if (ofd.ShowDialog() != DialogResult.OK) return;
                try
                {
                   PerlinGen.MapSaveData map = JsonConvert.DeserializeObject<PerlinGen.MapSaveData>(File.ReadAllText(ofd.FileName));
                    if (map != null)
                    {
                        Collums = map.Columns;
                        rows = map.Rows;
                        seed = map.Seed;
                        noiseScale = map.NoiseScale;
                      
                        
                        
                        GenerateFromSaved(map);
                        MessageBox.Show("Map loaded successfully!");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Failed to load map: " + ex.Message);
                }
            }
        }
        private void GenerateFromSaved(PerlinGen.MapSaveData map)
        {
            Factory.Clear();
            FactoryTypes.Clear();
            FactoryLevels.Clear();

            playPanel.Controls.Clear();
            TileButtons.Clear();
            Panel canvas = new Panel
            {
                Location = new Point(0, 0),
                Size = new Size(map.Columns * Config.TileSize,
                map.Rows * Config.TileSize)
            };
            playPanel.Controls.Add(canvas);
            foreach (PerlinGen.TileInfo info in map.Tiles)
            {
                Button tile = new Button
                {
                    Location = new Point(info.Col * Config.TileSize, info.Row * Config.TileSize),
                    Size = new Size(Config.TileSize - 2, Config.TileSize - 2),
                    BackColor = TileColor((PerlinGen.TileType)info.Type),
                    ForeColor = Color.White,
                    Text = info.Type.ToString(),
                    TextAlign = ContentAlignment.BottomCenter,
                    Font = new Font("Segoe UI", 9, FontStyle.Bold),
                    Tag = info

                };

                
                canvas.Controls.Add(tile);
                TileButtons.Add(tile);
                tile.Click += TileButtonClick;
            }
            
            
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

        private void TileButtonClick(object sender, EventArgs e)
        {
            {
               
                Button b = sender as Button;
                if (b == null)
                    return;
                int index = TileButtons.IndexOf(b);
                if (index < 0)
                    return;
                if (b?.Tag is PerlinGen.TileInfo info)
                {
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
                            info.HasFactory = true;
                            info.FactoryType = SelectedFactoryType;
                            info.Level = 1;
                            b.BackColor = GetFactoryColor(SelectedFactoryType);
                            b.Text = $"{info.FactoryType} L1";



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

                        }
                        else if (action == "Demolish")
                        {
                            info.HasFactory = false;
                            info.FactoryType = null;
                            info.Level = 0;
                            b.Text = "Empty";


                            switch (info.FactoryType)
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
                            if (info.HasFactory == false)
                            {
                                MessageBox.Show("No factory here to upgrade!");
                                return;
                            }

                            if (info.Level >= 3)
                            {
                                MessageBox.Show("Already max level");
                                return;
                            }

                            int cost = 0;
                            switch (FactoryTypes[index])
                            {
                                case Config.TitaniumFact:
                                    cost = Config.TitaniumMineUpgradeCosts[info.Level];
                                    break;
                                case Config.WaterFact:
                                    cost = Config.WaterPumpUpgradeCosts[info.Level];
                                    break;
                                case Config.EnergyBrickFact:
                                    cost = Config.EnergyBrickGeneratorUpgradeCosts[info.Level];
                                    break;
                                case Config.FoodFact:
                                    cost = Config.FarmUpgradeCosts[info.Level];
                                    break;
                                case Config.PopulationFact:
                                    cost = Config.ResearchLabUpgradeCosts[info.Level];
                                    break;
                            }

                            int have = GameResourceManager.GetResourceAmount("Titanium");
                            if (have < cost)
                            {
                                MessageBox.Show("Not enough Titanium. Need " + cost);
                                return;
                            }

                            GameResourceManager.DeductResource("Titanium", cost);

                            b.Text = $"{info.FactoryType} L{info.Level}";
                            GameResourceManager.UpgradeFactory(index);
                        }
                    }
                    else
                    {
                        MessageBox.Show("Select an action first (Factory / Demolish / Upgrade).");
                    }
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
       
        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            // Unsubscribe from the event when form closes
            GameResourceManager.GameStateChanged -= GameManager_GameStateChanged;
            base.OnFormClosing(e);
        }

        private void MainMenu_Load(object sender, EventArgs e)
        {

        }

    }
}





