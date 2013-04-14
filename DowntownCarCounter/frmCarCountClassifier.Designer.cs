namespace DowntownCarCounter
{
    partial class frmCarCountClassifier
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
            this.btnStart = new System.Windows.Forms.Button();
            this.lvTargets = new System.Windows.Forms.ListView();
            this.btnPause = new System.Windows.Forms.Button();
            this.tbTime = new System.Windows.Forms.TrackBar();
            this.myDisplay1 = new DowntownCarCounter.MyDisplay();
            this.btnStep = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.tbTime)).BeginInit();
            this.SuspendLayout();
            // 
            // btnStart
            // 
            this.btnStart.Location = new System.Drawing.Point(12, 12);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(75, 23);
            this.btnStart.TabIndex = 1;
            this.btnStart.Text = "Start";
            this.btnStart.UseVisualStyleBackColor = true;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // lvTargets
            // 
            this.lvTargets.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lvTargets.FullRowSelect = true;
            this.lvTargets.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.lvTargets.Location = new System.Drawing.Point(12, 41);
            this.lvTargets.MultiSelect = false;
            this.lvTargets.Name = "lvTargets";
            this.lvTargets.Size = new System.Drawing.Size(347, 394);
            this.lvTargets.TabIndex = 2;
            this.lvTargets.UseCompatibleStateImageBehavior = false;
            this.lvTargets.View = System.Windows.Forms.View.Details;
            this.lvTargets.SelectedIndexChanged += new System.EventHandler(this.lvTargets_SelectedIndexChanged);
            // 
            // btnPause
            // 
            this.btnPause.Location = new System.Drawing.Point(93, 12);
            this.btnPause.Name = "btnPause";
            this.btnPause.Size = new System.Drawing.Size(75, 23);
            this.btnPause.TabIndex = 4;
            this.btnPause.Text = "Pause";
            this.btnPause.UseVisualStyleBackColor = true;
            this.btnPause.Click += new System.EventHandler(this.btnPause_Click);
            // 
            // tbTime
            // 
            this.tbTime.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.tbTime.Enabled = false;
            this.tbTime.Location = new System.Drawing.Point(365, 277);
            this.tbTime.Maximum = 6000;
            this.tbTime.Name = "tbTime";
            this.tbTime.Size = new System.Drawing.Size(380, 45);
            this.tbTime.TabIndex = 5;
            this.tbTime.TickFrequency = 100;
            // 
            // myDisplay1
            // 
            this.myDisplay1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.myDisplay1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.myDisplay1.Location = new System.Drawing.Point(365, 41);
            this.myDisplay1.Name = "myDisplay1";
            this.myDisplay1.Size = new System.Drawing.Size(380, 230);
            this.myDisplay1.TabIndex = 3;
            this.myDisplay1.Paint += new System.Windows.Forms.PaintEventHandler(this.pnlRoad_Paint);
            // 
            // btnStep
            // 
            this.btnStep.Location = new System.Drawing.Point(174, 12);
            this.btnStep.Name = "btnStep";
            this.btnStep.Size = new System.Drawing.Size(75, 23);
            this.btnStep.TabIndex = 6;
            this.btnStep.Text = "Step";
            this.btnStep.UseVisualStyleBackColor = true;
            this.btnStep.Click += new System.EventHandler(this.btnStep_Click);
            // 
            // frmCarCountClassifier
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(757, 447);
            this.Controls.Add(this.btnStep);
            this.Controls.Add(this.tbTime);
            this.Controls.Add(this.btnPause);
            this.Controls.Add(this.myDisplay1);
            this.Controls.Add(this.lvTargets);
            this.Controls.Add(this.btnStart);
            this.Name = "frmCarCountClassifier";
            this.Text = "CarCountClassifier";
            ((System.ComponentModel.ISupportInitialize)(this.tbTime)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.ListView lvTargets;
        private MyDisplay myDisplay1;
        private System.Windows.Forms.Button btnPause;
        private System.Windows.Forms.TrackBar tbTime;
        private System.Windows.Forms.Button btnStep;
    }
}

