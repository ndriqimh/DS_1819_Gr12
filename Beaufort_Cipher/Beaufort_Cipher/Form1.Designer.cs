namespace Beaufort_Cipher
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
            this.decodeButton = new System.Windows.Forms.Button();
            this.encodeButton = new System.Windows.Forms.Button();
            this.txtCiphertexti = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtCelesi = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.txtPlaintexti = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // decodeButton
            // 
            this.decodeButton.Location = new System.Drawing.Point(255, 234);
            this.decodeButton.Name = "decodeButton";
            this.decodeButton.Size = new System.Drawing.Size(75, 23);
            this.decodeButton.TabIndex = 15;
            this.decodeButton.Text = "Dekodimi";
            this.decodeButton.UseVisualStyleBackColor = true;
            // 
            // encodeButton
            // 
            this.encodeButton.Location = new System.Drawing.Point(39, 234);
            this.encodeButton.Name = "encodeButton";
            this.encodeButton.Size = new System.Drawing.Size(75, 23);
            this.encodeButton.TabIndex = 14;
            this.encodeButton.Text = "Enkodimi";
            this.encodeButton.UseVisualStyleBackColor = true;
            this.encodeButton.Click += new System.EventHandler(this.EncodeButton_Click);
            // 
            // txtCiphertexti
            // 
            this.txtCiphertexti.Location = new System.Drawing.Point(39, 185);
            this.txtCiphertexti.Name = "txtCiphertexti";
            this.txtCiphertexti.Size = new System.Drawing.Size(291, 20);
            this.txtCiphertexti.TabIndex = 13;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(36, 155);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(56, 13);
            this.label3.TabIndex = 12;
            this.label3.Text = "Ciphertexti";
            // 
            // txtCelesi
            // 
            this.txtCelesi.Location = new System.Drawing.Point(39, 113);
            this.txtCelesi.Name = "txtCelesi";
            this.txtCelesi.Size = new System.Drawing.Size(108, 20);
            this.txtCelesi.TabIndex = 11;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(39, 82);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(35, 13);
            this.label2.TabIndex = 10;
            this.label2.Text = "Celesi";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(39, 27);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(49, 13);
            this.label1.TabIndex = 9;
            this.label1.Text = "Plaintexti";
            // 
            // txtPlaintexti
            // 
            this.txtPlaintexti.Location = new System.Drawing.Point(39, 46);
            this.txtPlaintexti.Name = "txtPlaintexti";
            this.txtPlaintexti.Size = new System.Drawing.Size(291, 20);
            this.txtPlaintexti.TabIndex = 8;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(378, 298);
            this.Controls.Add(this.decodeButton);
            this.Controls.Add(this.encodeButton);
            this.Controls.Add(this.txtCiphertexti);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtCelesi);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtPlaintexti);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button decodeButton;
        private System.Windows.Forms.Button encodeButton;
        private System.Windows.Forms.TextBox txtCiphertexti;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtCelesi;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtPlaintexti;
    }
}

