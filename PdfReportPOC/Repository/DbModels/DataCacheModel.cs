namespace PdfReportPOC.Repository.DbModels;

public class DataCacheModel
{
    public int DataCacheId { get; set; }
    public string DataSectionName { get; set; }
    public string CacheData { get; set; }
    public DateTime ExpirationDateUTC { get; set; }
}