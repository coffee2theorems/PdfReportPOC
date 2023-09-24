using WkHtmlSmartConvert;

namespace PdfReportPOC.Services;

public interface IPdfService
{
    Task<byte[]> GeneratePdfAsync(string htmlContent);
}
public class PdfService : IPdfService
{
    private readonly IPdfConvert _pdfConvert;
    public PdfService(IPdfConvert pdfConvert)
    {
        _pdfConvert = pdfConvert;
    }
    public async Task<byte[]> GeneratePdfAsync(string htmlContent)
    {
        var pdf = await _pdfConvert.ConvertAsync(htmlContent);
        return pdf;
    }
}