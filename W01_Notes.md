# W01 Assignment Notes

## Part 1: Web API Evidence

For this part of the assignment, I created the ContosoPizza Web API project. I added the Pizza model, the PizzaService file for the in-memory pizza list, and the PizzaController with the GET, POST, PUT, and DELETE actions.

The original module started with two pizzas. I added one more pizza record to the starting list:

new Pizza { Id = 3, Name = "Pepperoni", IsGlutenFree = false }

This made the starting pizza list show three records instead of only two.

### GET Request

Request:

curl -i http://localhost:5291/pizza

Response:

HTTP/1.1 200 OK
Content-Type: application/json; charset=utf-8

[{"id":1,"name":"Classic Italian","isGlutenFree":false},{"id":2,"name":"Veggie","isGlutenFree":true},{"id":3,"name":"Pepperoni","isGlutenFree":false}]

Status Code: 200 OK

This shows that the API returned the full pizza list, including the extra Pepperoni record I added.

### POST Request

Request:

curl -i -X POST http://localhost:5291/pizza -H "Content-Type: application/json" -d '{"name":"Hawaii","isGlutenFree":false}'

Response:

HTTP/1.1 201 Created
Content-Type: application/json; charset=utf-8
Location: http://localhost:5291/Pizza/4

{"id":4,"name":"Hawaii","isGlutenFree":false}

Status Code: 201 Created

This shows that the API created a new pizza and returned the new pizza with an id of 4.

### PUT Request

Request:

curl -i -X PUT http://localhost:5291/pizza/4 -H "Content-Type: application/json" -d '{"id":4,"name":"Hawaiian","isGlutenFree":false}'

Response:

HTTP/1.1 204 No Content

Status Code: 204 No Content

I also checked the pizza after updating it.

Verification response:

HTTP/1.1 200 OK
Content-Type: application/json; charset=utf-8

{"id":4,"name":"Hawaiian","isGlutenFree":false}

This shows that the PUT request updated the pizza name from Hawaii to Hawaiian.

### DELETE Request

Request:

curl -i -X DELETE http://localhost:5291/pizza/4

Response:

HTTP/1.1 204 No Content

Status Code: 204 No Content

I also checked the same pizza again after deleting it.

Verification response:

HTTP/1.1 404 Not Found
Content-Type: application/problem+json; charset=utf-8

{"title":"Not Found","status":404}

This shows that the DELETE request removed the pizza, because the API could no longer find pizza id 4.

## Part 2: Sales Summary Function

For the files and directories module, I added a function that creates a sales summary report file. The report shows the total sales amount and also lists each sales file with its own total. I used StringBuilder to build the report text before writing it to the file.

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

The generated report output was:

Sales Summary
----------------------------
Total Sales: $72,577.96

Details:
stores/203/sales.json: $31,750.45
stores/202/sales.json: $18,442.19
stores/201/sales.json: $22,385.32

## Build Confirmation

After finishing the projects, I ran the full build from the main repository folder.

Command:

dotnet build

Result:

Build succeeded.
0 Warning(s)
0 Error(s)
