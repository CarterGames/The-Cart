# Timestamps (API)

| [Usage](../../../../Docs/Usage/Cart/Runtime/Docs_Timestamps.md) | [API](API_Timestamps.md) |

A small wrapper for a timestamp to make converting epoch to human-readable date and vice versa easy.

|             |            |
|-------------|:-----------|
| Revision    | `1`         |
| Last update | `2026-04-02`         |

<br/>

|                  |     |
|------------------|:----|
| Assembly         | `CarterGames.Cart.Runtime`  |
| Namespace        | `CarterGames.Cart`  |

<br/>

### `Timestamp`
A timestamp class to aid with timestamps to dates and back again.

<br/>

### Constructors

```csharp
public Timestamp(long timestamp);
public Timestamp(DateTime dateTime);
```

<br/>

### Properties

#### `Value`
Gets the raw timestamp value.

```csharp
public long Value { get; }
```

```csharp
Timestamp timestamp = new Timestamp(DateTime.UtcNow);

private void OnEnable()
{
    Debug.Log(timestamp.Value);
}
```

<br/>

#### `DateTime`
The date time the timestamp converts to.

```csharp
public DateTime DateTime { get; }
```

```csharp
int[] myArray = new int[3] { 1,2,3 };

private void OnEnable()
{
    if (myArray.IsEmpty()) return;
    // Logic here with the array now its confirmed as not empty.
}
```
