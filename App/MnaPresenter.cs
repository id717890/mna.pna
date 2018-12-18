using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using App.Data;
using App.Interface.Presenter;
using App.Interface.View;
using App.Interface.Model;
using App.Models;
using OfficeOpenXml;

namespace App
{
    public class MnaPresenterNew : IMnaPresenter, IMnaPresenterCallback
    {
        const string CfgFile = "\\configs\\mna_service.xml";

        private readonly IMnaView _view;
        private IMnaViewModel _model;

        private static bool _isInit = false;


        public MnaPresenterNew(IMnaView view, IMnaViewModel model)
        {
            _view = view;
            _model = model;
        }

        public object Ui
        {
            get { return _view; }
        }

        public void Initialize()
        {
            ReadConfig();
            _view.Attach(this);

        }

        public void OnMnaChenged(Mna mna)
        {
            SetCurrentMna(mna);
        }

        private void FindTagInList(IEnumerable<App.Data.ExcelColumn> excel, IEnumerable<Tag> tags, int mnaNumber = -1)
        {
            if (tags !=null && tags.Any())
            {
                foreach (Tag tag in tags)
                {
                    if (tag.Checkable)
                    {
                        int count = 0;
                        if (mnaNumber != -1)
                            count = excel.ToList().Count(
                                x => x.Tag.ToLower() == string.Format(tag.FullName, mnaNumber).ToLower() 
                                && x.Caption.ToLower().Contains(tag.Caption.ToLower())
                                && !x.Caption.ToLower().Contains("снят"));
                        else
                            count = excel.ToList().Count(
                                x => x.Tag.ToLower() == tag.FullName.ToLower() 
                                && x.Caption.ToLower().Contains(tag.Caption.ToLower())
                                && !x.Caption.ToLower().Contains("снят"));

                        if (count == 1)
                        {
                            tag.Status = Status.Ok;
                        }
                        else if (count > 1)
                        {
                            tag.Status = Status.NotSingleResult;
                        }
                        else tag.Status = Status.NotFound;
                    } else
                    {
                        int count = 0;
                        if (mnaNumber != -1)
                            count = excel.ToList().Count(
                                x => x.Tag.ToLower() == string.Format(tag.FullName, mnaNumber).ToLower() && !x.Caption.ToLower().Contains("снят"));
                        else
                            count = excel.ToList().Count(x => x.Tag.ToLower() == tag.FullName.ToLower() && !x.Caption.ToLower().Contains("снят"));

                        if (count == 1)
                        {
                            tag.Status = Status.Ok;
                        }
                        else if (count > 1)
                        {
                            tag.Status = Status.NotSingleResult;
                        }
                        else tag.Status = Status.NotFound;
                    }
                }
            }
        }

        private Tag FindTagInList(string tag, IEnumerable<Tag> tags)
        {
            try
            {
                return tags.ToList().SingleOrDefault(x => x.FullName.ToLower() == tag.ToLower());
            }
            catch
            {
                return null;
            }
        }

        private Tag FindTagInList(string tag, IEnumerable<Tag> tags, string mnaNumber)
        {
            try
            {
                return tags.ToList().SingleOrDefault(x => string.Format(x.FullName, mnaNumber).ToLower() == tag.ToLower());
            }
            catch
            {
                return null;
            }
        }

