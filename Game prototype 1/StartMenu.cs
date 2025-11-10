using Game_prototype_1.Game_prototype_1;
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
 
        private void ContinueButton_Click(object sender, EventArgs e)
        {
            
        }
        
       private void LoadGameButton_Click(object sender, EventArgs e)
        {
            using (Research research = new Research())
            {
                research.ShowDialog();
            }
        }

        private void New_Game_button_Click_1(object sender, EventArgs e)
        {
            PrototypeErrorMessage.Hide();

            this.Hide();


            using (MainMenu mainMenu = new MainMenu())
            {
                mainMenu.ShowDialog();
            }


            this.Show();
        }
    }
}
