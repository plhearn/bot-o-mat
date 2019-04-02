namespace Bot_O_Mat
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnAddRobot = new System.Windows.Forms.Button();
            this.cbRobotType = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtRobotName = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btnStartTask = new System.Windows.Forms.Button();
            this.cbTask = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.cbRobot = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.lblLeaderBoard = new System.Windows.Forms.Label();
            this.lvTasks = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader4 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.btnSimulate = new System.Windows.Forms.Button();
            this.btnClearHistory = new System.Windows.Forms.Button();
            this.lvLeaderBoard = new System.Windows.Forms.ListView();
            this.columnHeader5 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader6 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader7 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnAddRobot);
            this.groupBox1.Controls.Add(this.cbRobotType);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.txtRobotName);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(354, 204);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Add Robot";
            // 
            // btnAddRobot
            // 
            this.btnAddRobot.Location = new System.Drawing.Point(219, 142);
            this.btnAddRobot.Name = "btnAddRobot";
            this.btnAddRobot.Size = new System.Drawing.Size(109, 36);
            this.btnAddRobot.TabIndex = 4;
            this.btnAddRobot.Text = "Add Robot";
            this.btnAddRobot.UseVisualStyleBackColor = true;
            this.btnAddRobot.Click += new System.EventHandler(this.btnAddRobot_Click);
            // 
            // cbRobotType
            // 
            this.cbRobotType.FormattingEnabled = true;
            this.cbRobotType.Location = new System.Drawing.Point(142, 95);
            this.cbRobotType.Name = "cbRobotType";
            this.cbRobotType.Size = new System.Drawing.Size(186, 28);
            this.cbRobotType.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(17, 103);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(43, 20);
            this.label2.TabIndex = 2;
            this.label2.Text = "Type";
            // 
            // txtRobotName
            // 
            this.txtRobotName.Location = new System.Drawing.Point(147, 45);
            this.txtRobotName.Name = "txtRobotName";
            this.txtRobotName.Size = new System.Drawing.Size(181, 26);
            this.txtRobotName.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(17, 39);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(51, 20);
            this.label1.TabIndex = 0;
            this.label1.Text = "Name";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.btnStartTask);
            this.groupBox2.Controls.Add(this.cbTask);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.cbRobot);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Location = new System.Drawing.Point(12, 237);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(354, 215);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Start Task";
            // 
            // btnStartTask
            // 
            this.btnStartTask.Location = new System.Drawing.Point(237, 150);
            this.btnStartTask.Name = "btnStartTask";
            this.btnStartTask.Size = new System.Drawing.Size(100, 29);
            this.btnStartTask.TabIndex = 4;
            this.btnStartTask.Text = "Start Task";
            this.btnStartTask.UseVisualStyleBackColor = true;
            this.btnStartTask.Click += new System.EventHandler(this.btnStartTask_Click);
            // 
            // cbTask
            // 
            this.cbTask.FormattingEnabled = true;
            this.cbTask.Location = new System.Drawing.Point(75, 101);
            this.cbTask.Name = "cbTask";
            this.cbTask.Size = new System.Drawing.Size(262, 28);
            this.cbTask.TabIndex = 3;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(14, 109);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(43, 20);
            this.label4.TabIndex = 2;
            this.label4.Text = "Task";
            // 
            // cbRobot
            // 
            this.cbRobot.FormattingEnabled = true;
            this.cbRobot.Location = new System.Drawing.Point(75, 41);
            this.cbRobot.Name = "cbRobot";
            this.cbRobot.Size = new System.Drawing.Size(118, 28);
            this.cbRobot.TabIndex = 1;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(14, 41);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 20);
            this.label3.TabIndex = 0;
            this.label3.Text = "Robot";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.lvLeaderBoard);
            this.groupBox3.Controls.Add(this.lblLeaderBoard);
            this.groupBox3.Location = new System.Drawing.Point(12, 458);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(384, 386);
            this.groupBox3.TabIndex = 2;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Leader Board";
            // 
            // lblLeaderBoard
            // 
            this.lblLeaderBoard.AutoSize = true;
            this.lblLeaderBoard.Location = new System.Drawing.Point(47, 47);
            this.lblLeaderBoard.Name = "lblLeaderBoard";
            this.lblLeaderBoard.Size = new System.Drawing.Size(0, 20);
            this.lblLeaderBoard.TabIndex = 0;
            // 
            // lvTasks
            // 
            this.lvTasks.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader4,
            this.columnHeader3});
            this.lvTasks.Location = new System.Drawing.Point(410, 36);
            this.lvTasks.Name = "lvTasks";
            this.lvTasks.Size = new System.Drawing.Size(1004, 722);
            this.lvTasks.TabIndex = 3;
            this.lvTasks.UseCompatibleStateImageBehavior = false;
            this.lvTasks.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Robot";
            this.columnHeader1.Width = 150;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Task";
            this.columnHeader2.Width = 150;
            // 
            // columnHeader4
            // 
            this.columnHeader4.Text = "Progress";
            this.columnHeader4.Width = 100;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "Bar";
            this.columnHeader3.Width = 240;
            // 
            // btnSimulate
            // 
            this.btnSimulate.Location = new System.Drawing.Point(1273, 788);
            this.btnSimulate.Name = "btnSimulate";
            this.btnSimulate.Size = new System.Drawing.Size(141, 56);
            this.btnSimulate.TabIndex = 4;
            this.btnSimulate.Text = "Simulate 10 Robots";
            this.btnSimulate.UseVisualStyleBackColor = true;
            this.btnSimulate.Click += new System.EventHandler(this.btnSimulate_Click);
            // 
            // btnClearHistory
            // 
            this.btnClearHistory.Location = new System.Drawing.Point(1141, 788);
            this.btnClearHistory.Name = "btnClearHistory";
            this.btnClearHistory.Size = new System.Drawing.Size(114, 56);
            this.btnClearHistory.TabIndex = 5;
            this.btnClearHistory.Text = "Clear Task History";
            this.btnClearHistory.UseVisualStyleBackColor = true;
            this.btnClearHistory.Click += new System.EventHandler(this.btnClearHistory_Click);
            // 
            // lvLeaderBoard
            // 
            this.lvLeaderBoard.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader5,
            this.columnHeader6,
            this.columnHeader7});
            this.lvLeaderBoard.Location = new System.Drawing.Point(6, 25);
            this.lvLeaderBoard.Name = "lvLeaderBoard";
            this.lvLeaderBoard.Size = new System.Drawing.Size(348, 345);
            this.lvLeaderBoard.TabIndex = 1;
            this.lvLeaderBoard.UseCompatibleStateImageBehavior = false;
            this.lvLeaderBoard.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader5
            // 
            this.columnHeader5.Text = "Rank";
            this.columnHeader5.Width = 45;
            // 
            // columnHeader6
            // 
            this.columnHeader6.Text = "Robot";
            this.columnHeader6.Width = 120;
            // 
            // columnHeader7
            // 
            this.columnHeader7.Text = "Avg Time";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1445, 856);
            this.Controls.Add(this.btnClearHistory);
            this.Controls.Add(this.btnSimulate);
            this.Controls.Add(this.lvTasks);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Name = "Form1";
            this.Text = "Bot-O-Mat";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnAddRobot;
        private System.Windows.Forms.ComboBox cbRobotType;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtRobotName;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button btnStartTask;
        private System.Windows.Forms.ComboBox cbTask;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cbRobot;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label lblLeaderBoard;
        private System.Windows.Forms.ListView lvTasks;
        private System.Windows.Forms.Button btnSimulate;
        private System.Windows.Forms.Button btnClearHistory;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.ColumnHeader columnHeader4;
        private System.Windows.Forms.ListView lvLeaderBoard;
        private System.Windows.Forms.ColumnHeader columnHeader5;
        private System.Windows.Forms.ColumnHeader columnHeader6;
        private System.Windows.Forms.ColumnHeader columnHeader7;
    }
}

