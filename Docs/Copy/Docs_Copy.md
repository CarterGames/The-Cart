# Copy

| [Usage]() | [API]() |

Stores any copy API the library provides.

|             |            |
|-------------|:-----------|
| Revision    | `1`         |
| Last update | `2026-04-01`         |

<br/>


### CopyToClipboard
Lets you copy a string to the users device clipboard. 

```csharp
private void OnEnable()
{
    var myString = "Fudge Banana";
    myString.CopyToClipboard(); // Copies "Fudge Banana" to the users clipboard.
}
```
