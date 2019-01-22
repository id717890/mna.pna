using App.Data;
using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using App.Interface.View;
using App.Interface.Presenter;
using App.Interface.Model;
using System.IO;
using System.Reflection;
using OfficeOpenXml;

namespace App
{
    public partial class MNA : Form, IMnaView
    {
        public void Attach(IMnaPresenterCallback callback)
        {
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
            btnCreateProtocol.Click += (sender, e) =>
            {
                callback.OnCreateProtocol();
            };
        }

        public int MnaNumber
        {
            get => (Int32)nMnaNumber.Value;
            set => nMnaNumber.Value = value;
        }

        public bool IsMnaNumber
        {
            get => nMnaNumber.Enabled;
            set => nMnaNumber.Enabled = value;
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

        public string Enginer {
            get => cbEnginer.Text;
            set => cbEnginer.Text = value;
        }

        public string Order {
            get => tbOrder.Text;
            set => tbOrder.Text = value;
        }

        public string[] Enginers 
{
            set {
                cbEnginer.Items.Clear();
                cbEnginer.Items.AddRange(value);
            }
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
            foreach(DataGridViewColumn column in dgParameters.Columns)
            {
                column.SortMode = DataGridViewColumnSortMode.NotSortable;
            }
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
                    Int16 count = 1;
                    if (selectedMna.TsSecurity !=null && selectedMna.TsSecurity.Any())
                    {
                        dgParameters.Rows.Add(String.Empty, selectedMna.TsSecurityCaption);
                        dgParameters.Rows[rowNum - 1].Cells[1].Style.Font = new Font("Arial", 14, FontStyle.Bold);
                        foreach (Tag tag in selectedMna.TsSecurity)
                        {
                            dgParameters.Rows.Add(count, string.Format(tag.Caption, MnaNumber), tag.Status);
                            if (tag.Status == Status.Ok) dgParameters.Rows[rowNum].Cells[2].Style.BackColor = Color.Green;
                            else if (tag.Status == Status.NotFound) dgParameters.Rows[rowNum].Cells[2].Style.BackColor = Color.Red;
                            else if (tag.Status == Status.NotSingleResult) dgParameters.Rows[rowNum].Cells[2].Style.BackColor = Color.LightGreen;

                            if (string.IsNullOrEmpty(tag.Name))
                            {
                                dgParameters.Rows[rowNum].Cells[0].Style.BackColor = Color.Silver;
                                dgParameters.Rows[rowNum].Cells[1].Style.BackColor = Color.Silver;
                                dgParameters.Rows[rowNum].Cells[2].Style.BackColor = Color.Silver;
                            }
                            rowNum++;
                            count++;
                        }
                    }

                    if (selectedMna.TsOther != null && selectedMna.TsOther.Any())
                    {
                        dgParameters.Rows.Add();
                        rowNum++;
                        dgParameters.Rows.Add(String.Empty, selectedMna.TsOtherCaption);
                        dgParameters.Rows[rowNum].Cells[1].Style.Font = new Font("Arial", 14, FontStyle.Bold);
                        rowNum++;

                        count = 1;
                        foreach (Tag tag in selectedMna.TsOther)
                        {
                            dgParameters.Rows.Add(count, string.Format(tag.Caption, MnaNumber), tag.Status);
                            if (tag.Status == Status.Ok) dgParameters.Rows[rowNum].Cells[2].Style.BackColor = Color.Green;
                            else if (tag.Status == Status.NotFound) dgParameters.Rows[rowNum].Cells[2].Style.BackColor = Color.Red;
                            else if (tag.Status == Status.NotSingleResult) dgParameters.Rows[rowNum].Cells[2].Style.BackColor = Color.LightGreen;

                            if (string.IsNullOrEmpty(tag.Name))
                            {
                                dgParameters.Rows[rowNum].Cells[0].Style.BackColor = Color.Silver;
                                dgParameters.Rows[rowNum].Cells[1].Style.BackColor = Color.Silver;
                                dgParameters.Rows[rowNum].Cells[2].Style.BackColor = Color.Silver;
                            }

                            rowNum++;
                            count++;
                        }
                    }


                    if (selectedMna.Tu != null && selectedMna.Tu.Any())
                    {
                        dgParameters.Rows.Add();
                        rowNum++;
                        dgParameters.Rows.Add(String.Empty, selectedMna.TuCaption);
                        dgParameters.Rows[rowNum].Cells[1].Style.Font = new Font("Arial", 14, FontStyle.Bold);
                        rowNum++;

                        count = 1;
                        foreach (Tag tag in selectedMna.Tu)
                        {
                            dgParameters.Rows.Add(rowNum, string.Format(tag.Caption, MnaNumber), tag.Status);
                            if (tag.Status == Status.Ok) dgParameters.Rows[rowNum].Cells[2].Style.BackColor = Color.Green;
                            else if (tag.Status == Status.NotFound) dgParameters.Rows[rowNum].Cells[2].Style.BackColor = Color.Red;
                            else if (tag.Status == Status.NotSingleResult) dgParameters.Rows[rowNum].Cells[2].Style.BackColor = Color.LightGreen;

                            if (string.IsNullOrEmpty(tag.Name))
                            {
                                dgParameters.Rows[rowNum].Cells[0].Style.BackColor = Color.Silver;
                                dgParameters.Rows[rowNum].Cells[1].Style.BackColor = Color.Silver;
                                dgParameters.Rows[rowNum].Cells[2].Style.BackColor = Color.Silver;
                            }

                            rowNum++;
                            count++;
                        }
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
