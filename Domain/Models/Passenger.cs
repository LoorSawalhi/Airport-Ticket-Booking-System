namespace Domain.Models;

public class Passenger
{
    private string _id;
    private string _name;
    private int _age;

    public Passenger(string id, string name, int age)
    {
        _id = id;
        _name = name;
        _age = age;
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

    public int age
    {
        get => _age;
        set => _age = value;
    }
}