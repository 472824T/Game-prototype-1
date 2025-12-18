
using Graphing;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Security;
using System.Windows.Forms;
using ContentAlignment = System.Drawing.ContentAlignment;
namespace Game_prototype_1
{
    public partial class PlanetScreen : Form
    {

        private Panel playPanel;
        private List<Button> TileButtons = new List<Button>();
        private ListBox ListOfBuildAction;
        private Label LabelTitaniumCount = new Label();
        private Label LabelWaterCount = new Label();
        private Label LabelEnergyBricksCount = new Label();
        private Label LabelFoodCount = new Label();
        private Label LabelPopulationCount = new Label();
        private Label LabelResearchCount = new Label();

        private Label LabelTitaniumCost = new Label();
        private Label LabelWaterCost = new Label();
        private Label LabelEnergyBricksCost = new Label();
        private Label LabelFoodCost = new Label();
        private Label LabelPopulationCost = new Label();
       
        // Factory selection and actions
        private ListBox FactoryTypeList;
        private string SelectedFactoryType = null;
        private int GridCollums = 5;
        private int Collums = 5;
        private int rows = 4;
        private float noiseScale = 10f;
        private int seed = 0;
        private GameResourceFactory factory;
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
            int CollumY = 10;
            Button ResearchTreeButton = new Button
            {
                Text = "Research",
                Location = new Point(10, CollumY),
            
            };
            ResearchTreeButton.Click += ButtonResearchClick;
            left.Controls.Add(ResearchTreeButton);

            CollumY += 30;
            left.Controls.Add(new Label
            {
                Text = "Actions:",
                Location = new Point(10, CollumY)
            }
            );
            CollumY += 30;
            ListOfBuildAction = new CheckedListBox
            {
                Location = new Point(10, CollumY),
                Size = new Size(220, 80)
            };
            ListOfBuildAction.Items.AddRange(Config.ListedActions.ToArray());
            
            left.Controls.Add(ListOfBuildAction);
            CollumY += 90;

            left.Controls.Add(new Label
            {
                Text = "Factory Types:",
                Location = new Point(10, CollumY)
            }
            );
            CollumY += 30;

            FactoryTypeList = new ListBox
            {
                Location = new Point(10, CollumY),
                Size = new Size(220, 120)
            };
            FactoryTypeList.Items.AddRange(new object[]
            {
                Config.TitaniumFact ,
                Config.WaterFact,
                Config.EnergyBrickFact,
                Config.FoodFact,
                Config.PopulationFact
            }
            );
            left.Controls.Add(FactoryTypeList);
            CollumY += 30;

            FactoryTypeList.SelectedIndexChanged += FactoryTypeList_SelectedIndexChanged;
            FactoryTypeList.Visible = true;
            left.Controls.Add(FactoryTypeList); CollumY += 140;

            left.Controls.Add(new Label
            {
                Text = "Costs:",
                Location = new Point(10, CollumY),
                Font = new Font("Segoe UI", 9, FontStyle.Bold)
            }
           );
            CollumY += 22;

            LabelTitaniumCost = new Label
            {
                Text = "0",
                Location = new Point(10, CollumY),
                AutoSize = true
            };
            MakeLabelSmall(left, "Titanium:", CollumY);

            LabelTitaniumCost.Location = new Point(120, CollumY);
            left.Controls.Add(LabelTitaniumCost);
            CollumY += 22;

            LabelWaterCost = new Label
            {
                Text = "0",
                Location = new Point(120, CollumY),
                AutoSize = true
            };
            MakeLabelSmall(left, "Water:", CollumY);

            LabelWaterCost.Location = new Point(120, CollumY);
            left.Controls.Add(LabelWaterCost);
            CollumY += 22;

            LabelEnergyBricksCost = new Label
            {
                Text = "0",
                Location = new Point(120, CollumY),
                AutoSize = true
            };
            MakeLabelSmall(left, "Energy:", CollumY);

            LabelEnergyBricksCost.Location = new Point(120, CollumY);
            left.Controls.Add(LabelEnergyBricksCost);
            CollumY += 22;

            LabelFoodCost = new Label
            {
                Text = "0",
                Location = new Point(120, CollumY),
                AutoSize = true
            };
            MakeLabelSmall(left, "Food:", CollumY);

