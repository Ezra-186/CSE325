Console.WriteLine("Hello, World!");

DateTime today = DateTime.Now;
Console.WriteLine($"The current time is {today}");

DateTime christmas = new DateTime(today.Year, 12, 25);

if (today > christmas)
{
    christmas = new DateTime(today.Year + 1, 12, 25);
}

TimeSpan daysUntilChristmas = christmas - today;
Console.WriteLine($"There are {daysUntilChristmas.Days} days until Christmas.");