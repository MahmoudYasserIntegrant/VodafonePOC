using Microsoft.Office.Interop.Excel;
using System;
using excel = Microsoft.Office.Interop.Excel;

namespace VodafonePOC
{
    /// <summary>
    /// This class reads data from External Excel sheet
    /// data is stored in Array of strings to minimize the number of calls as possible
    /// </summary>
   public class ReadExcel
    {
        /********* Variables ***********/
        excel.Application app;
        excel.Workbook workBook;
        excel.Worksheet workSheet;
        excel.Range range;
        private static string[] Data;
        

        public static string GetCurrentPath()
        {
            string pth = System.Reflection.Assembly.GetCallingAssembly().CodeBase;
            string actualPath = pth.Substring(0, pth.LastIndexOf("bin"));
            string projectPath = new Uri(actualPath).LocalPath;
            return projectPath;
        }
        /********* Read and return data ***********/
        public void ReadData()
        {
            app = new excel.Application();
            workBook = app.Workbooks.Open(GetCurrentPath()+ "\\ReadExcel\\TestData");
            workSheet = workBook.Sheets[1];
            range = workSheet.UsedRange;
            Data = new string[12];
            for (int i = 1; i < Data.Length; i++)
            {
                Data[i] = (string)(WorkSheet.Cells[i, 2] as Range).Value;
            }

        }
        public string GetStartUrl()
        {
            return Data[1];
        }
        public String GeteShopButtonLocator()
        {
            return Data[3];
        }
        public String GetSearchLocator()
        {
            return Data[4];
        }
        public String GetSearchQuery()
        {
            return Data[10];
        }
        public String GetCountLabelLocator()
        {
            return Data[5];
        }
        public String GetRealCountLocator()
        {
            return Data[6];
        }
        public String GetNoResultLocator()
        {
            return Data[7];

        }
        public String GetResultPageURL()
        {
            return Data[2];

        }
        public String GetPricesLocator()
        {
            return Data[8];
        }
        public String GetSortLocator()
        {
            return Data[9];
        }
        public String GetSortingOrder()
        {
            return Data[11];
        }

        /********* Getters ***********/
        public Range Range { get => range; }
        public Worksheet WorkSheet { get => workSheet; }
        public Workbook WorkBook { get => workBook; }
        public Application App { get => app; }
    }


}
