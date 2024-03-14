namespace Domain.Models;

public class FlightClass
{
    private string _id;
    private string _name;

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

    public float price { get; set; }

    public int maxCapacity { get; set; }

    private int availableSeats { get; set; }

    public bool IsSeatAvailable()
    {
        return availableSeats > 0;
    }

    public void ReserveSeat()
    {
        if (availableSeats > 0)
            availableSeats -= 1;
        else
            Console.WriteLine("/////////");
    }
}