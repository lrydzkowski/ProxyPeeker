# ProxyPeeker

A simple ASP.NET Core application as a proxy for logging requests that are sent to external systems.

## How to run it

1. Set up a connection string to SQL Server database (`ConnectionStrings.SqlServerDb`).
2. Set up mappings between paths and URLs (`Proxy.Mapping`) in the following way:

```json
"Proxy": {
  "Mapping": [
    {
      "path": "/corporate-buzz-words",
      "url": "https://corporatebs-generator.sameerkumar.website/"
    },
    {
      "path": "/chuck-norris-jokes",
      "url": "https://api.chucknorris.io/jokes/random"
    }
  ]
}
```

Open in the browser:

- https://localhost:7064/corporate-buzz-words
- https://localhost:7064/chuck-norris-jokes
