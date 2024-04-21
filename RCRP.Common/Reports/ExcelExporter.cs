//using NPOI.SS.UserModel;
//using NPOI.SS.Util;
//using NPOI.XSSF.UserModel;
//using Ohmio.Modelos;
//using Ohmio.Servicios.Helpers;
//using System;
//using System.Collections.Generic;
//using System.IO;
//using System.Linq;

//namespace RCRP.Common;

//public static class ExcelExporter<T>
//{
//    private static BorderStyle dataBorderStyle = BorderStyle.Dotted;
//    private static (byte Red, byte Green, byte Blue) darkBackground = (217, 217, 217);
//    private static (byte Red, byte Green, byte Blue) clearBackground = (255, 255, 255);
//    private static IFont dataFont;
//    private static IFont dataBooleanFont;
//    private static ICellStyle darkDataStyle;
//    private static ICellStyle darkBooleanDataStyle;
//    private static ICellStyle clearDataStyle;
//    private static ICellStyle clearBooleanDataStyle;

//    public static MemoryStream Export(IEnumerable<T> data,
//        string newFile,
//        string title,
//        string companyName,
//        string detail,
//        byte[] logo)
//    {
//        const int MaxCellWidth = 15000;
//        const int MinCellWidth = 3000;

//        var sObject = newFile.Substring(1, newFile.IndexOf("."));
//        var fs = new MemoryStream();

//        IWorkbook workbook = new XSSFWorkbook(); ;
//        InitializeFonts(workbook);
//        InitializeStyles(workbook);

//        var sheet = workbook.CreateSheet(sObject);

//        (byte Red, byte Green, byte Blue) headerFooterBackgroundColor = (197, 217, 241);

//        CreateHeader(workbook,
//            sheet,
//            title,
//            subtitle: detail,
//            companyName,
//            backgroundColor: headerFooterBackgroundColor);

//        var dataTitleRow = sheet.CreateRow(7);
//        var dataTitleStyle = CreateStyle(workbook,
//            font: ("Calibri", 10, true, IndexedColors.White.Index),
//            backgroundColor: (54, 96, 146));

//        var logoData = logo == null
//            ? File.ReadAllBytes(@"Resources/DSILogo.png")
//            : logo;

//        AddLogo(workbook,
//            sheet,
//            logoData,
//            position: (0, 0),
//            scale: (1, 2.95));

//        var columnIndex = 0;
//        var properties = (typeof(T)).GetProperties().ToList();
//        var columnDefinition = new Dictionary<int, (Type DataType, int MaxLength)>();

//        foreach (var prop in properties)
//        {
//            var cellRowTitle = dataTitleRow.CreateCell(columnIndex);
//            cellRowTitle.CellStyle = dataTitleStyle;
//            cellRowTitle.SetCellValue(prop.Name);
//            columnDefinition.Add(columnIndex, (null, 0));
//            columnIndex++;
//        }

//        int row = 8;
//        var backgroundAlternator = true;

//        foreach (T item in data)
//        {
//            dataTitleRow = sheet.CreateRow(row++);
//            backgroundAlternator = !backgroundAlternator;

//            columnIndex = 0;
//            foreach (var prop in properties)
//            {
//                var dataCell = dataTitleRow.CreateCell(columnIndex);
//                var cellValue = prop.GetValue(item);
//                var cellType = SetCellValue(workbook,
//                    dataCell,
//                    cellValue,
//                    backgroundAlternator);

//                if (cellType != null && columnDefinition.ContainsKey(columnIndex))
//                {
//                    (Type DataType, int MaxLength) currentDefinition = columnDefinition[columnIndex];

//                    var strCellCalue = cellValue.ToString();
//                    var length = strCellCalue.Split("\n").Max(s => s.Length);

//                    currentDefinition.DataType = cellType;
//                    currentDefinition.MaxLength = length > currentDefinition.MaxLength
//                        ? length
//                        : currentDefinition.MaxLength;
//                    columnDefinition[columnIndex] = currentDefinition;
//                }

//                columnIndex++;
//            }
//        }
//        var dataFooterRow = sheet.CreateRow(row++);
//        for (var column = 0; column < columnIndex; column++)
//        {
//            var dataFooterCell = dataFooterRow.CreateCell(column);
//            dataFooterCell.CellStyle = dataTitleStyle;
//            if (column == 0)
//            {
//                dataFooterCell.SetCellValue($"{data.Count()} Registros");
//            }
//            else
//            {
//                if (columnDefinition.ContainsKey(column))
//                {
//                    var width = (int)(columnDefinition[column].MaxLength * 1.45f) * 256;
//                    width = width < MinCellWidth ? MinCellWidth : width;
//                    width = width > MaxCellWidth ? MaxCellWidth : width;
//                    sheet.SetColumnWidth(column, width);
//                }
//            }
//        }

