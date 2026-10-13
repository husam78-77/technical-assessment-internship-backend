namespace TechnicalAssesmentBackendDeveloper;

class Booking
{
    private string guestname;
    private string roomnumber;
    private DateTime checkindate;
    private DateTime checkoutdate;
    private int totaldays;
    private double rateperday;
    private double discount;
    private double totalamount;

    public async Task BookRoom(string name, string room, DateTime checkin, DateTime checkout, double rate, double discountRate)
    {
         
        if (string.IsNullOrWhiteSpace(name)) 
        {
            Console.WriteLine("invalid name");
            return;
        }
        if (room == null || (!int.TryParse(room, out int result) ) || result <= 0  ) 
        {
            Console.WriteLine("invalid room Number");
            return;
        }
        if (checkout <= checkin)
        {
            Console.WriteLine("invalid date");
            return;
        }

        if (rate < 0)
        {
            Console.WriteLine("invalid rate");
            return;
        }
        if (discountRate < 0 || discountRate > 100)
        {
            Console.WriteLine("invalid Discount");
            return;
        }
        guestname = name;
        roomnumber = room;
        checkindate = checkin;
        checkoutdate = checkout;
        rateperday = rate;
        discount = discountRate;

        
            totaldays = (checkout - checkin).Days;
            totalamount = totaldays * rateperday;
            totalamount = totalamount - (totalamount * discount / 100);
            await LogBookingDetailsAsync();

            Console.WriteLine("Room Booked for " + guestname);
            Console.WriteLine("Room No: " + roomnumber);
            Console.WriteLine("Check-In: " + checkindate.ToString());
            Console.WriteLine("Check-Out: " + checkoutdate.ToString());
            Console.WriteLine("Total Days: " + totaldays);
            Console.WriteLine("Amount: " + totalamount);


    }

    public async Task LogBookingDetailsAsync()
    {
        // Simulate writing to a log file or remote system
        await Task.Delay(1000);
        Console.WriteLine("Booking log saved.");
    }

    public void Cancel()
    {
        guestname = null;
        roomnumber = null;
        checkindate = DateTime.MinValue;
        checkoutdate = DateTime.MinValue;
        totaldays = 0;
        rateperday = 0;
        discount = 0;
        totalamount = 0;

        Console.WriteLine("Booking cancelled");
        
    }
}

public class AppHost
{
    static async Task Main(string[] args)
    {
        Booking b = new Booking();

        
        await b.BookRoom("Bob", "102", DateTime.Now, DateTime.Now.AddDays(-1), 150.5, 10);
        Console.WriteLine("----");

        DateTime sameDate = DateTime.Now;
        await b.BookRoom("Henry", "108", sameDate, sameDate, 150, 10);
        Console.WriteLine("----");

        await b.BookRoom("David", "104", DateTime.Now, DateTime.Now.AddDays(2), 150, -5);
        Console.WriteLine("----");

        await b.BookRoom("Emma", "105", DateTime.Now, DateTime.Now.AddDays(2), 150, 110);
        Console.WriteLine("----");

        await b.BookRoom("Alice", "101", DateTime.Now, DateTime.Now.AddDays(3), 150.5, 10);
        Console.WriteLine("----");
    }
}