namespace PdfReportPOC.Repository.DbModels;

public class GraphDataModel
{
    public int GraphDataId { get; set; }
    public string GraphName { get; set; }
    public string DataName { get; set; }
    public decimal DataValue { get; set; }
}