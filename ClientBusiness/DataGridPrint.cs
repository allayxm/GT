using System.Windows.Forms;
using VBprinter;
using System.Drawing;

namespace JXDL.ClientBusiness
{
    public class DataGridPrint : DGVprint
    {
        //public DataGridPrint(DataGridView DGV, string ProjectName):base(DGV,ProjectName)
        //{
        //    ReportPrint vReportPrint = new ReportPrint();
        //    PageSettings(vReportPrint.PageSet);
        //}
        public DataGridPrint()
        {
        }

        /// <summary>
        /// 打印datagridview上边的内容-带页眉，页脚
        /// </summary>
        /// <param name="dgv">需要打印的datagridview</param>
        /// <param name="title">需要打印的标题</param>
        /// <param name="AuxiliaryTitle">需要打印的辅助标题，（查询条件）</param>
        /// <param name="TableHeaderLeft">页眉左边内容</param>
        /// <param name="TableHeaderRight">页眉右边内容</param>
        /// <param name="TableFooterLeft">页脚左边内容</param>
        /// <param name="sumcolumns">如：(列名,列名,列名)需要汇总的列,不需要汇总填写"",如果要汇总多列的话，可用英文的分号连接起来（注意，此处是列的名称而不是显示的名称）</param>
        public void NewPrint(DataGridView dgv, string title)
        {
            MainTitle = title;
            AutoFormat = true;
            Alignment = StringAlignment.Center;//'表格居中
            PrintType = VBprinter.DGVprint.mytype.MulPagesPrint;  
            GroupNewPage = false;// '每组连续打印
            IsAddRowID = true;// '添加行号
            IsGroupNewRowID = true; //'新组行号从1开始重新编号
            PaperLandscape = true; //'纵向打印
            IsImmediatePrint = true;
            IsImmediatePrintShowPrintDialog = true;
            Print(dgv, false);// '也可用dgvprint1.print(d1,true),此时会显示一个打印参数设置窗口
        }

        private void InitializeComponent()
        {

        }


    }
}
