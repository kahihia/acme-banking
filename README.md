## Simple .NET Core API / Ionic app

The API uses Entity Framework Core as an in-memory database. When the API is started, it will populate with a Checking and Savings account with random transactions for each of the accounts.

### Instructions
Clone repo: `git clone https://github.com/teddest/acme-banking.git`

For .NET Core and Ionic Framework setup instruction see:
* [Getting started ASP.NET Core](https://docs.asp.net/en/latest/getting-started.html)
* [Getting started with Ionic](http://ionicframework.com/getting-started/)

Once you have your environment setup...
* Restore and run the API
 * `cd acme-banking/api`
 * `dotnet restore`
 * `dotnet run`



* In a separate terminal, start the Ionic mobile app
 * `cd acme-banking/mobile`
 * `ionic serve --lab`