            LabelFoodCost.Location = new Point(120, CollumY);
            left.Controls.Add(LabelFoodCost);
            CollumY += 22;

            LabelPopulationCost = new Label
            {
                Text = "0",
                Location = new Point(120, CollumY),
                AutoSize = true
            };
            MakeLabelSmall(left, "Population:", CollumY);

            LabelPopulationCost.Location = new Point(120, CollumY);
            left.Controls.Add(LabelPopulationCost);
            CollumY += 22;

        
            left.Controls.Add(new Label
            {
                Text = "Resources:",
                Location = new Point(10, CollumY),
                Font = new Font("Segoe UI", 9, FontStyle.Bold)
            }
            );
            CollumY += 22;

            LabelTitaniumCount = new Label
            {
                Text = "0",
                Location = new Point(10, CollumY),
                AutoSize = true
            };
            MakeLabelSmall(left, "Titanium:", CollumY); 
            
            LabelTitaniumCount.Location = new Point(120, CollumY); 
            left.Controls.Add(LabelTitaniumCount); 
            CollumY+= 22;

            LabelWaterCount = new Label 
            { 
                Text = "0",
                Location = new Point(120, CollumY), 
                AutoSize = true 
            };
            MakeLabelSmall(left, "Water:", CollumY);
         
            LabelWaterCount.Location = new Point(120, CollumY); 
            left.Controls.Add(LabelWaterCount); 
            CollumY+= 22;

            LabelEnergyBricksCount = new Label 
            { 
                Text = "0", 
                Location = new Point(120, CollumY), 
                AutoSize = true 
            };
            MakeLabelSmall(left, "Energy:", CollumY);
         
            LabelEnergyBricksCount.Location = new Point(120, CollumY); 
            left.Controls.Add(LabelEnergyBricksCount); 
            CollumY+= 22;

            LabelFoodCount = new Label 
            { 
                Text = "0", 
                Location = new Point(120, CollumY), 
                AutoSize = true 
            };
            MakeLabelSmall(left, "Food:", CollumY);

            LabelFoodCount.Location = new Point(120, CollumY); 
            left.Controls.Add(LabelFoodCount); 
            CollumY+= 22;

            LabelPopulationCount = new Label
            {
                Text = "0",
                Location = new Point(120, CollumY),
                AutoSize = true
            };
            MakeLabelSmall(left, "Population:", CollumY);

            LabelPopulationCount.Location = new Point(120, CollumY);
            left.Controls.Add(LabelPopulationCount);
            CollumY += 22;

            LabelResearchCount = new Label 
            { 
                Text = "0",
                Location = new Point(120, CollumY), 
                AutoSize = true 
            };
            MakeLabelSmall(left, "Research:", CollumY);

            LabelResearchCount.Location = new Point(120, CollumY); 
            left.Controls.Add(LabelResearchCount); 
            CollumY+= 30;

            Button buttonSave = new Button
            {
                Text = "Save Map",
                Location = new Point(10, CollumY),
                Width = 220
            };
            buttonSave.Click += ButtonSaveClick;
            left.Controls.Add(buttonSave);
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

            Button NewAIButton = new Button
            {
                Text = "New Player",
                Location = new Point(10, CollumY),
                Width = 220
            };
            NewAIButton.Click += NewAIButtonClick;
            left.Controls.Add(NewAIButton);
            CollumY += 40;

