namespace Papirus.WebApi.Application.Dtos.DataExtractor;

public enum FormatType
{
    /// <summary>
    /// PDF file
    /// </summary>
    Pdf = 1,

    /// <summary>
    /// Excel File
    /// </summary>
    Excel = 2,

    /// <summary>
    /// Image file (OCR)
    /// </summary>
    Image = 3,

    /// <summary>
    /// Text
    /// </summary>
    Text = 4
}