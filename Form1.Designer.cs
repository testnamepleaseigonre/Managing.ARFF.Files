
namespace Managing.ARFF.Files
{
    partial class Form1
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
            this.catalogPathTextBox = new System.Windows.Forms.TextBox();
            this.chooseCatalogButton = new System.Windows.Forms.Button();
            this.checkedListBoxAll = new System.Windows.Forms.CheckedListBox();
            this.button1 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.button5 = new System.Windows.Forms.Button();
            this.combineButton = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.progressBar = new System.Windows.Forms.ProgressBar();
            this.stopButton = new System.Windows.Forms.Button();
            this.pauseButton = new System.Windows.Forms.Button();
            this.continueButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // catalogPathTextBox
            // 
            this.catalogPathTextBox.Location = new System.Drawing.Point(14, 14);
            this.catalogPathTextBox.Name = "catalogPathTextBox";
            this.catalogPathTextBox.ReadOnly = true;
            this.catalogPathTextBox.Size = new System.Drawing.Size(496, 20);
            this.catalogPathTextBox.TabIndex = 0;
            // 
            // chooseCatalogButton
            // 
            this.chooseCatalogButton.Location = new System.Drawing.Point(14, 40);
            this.chooseCatalogButton.Name = "chooseCatalogButton";
            this.chooseCatalogButton.Size = new System.Drawing.Size(94, 23);
            this.chooseCatalogButton.TabIndex = 1;
            this.chooseCatalogButton.Text = "Choose folder";
            this.chooseCatalogButton.UseVisualStyleBackColor = true;
            this.chooseCatalogButton.Click += new System.EventHandler(this.chooseCatalogButton_Click);
            // 
            // checkedListBoxAll
            // 
            this.checkedListBoxAll.CheckOnClick = true;
            this.checkedListBoxAll.FormattingEnabled = true;
            this.checkedListBoxAll.Location = new System.Drawing.Point(14, 98);
            this.checkedListBoxAll.Name = "checkedListBoxAll";
            this.checkedListBoxAll.Size = new System.Drawing.Size(496, 214);
            this.checkedListBoxAll.TabIndex = 2;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(114, 40);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(102, 23);
            this.button1.TabIndex = 3;
            this.button1.Text = "Get all attributes";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(14, 318);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(75, 23);
            this.button4.TabIndex = 7;
            this.button4.Text = "All";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // button5
            // 
            this.button5.Location = new System.Drawing.Point(95, 318);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(75, 23);
            this.button5.TabIndex = 8;
            this.button5.Text = "None";
            this.button5.UseVisualStyleBackColor = true;
            this.button5.Click += new System.EventHandler(this.button5_Click);
            // 
            // combineButton
            // 
            this.combineButton.Location = new System.Drawing.Point(340, 318);
            this.combineButton.Name = "combineButton";
            this.combineButton.Size = new System.Drawing.Size(75, 23);
            this.combineButton.TabIndex = 11;
            this.combineButton.Text = "Combine";
            this.combineButton.UseVisualStyleBackColor = true;
            this.combineButton.Click += new System.EventHandler(this.combineButton_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(421, 318);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(89, 23);
            this.button2.TabIndex = 13;
            this.button2.Text = "Write to .arff";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // progressBar
            // 
            this.progressBar.Location = new System.Drawing.Point(14, 69);
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(496, 23);
            this.progressBar.TabIndex = 14;
            // 
            // stopButton
            // 
            this.stopButton.Location = new System.Drawing.Point(273, 40);
            this.stopButton.Name = "stopButton";
            this.stopButton.Size = new System.Drawing.Size(75, 23);
            this.stopButton.TabIndex = 15;
            this.stopButton.Text = "Stop";
            this.stopButton.UseVisualStyleBackColor = true;
            this.stopButton.Click += new System.EventHandler(this.stopButton_Click);
            // 
            // pauseButton
            // 
            this.pauseButton.Location = new System.Drawing.Point(354, 40);
            this.pauseButton.Name = "pauseButton";
            this.pauseButton.Size = new System.Drawing.Size(75, 23);
            this.pauseButton.TabIndex = 16;
            this.pauseButton.Text = "Pause";
            this.pauseButton.UseVisualStyleBackColor = true;
            this.pauseButton.Click += new System.EventHandler(this.pauseButton_Click);
            // 
            // continueButton
            // 
            this.continueButton.Location = new System.Drawing.Point(435, 40);
            this.continueButton.Name = "continueButton";
            this.continueButton.Size = new System.Drawing.Size(75, 23);
            this.continueButton.TabIndex = 17;
            this.continueButton.Text = "Continue";
            this.continueButton.UseVisualStyleBackColor = true;
            this.continueButton.Click += new System.EventHandler(this.continueButton_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(522, 366);
            this.Controls.Add(this.continueButton);
            this.Controls.Add(this.pauseButton);
            this.Controls.Add(this.stopButton);
            this.Controls.Add(this.progressBar);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.combineButton);
            this.Controls.Add(this.button5);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.checkedListBoxAll);
            this.Controls.Add(this.chooseCatalogButton);
            this.Controls.Add(this.catalogPathTextBox);
            this.Name = "Form1";
            this.Text = "ARFF MANAGEMENT";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox catalogPathTextBox;
        private System.Windows.Forms.Button chooseCatalogButton;
        private System.Windows.Forms.CheckedListBox checkedListBoxAll;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.Button combineButton;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.ProgressBar progressBar;
        private System.Windows.Forms.Button stopButton;
        private System.Windows.Forms.Button pauseButton;
        private System.Windows.Forms.Button continueButton;
    }
}

