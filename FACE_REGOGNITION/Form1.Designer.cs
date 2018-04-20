namespace FACE_REGOGNITION
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
            this.components = new System.ComponentModel.Container();
            this.videofeedBox = new Emgu.CV.UI.ImageBox();
            this.DetectfaceTimer = new System.Windows.Forms.Timer(this.components);
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            ((System.ComponentModel.ISupportInitialize)(this.videofeedBox)).BeginInit();
            this.SuspendLayout();
            // 
            // videofeedBox
            // 
            this.videofeedBox.Location = new System.Drawing.Point(12, 12);
            this.videofeedBox.Name = "videofeedBox";
            this.videofeedBox.Size = new System.Drawing.Size(679, 297);
            this.videofeedBox.TabIndex = 2;
            this.videofeedBox.TabStop = false;
            // 
            // DetectfaceTimer
            // 
            this.DetectfaceTimer.Enabled = true;
            this.DetectfaceTimer.Interval = 300;
            // 
            // richTextBox1
            // 
            this.richTextBox1.Location = new System.Drawing.Point(12, 315);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.Size = new System.Drawing.Size(249, 184);
            this.richTextBox1.TabIndex = 3;
            this.richTextBox1.Text = "";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(861, 511);
            this.Controls.Add(this.richTextBox1);
            this.Controls.Add(this.videofeedBox);
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.videofeedBox)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Emgu.CV.UI.ImageBox videofeedBox;
        private System.Windows.Forms.Timer DetectfaceTimer;
        private System.Windows.Forms.Timer DelayTimer;
        private System.Windows.Forms.RichTextBox richTextBox1;
    }
}

