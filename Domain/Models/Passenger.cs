namespace Domain.Models;

public class Passenger
{
    private string _id;
    private string _name;

    public Passenger(string id, string name)
    {
        _id = id;
        _name = name;
    }

    public string id
    {
        get => _id;
        set => _id = value ?? throw new ArgumentNullException(nameof(value));
    }

    public string name
    {
        get => _name;
        set => _name = value ?? throw new ArgumentNullException(nameof(value));
    }
}