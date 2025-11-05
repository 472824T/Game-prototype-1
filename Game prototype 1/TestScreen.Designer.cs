namespace Game_prototype_1
{
    partial class TestScreen
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
            this.components = new System.ComponentModel.Container();
            this.New_Game_button = new System.Windows.Forms.Button();
            this.LoadGameButton = new System.Windows.Forms.Button();
            this.OptionsButton = new System.Windows.Forms.Button();
            this.ContinueButton = new System.Windows.Forms.Button();
            this.Title_label = new System.Windows.Forms.Label();
            this.PrototypeErrorMessage = new System.Windows.Forms.Label();
            this.ProductionTimer = new System.Windows.Forms.Timer(this.components);
            this.SuspendLayout();
            // 
            // New_Game_button
            // 
            this.New_Game_button.AutoSize = true;
            this.New_Game_button.Location = new System.Drawing.Point(1060, 523);
            this.New_Game_button.Name = "New_Game_button";
            this.New_Game_button.Size = new System.Drawing.Size(484, 71);
            this.New_Game_button.TabIndex = 4;
            this.New_Game_button.Text = "New Game ";
            this.New_Game_button.UseVisualStyleBackColor = true;
            this.New_Game_button.Click += new System.EventHandler(this.New_Game_button_Click);
            // 
            // LoadGameButton
            // 
            this.LoadGameButton.AutoSize = true;
            this.LoadGameButton.Location = new System.Drawing.Point(1060, 638);
            this.LoadGameButton.Name = "LoadGameButton";
            this.LoadGameButton.Size = new System.Drawing.Size(484, 71);
            this.LoadGameButton.TabIndex = 5;
            this.LoadGameButton.Text = "Load Game";
            this.LoadGameButton.UseVisualStyleBackColor = true;
            this.LoadGameButton.Click += new System.EventHandler(this.LoadGameButton_Click);
            // 
            // OptionsButton
            // 
            this.OptionsButton.AutoSize = true;
            this.OptionsButton.Location = new System.Drawing.Point(1060, 758);
            this.OptionsButton.Name = "OptionsButton";
            this.OptionsButton.Size = new System.Drawing.Size(484, 71);
            this.OptionsButton.TabIndex = 6;
            this.OptionsButton.Text = "Options";
            this.OptionsButton.UseVisualStyleBackColor = true;
            // 
            // ContinueButton
            // 
            this.ContinueButton.Location = new System.Drawing.Point(1060, 405);
            this.ContinueButton.Name = "ContinueButton";
            this.ContinueButton.Size = new System.Drawing.Size(484, 71);
            this.ContinueButton.TabIndex = 7;
            this.ContinueButton.Text = "Continue";
            this.ContinueButton.UseVisualStyleBackColor = true;
            this.ContinueButton.Click += new System.EventHandler(this.ContinueButton_Click);
            // 
            // Title_label
            // 
            this.Title_label.AutoSize = true;
            this.Title_label.Font = new System.Drawing.Font("Microsoft Sans Serif", 26F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Title_label.Location = new System.Drawing.Point(972, 172);
            this.Title_label.Name = "Title_label";
            this.Title_label.Size = new System.Drawing.Size(682, 59);
            this.Title_label.TabIndex = 8;
            this.Title_label.Text = "Welcome to the game user ";
            this.Title_label.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // PrototypeErrorMessage
            // 
            this.PrototypeErrorMessage.AutoSize = true;
            this.PrototypeErrorMessage.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.PrototypeErrorMessage.Location = new System.Drawing.Point(1040, 305);
            this.PrototypeErrorMessage.Name = "PrototypeErrorMessage";
            this.PrototypeErrorMessage.Size = new System.Drawing.Size(554, 29);
            this.PrototypeErrorMessage.TabIndex = 14;
            this.PrototypeErrorMessage.Text = "Please click the new game button to start the game";
            this.PrototypeErrorMessage.Visible = false;
            // 
            // ProductionTimer
            // 
            this.ProductionTimer.Interval = 4000;
            this.ProductionTimer.Tick += new System.EventHandler(this.ProductionTimer_Tick);
            // 
            // TestScreen
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(2939, 1412);
            this.Controls.Add(this.PrototypeErrorMessage);
            this.Controls.Add(this.Title_label);
            this.Controls.Add(this.ContinueButton);
            this.Controls.Add(this.OptionsButton);
            this.Controls.Add(this.LoadGameButton);
            this.Controls.Add(this.New_Game_button);
            this.Name = "TestScreen";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.TestScreen_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button New_Game_button;
        private System.Windows.Forms.Button LoadGameButton;
        private System.Windows.Forms.Button OptionsButton;
        private System.Windows.Forms.Button ContinueButton;
        private System.Windows.Forms.Label Title_label;
        private System.Windows.Forms.Label PrototypeErrorMessage;
        private System.Windows.Forms.Timer ProductionTimer;
    }
}

