

namespace DO;

public record Engineer
{
    int? Id { get; set; } = null;
    string Name { get; set; }
    string Email { get; set; }
    int Level { get; set; }
    double Cost { get; set; }

    public Engineer(string Name,string Email,int Level,double Cost)
    {
        this.Name = Name;
        this.Email = Email;
        this.Level = Level;
        this.Cost = Cost;
    }

}
