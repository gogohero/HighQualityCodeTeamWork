namespace  Poker

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
        this.bCheck = new System.Windows.Forms.Button();
        this.buttonCall = new System.Windows.Forms.Button();
        this.buttonRaise = new System.Windows.Forms.Button();
        this.progressBarTimer = new System.Windows.Forms.ProgressBar();
        this.textBoxChips = new System.Windows.Forms.TextBox();
        this.bAdd = new System.Windows.Forms.Button();
        this.tbAdd = new System.Windows.Forms.TextBox();
        this.textBoxBotChips5 = new System.Windows.Forms.TextBox();
        this.textBoxBotChips4 = new System.Windows.Forms.TextBox();
        this.textBoxBotChips3 = new System.Windows.Forms.TextBox();
        this.textBoxBotChips2 = new System.Windows.Forms.TextBox();
        this.textBoxBotChips1 = new System.Windows.Forms.TextBox();
        this.textBoxPot = new System.Windows.Forms.TextBox();
        this.bOptions = new System.Windows.Forms.Button();
        this.buttonBigBlind = new System.Windows.Forms.Button();
        this.textBoxSmallBlind = new System.Windows.Forms.TextBox();
        this.buttoneSmallBlind = new System.Windows.Forms.Button();
        this.textBoxBigBlind = new System.Windows.Forms.TextBox();
        this.bot5Status = new System.Windows.Forms.Label();
        this.bot4Status = new System.Windows.Forms.Label();
        this.bot3Status = new System.Windows.Forms.Label();
        this.bot1Status = new System.Windows.Forms.Label();
        this.playerStatus = new System.Windows.Forms.Label();
        this.bot2Status = new System.Windows.Forms.Label();
        this.label1 = new System.Windows.Forms.Label();
        this.textBoxRaise = new System.Windows.Forms.TextBox();
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
        this.buttonFold.Click += new System.EventHandler(this.ButtonFoldClick);
        // 
        // bCheck
        // 
        this.bCheck.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
        this.bCheck.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
        this.bCheck.Location = new System.Drawing.Point(494, 660);
        this.bCheck.Name = "bCheck";
        this.bCheck.Size = new System.Drawing.Size(134, 62);
        this.bCheck.TabIndex = 2;
        this.bCheck.Text = "Check";
        this.bCheck.UseVisualStyleBackColor = true;
        this.bCheck.Click += new System.EventHandler(this.ButonCheckClick);
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
        this.buttonCall.Click += new System.EventHandler(this.ButtonCallClick);
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
        this.buttonRaise.Click += new System.EventHandler(this.ButtonRaiseClick);
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
        // textBoxChips
        // 
        this.textBoxChips.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
        this.textBoxChips.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
        this.textBoxChips.Location = new System.Drawing.Point(755, 553);
        this.textBoxChips.Name = "textBoxChips";
        this.textBoxChips.Size = new System.Drawing.Size(163, 23);
        this.textBoxChips.TabIndex = 6;
        this.textBoxChips.Text = "Chips : 0";
        // 
        // bAdd
        // 
        this.bAdd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
        this.bAdd.Location = new System.Drawing.Point(12, 697);
        this.bAdd.Name = "bAdd";
        this.bAdd.Size = new System.Drawing.Size(75, 25);
        this.bAdd.TabIndex = 7;
        this.bAdd.Text = "AddChips";
        this.bAdd.UseVisualStyleBackColor = true;
        this.bAdd.Click += new System.EventHandler(this.ButtonAddClick);
        // 
        // tbAdd
        // 
        this.tbAdd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
        this.tbAdd.Location = new System.Drawing.Point(93, 700);
        this.tbAdd.Name = "tbAdd";
        this.tbAdd.Size = new System.Drawing.Size(125, 20);
        this.tbAdd.TabIndex = 8;
        // 
        // textBoxBotChips5
        // 
        this.textBoxBotChips5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
        this.textBoxBotChips5.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
        this.textBoxBotChips5.Location = new System.Drawing.Point(1012, 553);
        this.textBoxBotChips5.Name = "textBoxBotChips5";
        this.textBoxBotChips5.Size = new System.Drawing.Size(152, 23);
        this.textBoxBotChips5.TabIndex = 9;
        this.textBoxBotChips5.Text = "Chips : 0";
        // 
        // textBoxBotChips4
        // 
        this.textBoxBotChips4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
        this.textBoxBotChips4.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
        this.textBoxBotChips4.Location = new System.Drawing.Point(970, 81);
        this.textBoxBotChips4.Name = "textBoxBotChips4";
        this.textBoxBotChips4.Size = new System.Drawing.Size(123, 23);
        this.textBoxBotChips4.TabIndex = 10;
        this.textBoxBotChips4.Text = "Chips : 0";
        // 
        // textBoxBotChips3
        // 
        this.textBoxBotChips3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
        this.textBoxBotChips3.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
        this.textBoxBotChips3.Location = new System.Drawing.Point(755, 81);
        this.textBoxBotChips3.Name = "textBoxBotChips3";
        this.textBoxBotChips3.Size = new System.Drawing.Size(125, 23);
        this.textBoxBotChips3.TabIndex = 11;
        this.textBoxBotChips3.Text = "Chips : 0";
        // 
        // textBoxBotChips2
        // 
        this.textBoxBotChips2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
        this.textBoxBotChips2.Location = new System.Drawing.Point(276, 81);
        this.textBoxBotChips2.Name = "textBoxBotChips2";
        this.textBoxBotChips2.Size = new System.Drawing.Size(133, 23);
        this.textBoxBotChips2.TabIndex = 12;
        this.textBoxBotChips2.Text = "Chips : 0";
        // 
        // textBoxBotChips1
        // 
        this.textBoxBotChips1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
        this.textBoxBotChips1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
        this.textBoxBotChips1.Location = new System.Drawing.Point(181, 553);
        this.textBoxBotChips1.Name = "textBoxBotChips1";
        this.textBoxBotChips1.Size = new System.Drawing.Size(142, 23);
        this.textBoxBotChips1.TabIndex = 13;
        this.textBoxBotChips1.Text = "Chips : 0";
        // 
        // textBoxPot
        // 
        this.textBoxPot.Anchor = System.Windows.Forms.AnchorStyles.None;
        this.textBoxPot.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
        this.textBoxPot.Location = new System.Drawing.Point(606, 212);
        this.textBoxPot.Name = "textBoxPot";
        this.textBoxPot.Size = new System.Drawing.Size(125, 23);
        this.textBoxPot.TabIndex = 14;
        this.textBoxPot.Text = "0";
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
        this.bOptions.Click += new System.EventHandler(this.ButtonOptionsClick);
        // 
        // buttonBigBlind
        // 
        this.buttonBigBlind.Location = new System.Drawing.Point(12, 254);
        this.buttonBigBlind.Name = "buttonBigBlind";
        this.buttonBigBlind.Size = new System.Drawing.Size(75, 23);
        this.buttonBigBlind.TabIndex = 16;
        this.buttonBigBlind.Text = "Big Blind";
        this.buttonBigBlind.UseVisualStyleBackColor = true;
        this.buttonBigBlind.Click += new System.EventHandler(this.ButtonBigBlindClick);
        // 
        // textBoxSmallBlind
        // 
        this.textBoxSmallBlind.Location = new System.Drawing.Point(12, 228);
        this.textBoxSmallBlind.Name = "textBoxSmallBlind";
        this.textBoxSmallBlind.Size = new System.Drawing.Size(75, 20);
        this.textBoxSmallBlind.TabIndex = 17;
        this.textBoxSmallBlind.Text = "250";
        // 
        // buttoneSmallBlind
        // 
        this.buttoneSmallBlind.Location = new System.Drawing.Point(12, 199);
        this.buttoneSmallBlind.Name = "buttoneSmallBlind";
        this.buttoneSmallBlind.Size = new System.Drawing.Size(75, 23);
        this.buttoneSmallBlind.TabIndex = 18;
        this.buttoneSmallBlind.Text = "Small Blind";
        this.buttoneSmallBlind.UseVisualStyleBackColor = true;
        this.buttoneSmallBlind.Click += new System.EventHandler(this.ButtonSmallBlindClick);
        // 
        // textBoxBigBlind
        // 
        this.textBoxBigBlind.Location = new System.Drawing.Point(12, 283);
        this.textBoxBigBlind.Name = "textBoxBigBlind";
        this.textBoxBigBlind.Size = new System.Drawing.Size(75, 20);
        this.textBoxBigBlind.TabIndex = 19;
        this.textBoxBigBlind.Text = "500";
        // 
        // bot5Status
        // 
        this.bot5Status.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
        this.bot5Status.Location = new System.Drawing.Point(1012, 579);
        this.bot5Status.Name = "bot5Status";
        this.bot5Status.Size = new System.Drawing.Size(152, 32);
        this.bot5Status.TabIndex = 26;
        // 
        // bot4Status
        // 
        this.bot4Status.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
        this.bot4Status.Location = new System.Drawing.Point(970, 107);
        this.bot4Status.Name = "bot4Status";
        this.bot4Status.Size = new System.Drawing.Size(123, 32);
        this.bot4Status.TabIndex = 27;
        // 
        // bot3Status
        // 
        this.bot3Status.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
        this.bot3Status.Location = new System.Drawing.Point(755, 107);
        this.bot3Status.Name = "bot3Status";
        this.bot3Status.Size = new System.Drawing.Size(125, 32);
        this.bot3Status.TabIndex = 28;
        // 
        // bot1Status
        // 
        this.bot1Status.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
        this.bot1Status.Location = new System.Drawing.Point(181, 579);
        this.bot1Status.Name = "bot1Status";
        this.bot1Status.Size = new System.Drawing.Size(142, 32);
        this.bot1Status.TabIndex = 29;
        // 
        // playerStatus
        // 
        this.playerStatus.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
        this.playerStatus.Location = new System.Drawing.Point(755, 579);
        this.playerStatus.Name = "playerStatus";
        this.playerStatus.Size = new System.Drawing.Size(163, 32);
        this.playerStatus.TabIndex = 30;
        // 
        // bot2Status
        // 
        this.bot2Status.Location = new System.Drawing.Point(276, 107);
        this.bot2Status.Name = "bot2Status";
        this.bot2Status.Size = new System.Drawing.Size(133, 32);
        this.bot2Status.TabIndex = 31;
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
        // textBoxRaise
        // 
        this.textBoxRaise.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
        this.textBoxRaise.Location = new System.Drawing.Point(965, 703);
        this.textBoxRaise.Name = "textBoxRaise";
        this.textBoxRaise.Size = new System.Drawing.Size(108, 20);
        this.textBoxRaise.TabIndex = 0;
        // 
        // Form1
        // 
        this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
        this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        this.BackgroundImage = global::Poker.Properties.Resources.poker_table___Copy;
        this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
        this.ClientSize = new System.Drawing.Size(1350, 729);
        this.Controls.Add(this.textBoxRaise);
        this.Controls.Add(this.label1);
        this.Controls.Add(this.bot2Status);
        this.Controls.Add(this.playerStatus);
        this.Controls.Add(this.bot1Status);
        this.Controls.Add(this.bot3Status);
        this.Controls.Add(this.bot4Status);
        this.Controls.Add(this.bot5Status);
        this.Controls.Add(this.textBoxBigBlind);
        this.Controls.Add(this.buttoneSmallBlind);
        this.Controls.Add(this.textBoxSmallBlind);
        this.Controls.Add(this.buttonBigBlind);
        this.Controls.Add(this.bOptions);
        this.Controls.Add(this.textBoxPot);
        this.Controls.Add(this.textBoxBotChips1);
        this.Controls.Add(this.textBoxBotChips2);
        this.Controls.Add(this.textBoxBotChips3);
        this.Controls.Add(this.textBoxBotChips4);
        this.Controls.Add(this.textBoxBotChips5);
        this.Controls.Add(this.tbAdd);
        this.Controls.Add(this.bAdd);
        this.Controls.Add(this.textBoxChips);
        this.Controls.Add(this.progressBarTimer);
        this.Controls.Add(this.buttonRaise);
        this.Controls.Add(this.buttonCall);
        this.Controls.Add(this.bCheck);
        this.Controls.Add(this.buttonFold);
        this.DoubleBuffered = true;
        this.Name = "Form1";
        this.Text = "GLS Texas Poker";
        this.Layout += new System.Windows.Forms.LayoutEventHandler(this.LayoutChange);
        this.ResumeLayout(false);
        this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.Button buttonFold;
    private System.Windows.Forms.Button bCheck;
    private System.Windows.Forms.Button buttonCall;
    private System.Windows.Forms.Button buttonRaise;
    private System.Windows.Forms.ProgressBar progressBarTimer;
    private System.Windows.Forms.TextBox textBoxChips;
    private System.Windows.Forms.Button bAdd;
    private System.Windows.Forms.TextBox tbAdd;
    private System.Windows.Forms.TextBox textBoxBotChips5;
    private System.Windows.Forms.TextBox textBoxBotChips4;
    private System.Windows.Forms.TextBox textBoxBotChips3;
    private System.Windows.Forms.TextBox textBoxBotChips2;
    private System.Windows.Forms.TextBox textBoxBotChips1;
    private System.Windows.Forms.TextBox textBoxPot;
    private System.Windows.Forms.Button bOptions;
    private System.Windows.Forms.Button buttonBigBlind;
    private System.Windows.Forms.TextBox textBoxSmallBlind;
    private System.Windows.Forms.Button buttoneSmallBlind;
    private System.Windows.Forms.TextBox textBoxBigBlind;
    private System.Windows.Forms.Label bot5Status;
    private System.Windows.Forms.Label bot4Status;
    private System.Windows.Forms.Label bot3Status;
    private System.Windows.Forms.Label bot1Status;
    private System.Windows.Forms.Label playerStatus;
    private System.Windows.Forms.Label bot2Status;
    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.TextBox textBoxRaise;



}
}