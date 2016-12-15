using System;
using System.Data;
using System.Globalization;
using System.IO;
using System.Text;
using NPOI.HPSF;
using NPOI.SS.Formula.Eval;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.SS.Util;
using System.Collections;

namespace AchieveCommon.Execl
{
    /// <summary>
    /// 类说明：Excel操作静态类（基于NPOI组件）
    /// </summary>
    public static class ExcelHelper
    {
        /// <summary>
        /// 获取Excel文件sheet页总数
        /// </summary>
        /// <param name="outputFile">要读取Excel文件</param>
        /// <returns></returns>
        public static int GetSheetNumber(string outputFile)
        {
            var number = 0;
            try
            {
                var readFile = new FileStream(outputFile, FileMode.Open, FileAccess.Read);
                var workbook = new HSSFWorkbook(readFile);
                number = workbook.NumberOfSheets;
            }
            catch (Exception ex)
            {
                //这里添加异常异常日志记录
                WriteLog.WriteErorrLog(ex, "ExcelHelper类，GetSheetNumber方法异常");
                return 0;
            }
            return number;
        }
        /// <summary>
        /// 获取Excel文件sheet名称集合
        /// </summary>
        /// <param name="outputFile">要读取Excel文件</param>
        /// <returns></returns>
        public static ArrayList GetSheetName(string outputFile)
        {
            var arrayList = new ArrayList();
            try
            {
                var readFile = new FileStream(outputFile, FileMode.Open, FileAccess.Read);
                var workbook = new HSSFWorkbook(readFile);
                for (var i = 0; i < workbook.NumberOfSheets; i++)
                {
                    arrayList.Add(workbook.GetSheetName(i));
                }

            }
            catch (Exception ex)
            {
                //这里添加异常异常日志记录
                WriteLog.WriteErorrLog(ex, "ExcelHelper类，GetSheetNumber方法异常");
                return null;
            }
            return arrayList;
        }
        /// <summary>
        /// 从DataTable中将数据导出到Excel文件
        /// </summary>
        /// <param name="dtSource">提供导出数据的DataTable</param>
        /// <param name="headerText">表头文本</param>
        /// <returns></returns>
        public static MemoryStream ExportDataTable(DataTable dtSource, string headerText)
        {
            //创建工作表
            var workbook = new HSSFWorkbook();
            //创建sheet页
            var sheet = workbook.CreateSheet();
            #region 添加Excel文件属性信息

            var dsi = PropertySetFactory.CreateDocumentSummaryInformation();
            dsi.Company = "ZWKJ";
            workbook.DocumentSummaryInformation = dsi;

            var si = PropertySetFactory.CreateSummaryInformation();
            si.Author = "赵雄";
            si.ApplicationName = "使用NPOI创建的Excel文件";
            si.CreateDateTime = DateTime.Now;
            workbook.SummaryInformation = si;

            #endregion

            //设置日期格式
            var dateStyle = workbook.CreateCellStyle();
            var format = workbook.CreateDataFormat();
            dateStyle.DataFormat = format.GetFormat("yyyy-mm-dd");
            //取得列宽
            var columnWidth = new int[dtSource.Columns.Count];
            //遍历DataTable的列
            foreach (DataColumn column in dtSource.Columns)
            {
                columnWidth[column.Ordinal] = Encoding.GetEncoding(936).GetBytes(column.ColumnName).Length;
            }
            //遍历所有的Row,若当前Row内容长度超出列名长度，则将此列的长度设为该Row内容长度
            for (var i = 0; i < dtSource.Rows.Count; i++)
            {
                for (var j = 0; j < dtSource.Columns.Count; j++)
                {
                    var currentRowLength = Encoding.GetEncoding(936).GetBytes(dtSource.Rows[i][j].ToString()).Length;
                    if (currentRowLength > columnWidth[j])
                        columnWidth[j] = currentRowLength;
                }
            }
            
            var rowIndex = 0;
            #region 表头及样式

            var headRow = sheet.CreateRow(0);
            headRow.HeightInPoints = 25;
            headRow.CreateCell(0).SetCellValue(headerText);

            var headStyle = workbook.CreateCellStyle();
            headStyle.Alignment = HorizontalAlignment.CENTER;
            var font = workbook.CreateFont();
            font.Boldweight = 700;
            font.FontHeightInPoints = 20;
            headStyle.SetFont(font);
            headRow.GetCell(0).CellStyle = headStyle;
            sheet.AddMergedRegion(new CellRangeAddress(0, 0, 0, dtSource.Columns.Count - 1));
            #endregion
            #region 列头及样式

            var columnRow = sheet.CreateRow(1);
            var columnStyle = workbook.CreateCellStyle();
            columnStyle.Alignment = HorizontalAlignment.CENTER;
            var columnFont = workbook.CreateFont();
            columnFont.Boldweight = 700;
            columnFont.FontHeightInPoints = 10;
            columnStyle.SetFont(columnFont);
            foreach (DataColumn column in dtSource.Columns)
            {
                //设置列头内容
                columnRow.CreateCell(column.Ordinal).SetCellValue(column.ColumnName);
                //设置列头样式
                columnRow.GetCell(column.Ordinal).CellStyle = columnStyle;
                //设置列宽
                sheet.SetColumnWidth(column.Ordinal, (columnWidth[column.Ordinal] + 1) * 256);

            }
            #endregion
            rowIndex = 2;
            foreach (DataRow row in dtSource.Rows)
            {
                if(rowIndex == 65535)
                    sheet = workbook.CreateSheet();
                #region 填充数据

                var dataRow = sheet.CreateRow(rowIndex);
                foreach (DataColumn column in dtSource.Columns)
                {
                    var newCell = dataRow.CreateCell(column.Ordinal);
                    var cellValue = row[column].ToString();
                    switch (column.DataType.ToString())
                    {
                        //字符串类型
                        case "System.String":
                            double result;
                            if (double.TryParse(cellValue, out result))
                            {
                                newCell.SetCellValue(result);
                                break;
                            }
                            newCell.SetCellValue(cellValue);
                            break;
                        //DateTime类型
                        case "System.DateTime":
                            DateTime tmpdt;
                            if (DateTime.TryParse(cellValue, out tmpdt))
                            {
                                newCell.SetCellValue(tmpdt);
                                newCell.CellStyle = dateStyle;
                                break;
                            }
                            newCell.SetCellValue(cellValue);
                            break;
                        //布尔类型
                        case "System.Boolean":
                            bool boolV;
                            if (bool.TryParse(cellValue, out boolV))
                            {
                                newCell.SetCellValue(boolV);
                                break;
                            }
                            newCell.SetCellValue(cellValue);
                            break;
                        //整型
                        case "System.Int16":
                        case "System.Int32":
                        case "System.Int64":
                        case "System.Byte":
                            int intV;
                            if (int.TryParse(cellValue, out intV))
                            {
                                newCell.SetCellValue(intV);
                                break;
                            }
                            newCell.SetCellValue(cellValue);
                            break;
                        //浮点型
                        case "System.Decimal":
                        case "System.Double":
                            double doubV;
                            if (double.TryParse(cellValue, out doubV))
                            {
                                newCell.SetCellValue(doubV);
                                break;
                            }
                            newCell.SetCellValue(cellValue);
                            break;
                        //空值处理
                        case "System.DBNull":
                            newCell.SetCellValue("");
                            break;
                        default:
                            newCell.SetCellValue("");
                            break;


                    }
                }
                rowIndex++;

                #endregion

            }
            using (var ms = new MemoryStream())
            {
                workbook.Write(ms);
                ms.Flush();
                ms.Position = 0;
                return ms;
            }
        }
        /// <summary>
        /// DataTable导出到Excel文件
        /// </summary>
        /// <param name="dtSource">源DataTable</param>
        /// <param name="headerText">表头</param>
        /// <param name="fileName">Excel文件保存位置(包含文件名)</param>
        public static void ExportDataTableToExcel(DataTable dtSource, string headerText,string fileName)
        {
            using (var ms = ExportDataTable(dtSource, headerText))
            {
                using (var fileStream = new FileStream(fileName, FileMode.Create, FileAccess.Write))
                {
                    var data = ms.ToArray();
                    fileStream.Write(data,0,data.Length);
                    fileStream.Flush();
                }
            }
        }
        /// <summary>
        /// 将sheet页导出至DataTable中
        /// </summary>
        /// <param name="sheet">需要导出的sheet</param>
        /// <param name="headerRowIndex">表头所在行号,-1表示没有列头</param>
        /// <returns>返回的DataTable</returns>
        public static DataTable ImportSheetToDataTable(HSSFSheet sheet, int headerRowIndex)
        {
            var dataTable = new DataTable();
            try
            {
                HSSFRow headerRow;
                int cellCount;
                if (headerRowIndex < 0)
                {
                    headerRow = (HSSFRow)sheet.GetRow(0);
                    cellCount = headerRow.LastCellNum;

                    for (int i = headerRow.FirstCellNum; i <= cellCount; i++)
                    {
                        var column = new DataColumn(i.ToString(CultureInfo.InvariantCulture));
                        dataTable.Columns.Add(column);
                    }
                }
                else
                {
                    headerRow = (HSSFRow)sheet.GetRow(headerRowIndex+1);
                    cellCount = headerRow.LastCellNum;

                    for (int i = headerRow.FirstCellNum; i < cellCount; i++)
                    {
                        if (headerRow.GetCell(i) == null)
                        {
                            if (dataTable.Columns.IndexOf(i.ToString(CultureInfo.InvariantCulture)) > 0)
                            {
                                var column = new DataColumn("重复列名" + i);
                                dataTable.Columns.Add(column);
                            }
                            else
                            {
                                var column = new DataColumn(Convert.ToString(i));
                                dataTable.Columns.Add(column);
                            }

                        }
                        else if (dataTable.Columns.IndexOf(headerRow.GetCell(i).ToString()) > 0)
                        {
                            var column = new DataColumn("重复列名" + i);
                            dataTable.Columns.Add(column);
                        }
                        else
                        {
                            var column = new DataColumn(headerRow.GetCell(i).ToString());
                            dataTable.Columns.Add(column);
                        }
                    }
                }
                for (var i = (headerRowIndex + 2); i <= sheet.LastRowNum; i++)
                {
                    try
                    {
                        HSSFRow row;
                        if (sheet.GetRow(i) == null)
                        {
                            row = sheet.CreateRow(i) as HSSFRow;
                        }
                        else
                        {
                            row = sheet.GetRow(i) as HSSFRow;
                        }

                        var dataRow = dataTable.NewRow();

                        if (row != null)
                            for (int j = row.FirstCellNum; j <= cellCount; j++)
                            {
                                try
                                {
                                    if (row.GetCell(j) == null) continue;
                                    switch (row.GetCell(j).CellType)
                                    {
                                        case CellType.STRING:
                                            var str = row.GetCell(j).StringCellValue;
                                            if (!string.IsNullOrEmpty(str))
                                            {
                                                dataRow[j] = str;
                                            }
                                            else
                                            {
                                                dataRow[j] = null;
                                            }
                                            break;
                                        case CellType.NUMERIC:
                                            if (DateUtil.IsCellDateFormatted(row.GetCell(j)))
                                            {
                                                dataRow[j] = DateTime.FromOADate(row.GetCell(j).NumericCellValue);
                                            }
                                            else
                                            {
                                                dataRow[j] = Convert.ToDouble(row.GetCell(j).NumericCellValue);
                                            }
                                            break;
                                        case CellType.BOOLEAN:
                                            dataRow[j] = Convert.ToString(row.GetCell(j).BooleanCellValue);
                                            break;
                                        case CellType.ERROR:
                                            dataRow[j] = ErrorEval.GetText(row.GetCell(j).ErrorCellValue);
                                            break;
                                        case CellType.FORMULA:
                                            switch (row.GetCell(j).CachedFormulaResultType)
                                            {
                                                case CellType.STRING:
                                                    var strFormula = row.GetCell(j).StringCellValue;
                                                    if (!string.IsNullOrEmpty(strFormula))
                                                    {
                                                        dataRow[j] = strFormula;
                                                    }
                                                    else
                                                    {
                                                        dataRow[j] = null;
                                                    }
                                                    break;
                                                case CellType.NUMERIC:
                                                    dataRow[j] = Convert.ToString(row.GetCell(j).NumericCellValue);
                                                    break;
                                                case CellType.BOOLEAN:
                                                    dataRow[j] = Convert.ToString(row.GetCell(j).BooleanCellValue);
                                                    break;
                                                case CellType.ERROR:
                                                    dataRow[j] = ErrorEval.GetText(row.GetCell(j).ErrorCellValue);
                                                    break;
                                                default:
                                                    dataRow[j] = "";
                                                    break;
                                            }
                                            break;
                                        default:
                                            dataRow[j] = "";
                                            break;
                                    }
                                }
                                catch (Exception ex)
                                {
                                    //这里添加异常异常日志记录
                                    WriteLog.WriteErorrLog(ex, "ExcelHelper类，ImportSheetToDataTable方法异常1");
                                    return null;
                                }
                            }
                        dataTable.Rows.Add(dataRow);
                    }
                    catch (Exception ex)
                    {
                        //这里添加异常异常日志记录
                        WriteLog.WriteErorrLog(ex, "ExcelHelper类，ImportSheetToDataTable方法异常2");
                        return null;
                    }
                }
            }
            catch (Exception ex)
            {
                //这里添加异常异常日志记录
                WriteLog.WriteErorrLog(ex, "ExcelHelper类，ImportSheetToDataTable方法异常3");
                return null;
            }
            return dataTable;
        }
        /// <summary>
        /// 将Excel指定sheet数据导出至DataTable中
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="sheetIndex"></param>
        /// <param name="headerRowIndex"></param>
        /// <returns>返回的DataTable</returns>
        public static DataTable ImportExcelToDataTable(string fileName, int sheetIndex, int headerRowIndex)
        {
            HSSFWorkbook workbook;
            using (var fileStream = new FileStream(fileName, FileMode.Open, FileAccess.Read))
            {
                workbook = new HSSFWorkbook(fileStream);
            }
            HSSFSheet sheet = (HSSFSheet)workbook.GetSheetAt(sheetIndex);
            var dataTable = ImportSheetToDataTable(sheet, headerRowIndex);
            workbook = null;
            sheet = null;
            return dataTable;
        }
        #region 更新excel中的数据
        /// <summary>
        /// 更新Excel表格
        /// </summary>
        /// <param name="outputFile">需更新的excel表格路径</param>
        /// <param name="sheetname">sheet名</param>
        /// <param name="updateData">需更新的数据</param>
        /// <param name="coluid">需更新的列号</param>
        /// <param name="rowid">需更新的开始行号</param>
        public static void UpdateExcel(string outputFile, string sheetname, string[] updateData, int coluid, int rowid)
        {
            FileStream readfile = new FileStream(outputFile, FileMode.Open, FileAccess.Read);

            HSSFWorkbook hssfworkbook = new HSSFWorkbook(readfile);
            ISheet sheet1 = hssfworkbook.GetSheet(sheetname);
            for (int i = 0; i < updateData.Length; i++)
            {
                try
                {
                    if (sheet1.GetRow(i + rowid) == null)
                    {
                        sheet1.CreateRow(i + rowid);
                    }
                    if (sheet1.GetRow(i + rowid).GetCell(coluid) == null)
                    {
                        sheet1.GetRow(i + rowid).CreateCell(coluid);
                    }

                    sheet1.GetRow(i + rowid).GetCell(coluid).SetCellValue(updateData[i]);
                }
                catch (Exception ex)
                {
                    // 这里添加异常异常日志记录
                    WriteLog.WriteErorrLog(ex, "ExcelHelper类，UpdateExcel方法异常001");
                    throw;
                }
            }
            try
            {
                readfile.Close();
                FileStream writefile = new FileStream(outputFile, FileMode.Create, FileAccess.Write);
                hssfworkbook.Write(writefile);
                writefile.Close();
            }
            catch (Exception ex)
            {
                // 这里添加异常异常日志记录
                WriteLog.WriteErorrLog(ex, "ExcelHelper类，UpdateExcel方法异常002");
            }

        }

