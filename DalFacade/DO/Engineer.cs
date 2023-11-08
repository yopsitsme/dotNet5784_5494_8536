

namespace DO;

// decleration of the entity engineer
public record Engineer
{
 public   int Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public EngineerExperience‏ Level { get; set; }
    public double Cost { get; set; }

    //defult constructor
    public Engineer() { }

    //prametrise constructor 
    public Engineer(string Name,string Email, EngineerExperience‏ Level,double Cost ,int Id)
    {
        this.Id = Id;   
        this.Name = Name;
        this.Email = Email;
        this.Level = Level;
        this.Cost = Cost;
    }

}
