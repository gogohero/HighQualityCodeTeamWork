﻿namespace Poker
{
    partial class PokerGameForm
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
            this.buttonFold = new System.Windows.Forms.Button();
            this.buttonCheck = new System.Windows.Forms.Button();
            this.buttonCall = new System.Windows.Forms.Button();
            this.buttonRaise = new System.Windows.Forms.Button();
            this.progressBarTimer = new System.Windows.Forms.ProgressBar();
            this.tableChips = new System.Windows.Forms.TextBox();
            this.buttonAdd = new System.Windows.Forms.Button();
            this.tbAdd = new System.Windows.Forms.TextBox();
            this.tbBotChips5 = new System.Windows.Forms.TextBox();
            this.tbBotChips4 = new System.Windows.Forms.TextBox();
            this.tbBotChips3 = new System.Windows.Forms.TextBox();
            this.tbBotChips2 = new System.Windows.Forms.TextBox();
            this.tbBotChips1 = new System.Windows.Forms.TextBox();
            this.tbPot = new System.Windows.Forms.TextBox();
            this.bOptions = new System.Windows.Forms.Button();
            this.bBB = new System.Windows.Forms.Button();
            this.tbSB = new System.Windows.Forms.TextBox();
            this.bSB = new System.Windows.Forms.Button();
            this.tbBB = new System.Windows.Forms.TextBox();
            this.b5Status = new System.Windows.Forms.Label();
            this.b4Status = new System.Windows.Forms.Label();
            this.b3Status = new System.Windows.Forms.Label();
            this.b1Status = new System.Windows.Forms.Label();
            this.pStatus = new System.Windows.Forms.Label();
            this.b2Status = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.tbRaise = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // buttonFold
            // 
            this.buttonFold.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.buttonFold.Font = new System.Drawing.Font("Microsoft Sans Serif", 17F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.buttonFold.Location = new System.Drawing.Point(335, 660);
            this.buttonFold.Name = "buttonFold";
            this.buttonFold.Size = new System.Drawing.Size(130, 62);
            this.buttonFold.TabIndex = 0;
            this.buttonFold.Text = "Fold";
            this.buttonFold.UseVisualStyleBackColor = true;
            this.buttonFold.Click += new System.EventHandler(this.bFold_Click);
            // 
            // buttonCheck
            // 
            this.buttonCheck.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.buttonCheck.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.buttonCheck.Location = new System.Drawing.Point(494, 660);
            this.buttonCheck.Name = "buttonCheck";
            this.buttonCheck.Size = new System.Drawing.Size(134, 62);
            this.buttonCheck.TabIndex = 2;
            this.buttonCheck.Text = "Check";
            this.buttonCheck.UseVisualStyleBackColor = true;
            this.buttonCheck.Click += new System.EventHandler(this.bCheck_Click);
            // 
            // buttonCall
            // 
            this.buttonCall.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.buttonCall.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.buttonCall.Location = new System.Drawing.Point(667, 661);
            this.buttonCall.Name = "buttonCall";
            this.buttonCall.Size = new System.Drawing.Size(126, 62);
            this.buttonCall.TabIndex = 3;
            this.buttonCall.Text = "Call";
            this.buttonCall.UseVisualStyleBackColor = true;
            this.buttonCall.Click += new System.EventHandler(this.bCall_Click);
            // 
            // buttonRaise
            // 
            this.buttonRaise.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.buttonRaise.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.buttonRaise.Location = new System.Drawing.Point(835, 661);
            this.buttonRaise.Name = "buttonRaise";
            this.buttonRaise.Size = new System.Drawing.Size(124, 62);
            this.buttonRaise.TabIndex = 4;
            this.buttonRaise.Text = "Raise";
            this.buttonRaise.UseVisualStyleBackColor = true;
            this.buttonRaise.Click += new System.EventHandler(this.bRaise_Click);
            // 
            // progressBarTimer
            // 
            this.progressBarTimer.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.progressBarTimer.BackColor = System.Drawing.SystemColors.Control;
            this.progressBarTimer.Location = new System.Drawing.Point(335, 631);
            this.progressBarTimer.Maximum = 1000;
            this.progressBarTimer.Name = "progressBarTimer";
            this.progressBarTimer.Size = new System.Drawing.Size(667, 23);
            this.progressBarTimer.TabIndex = 5;
            this.progressBarTimer.Value = 1000;
            // 
            // tableChips
            // 
            this.tableChips.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.tableChips.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.tableChips.Location = new System.Drawing.Point(755, 553);
            this.tableChips.Name = "tableChips";
            this.tableChips.Size = new System.Drawing.Size(163, 23);
            this.tableChips.TabIndex = 6;
            this.tableChips.Text = "playerChips : 0";
            // 
            // buttonAdd
            // 
            this.buttonAdd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonAdd.Location = new System.Drawing.Point(12, 697);
            this.buttonAdd.Name = "buttonAdd";
            this.buttonAdd.Size = new System.Drawing.Size(75, 25);
            this.buttonAdd.TabIndex = 7;
            this.buttonAdd.Text = "AddChips";
            this.buttonAdd.UseVisualStyleBackColor = true;
            this.buttonAdd.Click += new System.EventHandler(this.bAdd_Click);
            // 
            // tbAdd
            // 
            this.tbAdd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.tbAdd.Location = new System.Drawing.Point(93, 700);
            this.tbAdd.Name = "tbAdd";
            this.tbAdd.Size = new System.Drawing.Size(125, 20);
            this.tbAdd.TabIndex = 8;
            // 
            // tbBotChips5
            // 
            this.tbBotChips5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.tbBotChips5.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.tbBotChips5.Location = new System.Drawing.Point(1012, 553);
            this.tbBotChips5.Name = "tbBotChips5";
            this.tbBotChips5.Size = new System.Drawing.Size(152, 23);
            this.tbBotChips5.TabIndex = 9;
            this.tbBotChips5.Text = "playerChips : 0";
            // 
            // tbBotChips4
            // 
            this.tbBotChips4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.tbBotChips4.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.tbBotChips4.Location = new System.Drawing.Point(970, 81);
            this.tbBotChips4.Name = "tbBotChips4";
            this.tbBotChips4.Size = new System.Drawing.Size(123, 23);
            this.tbBotChips4.TabIndex = 10;
            this.tbBotChips4.Text = "playerChips : 0";
            // 
            // tbBotChips3
            // 
            this.tbBotChips3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.tbBotChips3.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.tbBotChips3.Location = new System.Drawing.Point(755, 81);
            this.tbBotChips3.Name = "tbBotChips3";
            this.tbBotChips3.Size = new System.Drawing.Size(125, 23);
            this.tbBotChips3.TabIndex = 11;
            this.tbBotChips3.Text = "playerChips : 0";
            // 
            // tbBotChips2
            // 
            this.tbBotChips2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.tbBotChips2.Location = new System.Drawing.Point(276, 81);
            this.tbBotChips2.Name = "tbBotChips2";
            this.tbBotChips2.Size = new System.Drawing.Size(133, 23);
            this.tbBotChips2.TabIndex = 12;
            this.tbBotChips2.Text = "playerChips : 0";
            // 
            // tbBotChips1
            // 
            this.tbBotChips1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.tbBotChips1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.tbBotChips1.Location = new System.Drawing.Point(181, 553);
            this.tbBotChips1.Name = "tbBotChips1";
            this.tbBotChips1.Size = new System.Drawing.Size(142, 23);
            this.tbBotChips1.TabIndex = 13;
            this.tbBotChips1.Text = "playerChips : 0";
            // 
            // tbPot
            // 
            this.tbPot.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.tbPot.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.tbPot.Location = new System.Drawing.Point(606, 212);
            this.tbPot.Name = "tbPot";
            this.tbPot.Size = new System.Drawing.Size(125, 23);
            this.tbPot.TabIndex = 14;
            this.tbPot.Text = "0";
            // 
            // bOptions
            // 
            this.bOptions.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.bOptions.Location = new System.Drawing.Point(12, 12);
            this.bOptions.Name = "bOptions";
            this.bOptions.Size = new System.Drawing.Size(75, 36);
            this.bOptions.TabIndex = 15;
            this.bOptions.Text = "BB/SB";
            this.bOptions.UseVisualStyleBackColor = true;
            this.bOptions.Click += new System.EventHandler(this.bOptions_Click);
            // 
            // bBB
            // 
            this.bBB.Location = new System.Drawing.Point(12, 254);
            this.bBB.Name = "bBB";
            this.bBB.Size = new System.Drawing.Size(75, 23);
            this.bBB.TabIndex = 16;
            this.bBB.Text = "Big Blind";
            this.bBB.UseVisualStyleBackColor = true;
            this.bBB.Click += new System.EventHandler(this.bBB_Click);
            // 
            // tbSB
            // 
            this.tbSB.Location = new System.Drawing.Point(12, 228);
            this.tbSB.Name = "tbSB";
            this.tbSB.Size = new System.Drawing.Size(75, 20);
            this.tbSB.TabIndex = 17;
            this.tbSB.Text = "250";
            // 
            // bSB
            // 
            this.bSB.Location = new System.Drawing.Point(12, 199);
            this.bSB.Name = "bSB";
            this.bSB.Size = new System.Drawing.Size(75, 23);
            this.bSB.TabIndex = 18;
            this.bSB.Text = "Small Blind";
            this.bSB.UseVisualStyleBackColor = true;
            this.bSB.Click += new System.EventHandler(this.bSB_Click);
            // 
            // tbBB
            // 
            this.tbBB.Location = new System.Drawing.Point(12, 283);
            this.tbBB.Name = "tbBB";
            this.tbBB.Size = new System.Drawing.Size(75, 20);
            this.tbBB.TabIndex = 19;
            this.tbBB.Text = "500";
            // 
            // b5Status
            // 
            this.b5Status.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.b5Status.Location = new System.Drawing.Point(1012, 579);
            this.b5Status.Name = "b5Status";
            this.b5Status.Size = new System.Drawing.Size(152, 32);
            this.b5Status.TabIndex = 26;
            // 
            // b4Status
            // 
            this.b4Status.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.b4Status.Location = new System.Drawing.Point(970, 107);
            this.b4Status.Name = "b4Status";
            this.b4Status.Size = new System.Drawing.Size(123, 32);
            this.b4Status.TabIndex = 27;
            // 
            // b3Status
            // 
            this.b3Status.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.b3Status.Location = new System.Drawing.Point(755, 107);
            this.b3Status.Name = "b3Status";
            this.b3Status.Size = new System.Drawing.Size(125, 32);
            this.b3Status.TabIndex = 28;
            // 
            // b1Status
            // 
            this.b1Status.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.b1Status.Location = new System.Drawing.Point(181, 579);
            this.b1Status.Name = "b1Status";
            this.b1Status.Size = new System.Drawing.Size(142, 32);
            this.b1Status.TabIndex = 29;
            // 
            // pStatus
            // 
            this.pStatus.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.pStatus.Location = new System.Drawing.Point(755, 579);
            this.pStatus.Name = "pStatus";
            this.pStatus.Size = new System.Drawing.Size(163, 32);
            this.pStatus.TabIndex = 30;
            // 
            // b2Status
            // 
            this.b2Status.Location = new System.Drawing.Point(276, 107);
            this.b2Status.Name = "b2Status";
            this.b2Status.Size = new System.Drawing.Size(133, 32);
            this.b2Status.TabIndex = 31;
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label1.Location = new System.Drawing.Point(654, 188);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(31, 21);
            this.label1.TabIndex = 0;
            this.label1.Text = "Pot";
            // 
            // tbRaise
            // 
            this.tbRaise.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.tbRaise.Location = new System.Drawing.Point(965, 703);
            this.tbRaise.Name = "tbRaise";
            this.tbRaise.Size = new System.Drawing.Size(108, 20);
            this.tbRaise.TabIndex = 0;
            // 
            // PokerGameForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::Poker.Properties.Resources.poker_table___Copy;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(1350, 729);
            this.Controls.Add(this.tbRaise);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.b2Status);
            this.Controls.Add(this.pStatus);
            this.Controls.Add(this.b1Status);
            this.Controls.Add(this.b3Status);
            this.Controls.Add(this.b4Status);
            this.Controls.Add(this.b5Status);
            this.Controls.Add(this.tbBB);
            this.Controls.Add(this.bSB);
            this.Controls.Add(this.tbSB);
            this.Controls.Add(this.bBB);
            this.Controls.Add(this.bOptions);
            this.Controls.Add(this.tbPot);
            this.Controls.Add(this.tbBotChips1);
            this.Controls.Add(this.tbBotChips2);
            this.Controls.Add(this.tbBotChips3);
            this.Controls.Add(this.tbBotChips4);
            this.Controls.Add(this.tbBotChips5);
            this.Controls.Add(this.tbAdd);
            this.Controls.Add(this.buttonAdd);
            this.Controls.Add(this.tableChips);
            this.Controls.Add(this.progressBarTimer);
            this.Controls.Add(this.buttonRaise);
            this.Controls.Add(this.buttonCall);
            this.Controls.Add(this.buttonCheck);
            this.Controls.Add(this.buttonFold);
            this.DoubleBuffered = true;
            this.Name = "PokerGameForm";
            this.Text = "GLS Texas Poker";
            this.Layout += new System.Windows.Forms.LayoutEventHandler(this.Layout_Change);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonFold;
        private System.Windows.Forms.Button buttonCheck;
        private System.Windows.Forms.Button buttonCall;
        private System.Windows.Forms.Button buttonRaise;
        private System.Windows.Forms.ProgressBar progressBarTimer;
        private System.Windows.Forms.TextBox tableChips;
        private System.Windows.Forms.Button buttonAdd;
        private System.Windows.Forms.TextBox tbAdd;
        private System.Windows.Forms.TextBox tbBotChips5;
        private System.Windows.Forms.TextBox tbBotChips4;
        private System.Windows.Forms.TextBox tbBotChips3;
        private System.Windows.Forms.TextBox tbBotChips2;
        private System.Windows.Forms.TextBox tbBotChips1;
        private System.Windows.Forms.TextBox tbPot;
        private System.Windows.Forms.Button bOptions;
        private System.Windows.Forms.Button bBB;
        private System.Windows.Forms.TextBox tbSB;
        private System.Windows.Forms.Button bSB;
        private System.Windows.Forms.TextBox tbBB;
        private System.Windows.Forms.Label b5Status;
        private System.Windows.Forms.Label b4Status;
        private System.Windows.Forms.Label b3Status;
        private System.Windows.Forms.Label b1Status;
        private System.Windows.Forms.Label pStatus;
        private System.Windows.Forms.Label b2Status;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tbRaise;



    }
}

