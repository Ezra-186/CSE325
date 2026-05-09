using Newtonsoft.Json;
using System.Text;

string rootFolder = Path.Combine(Directory.GetCurrentDirectory(), "stores");
string salesTotalDir = Path.Combine(rootFolder, "salesTotalDir");

Directory.CreateDirectory(rootFolder);
Directory.CreateDirectory(salesTotalDir);

CreateSampleSalesFiles(rootFolder);

IEnumerable<string> salesFiles = FindFiles(rootFolder);

double totalSales = CalculateSalesTotal(salesFiles);
string reportPath = Path.Combine(salesTotalDir, "salesSummary.txt");

WriteSalesSummaryReport(salesFiles, totalSales, reportPath);

Console.WriteLine($"Sales summary created at: {reportPath}");

static void CreateSampleSalesFiles(string rootFolder)
{
    string storeOne = Path.Combine(rootFolder, "201");
    string storeTwo = Path.Combine(rootFolder, "202");
    string storeThree = Path.Combine(rootFolder, "203");

    Directory.CreateDirectory(storeOne);
    Directory.CreateDirectory(storeTwo);
    Directory.CreateDirectory(storeThree);

    File.WriteAllText(Path.Combine(storeOne, "sales.json"), "{ \"total\": 22385.32 }");
    File.WriteAllText(Path.Combine(storeTwo, "sales.json"), "{ \"total\": 18442.19 }");
    File.WriteAllText(Path.Combine(storeThree, "sales.json"), "{ \"total\": 31750.45 }");
}

static IEnumerable<string> FindFiles(string folderName)
{
    List<string> salesFiles = new List<string>();
    IEnumerable<string> foundFiles = Directory.EnumerateFiles(folderName, "*", SearchOption.AllDirectories);

    foreach (string file in foundFiles)
    {
        string extension = Path.GetExtension(file);

        if (extension == ".json")
        {
            salesFiles.Add(file);
        }
    }

    return salesFiles;
}

static double CalculateSalesTotal(IEnumerable<string> salesFiles)
{
    double salesTotal = 0;

    foreach (string file in salesFiles)
    {
        string salesJson = File.ReadAllText(file);
        SalesData? data = JsonConvert.DeserializeObject<SalesData>(salesJson);

        salesTotal += data?.Total ?? 0;
    }

    return salesTotal;
}

static void WriteSalesSummaryReport(IEnumerable<string> salesFiles, double totalSales, string reportPath)
{
    StringBuilder report = new StringBuilder();

    report.AppendLine("Sales Summary");
    report.AppendLine("----------------------------");
    report.AppendLine($"Total Sales: {totalSales:C}");
    report.AppendLine();
    report.AppendLine("Details:");

    foreach (string file in salesFiles)
    {
        string salesJson = File.ReadAllText(file);
        SalesData? data = JsonConvert.DeserializeObject<SalesData>(salesJson);
        string fileName = Path.GetRelativePath(Directory.GetCurrentDirectory(), file);

        report.AppendLine($"{fileName}: {data?.Total ?? 0:C}");
    }

    File.WriteAllText(reportPath, report.ToString());
}

record SalesData(double Total);





