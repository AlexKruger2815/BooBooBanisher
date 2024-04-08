namespace bbb.Models;
public class MessageModel
{
    public int messageID { get; }
    public int categoryID { get; set; }
    public string content { get; set; }

    public override string ToString()
{
    return $"{messageID}, {categoryID}, {content}";
}
}