            Label legendTitle = new Label
            {
                Text = "Legend",
                Location = new Point(10, CollumY),
                Font = new Font(FontFamily.GenericSansSerif, 10, FontStyle.Bold)
            };
            left.Controls.Add(legendTitle);
            CollumY += 24;

            
            foreach (Config.TileType t in Enum.GetValues(typeof(Config.TileType)))
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
                Size = new Size(ClientSize.Width - 300, ClientSize.Height - 20), 
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
           parent.Controls.Add(new Label 
            {
                Text = text,
                Location = new Point(10, y),
            }
            );
            
        }
         private void ArrayToString(ImmutableArray<int> array)
        { string s = "";
            foreach (int i in array)
            {
                s = s + i.ToString();

            }
        }
        private Color TileColor(Config.TileType t)
        {
            switch (t)
            {
                case Config.TileType.Ocean:
                    return Config.Blue;

                case Config.TileType.GrassLands:
                    return Config.LightGreen;

                case Config.TileType.Forest:
                    return Config.DarkGreen;

                case Config.TileType.Desert:
                    return Config.Yellow;

                case Config.TileType.Mountains:
                    return Config.Brown;

                default:
                    return Color.White;
            }
        }
        private void ButtonResearchClick(object sender, EventArgs e)
        {
            using (Research research = new Research())
            {
                research.ShowDialog();
            }
        }
        private void ButtonSaveClick(object sender, EventArgs e)
        {
           JsonSerializer serializer = new JsonSerializer();
            serializer.Converters.Add(new Newtonsoft.Json.Converters.JavaScriptDateTimeConverter());
            serializer.NullValueHandling = NullValueHandling.Ignore;
            serializer.TypeNameHandling = TypeNameHandling.Auto;
            serializer.Formatting = Formatting.Indented;
          

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
      
                SaveData.FullGameSaveData gameSaveData = new SaveData.FullGameSaveData()
                {
                    PlayerTitaniumValue = GameResourceManager.GetResourceAmount(Config.TitaniumName),
                    PlayerWaterValue = GameResourceManager.GetResourceAmount(Config.WaterName),
                    PlayerEnergyBricksValue = GameResourceManager.GetResourceAmount(Config.EnergyBricksName),
                    PlayerFoodValue = GameResourceManager.GetResourceAmount(Config.FoodName),
                    PlayerPopulationValue = GameResourceManager.GetResourceAmount(Config.PopulationName),
                    PlayerResearchValue = GameResourceManager.GetResourceAmount(Config.ResearchName), 
                    Columns = Collums,
                    Rows = rows,
                    Seed = seed,
                    NoiseScale = noiseScale,
                    Tiles = tiles,
                };
                JsonTextWriter writer = new JsonTextWriter(new StreamWriter(savefiledialog.FileName));
                {
                    serializer.Serialize(writer,gameSaveData , typeof(SaveData.FullGameSaveData));
                }
                writer.Close();
                MessageBox.Show("Map saved successfully!");
            }
        }
        private void NewAIButtonClick(object sender, EventArgs e)
        {
            GameResourceManager.AiPlayers();
        }
        private void ButtonLoadClick(object sender, EventArgs e)
        {
            using (OpenFileDialog openfiledialog = new OpenFileDialog { Filter = Config.JSONFilter })
            {
                if (openfiledialog.ShowDialog() != DialogResult.OK) 
                return;
                {
                    try
                    {
                        SaveData.FullGameSaveData GameData = JsonConvert.DeserializeObject<SaveData.FullGameSaveData>(File.ReadAllText(openfiledialog.FileName),
                        new JsonSerializerSettings
                        {
                            TypeNameHandling = TypeNameHandling.Auto,
                            NullValueHandling = NullValueHandling.Ignore,
                        });
                        if (GameData != null)
                        {
                            Collums = GameData.Columns;
                            rows = GameData.Rows;
                            seed = GameData.Seed;
                            noiseScale = GameData.NoiseScale;
                            //GameResourceManager.ResetAll();
                            GenerateFromSaved(GameData);
                            MessageBox.Show("Map loaded successfully!");
                            GameResourceManager.AddResource(Config.TitaniumName, GameData.PlayerTitaniumValue);
                            GameResourceManager.AddResource(Config.WaterName, GameData.PlayerWaterValue);
                            GameResourceManager.AddResource(Config.EnergyBricksName, GameData.PlayerEnergyBricksValue);
                            GameResourceManager.AddResource(Config.FoodName, GameData.PlayerFoodValue);
                            GameResourceManager.AddResource(Config.PopulationName, GameData.PlayerPopulationValue);
                            GameResourceManager.AddResource(Config.ResearchName, GameData.PlayerResearchValue);
                            MessageBox.Show("Resources loaded successfully!");
                            GameResourceManager.GameStateChanged += GameManager_GameStateChanged;
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Failed to load map: " + ex.Message);
                    }
                }
            }
        }
        private void GenerateFromSaved(SaveData.FullGameSaveData map)
        {
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
                    BackColor = TileColor(info.Type),
                    ForeColor = Color.White,
                    Text = info.Type.ToString(),
                    TextAlign = ContentAlignment.BottomCenter,
                    Font = new Font("Segoe UI", 9, FontStyle.Bold),
                    Tag = info,
                };
                canvas.Controls.Add(tile);
                TileButtons.Add(tile);
                tile.Click += TileButtonClick;
                if (info.HasFactory)
                {
                    tile.BackColor = GetFactoryColor(info.FactoryType);
                    tile.Text = $"{info.FactoryType} L{info.Level}";
                    switch (info.FactoryType)
                    {
                        case Config.TitaniumFact:                        
                            GameResourceManager.AddFactory(info.Factory);
                            break;
                        case Config.WaterFact:
                            GameResourceManager.AddFactory(info.Factory);
                            break;
                        case Config.EnergyBrickFact:
                            GameResourceManager.AddFactory(info.Factory);
                            break;
                        case Config.FoodFact:
                            GameResourceManager.AddFactory(info.Factory);
                            break;
                        case Config.PopulationFact:
                            GameResourceManager.AddFactory(info.Factory);
                            break;
                    }
                }
            }
        }
        private void GameManager_GameStateChanged(object sender, EventArgs e)
        {
            UpdateDisplay();
        }
        private void UpdateDisplay()
        {
            LabelTitaniumCount.Text = GameResourceManager.PlayerTitaniumValue.ToString();
            LabelWaterCount.Text = GameResourceManager.PlayerWaterValue.ToString();
            LabelEnergyBricksCount.Text = GameResourceManager.PlayerEnergyBricksValue.ToString();
            LabelFoodCount.Text = GameResourceManager.PlayerFoodValue.ToString();
            LabelPopulationCount.Text = GameResourceManager.PlayerPopulationValue.ToString();
            LabelResearchCount.Text = GameResourceManager.PlayerResearchValue.ToString();
        }
      
        private void TileButtonClick(object sender, EventArgs e)
        {
            {               
                Button button = sender as Button;
                if (button == null)
                    return;
                int index = TileButtons.IndexOf(button);
                if (index < 0)
                    return;
                if (button?.Tag is PerlinGen.TileInfo info)
                {
                    if (ListOfBuildAction.SelectedIndex > -1)
                    {
                        string action = Config.ListedActions[ListOfBuildAction.SelectedIndex];
                        if (action == Config.ListedActions[0] && !info.HasFactory) // build
                        {
                            int[] myNum = {}; 
                            int i = 0;  
                            if (SelectedFactoryType == null)
                            {
                                MessageBox.Show("Select a factory type first from the list.");
                                return;
                            }
                            switch (SelectedFactoryType)
                            {
                                case Config.TitaniumFact:
                                    foreach (int cost in Config.TitaniumMineBuildingCosts)
                                    { 
                                        int have = GameResourceManager.GetResourceAmount(Config.ResourceNames[i]);
                                        if (have < cost)
                                        {
                                            MessageBox.Show($"Not enough {Config.ResourceNames[i]} Need " + cost);
                                            return;
                                        }
                                        GameResourceManager.DeductResource(Config.ResourceNames[i], cost);
                                         i ++;
                                    }
                                    break;
                                case Config.WaterFact:
                                    foreach (int cost in Config.WaterPumpBuildingCosts)
                                    {
                                        int have = GameResourceManager.GetResourceAmount(Config.ResourceNames[i]);
                                        if (have < cost)
                                        {
                                            MessageBox.Show($"Not enough {Config.ResourceNames[i]} Need " + cost);
                                            return;
                                        }
                                        GameResourceManager.DeductResource(Config.ResourceNames[i], cost);
                                        i++;
                                    }
                                    break;
                                case Config.EnergyBrickFact:
                                    foreach (int cost in Config.EnergyBrickGeneratorBuildingCosts)
                                    {
                                        int have = GameResourceManager.GetResourceAmount(Config.ResourceNames[i]);
                                        if (have < cost)
                                        {
                                            MessageBox.Show($"Not enough {Config.ResourceNames[i]} Need " + cost);
                                            return;
                                        }
                                        GameResourceManager.DeductResource(Config.ResourceNames[i], cost);
                                        i++;
                                    }
                                    break;
                                case Config.FoodFact:
                                    foreach (int cost in Config.FarmBuildingCosts)
                                    {
                                        int have = GameResourceManager.GetResourceAmount(Config.ResourceNames[i]);
                                        if (have < cost)
                                        {
                                            MessageBox.Show($"Not enough {Config.ResourceNames[i]} Need " + cost);
                                            return;
                                        }
                                        GameResourceManager.DeductResource(Config.ResourceNames[i], cost);
                                        i++;
                                    }
                                    break;
                                case Config.PopulationFact:
                                    foreach (int cost in Config.HousingBuildingCosts)
                                    {
                                        int have = GameResourceManager.GetResourceAmount(Config.ResourceNames[i]);
                                        if (have < cost)
                                        {
                                            MessageBox.Show($"Not enough {Config.ResourceNames[i]} Need " + cost);
                                            return;
                                        }
                                        GameResourceManager.DeductResource(Config.ResourceNames[i], cost);
                                        i++;
                                    }
                                    break;
                            }
                            
                          
                            info.HasFactory = true;
                            info.FactoryType = SelectedFactoryType;
                            info.Level = 1;
                            button.BackColor = GetFactoryColor(SelectedFactoryType);
                            button.Text = $"{info.FactoryType} L1";

                            switch (SelectedFactoryType)
                            {
                                case Config.TitaniumFact:
                                    info.Factory = new TitaniumFactory(1);
                                    GameResourceManager.AddFactory(info.Factory);                             
                                    break;
                                case Config.WaterFact:
                                    info.Factory = new WaterFactory(1);
                                    GameResourceManager.AddFactory(info.Factory);
                                    break;
                                case Config.EnergyBrickFact:
                                    info.Factory = new EnergyBricksFactory(1);
                                    GameResourceManager.AddFactory(info.Factory);
                                    break;
                                case Config.FoodFact:
                                    info.Factory = new FarmFactory(1);
                                    GameResourceManager.AddFactory(info.Factory);
                                    break;
                                case Config.PopulationFact:
                                    info.Factory = new PopulationFactory(1);
                                    GameResourceManager.AddFactory(info.Factory);
                                    break;
                            }
                        }
                        else if (action == Config.ListedActions[1] && info.HasFactory)// demolish
                        {
                            switch (info.FactoryType)
                            {
                                case Config.TitaniumFact:
                                    GameResourceManager.RemoveFactory(info.Factory);
                                    break;

                                case Config.WaterFact:
                                    GameResourceManager.RemoveFactory(info.Factory);
                                    break;

                                case Config.EnergyBrickFact:
                                    GameResourceManager.RemoveFactory(info.Factory);
                                    break;

                                case Config.FoodFact:
                                    GameResourceManager.RemoveFactory(info.Factory);
                                    break;

                                case Config.PopulationFact:
                                    GameResourceManager.RemoveFactory(info.Factory);
                                    break;   
                            }
                            info.HasFactory = false;
                            info.FactoryType = null;
                            info.Factory = null;
                            info.Level = 0;
                            button.Text = Config.TileType.GrassLands.ToString();
                            info.Type = Config.TileType.GrassLands;
                            button.BackColor = TileColor(info.Type);
                        }
                        else if (action == Config.ListedActions[2] && info.HasFactory) // upgrade
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
                            switch (info.FactoryType)
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
                                    cost = Config.HousingUpgradeCosts[info.Level];
                                    break;
                            }
                            int have = GameResourceManager.GetResourceAmount(Config.TitaniumName);
                            if (have < cost)
                            {
                                MessageBox.Show("Not enough Titanium. Need " + cost);
                                return;
                            }
                            GameResourceManager.DeductResource(Config.TitaniumName, cost);
                            GameResourceManager.UpgradeFactory(info.Factory);
                            info.Level++;
                            button.Text = $"{info.FactoryType} L{info.Level}";
                        }
                    }
                    else
                    {
                        MessageBox.Show("Select an action first (Factory / Demolish / Upgrade).");
                    }
                }
            }
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





