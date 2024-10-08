# Fetch Rewards Backend Exercise
My submission for the Fetch backend internship coding exercise. Written in C# and uses ASP.NET WebAPIs.

## Build and Run
1. Install [.NET SDK 8.0](https://dotnet.microsoft.com/en-us/download/dotnet/8.0).
2. Clone the repository.
3. Run `dotnet run`.
4. The app will now be running at localhost:8000. The app will also run Swagger, which will be available at http://localhost:8000/swagger.

## Endpoints
### /add
- Method: POST
- Request body:
```typescript
{
  payer: string;
  points: int;
  timestamp: Date;
}
```
- Description: Adds a new point transaction to the list. If the transaction has negative points, internally subtracts the points from the previous transactions in chronological order, and sets `AvailablePoints` of the new transaction to 0.

### /spend
- Method: POST
- Request body:
```typescript
{
    points: int;
}
```
- Response body:
```typescript
[
    {
        payer: string;
        points: int;
    }
]
```
- Description: Subtracts `points` from the previously registered transactions in chronological order. Returns a list of payers and the corresponding number of points subtracted.

### /balance
- Method: GET
- Request body: none
- Description: Returns a map of payer names and their current balances based on the previous transactions.
