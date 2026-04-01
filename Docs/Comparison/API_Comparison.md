# Comparison (API)

| [Usage]() | [API]() |

The purpose of this section is to allow you to define comparisons in the inspector through readable enums. Mainly useful for settings etc.

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

### `NumericalComparisonType`
An enum that provides a inspector friendly comparison setting for numbers.

```csharp
public enum NumericalComparisonType
{
    Unassigned = 0,
    [InspectorName("< (Less Than)")] LessThan = 2,
    [InspectorName("<= (Less Than Or Equal To)")] LessThanOrEqual = 3,
    [InspectorName("== (Equal To)")] Equals = 4,
    [InspectorName(">= (Greater Than Or Equal To)")] GreaterThanOrEqual = 5,
    [InspectorName("> (Greater Than)")] GreaterThan = 6,
}
```

<br/>
