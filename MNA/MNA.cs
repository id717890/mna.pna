using MNA.Data;
using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using MNA.Interfaces;

namespace MNA
{
    public partial class MNA : Form, IMnaViewNew
    {
        public void Attach(IMnaPresenterCallback callback)
        {
            _saveButton.Click += (sender, e) => callback.OnSave();
            nColumnCaption.TextChanged += (sender, e) => callback.OnMyTextChanged();
            lbMnaList.SelectedIndexChanged += (sender, e) =>
            {
                RenderParametersGrid();
                callback.OnMnaChenged((Mna)lbMnaList.SelectedItem);
            };
            btnOpenFile.Click += (sender, e) =>
            {
                if (GetFileNameFromOpenedFile(out string fileName))
                {
                    callback.OnOpenExcelFile(fileName);
                }
            };
        }

        public int ColumnCaption
        {
            get => (Int32)nColumnCaption.Value;
            set => nColumnCaption.Value = value;
        }

        public int ColumnTag
        {
            get => (Int32)nColumnTag.Value;
            set => nColumnTag.Value = value;
        }

        public void SetModel(IMnaViewModel model)
        {
            lbMnaList.DisplayMember = "Caption";
            if (model.MnaList != null && model.MnaList.Any())
            {
                foreach (var mna in model.MnaList)
                {
                    lbMnaList.Items.Add(mna);
                }
            }
            if (lbMnaList.Items.Count > 0)
            {
                lbMnaList.SelectedIndex = 0;
                RenderParametersGrid();
            }
        }

        public MNA()
        {
            InitializeComponent();
            InitDataGrid();
        }

        private void InitDataGrid()
        {
            dgParameters.AutoGenerateColumns = false;
            dgParameters.Columns.Add("Num", "#");
            dgParameters.Columns.Add("Parameter", "Параметр");
            dgParameters.Columns.Add("Status", "Статус");
        }

        public void RenderParametersGrid()
        {
            if (lbMnaList.SelectedItem != null)
            {
                Mna selectedMna = (Mna)lbMnaList.SelectedItem;
                if (selectedMna != null)
                {
                    dgParameters.Rows.Clear();
                    Int16 rowNum = 1;
                    dgParameters.Rows.Add(String.Empty, "Агрегатные защиты");
                    foreach (Tag tag in selectedMna.TsSecurity)
                    {
                        dgParameters.Rows.Add(rowNum, tag.Caption, tag.Status);
                        if (tag.Status == "OK") dgParameters.Rows[rowNum].Cells[2].Style.BackColor = Color.Green;
                        rowNum++;
                    }

                    dgParameters.Rows.Add();
                    dgParameters.Rows.Add(String.Empty, "Прочие сигналы");
                    rowNum += 2;

                    foreach (Tag tag in selectedMna.TsOther)
                    {
                        dgParameters.Rows.Add(rowNum, tag.Caption, tag.Status);
                        if (tag.Status == "OK") dgParameters.Rows[rowNum].Cells[2].Style.BackColor = Color.Green;
                        rowNum++;
                    }

                    dgParameters.Rows.Add();
                    rowNum += 2;

                    dgParameters.Rows.Add(String.Empty, "Телеуправление");
                    foreach (Tag tag in selectedMna.Tu)
                    {
                        dgParameters.Rows.Add(rowNum, tag.Caption, tag.Status);
                        if (tag.Status == "OK") dgParameters.Rows[rowNum].Cells[2].Style.BackColor = Color.Green;
                        rowNum++;
                    }
                }
            }
        }

        private bool IsColumnsOfExcelTheSame()
        {
            return nColumnCaption.Value == nColumnTag.Value;
        }

        private bool GetFileNameFromOpenedFile(out string fileName)
        {
            fileName = string.Empty;
            if (!IsColumnsOfExcelTheSame())
            {
                OpenFileDialog fileDialog = new OpenFileDialog();
                fileDialog.Filter = @"Excel files|*.xls;*.xlsx";
                if (fileDialog.ShowDialog() == DialogResult.OK)
                {
                    fileName = fileDialog.FileName;
                    return true;
                }
            }
            else
            {
                MessageBox.Show(@"Столбцы для чтения файла не должны быть одинаковыми", @"Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            return false;
        }
    }
}
