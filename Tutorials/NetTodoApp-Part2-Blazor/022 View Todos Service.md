# Get All Todos

We start with the client layer, and first the interface.

We need to be able to retrieve Todos, and request them with filtering. We already have an endpoint for this.

## Interface

In ITodoService interface add the following method:

```csharp
Task<ICollection<Todo>> GetAsync(string? userName, int? userId, bool? completedStatus, string? titleContains);
```

## Implementation

Next up, we implement the method in TodoHttpClient.

We need to build up the URI with the correct query arguments. It looks like this:

```csharp

```