        /// <summary>
        /// 更新Excel表格
        /// </summary>
        /// <param name="outputFile">需更新的excel表格路径</param>
        /// <param name="sheetname">sheet名</param>
        /// <param name="updateData">需更新的数据</param>
        /// <param name="coluids">需更新的列号</param>
        /// <param name="rowid">需更新的开始行号</param>
        public static void UpdateExcel(string outputFile, string sheetname, string[][] updateData, int[] coluids, int rowid)
        {
            var readfile = new FileStream(outputFile, FileMode.Open, FileAccess.Read);

            var hssfworkbook = new HSSFWorkbook(readfile);
            readfile.Close();
            var sheet1 = hssfworkbook.GetSheet(sheetname);
            for (var j = 0; j < coluids.Length; j++)
            {
                for (var i = 0; i < updateData[j].Length; i++)
                {
                    try
                    {
                        if (sheet1.GetRow(i + rowid) == null)
                        {
                            sheet1.CreateRow(i + rowid);
                        }
                        if (sheet1.GetRow(i + rowid).GetCell(coluids[j]) == null)
                        {
                            sheet1.GetRow(i + rowid).CreateCell(coluids[j]);
                        }
                        sheet1.GetRow(i + rowid).GetCell(coluids[j]).SetCellValue(updateData[j][i]);
                    }
                    catch (Exception ex)
                    {
                        // 这里添加异常异常日志记录
                        WriteLog.WriteErorrLog(ex, "ExcelHelper类，UpdateExcel方法异常01");
                    }
                }
            }
            try
            {
                var writefile = new FileStream(outputFile, FileMode.Create);
                hssfworkbook.Write(writefile);
                writefile.Close();
            }
            catch (Exception ex)
            {
                //这里添加异常异常日志记录
                WriteLog.WriteErorrLog(ex, "ExcelHelper类，UpdateExcel方法异常02");
            }
        }

