
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

public class User
{ 

    public User(string name)
    {
        Name = name; 
    }

    public User()
    {
        
    }

    public User(long id, string name, string password)
    {
        this.ID = id;
        this.Name = name;
        this.Password = password;
    }

    public long ID { get; set; }
    public string? Name { get; set; }
    public string? Email { get; set; }
    public string? Password { get; set; }       
}