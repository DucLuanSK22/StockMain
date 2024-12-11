namespace Stock.BE.Core.Model;
public class AddTransactionResult
{
    public bool Success { get; set; } = true;

    public string Message { get; set; } = string.Empty;
}
