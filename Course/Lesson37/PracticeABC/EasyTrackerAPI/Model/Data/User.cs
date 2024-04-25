
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

public class User
{ 

    public User(string name)
    {
        UserName = name; 
    }

    public User()
    {
        
    }

    public string ID { get; set; }
    //public bool IsVerified = false;
    public string? UserName { get; set; }
    public string? Email { get; set; }
    public string? PasswordHash { get; set; }  
    public string? Role { get; set; }
}