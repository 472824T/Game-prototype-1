using Graphing;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Game_prototype_1
{
    public partial class StartMenu : Form
    {
        public StartMenu()
        {
            InitializeComponent();
        }

        private void StartMenu_Load(object sender, EventArgs e)
        {

        }

        private void New_Game_button_Click(object sender, EventArgs e)
        {
            PrototypeErrorMessage.Hide();
            this.Hide();
            using (PlanetScreen planetscreen = new PlanetScreen())
            {
                planetscreen.ShowDialog();
            }
            this.Show();
        }

        private void ContinueButton_Click(object sender, EventArgs e)
        {
            PrototypeErrorMessage.Hide();
            this.Hide();
            using (MapDesigner mapDesigner = new MapDesigner())
            {
                mapDesigner.ShowDialog();
            }
            this.Show();
        }
    }
}
