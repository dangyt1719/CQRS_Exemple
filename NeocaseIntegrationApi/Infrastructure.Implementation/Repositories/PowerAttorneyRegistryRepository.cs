using Dapper;
using Entities;
using Infrastructure.Implementation.Repositories.SqlQueries;
using Infrastructure.Interfaces.RepositoryInterfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using NPOI.SS.UserModel;
using NPOI.SS.Util;
using NPOI.XSSF.UserModel;
using System.Data.SqlClient;
using Utils;

namespace Infrastructure.Implementation.Repositories
{
    public class PowerAttorneyRegistryRepository : IPowerAttorneyRegistryRepository
    {
        private readonly ILogger<PowerAttorneyRegistryRepository> _logger;
        private readonly string _connectionString;

        public PowerAttorneyRegistryRepository(ILogger<PowerAttorneyRegistryRepository> logger, IConfiguration configuration)
        {
            _logger = logger;
            _connectionString = configuration.GetConnectionString("OcoIntegrationsConnection");
        }

        public async Task<IEnumerable<PowerAttorneyRegistry>> GetPowerAttorneyRegistriesAsync()
        {
            try
            {
                using var connection = new SqlConnection(_connectionString);
                connection.Open();

                var currentYear = DateTime.Now.Year;
                var powerAttorney = await connection.QueryAsync<PowerAttorneyRegistry>(PowerAttorneyQueries.AllPowerAttorneyRegistry, new { Year = currentYear });
                return powerAttorney;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }
        }

        public byte[] GetPowerAttorneyRegistriesReport(IEnumerable<PowerAttorneyRegistry> registries)
        {
            try
            {
                using var ms = new MemoryStream();
                //using var fs = new FileStream(@"c:\1\4.xlsx", FileMode.CreateNew, FileAccess.Write);
                var workbook = new XSSFWorkbook();
                CreatePowerAttorneySheet(workbook, "Реестр доверенностей", registries);
                //workbook.Write(fs);
                workbook.Write(ms);

                //return new byte[2];
                return ms.ToArray();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }
        }

        private static ISheet CreatePowerAttorneySheet(XSSFWorkbook workbook, string sheetName, IEnumerable<PowerAttorneyRegistry> registries)
        {
            var sheet = workbook.CreateSheet(sheetName);

            var font = workbook.CreateFont();
            font.IsBold = true;

            var fontBold = workbook.CreateCellStyle();
            fontBold.SetFont(font);

            var cellStyleBorderThin = workbook.CreateCellStyle();
            cellStyleBorderThin.BorderBottom = BorderStyle.Thin;
            cellStyleBorderThin.BorderLeft = BorderStyle.Thin;
            cellStyleBorderThin.BorderRight = BorderStyle.Thin;
            cellStyleBorderThin.BorderTop = BorderStyle.Thin;
            cellStyleBorderThin.VerticalAlignment = VerticalAlignment.Center;

            IDataFormat dataFormat = workbook.CreateDataFormat();

            var cellStyleBorderThinDateCell = workbook.CreateCellStyle();
            cellStyleBorderThinDateCell.BorderBottom = BorderStyle.Thin;
            cellStyleBorderThinDateCell.BorderLeft = BorderStyle.Thin;
            cellStyleBorderThinDateCell.BorderRight = BorderStyle.Thin;
            cellStyleBorderThinDateCell.BorderTop = BorderStyle.Thin;
            cellStyleBorderThinDateCell.VerticalAlignment = VerticalAlignment.Center;
            cellStyleBorderThinDateCell.DataFormat = dataFormat.GetFormat("dd.mm.yyyy");

            var cellHorizontalAlignment = workbook.CreateCellStyle();
            cellHorizontalAlignment.Alignment = HorizontalAlignment.Center;

            var cellStyleWrapTextCell = workbook.CreateCellStyle();
            cellStyleWrapTextCell.WrapText = true;
            cellStyleWrapTextCell.ShrinkToFit = true;

            //Шапка таблицы
            var header = sheet.CreateRow(0);
            header.CreateCell(0).SetCellValue("N№");
            header.CreateCell(1).SetCellValue("Дата выдачи");
            header.CreateCell(2).SetCellValue("Номер доверенности");
            header.CreateCell(3).SetCellValue("Вид доверенности");
            header.CreateCell(4).SetCellValue("Общество-доверитель");
            header.CreateCell(5).SetCellValue("От чьего имени предоставлены полномочия");
            header.CreateCell(6).SetCellValue("Перечень полномочий");
            header.CreateCell(7).SetCellValue("Поверенный (лицо получиышее доверенность)");
            header.CreateCell(8).SetCellValue("Инициатор выдачи доверенности (отправляет скан-копию подписанной доверенности)");
            header.CreateCell(9).SetCellValue("Дата окончания действия доверенности");
            header.CreateCell(10).SetCellValue("Дата отзыва доверенности");
            header.CreateCell(11).SetCellValue("Причина отзыва доверенности");

            Extentions.SetCellStyle(header, header.Cells.Count, cellStyleBorderThin, cellHorizontalAlignment, cellStyleWrapTextCell, fontBold);

            sheet.SetAutoFilter(new CellRangeAddress(0, 0, 0, 11));

            int j = 1;
            foreach (var item in registries)
            {
                var row = sheet.CreateRow(j);
                row.CreateCell(0).SetCellValue(item.PowerAttorneyIndex);

                row.CreateCell(1).SetCellValue(item.DateIssue?.ToShortDateString());
                row.Cells[1].CellStyle = cellStyleBorderThinDateCell;
                row.CreateCell(2).SetCellValue(item.PowerAttorneyNumber);
                row.CreateCell(3).SetCellValue(item.PowerAttorneyType);
                row.CreateCell(4).SetCellValue(item.Principal);
                row.CreateCell(5).SetCellValue(item.WhoGranted);
                row.CreateCell(6).SetCellValue(item.Powers);
                row.CreateCell(7).SetCellValue(item.Attorney);
                row.CreateCell(8).SetCellValue(item.Initiator);
                row.CreateCell(9).SetCellValue(item.Term);
                row.CreateCell(10).SetCellValue(string.Empty);
                row.CreateCell(11).SetCellValue(string.Empty);

                Extentions.SetCellStyle(row, row.Cells.Count, cellStyleBorderThin);

                j++;
            }

            var lastColumNum = sheet.GetRow(0).LastCellNum;

            for (int i = 0; i < lastColumNum; i++)
            {
                sheet.AutoSizeColumn(i);
            }

            sheet.CreateFreezePane(0, 1);

            return sheet;
        }
    }
}