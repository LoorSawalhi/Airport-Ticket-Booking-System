namespace Domain.Models;

public abstract record Airport
{
    private string _id;
    private string _name;
    private string _country;

    public string Id
    {
        get => _id;
        set => _id = value ?? throw new ArgumentNullException(nameof(value));
    }

    public string Name
    {
        get => _name;
        set => _name = value ?? throw new ArgumentNullException(nameof(value));
    }

    public string Country
    {
        get => _country;
        set => _country = value ?? throw new ArgumentNullException(nameof(value));
    }

    public override string? ToString()
    {
        return $"Airport id = {Id}, Country Name = {Country}, Airport Name = {Name} ";
    }
}