//        var footerStyle = workbook.CreateStyle()
//            .AddFont(workbook.CreateFont("Calibri", 8, false, IndexedColors.Automatic.Index))
//            .AddBackgroundColor(headerFooterBackgroundColor);

//        CreateFooterRow(++row,
//            sheet,
//            footerStyle,
//            @"Ohmio ERP -  https://ohmioerp.us/home");

//        CreateFooterRow(++row,
//            sheet,
//            footerStyle,
//            $"{DateTime.UtcNow.Year} - Ohmio ERP - Todos los derechos reservados - DSI Sistemas LLC");

//        //RegionUtil.SetBorderBottom((int)NPOI.SS.UserModel.BorderStyle.Medium, regionTable, excelSheet, workbook);
//        //RegionUtil.SetBorderTop((int)NPOI.SS.UserModel.BorderStyle.Medium, regionTable, excelSheet, workbook);
//        //RegionUtil.SetBorderLeft((int)NPOI.SS.UserModel.BorderStyle.Medium, regionTable, excelSheet, workbook);
//        //RegionUtil.SetBorderRight((int)NPOI.SS.UserModel.BorderStyle.Medium, regionTable, excelSheet, workbook);

//        var ohmioLogo = File.ReadAllBytes(@"Resources/LogoOhmio.png");

//        AddLogo(workbook, sheet,
//            ohmioLogo,
//            position: (row - 1, 0),
//            scale: (0.923, 1.75));

//        workbook.Write(fs);

//        return fs;
//    }

//    private static void InitializeFonts(IWorkbook workbook)
//    {
//        dataFont = workbook.CreateFont("Calibri",
//            10,
//            false,
//            IndexedColors.Automatic.Index);
//        dataBooleanFont = workbook.CreateFont("Wingdings",
//            10,
//            false,
//            IndexedColors.Automatic.Index);
//    }

//    private static void InitializeStyles(IWorkbook workbook)
//    {
//        darkDataStyle = workbook.CreateStyle()
//            .AddFont(dataFont)
//            .AddBackgroundColor(darkBackground)
//            .AddBorders(dataBorderStyle);
//        darkBooleanDataStyle = workbook.CreateStyle()
//            .AddFont(dataBooleanFont)
//            .AddBackgroundColor(darkBackground)
//            .AddBorders(dataBorderStyle);
//        clearDataStyle = workbook.CreateStyle()
//            .AddFont(dataFont)
//            .AddBackgroundColor(clearBackground)
//            .AddBorders(dataBorderStyle);
//        clearBooleanDataStyle = workbook.CreateStyle()
//            .AddFont(dataBooleanFont)
//            .AddBackgroundColor(clearBackground)
//            .AddBorders(dataBorderStyle);
//    }

//    private static ICellStyle GetDataCellStyle(bool isDark, bool isBooleanCell)
//    {
//        return isDark switch
//        {
//            true => isBooleanCell ? darkBooleanDataStyle : darkDataStyle,
//            false => isBooleanCell ? clearBooleanDataStyle : clearDataStyle
//        };
//    }

//    private static ICellStyle CreateStyle(
//        IWorkbook workbook,
//        (string Name, int Size, bool IsBold, short Color) font,
//        (byte Red, byte Green, byte Blue) backgroundColor)
//    {
//        ICellStyle style = (XSSFCellStyle)workbook.CreateCellStyle();

//        var styleFont = workbook.CreateFont(font.Name, font.Size, font.IsBold, font.Color);
//        style.SetFont(styleFont);

//        style.AddBackgroundColor(backgroundColor);

//        return style;
//    }

//    private static void CreateFooterRow(int index,
//        ISheet sheet,
//        ICellStyle style,
//        string value)
//    {
//        sheet.AddMergedRegion(new CellRangeAddress(index, index, 1, 9));

//        var row = sheet.CreateRow(index);
//        row.CreateCell(0).CellStyle = style;

//        var footerCell = row.CreateCell(1);
//        footerCell.CellStyle = style;
//        footerCell.CellStyle.Alignment = HorizontalAlignment.Center;
//        footerCell.SetCellValue(value);
//    }

