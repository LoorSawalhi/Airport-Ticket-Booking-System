namespace Domain.Models;

public class Airport
{
    private string _id;
    private string _name;
    private string _country;

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

    public string country
    {
        get => _country;
        set => _country = value ?? throw new ArgumentNullException(nameof(value));
    }
}