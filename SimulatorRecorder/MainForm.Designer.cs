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
            label_LstickX = new Label();
            label_LstickY = new Label();
            label_RstickY = new Label();
            label_RstickX = new Label();
            button_start = new Button();
            button_end = new Button();
            label_elapsed = new Label();
            timer_main = new System.Windows.Forms.Timer(components);
            SuspendLayout();
            // 
            // label_LstickX
            // 
            label_LstickX.AutoSize = true;
            label_LstickX.Font = new Font("Consolas", 15.75F);
            label_LstickX.Location = new Point(56, 41);
            label_LstickX.Name = "label_LstickX";
            label_LstickX.Size = new Size(166, 24);
            label_LstickX.TabIndex = 0;
            label_LstickX.Text = "L stick X : 0";
            // 
            // label_LstickY
            // 
            label_LstickY.AutoSize = true;
            label_LstickY.Font = new Font("Consolas", 15.75F);
            label_LstickY.Location = new Point(56, 81);
            label_LstickY.Name = "label_LstickY";
            label_LstickY.Size = new Size(166, 24);
            label_LstickY.TabIndex = 1;
            label_LstickY.Text = "L stick Y : 0";
            // 
            // label_RstickY
            // 
            label_RstickY.AutoSize = true;
            label_RstickY.Font = new Font("Consolas", 15.75F);
            label_RstickY.Location = new Point(56, 196);
            label_RstickY.Name = "label_RstickY";
            label_RstickY.Size = new Size(166, 24);
            label_RstickY.TabIndex = 3;
            label_RstickY.Text = "R stick Y : 0";
            // 
            // label_RstickX
            // 
            label_RstickX.AutoSize = true;
            label_RstickX.Font = new Font("Consolas", 15.75F);
            label_RstickX.Location = new Point(56, 157);
            label_RstickX.Name = "label_RstickX";
            label_RstickX.Size = new Size(166, 24);
            label_RstickX.TabIndex = 2;
            label_RstickX.Text = "R stick X : 0";
            // 
            // button_start
            // 
            button_start.BackColor = Color.FromArgb(192, 255, 192);
            button_start.FlatAppearance.BorderColor = Color.White;
            button_start.Font = new Font("맑은 고딕", 15.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            button_start.Location = new Point(327, 31);
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
            button_end.Location = new Point(327, 75);
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
            label_elapsed.Location = new Point(56, 269);
            label_elapsed.Name = "label_elapsed";
            label_elapsed.Size = new Size(135, 30);
            label_elapsed.TabIndex = 6;
            label_elapsed.Text = "진행 시간 : 0";
            // 
            // timer_main
            // 
            timer_main.Tick += TimerEvent;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            ClientSize = new Size(515, 450);
            Controls.Add(label_elapsed);
            Controls.Add(button_end);
            Controls.Add(button_start);
            Controls.Add(label_RstickY);
            Controls.Add(label_RstickX);
            Controls.Add(label_LstickY);
            Controls.Add(label_LstickX);
            Name = "MainForm";
            Text = "MainForm";
            Load += MainForm_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label_LstickX;
        private Label label_LstickY;
        private Label label_RstickY;
        private Label label_RstickX;
        private Button button_start;
        private Button button_end;
        private Label label_elapsed;
        private System.Windows.Forms.Timer timer_main;
    }
}
