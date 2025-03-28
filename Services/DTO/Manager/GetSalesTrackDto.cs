namespace EBISX_POS.API.Services.DTO.Manager
{
    public class GetSalesTrackDto
    {
        public decimal TotalSales { get; set; }
        public decimal CashInDrawer { get; set; }
        public string ReportDate { get; set; } = string.Empty;
        public string ReportTime { get; set; } = string.Empty;
    }
}
