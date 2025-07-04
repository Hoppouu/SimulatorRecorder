namespace SimulatorRecorder
{
    partial class MainForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            ROLL = new Label();
            PITCH = new Label();
            SWAY = new Label();
            YAW = new Label();
            button_start = new Button();
            button_end = new Button();
            label_elapsed = new Label();
            timer_main = new System.Windows.Forms.Timer(components);
            menuStrip1 = new MenuStrip();
            menu1 = new ToolStripMenuItem();
            menu1_1 = new ToolStripMenuItem();
            menu1_2 = new ToolStripMenuItem();
            SURGE = new Label();
            HEAVE = new Label();
            SPEED = new Label();
            BLOWER1 = new Label();
            menuStrip1.SuspendLayout();
            SuspendLayout();
            // 
            // ROLL
            // 
            ROLL.AutoSize = true;
            ROLL.Font = new Font("Consolas", 15.75F);
            ROLL.Location = new Point(25, 41);
            ROLL.Name = "ROLL";
            ROLL.Size = new Size(142, 24);
            ROLL.TabIndex = 0;
            ROLL.Text = "ROLL    : 0";
            // 
            // PITCH
            // 
            PITCH.AutoSize = true;
            PITCH.Font = new Font("Consolas", 15.75F);
            PITCH.Location = new Point(25, 78);
            PITCH.Name = "PITCH";
            PITCH.Size = new Size(142, 24);
            PITCH.TabIndex = 1;
            PITCH.Text = "PITCH   : 0";
            // 
            // SWAY
            // 
            SWAY.AutoSize = true;
            SWAY.Font = new Font("Consolas", 15.75F);
            SWAY.Location = new Point(25, 156);
            SWAY.Name = "SWAY";
            SWAY.Size = new Size(142, 24);
            SWAY.TabIndex = 3;
            SWAY.Text = "SWAY    : 0";
            // 
            // YAW
            // 
            YAW.AutoSize = true;
            YAW.Font = new Font("Consolas", 15.75F);
            YAW.Location = new Point(25, 115);
            YAW.Name = "YAW";
            YAW.Size = new Size(142, 24);
            YAW.TabIndex = 2;
            YAW.Text = "YAW     : 0";
            // 
            // button_start
            // 
            button_start.BackColor = Color.FromArgb(192, 255, 192);
            button_start.FlatAppearance.BorderColor = Color.White;
            button_start.Font = new Font("맑은 고딕", 15.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            button_start.Location = new Point(25, 400);
            button_start.Name = "button_start";
            button_start.Size = new Size(150, 38);
            button_start.TabIndex = 4;
            button_start.Text = "시작";
            button_start.UseVisualStyleBackColor = false;
            button_start.Click += button_start_Click;
            // 
            // button_end
            // 
            button_end.BackColor = Color.FromArgb(192, 255, 192);
            button_end.Font = new Font("맑은 고딕", 15.75F, FontStyle.Regular, GraphicsUnit.Point, 129);
            button_end.Location = new Point(188, 400);
            button_end.Name = "button_end";
            button_end.Size = new Size(150, 38);
            button_end.TabIndex = 5;
            button_end.Text = "종료";
            button_end.UseVisualStyleBackColor = false;
            button_end.Click += button_end_Click;
            // 
            // label_elapsed
            // 
            label_elapsed.AutoSize = true;
            label_elapsed.Font = new Font("맑은 고딕", 15.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label_elapsed.Location = new Point(112, 352);
            label_elapsed.Name = "label_elapsed";
            label_elapsed.Size = new Size(135, 30);
            label_elapsed.TabIndex = 6;
            label_elapsed.Text = "진행 시간 : 0";
            // 
            // timer_main
            // 
            timer_main.Tick += TimerEvent;
            // 
            // menuStrip1
            // 
            menuStrip1.Items.AddRange(new ToolStripItem[] { menu1 });
            menuStrip1.Location = new Point(0, 0);
            menuStrip1.Name = "menuStrip1";
            menuStrip1.Size = new Size(374, 24);
            menuStrip1.TabIndex = 7;
            menuStrip1.Text = "menuStrip1";
            // 
            // menu1
            // 
            menu1.DropDownItems.AddRange(new ToolStripItem[] { menu1_1, menu1_2 });
            menu1.Name = "menu1";
            menu1.Size = new Size(71, 20);
            menu1.Text = "저장 폴더";
            // 
            // menu1_1
            // 
            menu1_1.Name = "menu1_1";
            menu1_1.Size = new Size(154, 22);
            menu1_1.Text = "저장 폴더 설정";
            menu1_1.Click += menu1_1_Click;
            // 
            // menu1_2
            // 
            menu1_2.Name = "menu1_2";
            menu1_2.Size = new Size(154, 22);
            menu1_2.Text = "저장 폴더 확인";
            menu1_2.Click += menu1_2_Click;
            // 
            // SURGE
            // 
            SURGE.AutoSize = true;
            SURGE.Font = new Font("Consolas", 15.75F);
            SURGE.Location = new Point(25, 195);
            SURGE.Name = "SURGE";
            SURGE.Size = new Size(142, 24);
            SURGE.TabIndex = 0;
            SURGE.Text = "SURGE   : 0";
            // 
            // HEAVE
            // 
            HEAVE.AutoSize = true;
            HEAVE.Font = new Font("Consolas", 15.75F);
            HEAVE.Location = new Point(25, 232);
            HEAVE.Name = "HEAVE";
            HEAVE.Size = new Size(142, 24);
            HEAVE.TabIndex = 1;
            HEAVE.Text = "HEAVE   : 0";
            // 
            // SPEED
            // 
            SPEED.AutoSize = true;
            SPEED.Font = new Font("Consolas", 15.75F);
            SPEED.Location = new Point(25, 269);
            SPEED.Name = "SPEED";
            SPEED.Size = new Size(142, 24);
            SPEED.TabIndex = 2;
            SPEED.Text = "SPEED   : 0";
            // 
            // BLOWER1
            // 
            BLOWER1.AutoSize = true;
            BLOWER1.Font = new Font("Consolas", 15.75F);
            BLOWER1.Location = new Point(25, 310);
            BLOWER1.Name = "BLOWER1";
            BLOWER1.Size = new Size(142, 24);
            BLOWER1.TabIndex = 3;
            BLOWER1.Text = "BLOWER1 : 0";
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            ClientSize = new Size(374, 450);
            Controls.Add(label_elapsed);
            Controls.Add(button_end);
            Controls.Add(button_start);
            Controls.Add(BLOWER1);
            Controls.Add(SPEED);
            Controls.Add(SWAY);
            Controls.Add(HEAVE);
            Controls.Add(YAW);
            Controls.Add(SURGE);
            Controls.Add(PITCH);
            Controls.Add(ROLL);
            Controls.Add(menuStrip1);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MainMenuStrip = menuStrip1;
            MaximizeBox = false;
            Name = "MainForm";
            Text = "SimulatorRecorder";
            Load += MainForm_Load;
            menuStrip1.ResumeLayout(false);
            menuStrip1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label ROLL;
        private Label PITCH;
        private Label SWAY;
        private Label YAW;
        private Button button_start;
        private Button button_end;
        private Label label_elapsed;
        private System.Windows.Forms.Timer timer_main;
        private MenuStrip menuStrip1;
        private ToolStripMenuItem menu1;
        private ToolStripMenuItem menu1_1;
        private ToolStripMenuItem menu1_2;
        private Label SURGE;
        private Label HEAVE;
        private Label SPEED;
        private Label BLOWER1;
    }
}