//    private static NPOI.SS.UserModel.ICell CreateHeaderRow(
//        IWorkbook workbook,
//        ISheet sheet,
//        int index,
//        (string Name, int Size, bool IsBold, short Color) font,
//        (byte Red, byte Green, byte Blue) backgroundColor,
//        string value = "")
//    {
//        var cellStyle = CreateStyle(workbook,
//            font,
//            backgroundColor);

//        var row = sheet.CreateRow(index);

//        row.CreateCell(0).CellStyle = cellStyle; //Logo Cell
//        var cell = row.CreateCell(1);
//        cell.CellStyle = cellStyle;
//        if (!string.IsNullOrEmpty(value))
//        {
//            cell.SetCellValue(value);
//            cell.CellStyle.Alignment = HorizontalAlignment.Center;
//        }

//        sheet.AddMergedRegion(new CellRangeAddress(index, index, 1, 9));

//        return cell;
//    }
//    private static void CreateHeader(IWorkbook workbook,
//        ISheet sheet,
//        string title,
//        string subtitle,
//        string companyName,
//        (byte Red, byte Green, byte Blue) backgroundColor)
//    {
//        sheet.SetColumnWidth(0, 4535); //Logo column width

//        (string Name, int Size, bool IsBold, short Color) font = ("Calibri",
//                11,
//                false,
//                IndexedColors.Automatic.Index);

//        var dateCell = CreateHeaderRow(workbook, sheet, index: 0, font, backgroundColor);
//        dateCell.SetCellValue(MapperHelper.DateLong(DateTime.UtcNow));
//        dateCell.CellStyle.Alignment = HorizontalAlignment.Right;

//        var companyCell = CreateHeaderRow(workbook, sheet, index: 1, font, backgroundColor);
//        companyCell.SetCellValue(companyName);
//        companyCell.CellStyle.Alignment = HorizontalAlignment.Right;

//        CreateHeaderRow(workbook, sheet, index: 3, font, backgroundColor);

//        font.Name = "Tahoma";
//        font.Size = 14;
//        font.IsBold = true;
//        CreateHeaderRow(workbook, sheet, index: 2, font, backgroundColor, title);

//        var subtitleCell = sheet.CreateRow(5).CreateCell(0);
//        var subtitleStyle = (XSSFCellStyle)workbook.CreateCellStyle();
//        var subtitleFont = workbook.CreateFont(
//                "Tahoma",
//                14,
//                isBold: true,
//                IndexedColors.Automatic.Index);
//        subtitleStyle.SetFont(subtitleFont);
//        subtitleCell.CellStyle = subtitleStyle;
//        subtitleCell.SetCellValue(subtitle);
//    }

//    private static Type SetCellValue(IWorkbook workbook,
//        NPOI.SS.UserModel.ICell cell,
//        object value,
//        bool isDark)
//    {
//        var isBool = value != null && value.GetType().Equals(typeof(bool));
//        cell.CellStyle = GetDataCellStyle(isDark, isBool);

//        if (value == null) return null;

//        var valueType = value.GetType();

//        if (valueType.Equals(typeof(decimal)))
//        {
//            cell.SetCellValue(decimal.ToDouble((decimal)value));
//        }
//        else if (valueType.Equals(typeof(Int32)))
//        {
//            cell.SetCellValue((int)value);
//        }
//        else if (valueType.Equals(typeof(Int16)))
//        {
//            cell.SetCellValue((short)value);
//        }
//        else if (isBool)
//        {
//            cell.SetCellValue((bool)value ? @"ü" : @"û");
//        }
//        else
//        {
//            cell.SetCellValue(value?.ToString());
//        }

//        return valueType;
//    }

//    private static void AddLogo(
//        IWorkbook workbook,
//        ISheet sheet,
//        byte[] logoData,
//        (int Row, int Column) position,
//        (double X, double Y) scale)
//    {
//        int pictureIndex = workbook.AddPicture(logoData,
//            NPOI.SS.UserModel.PictureType.PNG);

//        var helper = workbook.GetCreationHelper();
//        var drawing = sheet.CreateDrawingPatriarch();

//        const int Margin = 5;
//        var anchor = helper.CreateClientAnchor();
//        anchor.Dx1 = Margin * XSSFShape.EMU_PER_PIXEL;
//        anchor.Dy1 = Margin * XSSFShape.EMU_PER_PIXEL;
//        anchor.Row1 = position.Row;
//        anchor.Col1 = position.Column;

//        var picture = drawing.CreatePicture(anchor, pictureIndex);
//        picture.Resize(scale.X, scale.Y);
//    }
//}

