namespace bbb.Models;
public class UserModel
{
    public UserModel()
    {
    }
    public UserModel(string name, int id=1)
    {
        username = name;
        userID = id;
    }
    public string username { get; set; }
    public int userID { get; set; }
}