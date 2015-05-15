namespace ImageRecognition
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
            this.btnSelectImage = new System.Windows.Forms.Button();
            this.imageBox = new System.Windows.Forms.PictureBox();
            this.stencilBox = new System.Windows.Forms.PictureBox();
            this.blurredBox = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.imageBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.stencilBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.blurredBox)).BeginInit();
            this.SuspendLayout();
            // 
            // btnSelectImage
            // 
            this.btnSelectImage.Location = new System.Drawing.Point(485, 285);
            this.btnSelectImage.Name = "btnSelectImage";
            this.btnSelectImage.Size = new System.Drawing.Size(163, 41);
            this.btnSelectImage.TabIndex = 0;
            this.btnSelectImage.Text = "Select Image";
            this.btnSelectImage.UseVisualStyleBackColor = true;
            this.btnSelectImage.Click += new System.EventHandler(this.button1_Click);
            // 
            // imageBox
            // 
            this.imageBox.Location = new System.Drawing.Point(12, 12);
            this.imageBox.Name = "imageBox";
            this.imageBox.Size = new System.Drawing.Size(260, 180);
            this.imageBox.TabIndex = 1;
            this.imageBox.TabStop = false;
            // 
            // stencilBox
            // 
            this.stencilBox.Location = new System.Drawing.Point(411, 12);
            this.stencilBox.Name = "stencilBox";
            this.stencilBox.Size = new System.Drawing.Size(260, 180);
            this.stencilBox.TabIndex = 2;
            this.stencilBox.TabStop = false;
            // 
            // blurredBox
            // 
            this.blurredBox.Location = new System.Drawing.Point(747, 13);
            this.blurredBox.Name = "blurredBox";
            this.blurredBox.Size = new System.Drawing.Size(297, 179);
            this.blurredBox.TabIndex = 3;
            this.blurredBox.TabStop = false;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1114, 338);
            this.Controls.Add(this.blurredBox);
            this.Controls.Add(this.stencilBox);
            this.Controls.Add(this.imageBox);
            this.Controls.Add(this.btnSelectImage);
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.imageBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.stencilBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.blurredBox)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnSelectImage;
        private System.Windows.Forms.PictureBox imageBox;
        private System.Windows.Forms.PictureBox stencilBox;
        private System.Windows.Forms.PictureBox blurredBox;
    }
}

