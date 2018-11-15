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
using OfficeOpenXml;

namespace MNA
{
    public partial class MNA : Form, IMnaView
    {
        private IMnaPresenter _presenter;

        const string cfg_file = "\\configs\\mna_service.xml";
        private static bool _isInit = false;
        private static IList<Mna> _mnaList;

        public int MnaNumber { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public int ExcelColumnCaption { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public int ExcelColumnTag { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public bool IsInit()
        {
            return _isInit;
        }

        public IEnumerable<Mna> GetMnaList()
        {
            return _mnaList;
        }

        public MNA(IMnaPresenter presenter)
        {


            //_presenter = CompositionRoot.Resolve<IMnaPresenter>();
            //_presenter = new MnaPresenter();
            _presenter = presenter;
            _presenter.View = this;
            InitializeComponent();
            ReadConfig();
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



            dgParameters.Columns.Add("Num","#");
            dgParameters.Columns.Add("Parameter","Параметр");
            dgParameters.Columns.Add("Status", "Статус");
            //dgParameters
        }

        public void ReadConfig()
        {
            if (_isInit) return;
            _mnaList = new List<Mna>();
            if (Directory.Exists(AppSettings.AppFolder + "\\configs"))
            {
                if (File.Exists(AppSettings.AppFolder + cfg_file))
                {
                    var xdoc = XDocument.Load(AppSettings.AppFolder + cfg_file);
                    var mnaList = xdoc.Element("sdku_reader").Element("to_mna").Elements("mna");
                    foreach (var mna in mnaList)
                    {
                        if (mna != null)
                        {
                            XAttribute captionMna = mna.Attribute("caption");
                            XAttribute tagMna = mna.Attribute("tag");
                            XAttribute positionMna = mna.Attribute("position");
                            if ((captionMna != null) && (tagMna != null) && (positionMna != null))
                            {
                                var mnaItem = new Mna
                                {
                                    Caption = captionMna.Value,
                                    BaseTag = tagMna.Value,
                                    Position = positionMna.Value
                                };

                                #region Читаем раздел "ts_security"
                                var tsSecurityInner = mna.Element("ts_security");
                                if (tsSecurityInner != null)
                                {
                                    var tsSecurityList = mna.Element("ts_security").Elements("ts");
                                    var tsSecurity = new List<Tag>();
                                    foreach (var ts in tsSecurityList)
                                    {
                                        if (ts != null)
                                        {
                                            XAttribute captionTag = ts.Attribute("caption");
                                            var nameTag = ts.Value;
                                            if ((captionTag != null) && (nameTag != null))
                                            {
                                                tsSecurity.Add(new Tag()
                                                {
                                                    Caption = captionTag.Value,
                                                    Name = nameTag,
                                                    FullName = mnaItem.BaseTag+ "."+ nameTag
                                                });
                                            }
                                        }
                                    }
                                    mnaItem.TsSecurity = tsSecurity;
                                }
                                #endregion

                                #region Читаем раздел "ts_other"
                                var tsOtherInner = mna.Element("ts_other");
                                if (tsOtherInner != null)
                                {
                                    var tsOtherList = mna.Element("ts_other").Elements("ts");
                                    var tsOther = new List<Tag>();
                                    foreach (var ts in tsOtherList)
                                    {
                                        if (ts != null)
                                        {
                                            XAttribute captionTag = ts.Attribute("caption");
                                            var nameTag = ts.Value;
                                            if ((captionTag != null) && (nameTag != null))
                                            {
                                                tsOther.Add(new Tag()
                                                {
                                                    Caption = captionTag.Value,
                                                    Name = nameTag,
                                                    FullName = mnaItem.BaseTag + "." + nameTag
                                                });
                                            }
                                        }
                                    }
                                    mnaItem.TsOther = tsOther;
                                }
                                #endregion
                                #region Читаем раздел "tu"
                                var tuInner = mna.Element("tu_command");
                                if (tuInner != null)
                                {
                                    var tuList = mna.Element("tu_command").Elements("tu");
                                    var tuCommand = new List<Tag>();
                                    foreach (var tu in tuList)
                                    {
                                        if (tu != null)
                                        {
                                            XAttribute captionTag = tu.Attribute("caption");
                                            var nameTag = tu.Value;
                                            if ((captionTag != null) && (nameTag != null))
                                            {
                                                tuCommand.Add(new Tag()
                                                {
                                                    Caption = captionTag.Value,
                                                    Name = nameTag,
                                                    FullName = mnaItem.BaseTag + "." + nameTag,
                                                    Id = new Guid()
                                                });
                                            }
                                        }
                                    }
                                    mnaItem.Tu = tuCommand;
                                }
                                #endregion
                                _mnaList.Add(mnaItem);

                            }
                        }
                    }
                    _isInit = true;
                }
            }

            if (_mnaList !=null && _mnaList.Any())
            {
                foreach (var mna in _mnaList)
                {
                    lbMnaList.Items.Add(mna.Caption);
                }
            }

        }

        private void ResetStatusCurrentMna(Mna mna)
        {
            foreach (Tag tag in mna.TsSecurity)
            {
                tag.Status = String.Empty;
            }
            foreach (Tag tag in mna.TsOther)
            {
                tag.Status = String.Empty;
            }
            foreach (Tag tag in mna.Tu)
            {
                tag.Status = String.Empty;
            }
        }

        private void RefreshView()
        {
            String mnaCaption = lbMnaList.Items[lbMnaList.SelectedIndex].ToString();
            if (mnaCaption != null)
            {
                var selectedMna = _mnaList.SingleOrDefault(x => x.Caption == mnaCaption);
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

        private void lbMnaList_SelectedIndexChanged(object sender, EventArgs e)
        {
            RefreshView();
        }

        private bool IsColumnsOfExcelTheSame()
        {
            return nColumnCaption.Value == nColumnTag.Value;
        }

        private Mna GetCurrentMna()
        {
            if (lbMnaList.Items.Count == 0) return null;
            return _mnaList.SingleOrDefault(x => x.Caption == lbMnaList.Items[lbMnaList.SelectedIndex].ToString());
        }

        private Tag FindTagInList(string tag, IEnumerable<Tag> tags)
        {
            return tags.ToList().SingleOrDefault(x => x.FullName.ToLower() == tag.ToLower());
        }

        private void btnOpenFile_Click(object sender, EventArgs e)
        {
            Mna currentMna = GetCurrentMna();
            int columnCaption = (int)nColumnCaption.Value;
            int columnTag = (int)nColumnTag.Value;

            if (!IsColumnsOfExcelTheSame() && currentMna != null)
            {
                OpenFileDialog fileDialog = new OpenFileDialog();
                fileDialog.Filter = "Excel files|*.xls;*.xlsx";
                if (fileDialog.ShowDialog() == DialogResult.OK)
                {
                    ResetStatusCurrentMna(currentMna);
                    using (var excel = new ExcelPackage(new FileInfo(fileDialog.FileName)))
                    {
                        ExcelWorksheet sheet = excel.Workbook.Worksheets.First();
                        var allTags = currentMna.TsSecurity.ToList().Concat(currentMna.TsOther).Concat(currentMna.Tu);
                        for (var rowNum = 2; rowNum <= sheet.Dimension.End.Row; rowNum++)
                        {
                            Tag findTag = FindTagInList(sheet.Cells[rowNum, columnTag].Text, allTags);
                            if ( findTag != null)
                            {
                                //currentMna.TsSecurity.FirstOrDefault().Status = "OK";
                                findTag.Status = "OK";
                            }
                        }

                        //var sheet = excel.Workbook.Worksheets.First();
                        //for (var rowNum = 2; rowNum <= sheet.Dimension.End.Row; rowNum++)
                        //{
                        //    try
                        //    {
                        //        goodList.Add(new Good
                        //        {
                        //            Code = sheet.Cells[rowNum, 2].Text,
                        //            Name = sheet.Cells[rowNum, 1].Text
                        //        });
                        //    }
                        //    catch { }
                        //}
                    }
                    RefreshView();
                    var p = 1;
                }
            } else
            {
                MessageBox.Show("Столбцы для чтения файла не должны быть одинаковыми", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }


            

        }
    }
}
