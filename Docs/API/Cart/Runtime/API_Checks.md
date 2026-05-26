# Checks (API)

| [Usage](../../../../Docs/Usage/Cart/Runtime/Docs_Checks.md) | [API](API_Checks.md) |

Stores any copy API the library provides.

|             |              |
|-------------|:-------------|
| Revision    | `1`          |
| Last update | `2026-05-26` |

<br/>

|                  |     |
|------------------|:----|
| Assembly         | `CarterGames.Cart.Runtime`  |
| Namespace        | `CarterGames.Cart`  |

<br/>

### `PreReq`
Provides the methods for any checks that should block logic if they fail.

<br/>

### Methods

#### `DisallowIfNull()`
Stops logic if a reference is null.


```csharp
public static void DisallowIfNull<T>(T reference, string message = "");
```

```csharp
public GameObject myGameObjectReference;

private void OnEnable()
{
    PreReq.DisallowIfNull(myGameObjectReference);
    // Safe to run logic here with myGameObjectReference as it is not null.
}
```

<br/>

#### `DisallowIfTrue()`
Stops logic if the value is true.


```csharp
public static void DisallowIfTrue(bool check, string message = "");
```

```csharp
public bool myBoolean;

private void OnEnable()
{
    PreReq.DisallowIfTrue(myBoolean);
    // Safe to run logic here.
}
```

<br/>

#### `DisallowIfFalse()`
Stops logic if the value is false.


```csharp
public static void DisallowIfFalse(bool check, string message = "");
```

```csharp
public bool myBoolean;

private void OnEnable()
{
    PreReq.DisallowIfFalse(myBoolean);
    // Safe to run logic here.
}
```

<br/>