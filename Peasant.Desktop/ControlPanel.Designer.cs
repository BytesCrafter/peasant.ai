namespace Peasant.Desktop
{
    partial class ControlPanel
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
            startService = new Button();
            stopService = new Button();
            SuspendLayout();
            // 
            // startService
            // 
            startService.Location = new Point(134, 136);
            startService.Name = "startService";
            startService.Size = new Size(171, 47);
            startService.TabIndex = 0;
            startService.Text = "Start Service";
            startService.UseVisualStyleBackColor = true;
            startService.Click += startService_Click;
            // 
            // stopService
            // 
            stopService.Location = new Point(134, 216);
            stopService.Name = "stopService";
            stopService.Size = new Size(171, 55);
            stopService.TabIndex = 1;
            stopService.Text = "Stop Service";
            stopService.UseVisualStyleBackColor = true;
            stopService.Click += stopService_Click;
            // 
            // ControlPanel
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(430, 360);
            Controls.Add(stopService);
            Controls.Add(startService);
            Name = "ControlPanel";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Peasant App";
            ResumeLayout(false);
        }

        #endregion

        private Button startService;
        private Button stopService;
    }
}