        /// <summary>
        /// 更新Excel表格
        /// </summary>
        /// <param name="outputFile">需更新的excel表格路径</param>
        /// <param name="sheetname">sheet名</param>
        /// <param name="updateData">需更新的数据</param>
        /// <param name="coluid">需更新的列号</param>
        /// <param name="rowid">需更新的开始行号</param>
        public static void UpdateExcel(string outputFile, string sheetname, double[] updateData, int coluid, int rowid)
        {
            var readfile = new FileStream(outputFile, FileMode.Open, FileAccess.Read);

            var hssfworkbook = new HSSFWorkbook(readfile);
            var sheet1 = hssfworkbook.GetSheet(sheetname);
            for (var i = 0; i < updateData.Length; i++)
            {
                try
                {
                    if (sheet1.GetRow(i + rowid) == null)
                    {
                        sheet1.CreateRow(i + rowid);
                    }
                    if (sheet1.GetRow(i + rowid).GetCell(coluid) == null)
                    {
                        sheet1.GetRow(i + rowid).CreateCell(coluid);
                    }

                    sheet1.GetRow(i + rowid).GetCell(coluid).SetCellValue(updateData[i]);
                }
                catch (Exception ex)
                {
                    //这里添加异常异常日志记录
                    WriteLog.WriteErorrLog(ex, "ExcelHelper类，UpdateExcel方法异常1");
                    throw;
                }
            }
            try
            {
                readfile.Close();
                var writefile = new FileStream(outputFile, FileMode.Create, FileAccess.Write);
                hssfworkbook.Write(writefile);
                writefile.Close();
            }
            catch (Exception ex)
            {
                //这里添加异常异常日志记录
                WriteLog.WriteErorrLog(ex, "ExcelHelper类，UpdateExcel方法异常2");
            }

        }

