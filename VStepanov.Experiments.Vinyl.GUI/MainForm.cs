using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using VStepanov.Experiments.Vinyl.Audio;
using VStepanov.Experiments.Vinyl.Imaging;

namespace VStepanov.Experiments.Vinyl.GUI
{
    public partial class MainForm : Form
    {
        private Imaging.Vinyl _vinyl;
        private bool _editing = false;

        public MainForm()
        {
            InitializeComponent();
        }

        private void buttonOpen_Click(object sender, EventArgs e)
        {
            openFileDialog.ShowDialog();
        }

        private void openFileDialog_FileOk(object sender, CancelEventArgs e)
        {
            var tempVinyl = new Imaging.Vinyl(openFileDialog.FileName);

            try
            {
                InitializeFields(tempVinyl);
            }
            catch (Exception ex)
            {
                if (ex is OverflowException || 
                    ex is ArgumentOutOfRangeException)
                {
                    MessageBox.Show(
                        "Вероятно, выбранное изображение не является изображением пластинки. " +
                        "Выберите другое изображение или попробуйте уменьшить исходное до размера " +
                        "2000*2000 или менее.",
                        "Что-то не так с изображением",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);
                    return;
                }
                
                throw;
            }

            _editing = false;
            _vinyl = tempVinyl;
            pictureBoxPlate.Image = _vinyl.GetOriginal();
            Text = String.Format("Восстановление \"виниловых\" пластинок - {0}", openFileDialog.SafeFileName);
        }

        private void InitializeFields(Imaging.Vinyl vinyl)
        {
            numericUpDownCenterX.Value = vinyl.Center.X;
            numericUpDownCenterY.Value = vinyl.Center.Y;

            numericUpDownGapWidth.Value = (decimal)vinyl.GapWidth;
            numericUpDownTrackWidth.Value = (decimal)vinyl.TrackWidth;

            textBoxSpinCount.Text = vinyl.SpinCount.ToString();
            textBoxDuration.Text = vinyl.Duration.ToString();

            panelProperties.Visible = true;
        }

        private void buttonReset_Click(object sender, EventArgs e)
        {
            InitializeFields(_vinyl);
        }

        private void buttonExtract_Click(object sender, EventArgs e)
        {
            var parameters = new ExtractionParameters
                {
                    Center = new Imaging.Point((int)numericUpDownCenterX.Value, (int)numericUpDownCenterY.Value),
                    GapWidth = (double)numericUpDownGapWidth.Value,
                    TrackWidth = (double)numericUpDownTrackWidth.Value
                };

            byte[] audioBytes;

            try
            {
                if (checkBoxTrack.Checked)
                {
                    audioBytes = Imaging.Vinyl.ExtractAudioBytes(_vinyl, parameters, Imaging.Vinyl.ExtractionOptions.SaveTrack);
                    pictureBoxPlate.Image = _vinyl.GetTrack();
                }
                else
                {
                    audioBytes = Imaging.Vinyl.ExtractAudioBytes(_vinyl, parameters, Imaging.Vinyl.ExtractionOptions.None);
                }
            }
            catch (IndexOutOfRangeException)
            {
                MessageBox.Show("Проверьте правильность всех параметров. Возможно, необходимо их слегка поправить.\n" +
                                "Так же стоит проверить корректность самого изображения.",
                                "Произошла ошибка при извлечении звука",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Warning);
            }
        }

        private void buttonEditApply_Click(object sender, EventArgs e)
        {
            _editing = !_editing;

            numericUpDownCenterX.Enabled = _editing;
            numericUpDownCenterY.Enabled = _editing;
            numericUpDownGapWidth.Enabled = _editing;
            numericUpDownTrackWidth.Enabled = _editing;

            buttonReset.Visible = _editing;

            buttonEditApply.Text = _editing ? "Применить" : "Изменить";
        }
    }
}
