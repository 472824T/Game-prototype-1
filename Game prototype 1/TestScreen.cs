using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Diagnostics.Eventing.Reader;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Security.Cryptography.X509Certificates;
using System.Windows.Forms;

namespace Game_prototype_1
{
    public partial class TestScreen : Form
    {
        class Resource// virtual class thingy

        {
            public int Amount;
            private string Name;
            public int Standard1;
            public int Standard2;
            public int Standard3;

            public Resource(string name, int amount,int standard1, int standard2, int standard3)
            {
                Name = name;
                Amount = amount;
                Standard1 = standard1;
                Standard2 = standard2;
                Standard3 = standard3;
            }
            public int GetAmount()
            {
                return Amount;
            }
            public string GetName()
            { 
                return Name; 
            }
            public virtual int Standards( int stand = 1)
            {
                return 0;
            }
          
        }
        class Titanium : Resource
        {
            public Titanium() : base("titanium", Config.TitaniumBaseAmount, Config.TitaniumNumOfStand1s, Config.TitaniumNumOfStand2s, Config.TitaniumNumOfStand3s) { } // calling the super class or the parent class with the five attruibutes eg string and five ints

         
            public override int Standards(int stand = 1)
            {
                switch (stand)
                {
                    case 1:
                    {
                            return Config.StandTitanium1;
                    }
                       
                    case 2:
                    { 
                        return Config.StandTitanium2; 
                    }

                    case 3:
                    {
                        return Config.StandTitanium3;
                    }
                } 
                return 0; 
            }
            class EnergyBricks : Resource
            {
                public EnergyBricks() : base("EnergyBricks", Config.EnergyBricksBaseAmount, Config.EnergyBricksNumOfStand1s, Config.EnergyBricksNumOfStand2s, Config.EnergyBricksNumOfStand3s) { }

                // will show the Standard of the producer 
                public override int Standards(int stand = 1)
                {
                    switch (stand)
                    {
                        case 1:
                            {
                                return 1;
                            }

                        case 2:
                            {
                                return 3;
                            }

                        case 3:
                            {
                                return 6;
                            }
                    }
                    return 0;
                }
            }
        }
        class ResourceManager
        { public void FactoryStandListMaker(List<Button> TitaniumFacts)
            {
                foreach (Button B in TitaniumFacts)
                {
                    switch (B.Text)
                    {
                        case "Level1":
                            {
                                
                            }
                            break;
                        case "Level2":
                            {

                            }
                            break;

                        case "Level3":
                            {

                            }
                            break;

                    }
                }
            } List<Resource> ResourcesList = new List<Resource>();
            public int Resourcethingy(Resource producer)
            {
                ResourcesList.Add(producer);
                foreach (Resource resource in ResourcesList)
                    switch (resource.GetName())
                    {
                        case "Titanium":
                            {
                                switch (resource.GetAmount())
                                {
                                    case 1: // build a way to send the amount of titanium into the class and move the code around using the config file
                                        { }
                                break;
                                }
                            }
                            break;


                    }
            }
            

                
        }
        // will handle all resource pluses
       
       
        List<Button> TileButtons = new List<Button>();
       
        
        List<bool> Factory = new List<bool>();
        void GenOfTilePicBoxs(int WantedNum = 0)
        {

            int X = 0;
           
            for (int i = 0; i < WantedNum; i++)
            {
                TileButtons.Add(new Button());
                TileButtons[i].Name = i.ToString();
                TileButtons[i].Location = new System.Drawing.Point(Config.TileX+X, Config.TileY);
                TileButtons[i].Size = new System.Drawing.Size(200, 200);
                TileButtons[i].TabIndex = 1;
                TileButtons[i].BackColor = Color.Yellow;
                TileButtons[i].Text = "Standard1";
                TileButtons[i].Enabled = true;
                TileButtons[i].Click += new EventHandler(MyClickEvent);

                this.Controls.Add(TileButtons[i]);
                X += 200;
                Factory.Add(false);

            }

        }

        void newgame(bool x = false)

