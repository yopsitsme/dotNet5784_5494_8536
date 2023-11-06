

namespace DO;

public record Engineer
{
 public   int Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public EngineerExperience‏ Level { get; set; }
    public double Cost { get; set; }
    public Engineer() { }
    public Engineer(string Name,string Email, EngineerExperience‏ Level,double Cost)
    {
        this.Name = Name;
        this.Email = Email;
        this.Level = Level;
        this.Cost = Cost;
    }

}