        public void OnOpenExcelFile(string fileName)
        {
            ResetStatusCurrentMna();
            using (var excel = new ExcelPackage(new FileInfo(fileName)))
            {
                ExcelWorksheet sheet = excel.Workbook.Worksheets.First();
                List<Tag> allTags = new List<Tag>();
                if (_model.CurrentMna.TsSecurity != null && _model.CurrentMna.TsSecurity.Any()) allTags.AddRange(_model.CurrentMna.TsSecurity);
                if (_model.CurrentMna.TsOther != null && _model.CurrentMna.TsOther.Any()) allTags.AddRange(_model.CurrentMna.TsOther);
                if (_model.CurrentMna.Tu != null && _model.CurrentMna.Tu.Any()) allTags.AddRange(_model.CurrentMna.Tu);
                //var allTags = _model.CurrentMna.TsSecurity.ToList().Concat(_model.CurrentMna.TsOther).Concat(_model.CurrentMna.Tu);

                List<App.Data.ExcelColumn> excelRows = new List<App.Data.ExcelColumn>();
                for (var rowNum = 2; rowNum <= sheet.Dimension.End.Row; rowNum++)
                {
                    excelRows.Add(new App.Data.ExcelColumn
                    {
                        Caption = sheet.Cells[rowNum, _view.ColumnCaption].Text,
                        Tag = sheet.Cells[rowNum, _view.ColumnTag].Text,
                    });
                }
                if (excelRows.Any())
                {
                    if (_model.CurrentMna.TagWithNumber) FindTagInList(excelRows, allTags, _view.MnaNumber);
                    else FindTagInList(excelRows, allTags);
                }



                //for (var rowNum = 2; rowNum <= sheet.Dimension.End.Row; rowNum++)
                //{
                //    Tag findTag = _model.CurrentMna.TagWithNumber
                //        ? FindTagInList(sheet.Cells[rowNum, _view.ColumnTag].Text, allTags, _view.MnaNumber.ToString())
                //        : FindTagInList(sheet.Cells[rowNum, _view.ColumnTag].Text, allTags);
                //    if (findTag != null)
                //    {
                //        findTag.Status = "OK";
                //    }
                //}
            }
            _view.RenderParametersGrid();
        }

