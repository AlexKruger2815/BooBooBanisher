namespace bbb.Models;

public class SessionModel
{
    public int sessionID { get; set; }
    public int userID { get; set; }
    public int messageID { get; set; }
    public DateTime createdAt { get; set; }

    public override string ToString(){
        return $"{userID} {messageID} {createdAt}";
    }
}