namespace MNA
{
    public interface IMnaView
    {
        //IMnaPresenter Presenter { get; set; }

        int MnaNumber { get; set; }
        int ExcelColumnCaption { get; set; }
        int ExcelColumnTag { get; set; }
    }
}
