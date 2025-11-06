using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.IO;
using Newtonsoft.Json;

namespace Game_prototype_1
    {
        public partial class MapDesigner : Form
        {
            private Panel gridPanel;
            private NumericUpDown nudCollums;
            private NumericUpDown nudRows;
            private NumericUpDown nudScale;
            private TextBox txtSeed;
            private Button btnGenerate;
            private Button btnStartGame;
            private ListBox FactoryTypeList;
            private string SelectedFactoryType;
            private bool buildMode = false;
            private Label LabelTitaniumCount = new Label();
            private Label LabelWaterCount = new Label();
            private Label LabelEnergyBricksCount = new Label();
            private Label LabelFoodCount = new Label();
            private Label LabelPopulationCount = new Label();
            private Label LabelResearchCount = new Label();

        private int Collums = 5;
            private int rows = 4;
            private float noiseScale = 10f;
            private int seed = 0;
            private List<Button> tileButtons = new List<Button>();
            private PerlinNoise perlin;

            public MapDesigner()
            {
                InitializeComponent();
                SetupDefaults();
                SetupUI();
                SetupPerlin();
                GenerateGrid();
            }

            private void SetupDefaults()
            {
                Collums = 5; 
                rows = 4; 
                noiseScale = 10f;
                seed = Environment.TickCount & 0x7fffffff;
            }

            private void SetupPerlin() { perlin = new PerlinNoise(seed); }

            private void SetupUI()
            {
                Text = "Main Menu";
                ClientSize = new Size(1200, 800);
                FormBorderStyle = FormBorderStyle.FixedSingle;
                MaximizeBox = false;

                Panel controls = new Panel 
                {
                    Location = new Point(10, 10), 
                    Size = new Size(260, ClientSize.Height - 20),
                    BorderStyle = BorderStyle.FixedSingle, 
                    AutoScroll = true 
                };
                Controls.Add(controls);

                int CollumY= 10;

                controls.Controls.Add(new Label { Text = "Columns:", Location = new Point(10, CollumY) }); 
                CollumY+= 20;

                nudCollums = new NumericUpDown { Location = new Point(10, CollumY), Minimum = 1, Maximum = 50, Value = Collums }; 
                controls.Controls.Add(nudCollums); 
                CollumY+= 30;

                controls.Controls.Add(new Label { Text = "Rows:", Location = new Point(10, CollumY) }); 
                CollumY+= 20;

                nudRows = new NumericUpDown { Location = new Point(10, CollumY), Minimum = 1, Maximum = 50, Value = rows }; 
                controls.Controls.Add(nudRows); 
                CollumY+= 30;

                controls.Controls.Add(new Label { Text = "Noise Scale (1–100):", Location = new Point(10, CollumY) });
                CollumY+= 20;

                nudScale = new NumericUpDown { Location = new Point(10, CollumY), Minimum = 1, Maximum = 100, Value = (decimal)noiseScale }; 
                controls.Controls.Add(nudScale); 
                CollumY+= 30;

                controls.Controls.Add(new Label { Text = "Seed:", Location = new Point(10, CollumY) }); 
                CollumY+= 20;

                txtSeed = new TextBox { Location = new Point(10, CollumY), Width = 200, Text = seed.ToString() }; 
                controls.Controls.Add(txtSeed); 
                CollumY+= 35;

                btnGenerate = new Button { Text = "Generate", Location = new Point(10, CollumY), Width = 220 }; 
                btnGenerate.Click += BtnGenerate_Click; 
                controls.Controls.Add(btnGenerate); 
                CollumY+= 40;

                Button btnSave = new Button { Text = "Save Map", Location = new Point(10, CollumY), Width = 220 }; 
                btnSave.Click += ButtonSaveClick; 
                controls.Controls.Add(btnSave); 
                CollumY+= 40;

                Button btnLoad = new Button { Text = "Load Map", Location = new Point(10, CollumY), Width = 220 }; 
                btnLoad.Click += ButtonLoadClick; 
                controls.Controls.Add(btnLoad); 
                CollumY+= 40;

                btnStartGame = new Button { Text = "Start Game", Location = new Point(10, CollumY), Width = 220 }; 
                btnStartGame.Click += BtnStartGame_Click; controls.Controls.Add(btnStartGame); 
                CollumY+= 44;
                Button btnBuildFactory = new Button
                {
                    Text = "Build Factory Mode",
                    Location = new Point(10, CollumY),
                    Width = 220
                };
                controls.Controls.Add(btnBuildFactory);
                CollumY += 40;
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
                controls.Controls.Add(new Label
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
                controls .Controls.Add(new Label
                {
                    Text = "Titanium:",
                    Location = new Point(10, CollumY)
                }
                );
                LabelTitaniumCount.Location = new Point(120, CollumY);
                controls.Controls.Add(LabelTitaniumCount);
                CollumY += 22;

                LabelWaterCount = new Label
                {
                    Text = "0",
                    Location = new Point(120, CollumY),
                    AutoSize = true
                };
                controls .Controls.Add(new Label
                {
                    Text = "Water:",
                    Location = new Point(10, CollumY)
                }
                );
                LabelWaterCount.Location = new Point(120, CollumY);
                controls.Controls.Add(LabelWaterCount);
                CollumY += 22;

                LabelEnergyBricksCount = new Label
                {
                    Text = "0",
                    Location = new Point(120, CollumY),
                    AutoSize = true
                };
                controls .Controls.Add(new Label
                {
                    Text = "Energy:",
                    Location = new Point(10, CollumY)
                }
                );
                LabelEnergyBricksCount.Location = new Point(120, CollumY);
                controls.Controls.Add(LabelEnergyBricksCount);
                CollumY += 22;

                LabelFoodCount = new Label
                {
                    Text = "0",
                    Location = new Point(120, CollumY),
                    AutoSize = true
                };
                controls.Controls.Add(new Label
                {
                    Text = "Food:",
                    Location = new Point(10, CollumY)
                }
                );
                LabelFoodCount.Location = new Point(120, CollumY);
                controls.Controls.Add(LabelFoodCount);
                CollumY += 22;

                LabelPopulationCount = new Label
                {
                    Text = "0",
                    Location = new Point(120, CollumY),
                    AutoSize = true
                };
                controls.Controls.Add(new Label
                {
                    Text = "Population:",
                    Location = new Point(10, CollumY)
                }
                );
                LabelPopulationCount.Location = new Point(120, CollumY);
                controls.Controls.Add(LabelPopulationCount);
                CollumY += 22;

                LabelResearchCount = new Label
                {
                    Text = "0",
                    Location = new Point(120, CollumY),
                    AutoSize = true
                };
                controls .Controls.Add(new Label
                {
                    Text = "Research:",
                    Location = new Point(10, CollumY)
                }
                );
                LabelResearchCount.Location = new Point(120, CollumY);
                controls .Controls.Add(LabelResearchCount);
                CollumY += 30;

                Label lblFactory = new Label 
                { 
                    Text = "Factory Types:", 
                    Location = new Point(10, CollumY) 
                };
                controls.Controls.Add(lblFactory); 
                CollumY+= 20;

                FactoryTypeList = new ListBox { Location = new Point(10, CollumY), Size = new Size(220, 120) };
                FactoryTypeList.Items.AddRange(new object[] 
                {
                    Config.TitaniumFact, 
                    Config.WaterFact, 
                    Config.EnergyBrickFact, 
                    Config.FoodFact, 
                    Config.PopulationFact 
                }
                );
                FactoryTypeList.SelectedIndexChanged += (s, e) =>
                {
                    if (FactoryTypeList.SelectedIndex >= 0)
                    { 
                        SelectedFactoryType = FactoryTypeList.SelectedItem.ToString();
                    }
                };
                controls.Controls.Add(FactoryTypeList);
                CollumY+= 130;

                Label legendTitle = new Label 
                { 
                    Text = "Legend", 
                    Location = new Point(10, CollumY), 
                    Font = new Font(FontFamily.GenericSansSerif, 10, FontStyle.Bold) 
                }; 
                controls.Controls.Add(legendTitle);
                CollumY+= 24;

                foreach (TileType t in Enum.GetValues(typeof(TileType))) {
                Label l = new Label 
                { 
                    Text = t.ToString(), 
                    Location = new Point(10, CollumY), 
                    AutoSize = true 
                }; 
                Panel p = new Panel 
                { 
                    BackColor = TileColor(t), 
                    Location = new Point(150, CollumY+ 3),
                    Size = new Size(25, 20) 
                }; 
                controls.Controls.Add(l); 
                controls.Controls.Add(p); 
                CollumY+= 25;
       
            }

                gridPanel = new Panel 
                { 
                    Location = new Point(280, 10),
                    Size = new Size(ClientSize.Width - 300, 
                    ClientSize.Height - 20), 
                    BorderStyle = BorderStyle.FixedSingle, 
                    AutoScroll = true 
                };
                Controls.Add(gridPanel);
            }

            private void BtnStartGame_Click(object sender, EventArgs e)
            {
                using (MainMenu ts = new MainMenu())
                {
                    ts.ShowDialog();
                }
            }

            private void BtnGenerate_Click(object sender, EventArgs e)
            {
                Collums = (int)nudCollums.Value; 
                rows = (int)nudRows.Value; 
                noiseScale = (float)nudScale.Value;
                seed = int.TryParse(txtSeed.Text, out int p) ? p : Environment.TickCount;
                SetupPerlin(); 
                GenerateGrid();
            }

            private void GenerateGrid()
            {
                gridPanel.Controls.Clear(); 
                tileButtons.Clear();
                int width = Collums * Config.TileSize;
                int height = rows * Config.TileSize;
                Panel canvas = new Panel 
                { 
                    Location = new Point(0, 0), 
                    Size = new Size(width, height) 
                }; 
                gridPanel.Controls.Add(canvas);

                for (int r = 0; r < rows; r++)
                {
                    for (int c = 0; c < Collums; c++)
                    {
                        float nx = c / noiseScale; 
                        float ny = r / noiseScale; 
                        float n = perlin.Noise(nx, ny);
                        TileType t = TileFromNoise(n);
                        Button tile = new Button 
                        { 
                            Location = new Point(c * Config.TileSize, r * Config.TileSize), 
                            Size = new Size(Config.TileSize - 2, Config.TileSize - 2), 
                            BackColor = TileColor(t), 
                            ForeColor = Color.White, 
                            Text = t.ToString(), 
                            TextAlign = ContentAlignment.BottomCenter, 
                            Font = new Font("Segoe UI", 9, FontStyle.Bold), 
                            Tag = new TileInfo 
                                { 
                                    Col = c, 
                                    Row = r, 
                                    Type = t, 
                                    Level = 1 
                                }
                        };
                        tile.Click += Tile_Click; 
                        canvas.Controls.Add(tile); 
                        tileButtons.Add(tile);
                    }
                }
            }
        private Color FactoryColor(string factoryType)
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
        private void Tile_Click(object sender, EventArgs e)
        {
            Button b = sender as Button;
            if (b ? .Tag is TileInfo info)
            {
                if (buildMode)
                {
                    if (SelectedFactoryType == null)
                    {
                        MessageBox.Show("Select a factory type first!");
                        return;
                    }
                    
                    if (!info.HasFactory)
                    {
                        info.HasFactory = true;
                        info.FactoryType = SelectedFactoryType;
                        info.Level = 1;
                        b.Text = $"{info.FactoryType} L1";
                        b.BackColor = FactoryColor(info.FactoryType);
                    }
                    else if (info.Level < 3)
                    {
                        info.Level++;
                        b.Text = $"{info.FactoryType} L{info.Level}";
                    }
                    else
                    {
                        MessageBox.Show("Factory already at max level!");
                    }
                    b.Tag = info;
                }
                else
                {
                    
                    info.Type = NextTileType(info.Type);
                    b.Tag = info;
                    b.BackColor = TileColor(info.Type);
                    b.Text = info.Type.ToString();
                }
            }
        }

            private TileType TileFromNoise(float n)
            {
                if (n < -0.35f)
                {
                    return TileType.Ocean;
                }
                if (n < -0.05f)
                { 
                    return TileType.GrassLands; 
                }
                if (n < 0.25f)
                {
                    return TileType.Forest;
                }
                if (n < 0.55f)
                {
                    return TileType.Desert;
                }

                return TileType.Mountains;
            }

            private TileType NextTileType(TileType t) 
            {
                TileType[] vals = (TileType[])Enum.GetValues(typeof(TileType)); 
                int index = Array.IndexOf(vals, t); 

                return vals[(index + 1) % vals.Length]; 
            }

            private Color TileColor(TileType t) 
            { 
                switch (t) 
                { 
                    case TileType.Ocean: 
                        return Color.FromArgb(68, 138, 255); 

                    case TileType.GrassLands: 
                        return Color.FromArgb(120, 200, 80); 

                    case TileType.Forest: 
                        return Color.FromArgb(34, 139, 34); 

                    case TileType.Desert: 
                        return Color.FromArgb(194, 178, 128); 

                    case TileType.Mountains: 
                        return Color.FromArgb(120, 120, 120); 

                    default:
                        return Color.White; 
                } 
            }

            private void ButtonSaveClick(object sender, EventArgs e)
            {
            using (SaveFileDialog sfd = new SaveFileDialog { Filter = Config.JSONFilter })
            {
                if (sfd.ShowDialog() != DialogResult.OK)
                {
                    return;
                }
                List<TileInfo> tiles = new List<TileInfo>();
                foreach (Button b in tileButtons)

                    if (b.Tag is TileInfo t)
                    { 
                        tiles.Add(t);
                    }
                    MapSaveData map = new MapSaveData
                    { 
                        Columns = Collums, 
                        Rows = rows,
                        Seed = seed, 
                        NoiseScale = noiseScale, 
                        Tiles = tiles 
                    };
                    File.WriteAllText(sfd.FileName, JsonConvert.SerializeObject(map, Formatting.Indented));
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
                            MapSaveData map = JsonConvert.DeserializeObject<MapSaveData>(File.ReadAllText(ofd.FileName));
                            if (map != null) 
                            { 
                                Collums = map.Columns; 
                                rows = map.Rows; 
                                seed = map.Seed; 
                                noiseScale = map.NoiseScale; 
                                nudCollums.Value = Collums; 
                                nudRows.Value = rows; 
                                nudScale.Value = (decimal)noiseScale; 
                                txtSeed.Text = seed.ToString(); 
                                GenerateFromSaved(map); 
                                MessageBox.Show("Map loaded successfully!"); }
                            }
                        catch (Exception ex) { MessageBox.Show("Failed to load map: " + ex.Message); 
                    }
                }
            }

            private void GenerateFromSaved(MapSaveData map)
            {
                gridPanel.Controls.Clear(); 
                tileButtons.Clear();
                Panel canvas = new Panel 
                { 
                    Location = new Point(0, 0), 
                    Size = new Size(map.Columns * Config.TileSize, 
                    map.Rows * Config.TileSize) 
                };
                gridPanel.Controls.Add(canvas);
                foreach (TileInfo info in map.Tiles)
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
                        Tag = info 
                    };

                    tile.Click += Tile_Click; 
                    canvas.Controls.Add(tile); 
                    tileButtons.Add(tile);
                }
            }

            private enum TileType { Ocean, GrassLands, Forest, Desert, Mountains }

            private class TileInfo 
            {
            public int Col { get; set; } 
            public int Row { get; set; } 
            public TileType Type { get; set; } 
            public int Level { get; set; }
            public bool HasFactory { get; set; }
            public string FactoryType { get; set; }
        }

            private class MapSaveData 
        { 
            public int Columns { get; set; } 
            public int Rows { get; set; } 
            public int Seed { get; set; } 
            public float NoiseScale { get; set; } 
            public List<TileInfo> Tiles { get; set; } }

            private class PerlinNoise
            {
                private readonly int[] perm;
                public PerlinNoise(int seed) { perm = new int[512]; int[] p = new int[256]; Random rnd = new Random(seed); 
                for (int i = 0; i < 256; i++) 
                    p[i] = i; 
                for (int i = 255; i > 0; i--) 
                { 
                    int j = rnd.Next(i + 1); 
                    int tmp = p[i]; 
                    p[i] = p[j]; p[j] = tmp; } 
                for (int i = 0; i < 512; i++) perm[i] = p[i & 255]; }
                public float Noise(float x, float y) { int X = FastFloor(x) & 255; int Y = FastFloor(y) & 255; float xf = x - (float)Math.Floor(x); 
                float yf = y - (float)Math.Floor(y); 
                float u = Fade(xf); float v = Fade(yf); 
                int aa = perm[X + perm[Y]]; int ab = perm[X + perm[Y + 1]]; 
                int ba = perm[X + 1 + perm[Y]]; 
                int bb = perm[X + 1 + perm[Y + 1]]; 
                float x1 = Lerp(Grad(aa, xf, yf), Grad(ba, xf - 1, yf), u); 
                float x2 = Lerp(Grad(ab, xf, yf - 1), Grad(bb, xf - 1, yf - 1), u); 
                return Lerp(x1, x2, v); }
                private static int FastFloor(float x) => x > 0 ? (int)x : (int)x - 1;
                private static float Fade(float t) => t * t * t * (t * (t * 6 - 15) + 10);
                private static float Lerp(float a, float b, float t) => a + t * (b - a);
                private static float Grad(int hash, float x, float y) { int h = hash & 7; 
                float u = h < 4 ? x : y; 
                float v = h < 4 ? y : x; 
                return ((h & 1) == 0 ? u : -u) + ((h & 2) == 0 ? v : -v); }
            }
        private void MainMenu_Load(object sender, EventArgs e)
        {

        }
    }
    }


