# Extensions

| [Usage](Docs_Extensions.md) | [API](../../../../Docs/API/Cart/Runtime/API_Extensions.md) |

Contains a load of extension methods for a load of different classes. This section only contains extension methods and nothing else.

|             |            |
|-------------|:-----------|
| Revision    | `1`         |
| Last update | `2026-04-02`         |

<br/>

Extension methods can be called from the library by appending their method to the end of the relevant type. An example would be:

```csharp
private void OnEnable()
{
    var myNumberInverted = 123456.Invert(); // Calls IntExtensions.Invert(); resulting in myNumberInverted = -123456
}
```
