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
            menu1_deadzon = new ToolStripMenuItem();
            menu1_manual = new ToolStripMenuItem();
            toolStripSeparator1 = new ToolStripSeparator();
            menu1_setSavePath = new ToolStripMenuItem();
            menu1_getSavePath = new ToolStripMenuItem();
            menu1_setUnityPath = new ToolStripMenuItem();
            SURGE = new Label();
            HEAVE = new Label();
            SPEED = new Label();
            BLOWER1 = new Label();
            didFindController = new Label();
            button_SelectVideo = new Button();
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
            button_start.Font = new Font("맑은 고딕", 15.75F, FontStyle.Bold, GraphicsUnit.Point, 129);
            button_start.Location = new Point(25, 487);
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
            button_end.Font = new Font("맑은 고딕", 15.75F, FontStyle.Bold, GraphicsUnit.Point, 129);
            button_end.Location = new Point(195, 487);
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
            label_elapsed.Font = new Font("맑은 고딕", 15.75F, FontStyle.Regular, GraphicsUnit.Point, 129);
            label_elapsed.Location = new Point(115, 404);
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
            menuStrip1.Size = new Size(373, 24);
            menuStrip1.TabIndex = 7;
            menuStrip1.Text = "menuStrip1";
            // 
            // menu1
            // 
            menu1.DropDownItems.AddRange(new ToolStripItem[] { menu1_deadzon, menu1_manual, toolStripSeparator1, menu1_setSavePath, menu1_getSavePath, menu1_setUnityPath });
            menu1.Name = "menu1";
            menu1.Size = new Size(43, 20);
            menu1.Text = "옵션";
            menu1.Click += menu1_Click;
            // 
            // menu1_deadzon
            // 
            menu1_deadzon.Name = "menu1_deadzon";
            menu1_deadzon.Size = new Size(180, 22);
            menu1_deadzon.Text = "데드존 설정";
            menu1_deadzon.Click += menu1_deadzon_Click;
            // 
            // menu1_manual
            // 
            menu1_manual.ImageScaling = ToolStripItemImageScaling.None;
            menu1_manual.Name = "menu1_manual";
            menu1_manual.Size = new Size(180, 22);
            menu1_manual.Text = "사용 설명";
            menu1_manual.Click += menu1_manual_Click;
            // 
            // toolStripSeparator1
            // 
            toolStripSeparator1.Name = "toolStripSeparator1";
            toolStripSeparator1.Size = new Size(177, 6);
            // 
            // menu1_setSavePath
            // 
            menu1_setSavePath.Name = "menu1_setSavePath";
            menu1_setSavePath.Size = new Size(180, 22);
            menu1_setSavePath.Text = "저장 폴더 설정";
            menu1_setSavePath.Click += menu1_setSavePath_Click;
            // 
            // menu1_getSavePath
            // 
            menu1_getSavePath.Name = "menu1_getSavePath";
            menu1_getSavePath.Size = new Size(180, 22);
            menu1_getSavePath.Text = "저장 폴더 확인";
            menu1_getSavePath.Click += menu1_getSavePath_Click;
            // 
            // menu1_setUnityPath
            // 
            menu1_setUnityPath.Name = "menu1_setUnityPath";
            menu1_setUnityPath.Size = new Size(180, 22);
            menu1_setUnityPath.Text = "유니티 파일 설정";
            menu1_setUnityPath.Click += menu1_setUnityPath_Click;
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
            // didFindController
            // 
            didFindController.AutoSize = true;
            didFindController.Font = new Font("맑은 고딕", 15.75F, FontStyle.Regular, GraphicsUnit.Point, 129);
            didFindController.ForeColor = Color.Red;
            didFindController.Location = new Point(44, 364);
            didFindController.Name = "didFindController";
            didFindController.Size = new Size(291, 30);
            didFindController.TabIndex = 8;
            didFindController.Text = "컨트롤러를 찾을 수 없습니다.";
            didFindController.Visible = false;
            // 
            // button_SelectVideo
            // 
            button_SelectVideo.BackColor = Color.FromArgb(192, 255, 192);
            button_SelectVideo.FlatAppearance.BorderColor = Color.White;
            button_SelectVideo.Font = new Font("맑은 고딕", 15.75F, FontStyle.Bold, GraphicsUnit.Point, 129);
            button_SelectVideo.Location = new Point(25, 443);
            button_SelectVideo.Name = "button_SelectVideo";
            button_SelectVideo.Size = new Size(320, 38);
            button_SelectVideo.TabIndex = 9;
            button_SelectVideo.Text = "비디오 선택";
            button_SelectVideo.UseVisualStyleBackColor = false;
            button_SelectVideo.Click += button_SelectVideo_Click;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            ClientSize = new Size(373, 537);
            Controls.Add(button_SelectVideo);
            Controls.Add(didFindController);
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
        private ToolStripMenuItem menu1_setSavePath;
        private ToolStripMenuItem menu1_getSavePath;
        private Label SURGE;
        private Label HEAVE;
        private Label SPEED;
        private Label BLOWER1;
        private Label didFindController;
        private ToolStripMenuItem menu1_manual;
        private ToolStripSeparator toolStripSeparator1;
        private Button button_SelectVideo;
        private ToolStripMenuItem menu1_setUnityPath;
        private ToolStripMenuItem menu1_deadzon;
    }
}
