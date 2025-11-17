namespace Game_prototype_1
{
    partial class ColonyCreationScreen
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.ColonyProgressBar = new System.Windows.Forms.ProgressBar();
            this.button1 = new System.Windows.Forms.Button();
            this.ColonyCreationLabel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // ColonyProgressBar
            // 
            this.ColonyProgressBar.Location = new System.Drawing.Point(590, 686);
            this.ColonyProgressBar.Name = "ColonyProgressBar";
            this.ColonyProgressBar.Size = new System.Drawing.Size(1597, 152);
            this.ColonyProgressBar.TabIndex = 0;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(948, 480);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(627, 171);
            this.button1.TabIndex = 1;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // ColonyCreationLabel
            // 
            this.ColonyCreationLabel.AutoSize = true;
            this.ColonyCreationLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 32F);
            this.ColonyCreationLabel.Location = new System.Drawing.Point(1021, 9);
            this.ColonyCreationLabel.Name = "ColonyCreationLabel";
            this.ColonyCreationLabel.Size = new System.Drawing.Size(492, 73);
            this.ColonyCreationLabel.TabIndex = 2;
            this.ColonyCreationLabel.Text = "Colony Creation";
            // 
            // ColonyCreationScreen
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(2651, 1039);
            this.Controls.Add(this.ColonyCreationLabel);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.ColonyProgressBar);
            this.Name = "ColonyCreationScreen";
            this.Text = "ColonyCreationScreen";
            this.Load += new System.EventHandler(this.ColonyCreationScreen_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ProgressBar ColonyProgressBar;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label ColonyCreationLabel;
    }
}