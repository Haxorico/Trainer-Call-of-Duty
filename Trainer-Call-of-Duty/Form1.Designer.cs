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
            this.txtDebug = new System.Windows.Forms.RichTextBox();
            this.bgwEntityListManager = new System.ComponentModel.BackgroundWorker();
            this.bgwFeatures = new System.ComponentModel.BackgroundWorker();
            this.btnCopyDebug = new System.Windows.Forms.Button();
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
            this.lblGameStatus.Location = new System.Drawing.Point(10, 10);
            this.lblGameStatus.Name = "lblGameStatus";
            this.lblGameStatus.Size = new System.Drawing.Size(94, 13);
            this.lblGameStatus.TabIndex = 0;
            this.lblGameStatus.Text = "Game Status: N/A";
            // 
            // txtDebug
            // 
            this.txtDebug.Location = new System.Drawing.Point(80, 40);
            this.txtDebug.Name = "txtDebug";
            this.txtDebug.Size = new System.Drawing.Size(400, 442);
            this.txtDebug.TabIndex = 1;
            this.txtDebug.Text = "";
            // 
            // bgwEntityListManager
            // 
            this.bgwEntityListManager.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bgwEntityListManager_DoWork);
            // 
            // bgwFeatures
            // 
            this.bgwFeatures.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bgwFeatures_DoWork);
            // 
            // btnCopyDebug
            // 
            this.btnCopyDebug.Location = new System.Drawing.Point(12, 40);
            this.btnCopyDebug.Name = "btnCopyDebug";
            this.btnCopyDebug.Size = new System.Drawing.Size(62, 23);
            this.btnCopyDebug.TabIndex = 2;
            this.btnCopyDebug.Text = "Copy";
            this.btnCopyDebug.UseVisualStyleBackColor = true;
            this.btnCopyDebug.Click += new System.EventHandler(this.btnCopyDebug_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(484, 494);
            this.Controls.Add(this.btnCopyDebug);
            this.Controls.Add(this.txtDebug);
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
        private System.Windows.Forms.RichTextBox txtDebug;
        private System.ComponentModel.BackgroundWorker bgwEntityListManager;
        private System.ComponentModel.BackgroundWorker bgwFeatures;
        private System.Windows.Forms.Button btnCopyDebug;
    }
}