        /// <summary>
        /// 更新Excel表格
        /// </summary>
        /// <param name="outputFile">需更新的excel表格路径</param>
        /// <param name="sheetname">sheet名</param>
        /// <param name="updateData">需更新的数据</param>
        /// <param name="coluids">需更新的列号</param>
        /// <param name="rowid">需更新的开始行号</param>
        public static void UpdateExcel(string outputFile, string sheetname, double[][] updateData, int[] coluids, int rowid)
        {
            var readfile = new FileStream(outputFile, FileMode.Open, FileAccess.Read);

            var hssfworkbook = new HSSFWorkbook(readfile);
            readfile.Close();
            var sheet1 = hssfworkbook.GetSheet(sheetname);
            for (var j = 0; j < coluids.Length; j++)
            {
                for (var i = 0; i < updateData[j].Length; i++)
                {
                    try
                    {
                        if (sheet1.GetRow(i + rowid) == null)
                        {
                            sheet1.CreateRow(i + rowid);
                        }
                        if (sheet1.GetRow(i + rowid).GetCell(coluids[j]) == null)
                        {
                            sheet1.GetRow(i + rowid).CreateCell(coluids[j]);
                        }
                        sheet1.GetRow(i + rowid).GetCell(coluids[j]).SetCellValue(updateData[j][i]);
                    }
                    catch (Exception ex)
                    {
                        //这里添加异常异常日志记录
                    }
                }
            }
            try
            {
                var writefile = new FileStream(outputFile, FileMode.Create);
                hssfworkbook.Write(writefile);
                writefile.Close();
            }
            catch (Exception ex)
            {
                //这里添加异常异常日志记录
                WriteLog.WriteErorrLog(ex, "ExcelHelper类，UpdateExcel方法异常");
            }
        }

        #endregion
        
    }
}