        {

            if (x)
            {
                TCSelectionButton.Show();
                WCSelectionButton.Show();
                TLButton.Show();
                TTSelectionButton.Show();

                Title_label.Hide();
                ContinueButton.Hide();
                New_Game_button.Hide();
                LoadGameButton.Hide();
                OptionsButton.Hide();


            }
            else
            {
                TCSelectionButton.Hide();
                WCSelectionButton.Hide();
                TLButton.Hide();
                TTSelectionButton.Hide();

                Title_label.Hide();
                ContinueButton.Hide();
                New_Game_button.Hide();
                LoadGameButton.Hide();
                OptionsButton.Hide();
                ListOfBuildAction.Show();
                LabelTitaniumCount.Show();
                LabelTextTitanium.Show();
                ListedBoxLoader();
            }
        }
        public TestScreen()
        {

            InitializeComponent();

        }

        void ListedBoxLoader()
        {



            foreach (string I in Config.ListedActions)
            {
                ListOfBuildAction.Items.Add(I);
            }
        }


        private void Title_label_Click(object sender, EventArgs e)
        {

        }

        private void ContinueButton_Click(object sender, EventArgs e)
        {
            PrototypeErrorMessage.Show();
        }

        private void New_Game_button_Click(object sender, EventArgs e)
        {
            PrototypeErrorMessage.Hide();
            newgame();
            GenOfTilePicBoxs(5);
        }
        void TileChecker(int IndexOfButton)
        {
            if (ListOfBuildAction.SelectedIndex > -1)
            {
                switch (Config.ListedActions[ListOfBuildAction.SelectedIndex])
                {
                    case "Factory":
                        TileButtons[IndexOfButton].BackColor = Color.Green;

                        break;
                    case "Demolish":
                        TileButtons[IndexOfButton].BackColor = Color.Yellow;
                        Factory[IndexOfButton] = false;
                        break;
                    case "Upgrade":
                        {
                            if (Convert.ToInt32(TileButtons[IndexOfButton].Text[5]) +1 < 4)
                            { 
                            TileButtons[IndexOfButton].Text = "Standard" + (Convert.ToInt32(TileButtons[IndexOfButton].Text[5]) + 1).ToString();
                            }
                            else
                            {
                                MessageBox.Show("Titanium Factoires only go up to level three!");
                            }
                        }
                        break;

                }
            }
            else
            {
                MessageBox.Show("Please select what you would like to build For now the Titanium factory for now.....");
            }

        }
      
        int ButtonFinder(string NameOfButton)
        {
            for (int i = 0; i < TileButtons.Count; i++)
            {
                if (TileButtons[i].Name == NameOfButton)
                {
                    return i;
                }

            }
            return -1;
        }
        void FactoryChecker(int IndexOfButton)
        {
            if (TileButtons[IndexOfButton].BackColor == Color.Green)
            {
                Factory[IndexOfButton] = true;
                FactoryProductAmount(TitaniumMaker());
            }
        }
        int TitaniumMaker()
        {   bool started = false;   
            int i = 0;
            foreach (bool x in Factory)
            {
                if (x)
                {
                    i++;
                }
            }
            if (i > 0 && !started)
            {
                ProductionTimer.Start();
                started = true; 
            }
            else if (i > 0)
            {
                return i; 
            }
            else
            {
                ProductionTimer.Stop(); 
                started = false;
            }
            return i;
        }
      
        void FactoryProductAmount(int AmountOfFacts)
        {
            int x = Convert.ToInt32(LabelTitaniumCount.Text);
            x += AmountOfFacts;


            LabelTitaniumCount.Text = x.ToString();
        }
        private void MyClickEvent(object sender, EventArgs e)
        {

            TileChecker(ButtonFinder(((Button)sender).Name));
            FactoryChecker(ButtonFinder(((Button)sender).Name));

        }

        private void ListOfBuildAction_SelectedIndexChanged(object sender, EventArgs e)
        {


        }

        private void ProductionTimer_Tick(object sender, EventArgs e)
        {
            FactoryProductAmount(TitaniumMaker());
        }
    }
}
