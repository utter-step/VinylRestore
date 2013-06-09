namespace VStepanov.Experiments.Vinyl.GUI
{
    partial class MainForm
    {
        /// <summary>
        /// Требуется переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Обязательный метод для поддержки конструктора - не изменяйте
        /// содержимое данного метода при помощи редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.panelImage = new System.Windows.Forms.Panel();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.buttonOpen = new System.Windows.Forms.Button();
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.panelProperties = new System.Windows.Forms.Panel();
            this.textBoxDuration = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.textBoxSpinCount = new System.Windows.Forms.TextBox();
            this.labelSpinCount = new System.Windows.Forms.Label();
            this.numericUpDownGapWidth = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.numericUpDownTrackWidth = new System.Windows.Forms.NumericUpDown();
            this.labelTrackWidth = new System.Windows.Forms.Label();
            this.labelCenterY = new System.Windows.Forms.Label();
            this.numericUpDownCenterY = new System.Windows.Forms.NumericUpDown();
            this.labelCenterX = new System.Windows.Forms.Label();
            this.numericUpDownCenterX = new System.Windows.Forms.NumericUpDown();
            this.labelCenter = new System.Windows.Forms.Label();
            this.buttonEditApply = new System.Windows.Forms.Button();
            this.pictureBoxPlate = new System.Windows.Forms.PictureBox();
            this.buttonReset = new System.Windows.Forms.Button();
            this.buttonExtract = new System.Windows.Forms.Button();
            this.checkBoxTrack = new System.Windows.Forms.CheckBox();
            this.saveFileDialog = new System.Windows.Forms.SaveFileDialog();
            this.panelImage.SuspendLayout();
            this.panelProperties.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownGapWidth)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownTrackWidth)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownCenterY)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownCenterX)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxPlate)).BeginInit();
            this.SuspendLayout();
            // 
            // panelImage
            // 
            this.panelImage.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelImage.AutoScroll = true;
            this.panelImage.Controls.Add(this.pictureBoxPlate);
            this.panelImage.Location = new System.Drawing.Point(13, 31);
            this.panelImage.Name = "panelImage";
            this.panelImage.Size = new System.Drawing.Size(412, 460);
            this.panelImage.TabIndex = 0;
            // 
            // menuStrip1
            // 
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(590, 24);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // buttonOpen
            // 
            this.buttonOpen.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonOpen.Location = new System.Drawing.Point(438, 468);
            this.buttonOpen.Name = "buttonOpen";
            this.buttonOpen.Size = new System.Drawing.Size(147, 23);
            this.buttonOpen.TabIndex = 2;
            this.buttonOpen.Text = "Выбрать файл";
            this.buttonOpen.UseVisualStyleBackColor = true;
            this.buttonOpen.Click += new System.EventHandler(this.buttonOpen_Click);
            // 
            // openFileDialog
            // 
            this.openFileDialog.Filter = "Файлы изображений|*.jpg;*.png;*.bmp;*.";
            this.openFileDialog.FileOk += new System.ComponentModel.CancelEventHandler(this.openFileDialog_FileOk);
            // 
            // panelProperties
            // 
            this.panelProperties.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.panelProperties.Controls.Add(this.checkBoxTrack);
            this.panelProperties.Controls.Add(this.buttonExtract);
            this.panelProperties.Controls.Add(this.buttonReset);
            this.panelProperties.Controls.Add(this.buttonEditApply);
            this.panelProperties.Controls.Add(this.textBoxDuration);
            this.panelProperties.Controls.Add(this.label2);
            this.panelProperties.Controls.Add(this.textBoxSpinCount);
            this.panelProperties.Controls.Add(this.labelSpinCount);
            this.panelProperties.Controls.Add(this.numericUpDownGapWidth);
            this.panelProperties.Controls.Add(this.label1);
            this.panelProperties.Controls.Add(this.numericUpDownTrackWidth);
            this.panelProperties.Controls.Add(this.labelTrackWidth);
            this.panelProperties.Controls.Add(this.labelCenterY);
            this.panelProperties.Controls.Add(this.numericUpDownCenterY);
            this.panelProperties.Controls.Add(this.labelCenterX);
            this.panelProperties.Controls.Add(this.numericUpDownCenterX);
            this.panelProperties.Controls.Add(this.labelCenter);
            this.panelProperties.Location = new System.Drawing.Point(432, 31);
            this.panelProperties.Name = "panelProperties";
            this.panelProperties.Size = new System.Drawing.Size(158, 431);
            this.panelProperties.TabIndex = 3;
            this.panelProperties.Visible = false;
            // 
            // textBoxDuration
            // 
            this.textBoxDuration.Location = new System.Drawing.Point(6, 230);
            this.textBoxDuration.Name = "textBoxDuration";
            this.textBoxDuration.ReadOnly = true;
            this.textBoxDuration.Size = new System.Drawing.Size(147, 20);
            this.textBoxDuration.TabIndex = 28;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(3, 213);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(141, 13);
            this.label2.TabIndex = 27;
            this.label2.Text = "Примерная длительность:";
            // 
            // textBoxSpinCount
            // 
            this.textBoxSpinCount.Location = new System.Drawing.Point(6, 185);
            this.textBoxSpinCount.Name = "textBoxSpinCount";
            this.textBoxSpinCount.ReadOnly = true;
            this.textBoxSpinCount.Size = new System.Drawing.Size(147, 20);
            this.textBoxSpinCount.TabIndex = 26;
            // 
            // labelSpinCount
            // 
            this.labelSpinCount.AutoSize = true;
            this.labelSpinCount.Location = new System.Drawing.Point(3, 168);
            this.labelSpinCount.Name = "labelSpinCount";
            this.labelSpinCount.Size = new System.Drawing.Size(150, 13);
            this.labelSpinCount.TabIndex = 25;
            this.labelSpinCount.Text = "Примерное число оборотов:";
            // 
            // numericUpDownGapWidth
            // 
            this.numericUpDownGapWidth.DecimalPlaces = 3;
            this.numericUpDownGapWidth.Enabled = false;
            this.numericUpDownGapWidth.Increment = new decimal(new int[] {
            1,
            0,
            0,
            131072});
            this.numericUpDownGapWidth.Location = new System.Drawing.Point(6, 141);
            this.numericUpDownGapWidth.Maximum = new decimal(new int[] {
            20,
            0,
            0,
            0});
            this.numericUpDownGapWidth.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDownGapWidth.Name = "numericUpDownGapWidth";
            this.numericUpDownGapWidth.Size = new System.Drawing.Size(147, 20);
            this.numericUpDownGapWidth.TabIndex = 24;
            this.numericUpDownGapWidth.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 112);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(112, 26);
            this.label1.TabIndex = 23;
            this.label1.Text = "Среднее расстояние\r\nмежду дорожками:";
            // 
            // numericUpDownTrackWidth
            // 
            this.numericUpDownTrackWidth.DecimalPlaces = 3;
            this.numericUpDownTrackWidth.Enabled = false;
            this.numericUpDownTrackWidth.Increment = new decimal(new int[] {
            1,
            0,
            0,
            131072});
            this.numericUpDownTrackWidth.Location = new System.Drawing.Point(6, 89);
            this.numericUpDownTrackWidth.Maximum = new decimal(new int[] {
            20,
            0,
            0,
            0});
            this.numericUpDownTrackWidth.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDownTrackWidth.Name = "numericUpDownTrackWidth";
            this.numericUpDownTrackWidth.Size = new System.Drawing.Size(147, 20);
            this.numericUpDownTrackWidth.TabIndex = 22;
            this.numericUpDownTrackWidth.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // labelTrackWidth
            // 
            this.labelTrackWidth.AutoSize = true;
            this.labelTrackWidth.Location = new System.Drawing.Point(3, 72);
            this.labelTrackWidth.Name = "labelTrackWidth";
            this.labelTrackWidth.Size = new System.Drawing.Size(141, 13);
            this.labelTrackWidth.TabIndex = 21;
            this.labelTrackWidth.Text = "Средняя ширина дорожки:";
            // 
            // labelCenterY
            // 
            this.labelCenterY.AutoSize = true;
            this.labelCenterY.Location = new System.Drawing.Point(115, 22);
            this.labelCenterY.Name = "labelCenterY";
            this.labelCenterY.Size = new System.Drawing.Size(12, 13);
            this.labelCenterY.TabIndex = 20;
            this.labelCenterY.Text = "y";
            // 
            // numericUpDownCenterY
            // 
            this.numericUpDownCenterY.Enabled = false;
            this.numericUpDownCenterY.Location = new System.Drawing.Point(90, 38);
            this.numericUpDownCenterY.Maximum = new decimal(new int[] {
            3000,
            0,
            0,
            0});
            this.numericUpDownCenterY.Name = "numericUpDownCenterY";
            this.numericUpDownCenterY.Size = new System.Drawing.Size(63, 20);
            this.numericUpDownCenterY.TabIndex = 19;
            // 
            // labelCenterX
            // 
            this.labelCenterX.AutoSize = true;
            this.labelCenterX.Location = new System.Drawing.Point(33, 22);
            this.labelCenterX.Name = "labelCenterX";
            this.labelCenterX.Size = new System.Drawing.Size(12, 13);
            this.labelCenterX.TabIndex = 18;
            this.labelCenterX.Text = "x";
            // 
            // numericUpDownCenterX
            // 
            this.numericUpDownCenterX.Enabled = false;
            this.numericUpDownCenterX.Location = new System.Drawing.Point(6, 38);
            this.numericUpDownCenterX.Maximum = new decimal(new int[] {
            3000,
            0,
            0,
            0});
            this.numericUpDownCenterX.Name = "numericUpDownCenterX";
            this.numericUpDownCenterX.Size = new System.Drawing.Size(66, 20);
            this.numericUpDownCenterX.TabIndex = 17;
            // 
            // labelCenter
            // 
            this.labelCenter.AutoSize = true;
            this.labelCenter.Location = new System.Drawing.Point(3, 9);
            this.labelCenter.Name = "labelCenter";
            this.labelCenter.Size = new System.Drawing.Size(97, 13);
            this.labelCenter.TabIndex = 16;
            this.labelCenter.Text = "Центр пластинки:";
            // 
            // buttonEditApply
            // 
            this.buttonEditApply.Location = new System.Drawing.Point(6, 256);
            this.buttonEditApply.Name = "buttonEditApply";
            this.buttonEditApply.Size = new System.Drawing.Size(147, 23);
            this.buttonEditApply.TabIndex = 29;
            this.buttonEditApply.Text = "Изменить";
            this.buttonEditApply.UseVisualStyleBackColor = true;
            this.buttonEditApply.Click += new System.EventHandler(this.buttonEditApply_Click);
            // 
            // pictureBoxPlate
            // 
            this.pictureBoxPlate.Location = new System.Drawing.Point(0, 0);
            this.pictureBoxPlate.Name = "pictureBoxPlate";
            this.pictureBoxPlate.Size = new System.Drawing.Size(157, 181);
            this.pictureBoxPlate.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pictureBoxPlate.TabIndex = 0;
            this.pictureBoxPlate.TabStop = false;
            // 
            // buttonReset
            // 
            this.buttonReset.Location = new System.Drawing.Point(6, 285);
            this.buttonReset.Name = "buttonReset";
            this.buttonReset.Size = new System.Drawing.Size(147, 23);
            this.buttonReset.TabIndex = 30;
            this.buttonReset.Text = "Сброс";
            this.buttonReset.UseVisualStyleBackColor = true;
            this.buttonReset.Visible = false;
            this.buttonReset.Click += new System.EventHandler(this.buttonReset_Click);
            // 
            // buttonExtract
            // 
            this.buttonExtract.Location = new System.Drawing.Point(6, 405);
            this.buttonExtract.Name = "buttonExtract";
            this.buttonExtract.Size = new System.Drawing.Size(147, 23);
            this.buttonExtract.TabIndex = 31;
            this.buttonExtract.Text = "Считать звук";
            this.buttonExtract.UseVisualStyleBackColor = true;
            this.buttonExtract.Click += new System.EventHandler(this.buttonExtract_Click);
            // 
            // checkBoxTrack
            // 
            this.checkBoxTrack.AutoSize = true;
            this.checkBoxTrack.Checked = true;
            this.checkBoxTrack.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxTrack.Location = new System.Drawing.Point(6, 382);
            this.checkBoxTrack.Name = "checkBoxTrack";
            this.checkBoxTrack.Size = new System.Drawing.Size(118, 17);
            this.checkBoxTrack.TabIndex = 32;
            this.checkBoxTrack.Text = "Отмечать замеры";
            this.checkBoxTrack.UseVisualStyleBackColor = true;
            // 
            // saveFileDialog
            // 
            this.saveFileDialog.DefaultExt = "wav";
            this.saveFileDialog.Filter = "WAV-аудио|*.wav";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(590, 503);
            this.Controls.Add(this.panelProperties);
            this.Controls.Add(this.buttonOpen);
            this.Controls.Add(this.panelImage);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "MainForm";
            this.Text = "Восстановление \"виниловых\" пластинок";
            this.panelImage.ResumeLayout(false);
            this.panelImage.PerformLayout();
            this.panelProperties.ResumeLayout(false);
            this.panelProperties.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownGapWidth)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownTrackWidth)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownCenterY)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownCenterX)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxPlate)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panelImage;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.Button buttonOpen;
        private System.Windows.Forms.OpenFileDialog openFileDialog;
        private System.Windows.Forms.Panel panelProperties;
        private System.Windows.Forms.TextBox textBoxDuration;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBoxSpinCount;
        private System.Windows.Forms.Label labelSpinCount;
        private System.Windows.Forms.NumericUpDown numericUpDownGapWidth;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown numericUpDownTrackWidth;
        private System.Windows.Forms.Label labelTrackWidth;
        private System.Windows.Forms.Label labelCenterY;
        private System.Windows.Forms.NumericUpDown numericUpDownCenterY;
        private System.Windows.Forms.Label labelCenterX;
        private System.Windows.Forms.NumericUpDown numericUpDownCenterX;
        private System.Windows.Forms.Label labelCenter;
        private System.Windows.Forms.Button buttonEditApply;
        private System.Windows.Forms.PictureBox pictureBoxPlate;
        private System.Windows.Forms.Button buttonReset;
        private System.Windows.Forms.Button buttonExtract;
        private System.Windows.Forms.CheckBox checkBoxTrack;
        private System.Windows.Forms.SaveFileDialog saveFileDialog;
    }
}

