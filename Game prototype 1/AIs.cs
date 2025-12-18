using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Game_prototype_1
{
    public class AIPlayer
    {
        public string Name { get; set; }

        public AIPlayer(string name)
        {
            Name = name;

        
        }
        public void DisplayInfo()
        {
            MessageBox.Show($"AI Name:{Name} ");
        }

    }

}
