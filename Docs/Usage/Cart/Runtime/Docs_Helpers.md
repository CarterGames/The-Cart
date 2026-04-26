# Helpers

| [Usage](Docs_Helpers.md) | [API](../../../../Docs/API/Cart/Runtime/API_Helpers.md) |

Helpers are like extension methods, but not. They are helpful methods for missing API 
but you need to call it from the helper class specifically for them to function.

|             |              |
|-------------|:-------------|
| Revision    | `1`          |
| Last update | `2026-04-26` |

<br/>

### Classes
Helpers are split into different classes by the type or system they are for. The built-in ones are:

- **Color**: For additional colour methods. 
- **DateTime**: For getting API that is not present in `2020.3.x` around date time logic.
- **Math**: Contains additional math functions.
- **Resources**: Contains handy methods for easier interaction with resources folder assets.
- **Texture**: Contains helpful methods for changing elements in textures.

<br/>

#### Example

```csharp
private void OnEnable()
{
    var myNumber = 123;
    
    // Logs out 123%
    Debug.Log(MathHelper.Percentage(myNumber));
}
```