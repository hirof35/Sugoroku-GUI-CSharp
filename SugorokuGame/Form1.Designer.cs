namespace SugorokuGame
{
    partial class Form1 : Form
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.buttonDice = new System.Windows.Forms.Button();
            this.labelStatus = new System.Windows.Forms.Label();
            this.txtMap = new System.Windows.Forms.RichTextBox();
            this.SuspendLayout();

            // 1. buttonDice の設定
            this.buttonDice.Location = new System.Drawing.Point(20, 20);
            this.buttonDice.Name = "buttonDice";
            this.buttonDice.Size = new System.Drawing.Size(150, 40);
            this.buttonDice.TabIndex = 0;
            this.buttonDice.Text = "🎲 サイコロを振る";
            this.buttonDice.UseVisualStyleBackColor = true;
            this.buttonDice.Click += new System.EventHandler(this.buttonDice_Click);

            // 2. labelStatus の設定
            this.labelStatus.Location = new System.Drawing.Point(20, 70);
            this.labelStatus.Name = "labelStatus";
            this.labelStatus.Size = new System.Drawing.Size(540, 50); // AutoSizeではなくサイズを明示
            this.labelStatus.TabIndex = 1;
            this.labelStatus.Text = "ゲーム準備完了";

            // 3. txtMap の設定
            this.txtMap.Location = new System.Drawing.Point(20, 130);
            this.txtMap.Name = "txtMap";
            this.txtMap.Size = new System.Drawing.Size(540, 280);
            this.txtMap.TabIndex = 2;
            this.txtMap.Text = "";

            // 4. Form1 (土台) の設定
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(600, 450); // フォーム自体のサイズを確保

            // 重要：これらが無いと画面に表示されません
            this.Controls.Add(this.buttonDice);
            this.Controls.Add(this.labelStatus);
            this.Controls.Add(this.txtMap);

            this.Name = "Form1";
            this.Text = "すごろくゲーム 2026";
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private System.Windows.Forms.Button buttonDice;
        private System.Windows.Forms.Label labelStatus;
        private System.Windows.Forms.RichTextBox txtMap;
    }

}
