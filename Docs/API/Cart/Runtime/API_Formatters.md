# Formatters (API)

| [Usage](../../../../Docs/Usage/Cart/Runtime/Docs_Formatters.md) | [API](API_Copy.md) |

Contains a load of different formatters to convert values into readable strings.

|             |              |
|-------------|:-------------|
| Revision    | `1`          |
| Last update | `2026-04-23` |

<br/>

|                  |     |
|------------------|:----|
| Assembly         | `CarterGames.Cart.Runtime`  |
| Namespace        | `CarterGames.Cart`  |

<br/>

### `FormatterExtensions`
The container class for the extension methods used as the main way to interact with the 
formatting setup in code.

<br/>

### Methods

#### `Format<T>()`
Formats the entered value into the requested format type entered.

```csharp
public static string Format<Formatter>(this int value);
public static string Format<Formatter>(this float value);
public static string Format<Formatter>(this double value);
public static string Format<TimeFormatter>(this Timespan value);
```

```csharp
private void OnEnable()
{
    var myNumber = 123456789;
    
    // Logs out ¥123,456,789
    Debug.Log(myNumber.Format<MoneyFormatterJapaneseYen>()); 
}
```

<br/>

### `Formatter`
The base class that all formatter derive from. Inherit from to make your own formatters 

<br/>

### Properties

#### `Category`
Defines the category the formatter is a part of.

```csharp
public virtual string Category { get; }
```

<br/>

### Methods

#### `Format()`
Implement to format the value passed in to the desired format.


```csharp
public abstract string Format(double value);
```

<br/>
