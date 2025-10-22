using Game_prototype_1;
using Game_prototype_1.Game_prototype_1;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics.Eventing.Reader;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Cryptography.X509Certificates;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using static Graphing.Research;

namespace Graphing
{
    public partial class Research : Form
    {
        public Research()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            TechLoading();
            try
            {
                List<PictureBox> PicboxArrows = new List<PictureBox> { ArrrowPicBox1, ArrrowPicBox2, ArrrowPicBox3, ArrrowPicBox4, ArrrowPicBox5 };
                foreach (PictureBox p in PicboxArrows)
                {
                    p.Load(Arrow);
                }

            }
            catch(System.IO.FileNotFoundException)
            { 
                MessageBox.Show("There was an error whilst loading the image pelase check if the arrow is placed into the correct place");
            }
        }

        public void TechLoading()
        {
            Graph graph = new Graph();
            bool AllResearched = true;

            graph.AddEdge(button3, button1);
            graph.AddEdge(button6, button2);
            graph.AddEdge(button4, button3);
            graph.AddEdge(button5, button3);
            graph.AddEdge(button6, button5);
            graph.AddEdge(button7, button6);

            List<Button> Tech = new List<Button> { button1, button2, button3, button4, button5, button6, button7 };
            //for every button b on the screen
            foreach (Button b in Tech)
            { 
                List<Button> predecessors = graph.GetAdjacency(b);
                if (b == null)
                {
                    b.Enabled = true;
                }
                else
                {
                    bool allPredecessorsResearched = true;

                    foreach (Button x in predecessors)
                    {
                        // test if that has been researched
                        if (x.BackColor != Color.Green)
                        {
                            allPredecessorsResearched = false;
                        }
                    }

                    if (allPredecessorsResearched)
                    {
                        b.Enabled = true;
                       
                    }
                    else
                    {
                        b.Enabled = false;
                       
                    }


                }
                if (b.BackColor != Color.Green)
                {
                    AllResearched = false ;
                }
             
            }
            if (AllResearched)
            {
                MessageBox.Show("Well done this is now complete");
            }
        }


        // To go in config 
        string Arrow = "M:\\Visual Studio 2022\\Graphing\\Graphing\\Arrow.png";

       
        public class Graph
        {
            private Dictionary<Button, List<Button>> AdjacencyList;

            public Graph()
            {
                AdjacencyList = new Dictionary<Button, List<Button>>();
            }
            public void AddVertex(Button vertex)
            {
                if (!AdjacencyList.ContainsKey(vertex))
                {
                    AdjacencyList[vertex] = new List<Button>();
                }
            }
            public void AddEdge(Button source, Button destination)
            {

                AddVertex(source);
                AddVertex(destination);
                AdjacencyList[source].Add(destination);

            }
            public List<Button> GetAdjacency(Button button)
            {
                return AdjacencyList[button];
            }
        }

        public void WhenPressed (Button t, int i = 50)
        {
            if (GetResourceAmount("Titanium") >= i && t.BackColor != Color.Green)
            {
                t.BackColor = Color.Green;
                titanuim -= i;
                t.Enabled = false;
                TechLoading();


            }
            else if (titanuim < i && t.BackColor != Color.Green)
            {
                t.BackColor = Color.Yellow;
                MessageBox.Show("U currently don't have enough resources to get this tech");
            }
        }
        
        private void button1_Click(object sender, EventArgs e)
        {
            WhenPressed(button1);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            WhenPressed(button2);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            WhenPressed(button3);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            WhenPressed(button4);
        }

        private void button5_Click_1(object sender, EventArgs e)
        {

            WhenPressed(button5);
        }

        private void button6_Click(object sender, EventArgs e)
        {
            WhenPressed(button6);
        }

        private void button7_Click(object sender, EventArgs e)
        {
            WhenPressed(button7);
        }
    }
    }
    

