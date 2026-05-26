# Copy (API)

| [Usage](../../../../Docs/Usage/Cart/Runtime/Docs_Copy.md) | [API](API_Copy.md) |

Stores any copy API the library provides.

|             |            |
|-------------|:-----------|
| Revision    | `1`         |
| Last update | `2026-04-01`         |

<br/>

|                  |     |
|------------------|:----|
| Assembly         | `CarterGames.Cart.Runtime`  |
| Namespace        | `CarterGames.Cart`  |

<br/>

### `CopyHandler`
Provides extension methods to copy data.

<br/>

### Methods

#### `CopyToClipboard()`
Copies the string called from to the users clipboard.


```csharp
public static void CopyToClipboard(this string str);
```

```csharp
private void OnEnable()
{
    var myString = "Fudge Banana";
    myString.CopyToClipboard(); // Copies "Fudge Banana" to the users clipboard.
}
```

<br/>
