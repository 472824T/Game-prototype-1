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
        public partial class MainMenu : Form
        {
            private Panel gridPanel;
            private NumericUpDown nudCols;
            private NumericUpDown nudRows;
            private NumericUpDown nudScale;
            private TextBox txtSeed;
            private Button btnGenerate;
            private Button btnStartGame;

            
            private int cols = 5;
            private int rows = 4;
            private float noiseScale = 10f;
            private int seed = 0;
            private List<Button> tileButtons = new List<Button>();
            private PerlinNoise perlin;

            public MainMenu()
            {
                InitializeComponent();
                SetupDefaults();
                SetupUI();
                SetupPerlin();
                GenerateGrid();
            }

            private void SetupDefaults()
            {
                cols = 5; rows = 4; noiseScale = 10f;
                seed = Environment.TickCount & 0x7fffffff;
            }

            private void SetupPerlin() { perlin = new PerlinNoise(seed); }

            private void SetupUI()
            {
                Text = "Main Menu";
                ClientSize = new Size(1200, 800);
                FormBorderStyle = FormBorderStyle.FixedSingle;
                MaximizeBox = false;

                Panel controls = new Panel { Location = new Point(10, 10), Size = new Size(260, ClientSize.Height - 20), BorderStyle = BorderStyle.FixedSingle, AutoScroll = true };
                Controls.Add(controls);

                int cy = 10;
                controls.Controls.Add(new Label { Text = "Columns:", Location = new Point(10, cy) }); 
                cy += 20;
                nudCols = new NumericUpDown { Location = new Point(10, cy), Minimum = 1, Maximum = 50, Value = cols }; 
                controls.Controls.Add(nudCols); 
                cy += 30;
                controls.Controls.Add(new Label { Text = "Rows:", Location = new Point(10, cy) }); 
                cy += 20;
                nudRows = new NumericUpDown { Location = new Point(10, cy), Minimum = 1, Maximum = 50, Value = rows }; 
                controls.Controls.Add(nudRows); 
                cy += 30;
                controls.Controls.Add(new Label { Text = "Noise Scale (1–100):", Location = new Point(10, cy) });
                cy += 20;
                nudScale = new NumericUpDown { Location = new Point(10, cy), Minimum = 1, Maximum = 100, Value = (decimal)noiseScale }; 
                controls.Controls.Add(nudScale); 
                cy += 30;
                controls.Controls.Add(new Label { Text = "Seed:", Location = new Point(10, cy) }); 
                cy += 20;
                txtSeed = new TextBox { Location = new Point(10, cy), Width = 200, Text = seed.ToString() }; 
                controls.Controls.Add(txtSeed); 
                cy += 35;
                btnGenerate = new Button { Text = "Generate", Location = new Point(10, cy), Width = 220 }; 
                btnGenerate.Click += BtnGenerate_Click; 
                controls.Controls.Add(btnGenerate); 
                cy += 40;
                Button btnSave = new Button { Text = "Save Map", Location = new Point(10, cy), Width = 220 }; 
                btnSave.Click += BtnSave_Click; 
                controls.Controls.Add(btnSave); 
                cy += 40;
                Button btnLoad = new Button { Text = "Load Map", Location = new Point(10, cy), Width = 220 }; 
                btnLoad.Click += BtnLoad_Click; 
                controls.Controls.Add(btnLoad); 
                cy += 40;

                btnStartGame = new Button { Text = "Start Game", Location = new Point(10, cy), Width = 220 }; btnStartGame.Click += BtnStartGame_Click; controls.Controls.Add(btnStartGame); cy += 44;

                Label legendTitle = new Label { Text = "Legend", Location = new Point(10, cy), Font = new Font(FontFamily.GenericSansSerif, 10, FontStyle.Bold) }; controls.Controls.Add(legendTitle); cy += 24;
                foreach (TileType t in Enum.GetValues(typeof(TileType))) {
                Label l = new Label { Text = t.ToString(), Location = new Point(10, cy), AutoSize = true }; Panel p = new Panel { BackColor = TileColor(t), Location = new Point(150, cy + 3), Size = new Size(25, 20) }; controls.Controls.Add(l); controls.Controls.Add(p); cy += 25; }

                gridPanel = new Panel { Location = new Point(280, 10), Size = new Size(ClientSize.Width - 300, ClientSize.Height - 20), BorderStyle = BorderStyle.FixedSingle, AutoScroll = true };
                Controls.Add(gridPanel);
            }

            private void BtnStartGame_Click(object sender, EventArgs e)
            {
                using (TestScreen ts = new TestScreen())
                {
                    ts.ShowDialog();
                }
            }

            private void BtnGenerate_Click(object sender, EventArgs e)
            {
                cols = (int)nudCols.Value; rows = (int)nudRows.Value; noiseScale = (float)nudScale.Value;
                seed = int.TryParse(txtSeed.Text, out int p) ? p : Environment.TickCount;
                SetupPerlin(); 
                GenerateGrid();
            }

            private void GenerateGrid()
            {
                gridPanel.Controls.Clear(); tileButtons.Clear();
                int width = cols * Config.TileSize;
                int height = rows * Config.TileSize;
                Panel canvas = new Panel { Location = new Point(0, 0), Size = new Size(width, height) }; 
                gridPanel.Controls.Add(canvas);

                for (int r = 0; r < rows; r++)
                {
                    for (int c = 0; c < cols; c++)
                    {
                        float nx = c / noiseScale; 
                        float ny = r / noiseScale; 
                        float n = perlin.Noise(nx, ny);
                        TileType t = TileFromNoise(n);
                        Button tile = new Button 
                        { 
                        Location = new Point(c * Config.TileSize, r * Config.TileSize), 
                        Size = new Size(Config.TileSize - 2, Config.TileSize - 2), 
                        BackColor = TileColor(t), ForeColor = Color.White, Text = t.ToString(), 
                        TextAlign = ContentAlignment.BottomCenter, 
                        Font = new Font("Segoe UI", 9, FontStyle.Bold), 
                        Tag = new TileInfo { Col = c, Row = r, Type = t, Level = 1 }
                        };
                        tile.Click += Tile_Click; 
                        canvas.Controls.Add(tile); 
                        tileButtons.Add(tile);
                    }
                }
            }

            private void Tile_Click(object sender, EventArgs e) { Button b = sender as Button; if (b?.Tag is TileInfo info) { info.Type = NextTileType(info.Type);
                b.Tag = info; b.BackColor = TileColor(info.Type); 
                b.Text = info.Type.ToString(); 
            } }

            private TileType TileFromNoise(float n)
            {
                if (n < -0.35f) return TileType.Ocean;
                if (n < -0.05f) return TileType.GrassLands;
                if (n < 0.25f) return TileType.Forest;
                if (n < 0.55f) return TileType.Desert;
                return TileType.Mountains;
            }

            private TileType NextTileType(TileType t) { TileType[] vals = (TileType[])Enum.GetValues(typeof(TileType)); int idx = Array.IndexOf(vals, t); return vals[(idx + 1) % vals.Length]; }

            private Color TileColor(TileType t) { switch (t) { case TileType.Ocean: return Color.FromArgb(68, 138, 255); case TileType.GrassLands: return Color.FromArgb(120, 200, 80); case TileType.Forest: return Color.FromArgb(34, 139, 34); case TileType.Desert: return Color.FromArgb(194, 178, 128); case TileType.Mountains: return Color.FromArgb(120, 120, 120); default: return Color.White; } }

            private void BtnSave_Click(object sender, EventArgs e)
            {
                using (SaveFileDialog sfd = new SaveFileDialog { Filter = "JSON Map (*.json)|*.json" })
                {
                    if (sfd.ShowDialog() != DialogResult.OK) return;
                    List<TileInfo> tiles = new List<TileInfo>();
                    foreach (Button b in tileButtons) if (b.Tag is TileInfo t) tiles.Add(t);
                    MapSaveData map = new MapSaveData { Columns = cols, Rows = rows, Seed = seed, NoiseScale = noiseScale, Tiles = tiles };
                    File.WriteAllText(sfd.FileName, JsonConvert.SerializeObject(map, Formatting.Indented));
                    MessageBox.Show("Map saved successfully!");
                }
            }

            private void BtnLoad_Click(object sender, EventArgs e)
            {
                using (OpenFileDialog ofd = new OpenFileDialog { Filter = "JSON Map (*.json)|*.json" })
                {
                    if (ofd.ShowDialog() != DialogResult.OK) return;
                        try
                        {
                            MapSaveData map = JsonConvert.DeserializeObject<MapSaveData>(File.ReadAllText(ofd.FileName));
                            if (map != null) 
                            { 
                                cols = map.Columns; 
                                rows = map.Rows; 
                                seed = map.Seed; 
                                noiseScale = map.NoiseScale; 
                                nudCols.Value = cols; 
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
                gridPanel.Controls.Clear(); tileButtons.Clear();
                Panel canvas = new Panel { Location = new Point(0, 0), Size = new Size(map.Columns * Config.TileSize, map.Rows * Config.TileSize) };
                gridPanel.Controls.Add(canvas);
                foreach (TileInfo info in map.Tiles)
                {
                    Button tile = new Button 
                    { Location = new Point(info.Col * Config.TileSize, info.Row * Config.TileSize),
                      Size = new Size(Config.TileSize - 2, Config.TileSize - 2), BackColor = TileColor(info.Type), ForeColor = Color.White, Text = info.Type.ToString(), TextAlign = ContentAlignment.BottomCenter, Font = new Font("Segoe UI", 9, FontStyle.Bold), Tag = info };
                    tile.Click += Tile_Click; 
                    canvas.Controls.Add(tile); 
                    tileButtons.Add(tile);
                }
            }

            private enum TileType { Ocean, GrassLands, Forest, Desert, Mountains }

            private class TileInfo { public int Col { get; set; } public int Row { get; set; } 
            public TileType Type { get; set; } 
            public int Level { get; set; } }

            private class MapSaveData 
        { public int Columns { get; set; } public int Rows { get; set; } public int Seed { get; set; } public float NoiseScale { get; set; } public List<TileInfo> Tiles { get; set; } }

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
                public float Noise(float x, float y) { int X = FastFloor(x) & 255; int Y = FastFloor(y) & 255; float xf = x - (float)Math.Floor(x); float yf = y - (float)Math.Floor(y); float u = Fade(xf); float v = Fade(yf); int aa = perm[X + perm[Y]]; int ab = perm[X + perm[Y + 1]]; int ba = perm[X + 1 + perm[Y]]; int bb = perm[X + 1 + perm[Y + 1]]; float x1 = Lerp(Grad(aa, xf, yf), Grad(ba, xf - 1, yf), u); float x2 = Lerp(Grad(ab, xf, yf - 1), Grad(bb, xf - 1, yf - 1), u); return Lerp(x1, x2, v); }
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


