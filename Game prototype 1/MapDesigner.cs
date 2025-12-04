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
            private int Collums = 5;
            private int rows = 4;
            private float noiseScale = 10f;
            private int seed = 0;
            private List<Button> tileButtons = new List<Button>();
            private PerlinGen.PerlinNoise perlin;

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
                rows = 5; 
                noiseScale = 10f;
                seed = Environment.TickCount & 0x7fffffff;
            }

            private void SetupPerlin() { perlin = new PerlinGen.PerlinNoise(seed); }

            private void SetupUI()
            {
                Text = "MapDesigner";
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

                controls.Controls.Add(new Label 
                {
                    Text = "Columns:", 
                    Location = new Point(10, CollumY) 
                }
                ); 
                CollumY+= 20;

                nudCollums = new NumericUpDown 
                { 
                    Location = new Point(10, CollumY), 
                    Minimum = 1, 
                    Maximum = 50, 
                    Value = Collums 
                }; 
                controls.Controls.Add(nudCollums); 
                CollumY+= 30;

                controls.Controls.Add(new Label 
                { 
                    Text = "Rows:", 
                    Location = new Point(10, CollumY) 
                }
                ); 
                CollumY+= 20;

                nudRows = new NumericUpDown 
                { 
                    Location = new Point(10, CollumY), 
                    Minimum = 1, 
                    Maximum = 50, 
                    Value = rows 
                }; 
                controls.Controls.Add(nudRows); 
                CollumY+= 30;

                controls.Controls.Add(new Label 
                { 
                    Text = "Noise Scale (1–100):", 
                    Location = new Point(10, CollumY) 
                }
                );
                CollumY+= 20;

                nudScale = new NumericUpDown 
                { 
                    Location = new Point(10, CollumY), 
                    Minimum = 1, 
                    Maximum = 100, 
                    Value = (decimal)noiseScale 
                }; 
                controls.Controls.Add(nudScale); 
                CollumY+= 30;

                controls.Controls.Add(new Label 
                { 
                    Text = "Seed:", 
                    Location = new Point(10, CollumY) 
                }
                ); 
                CollumY+= 20;

                txtSeed = new TextBox 
                { 
                    Location = new Point(10, CollumY), 
                    Width = 200, 
                    Text = seed.ToString() 
                }; 
                controls.Controls.Add(txtSeed); 
                CollumY+= 35;

                btnGenerate = new Button 
                { 
                    Text = "Generate", 
                    Location = new Point(10, CollumY), 
                    Width = 220 
                }; 
                btnGenerate.Click += BtnGenerate_Click; 
                controls.Controls.Add(btnGenerate); 
                CollumY+= 40;

                Button btnSave = new Button 
                { 
                    Text = "Save Map", 
                    Location = new Point(10, CollumY), 
                    Width = 220 
                }; 
                btnSave.Click += ButtonSaveClick; 
                controls.Controls.Add(btnSave); 
                CollumY+= 40;

                Button btnLoad = new Button 
                { 
                    Text = "Load Map", 
                    Location = new Point(10, CollumY), 
                    Width = 220 
                }; 
                btnLoad.Click += ButtonLoadClick; 
                controls.Controls.Add(btnLoad); 
                CollumY+= 40;

                btnStartGame = new Button 
                { 
                    Text = "Start Game", 
                    Location = new Point(10, CollumY),
                    Width = 220 
                }; 
                btnStartGame.Click += BtnStartGame_Click; 
                controls.Controls.Add(btnStartGame); 
                CollumY+= 44;
                

                Label legendTitle = new Label 
                { 
                    Text = "Legend", 
                    Location = new Point(10, CollumY), 
                    Font = new Font(FontFamily.GenericSansSerif, 10, FontStyle.Bold) 
                }; 
                controls.Controls.Add(legendTitle);
                CollumY+= 24;

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
                using (PlanetScreen ts = new PlanetScreen())
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
                        Config.TileType t = TileFromNoise(n);
                        Button tile = new Button 
                        { 
                            Location = new Point(c * Config.TileSize, r * Config.TileSize), 
                            Size = new Size(Config.TileSize - 2, Config.TileSize - 2), 
                            BackColor = TileColor(t), 
                            ForeColor = Color.White, 
                            Text = t.ToString(), 
                            TextAlign = ContentAlignment.BottomCenter, 
                            Font = new Font("Segoe UI", 9, FontStyle.Bold), 
                            Tag = new PerlinGen.TileInfo 
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
        
        private void Tile_Click(object sender, EventArgs e)
        {
            Button b = sender as Button;
            if (b?.Tag is PerlinGen.TileInfo info)
            { 

                info.Type = NextTileType(info.Type);
                b.Tag = info;
                b.BackColor = TileColor(info.Type);
                b.Text = info.Type.ToString();
            }
            
        }

            private Config.TileType TileFromNoise(float n)
            {
                if (n < -0.35f)
                {
                    return Config.TileType.Ocean;
                }
                if (n < -0.05f)
                { 
                    return Config.TileType.GrassLands; 
                }
                if (n < 0.25f)
                {
                    return Config.TileType.Forest;
                }
                if (n < 0.55f)
                {
                    return Config.TileType.Desert;
                }

                return Config.TileType.Mountains;
            }

            private Config.TileType NextTileType(Config.TileType t) 
            {
                Config.TileType[] vals = (Config.TileType[])Enum.GetValues(typeof(Config.TileType)); 
                int index = Array.IndexOf(vals, t); 

                return vals[(index + 1) % vals.Length]; 
            }

            private Color TileColor(Config.TileType t) 
            { 
                switch (t) 
                { 
                    case Config.TileType.Ocean: 
                        return Color.FromArgb(68, 138, 255); 

                    case Config.TileType.GrassLands: 
                        return Color.FromArgb(120, 200, 80); 

                    case Config.TileType.Forest: 
                        return Color.FromArgb(34, 139, 34); 

                    case Config.TileType.Desert: 
                        return Color.FromArgb(194, 178, 128); 

                    case Config.TileType.Mountains: 
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
                List<PerlinGen.TileInfo> tiles = new List<PerlinGen.TileInfo>();
                foreach (Button b in tileButtons)

                    if (b.Tag is PerlinGen.TileInfo t)
                    { 
                        tiles.Add(t);
                    }
                    SaveData.FullGameSaveData map = new SaveData.FullGameSaveData
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
                            SaveData.FullGameSaveData map = JsonConvert.DeserializeObject<SaveData.FullGameSaveData>(File.ReadAllText(ofd.FileName));
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

            private void GenerateFromSaved(SaveData.FullGameSaveData map)
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
                        Tag = info 
                    };

                    tile.Click += Tile_Click; 
                    canvas.Controls.Add(tile); 
                    tileButtons.Add(tile);
                }
            }

        

            
                
        private void MainMenu_Load(object sender, EventArgs e)
        {

        }
    }
    }


