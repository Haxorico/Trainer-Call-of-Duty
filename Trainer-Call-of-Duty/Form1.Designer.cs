namespace Trainer_Call_of_Duty
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
            this.bgwProcessLooker = new System.ComponentModel.BackgroundWorker();
            this.lblGameStatus = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // bgwProcessLooker
            // 
            this.bgwProcessLooker.WorkerReportsProgress = true;
            this.bgwProcessLooker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bgwProcessLooker_DoWork);
            this.bgwProcessLooker.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.bgwProcessLooker_ProgressChanged);
            // 
            // lblGameStatus
            // 
            this.lblGameStatus.AutoSize = true;
            this.lblGameStatus.Location = new System.Drawing.Point(13, 67);
            this.lblGameStatus.Name = "lblGameStatus";
            this.lblGameStatus.Size = new System.Drawing.Size(94, 13);
            this.lblGameStatus.TabIndex = 0;
            this.lblGameStatus.Text = "Game Status: N/A";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.lblGameStatus);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Shown += new System.EventHandler(this.Form1_Shown);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.ComponentModel.BackgroundWorker bgwProcessLooker;
        private System.Windows.Forms.Label lblGameStatus;
    }
}

