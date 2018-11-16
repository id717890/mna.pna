using MNA.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Configuration;
using System.Xml.Linq;
using MNA.Interfaces;
using MNA.Models;
using OfficeOpenXml;

namespace MNA
{
    public partial class MNA : Form, IMnaViewNew
    {
        //private IMnaPresenter _presenter;

        //private Button _saveButton;
        //private TextBox _myTextBox;

        private static IList<Mna> _mnaList;

        //public int MnaNumber { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        //public int ExcelColumnCaption { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        //public int ExcelColumnTag { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }


        public void Attach(IMnaPresenterCallbacks callback)
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

        //public string MyText
        // {
        //         get { return _myTextBox.Text; }
        //         set { _myTextBox.Text = value; }
        //     }

        public int ColumnCaption
        {
            get => (Int32) nColumnCaption.Value;
            set => nColumnCaption.Value = value;
        }

        public int ColumnTag
        {
            get => (Int32) nColumnTag.Value;
            set => nColumnTag.Value = value;
        }

        public string SaveButtonText
        {
            get { return _saveButton.Text; }
            set { _saveButton.Text = value; }
        }

        public bool SaveButtonEnabled
        {
            get { return _saveButton.Enabled; }
            set { _saveButton.Enabled = value; }
        }

        public ListBox MnaListBox
        {
            get => lbMnaList;
            set => lbMnaList = value;
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

        //public bool IsInit()
        //{
        //    return _isInit;
        //}

        //public IEnumerable<Mna> GetMnaList()
        //{
        //    return _mnaList;
        //}

        public MNA()
        {


            //_presenter = CompositionRoot.Resolve<IMnaPresenter>();
            //_presenter = new MnaPresenter();
            //_presenter = presenter;
            //_presenter.View = this;
            InitializeComponent();
            //ReadConfig();
            InitDataGrid();
            if (lbMnaList.Items.Count > 0) lbMnaList.SelectedIndex = 0;
            //_presenter = CompositionRoot.Resolve<IMnaPresenter>();

        }

        //public MNA()
        //{
        //    _presenter = presenter;
        //}

        private void InitDataGrid()
        {
            dgParameters.AutoGenerateColumns = false;
            //DataTable table = new DataTable();

            // column = new DataGridViewColumn
            //{
            //    DataPropertyName = "Caption",
            //    Name = "Параметр"
            //};



            dgParameters.Columns.Add("Num", "#");
            dgParameters.Columns.Add("Parameter", "Параметр");
            dgParameters.Columns.Add("Status", "Статус");
            //dgParameters
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
            //String mnaCaption = lbMnaList.Items[lbMnaList.SelectedIndex].ToString();
            //if (mnaCaption != null)
            //{
            //    var selectedMna = _mnaList.SingleOrDefault(x => x.Caption == mnaCaption);

            //}
        }

        //private void lbMnaList_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    //RefreshView();
        //}

        private bool IsColumnsOfExcelTheSame()
        {
            return nColumnCaption.Value == nColumnTag.Value;
        }

        //private Mna GetCurrentMna()
        //{
        //    if (lbMnaList.Items.Count == 0) return null;
        //    return _mnaList.SingleOrDefault(x => x.Caption == lbMnaList.Items[lbMnaList.SelectedIndex].ToString());
        //}

        

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