        public void ReadConfig()
        {
            if (_isInit) return;
            List<Mna> model = new List<Mna>();

            if (Directory.Exists(AppSettings.AppFolder + "\\configs"))
            {
                if (File.Exists(AppSettings.AppFolder + CfgFile))
                {
                    var xdoc = XDocument.Load(AppSettings.AppFolder + CfgFile);
                    var mnaList = xdoc.Element("sdku_reader")?.Element("to_object")?.Elements("object");
                    if (mnaList != null && mnaList.Any())
                    {
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
                                        Position = positionMna.Value,
                                        Id = Guid.NewGuid()
                                    };

                                    XAttribute tagWithNumber = mna.Attribute("numeric");
                                    if (tagWithNumber != null && Boolean.Parse(tagWithNumber.Value))
                                    {
                                        mnaItem.TagWithNumber = true;
                                    }
                                    else
                                    {
                                        mnaItem.TagWithNumber = false;
                                    }

                                    #region Читаем раздел "ts_security"
                                    var tsSecurityInner = mna.Element("ts_security");
                                    if (tsSecurityInner != null)
                                    {
                                        var tsSecurityCaption = mna.Element("ts_security").Attribute("caption");
                                        var tsSecurityList = mna.Element("ts_security").Elements("ts");
                                        var tsSecurity = new List<Tag>();
                                        foreach (var ts in tsSecurityList)
                                        {
                                            if (ts != null)
                                            {
                                                XAttribute captionTag = ts.Attribute("caption");
                                                XAttribute checkableTag = ts.Attribute("checkable");
                                                XAttribute textTag = ts.Attribute("text");
                                                
                                                var nameTag = ts.Value;
                                                if ((captionTag != null) && (nameTag != null))
                                                {
                                                    Tag newTag = new Tag()
                                                    {
                                                        Caption = captionTag.Value,
                                                        Name = nameTag,
                                                        FullName = mnaItem.BaseTag + "." + nameTag
                                                    };
                                                    if (checkableTag != null && Boolean.Parse(checkableTag.Value))
                                                    {
                                                        newTag.Checkable = true;
                                                    }
                                                    else
                                                    {
                                                        newTag.Checkable = false;
                                                    }
                                                    if (textTag != null) newTag.Text = textTag.Value;
                                                    tsSecurity.Add(newTag);
                                                }
                                            }
                                        }
                                        mnaItem.TsSecurity = tsSecurity;
                                        mnaItem.TsSecurityCaption = tsSecurityCaption.Value;
                                    }
                                    #endregion

                                    #region Читаем раздел "ts_other"
                                    var tsOtherInner = mna.Element("ts_other");
                                    if (tsOtherInner != null)
                                    {
                                        var tsOtherCaption = mna.Element("ts_other").Attribute("caption");
                                        var tsOtherList = mna.Element("ts_other").Elements("ts");
                                        var tsOther = new List<Tag>();
                                        foreach (var ts in tsOtherList)
                                        {
                                            if (ts != null)
                                            {
                                                XAttribute captionTag = ts.Attribute("caption");
                                                XAttribute checkableTag = ts.Attribute("checkable");
                                                XAttribute textTag = ts.Attribute("text");

                                                var nameTag = ts.Value;
                                                if ((captionTag != null) && (nameTag != null))
                                                {
                                                    Tag newTag = new Tag()
                                                    {
                                                        Caption = captionTag.Value,
                                                        Name = nameTag,
                                                        FullName = mnaItem.BaseTag + "." + nameTag
                                                    };
                                                    if (checkableTag != null && Boolean.Parse(checkableTag.Value))
                                                    {
                                                        newTag.Checkable = true;
                                                    }
                                                    else
                                                    {
                                                        newTag.Checkable = false;
                                                    }
                                                    if (textTag != null) newTag.Text = textTag.Value;
                                                    tsOther.Add(newTag);
                                                }
                                            }
                                        }
                                        mnaItem.TsOther = tsOther;
                                        mnaItem.TsOtherCaption = tsOtherCaption.Value;
                                    }
                                    #endregion
                                    #region Читаем раздел "tu"
                                    var tuInner = mna.Element("tu_command");
                                    if (tuInner != null)
                                    {
                                        var tuCaption = mna.Element("tu_command").Attribute("caption");
                                        var tuList = mna.Element("tu_command").Elements("tu");
                                        var tuCommand = new List<Tag>();
                                        foreach (var tu in tuList)
                                        {
                                            if (tu != null)
                                            {
                                                XAttribute captionTag = tu.Attribute("caption");
                                                XAttribute textTag = tu.Attribute("text");

                                                var nameTag = tu.Value;
                                                if ((captionTag != null) && (nameTag != null))
                                                {
                                                    var newTag = new Tag()
                                                    {
                                                        Caption = captionTag.Value,
                                                        Name = nameTag,
                                                        FullName = mnaItem.BaseTag + "." + nameTag,
                                                        Id = Guid.NewGuid()
                                                    };
                                                    if (textTag != null) newTag.Text = textTag.Value;
                                                    tuCommand.Add(newTag);
                                                }
                                            }
                                        }
                                        mnaItem.Tu = tuCommand;
                                        mnaItem.TuCaption = tuCaption.Value;
                                    }
                                    #endregion
                                    model.Add(mnaItem);
                                }
                            }
                        }
                        _isInit = true;
                        _model = new MnaViewModel { MnaList = model.OrderBy(x => x.Position), CurrentMna = model[0] };
                        _view.SetModel(_model);
                        _view.IsMnaNumber = _model.CurrentMna.TagWithNumber;
                    }
                }
            }
        }

        public void SetCurrentMna(Mna mna)
        {
            _model.CurrentMna = mna;
            _view.IsMnaNumber = mna.TagWithNumber;
        }

        public void ResetStatusCurrentMna()
        {
            if (_model.CurrentMna != null)
            {
                if (_model.CurrentMna.TsSecurity != null && _model.CurrentMna.TsSecurity.Any())
                    foreach (Tag tag in _model.CurrentMna.TsSecurity)
                    {
                        tag.Status = String.Empty;
                    }

                if (_model.CurrentMna.TsOther != null && _model.CurrentMna.TsOther.Any())
                    foreach (Tag tag in _model.CurrentMna.TsOther)
                    {
                        tag.Status = String.Empty;
                    }

                if (_model.CurrentMna.Tu != null && _model.CurrentMna.Tu.Any())
                    foreach (Tag tag in _model.CurrentMna.Tu)
                    {
                        tag.Status = String.Empty;
                    }
            }
        }
    }
}
