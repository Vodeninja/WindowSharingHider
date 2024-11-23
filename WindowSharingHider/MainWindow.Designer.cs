namespace WindowSharingHider
{
    partial class MainWindow
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.CheckedListBox windowListCheckBox;
        private System.IO.Ports.SerialPort serialPort1;

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.windowListCheckBox = new System.Windows.Forms.CheckedListBox();
            this.serialPort1 = new System.IO.Ports.SerialPort(this.components);
            this.SuspendLayout();
            // 
            // windowListCheckBox
            // 
            this.windowListCheckBox.CheckOnClick = true;
            this.windowListCheckBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.windowListCheckBox.FormattingEnabled = true;
            this.windowListCheckBox.Location = new System.Drawing.Point(0, 0);
            this.windowListCheckBox.Name = "windowListCheckBox";
            this.windowListCheckBox.Size = new System.Drawing.Size(806, 384);
            this.windowListCheckBox.TabIndex = 0;
            // 
            // MainWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(806, 384);
            this.Controls.Add(this.windowListCheckBox);
            this.Name = "MainWindow";
            this.Text = "Window Sharing Hider";
            this.Load += new System.EventHandler(this.MainWindow_Load);
            this.ResumeLayout(false);
        }
    }
}