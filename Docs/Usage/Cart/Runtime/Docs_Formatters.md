# Formatters

| [Usage](Docs_Formatters.md) | [API](../../../../Docs/API/Cart/Runtime/API_Formatters.md) |

Contains a load of different formatters to convert values into readable strings.

|             |              |
|-------------|:-------------|
| Revision    | `1`          |
| Last update | `2026-04-23` |

<br/>

### Types

The formatter setup is designed to convert values into formats for displaying in UI etc as strings.
Currently, there are 3 main categories built-in to the library, these are:
- Money
- Position
- Time

You can also make your own formatter types to extend the system. 
Under each of the built-in categories there are a number of formatter classes that can be used to format values. 
Again you can add your own easily to extend the library.
These are:

#### Money
| Type        | Description                                                                    |
|:------------|:-------------------------------------------------------------------------------|
| RawNumber   | Converts numbers to values such as 124, 1,240, 12,400, 124,000, 1,124,000 etc. |
| Generic     | Converts numbers to values such as 124, 1.24K, 12.4K, 124K, 1.24M etc.         |
| Pounds      | Converts numbers to values such as £124, £124,567 etc.                         |
| Dollars     | Converts numbers to values such as $124, $124,567 etc.                         |
| JapaneseYen | Converts numbers to values such as ¥124, ¥124,567 etc.                         |

<br/>

#### Position
| Type        | Description                                                     |
|:------------|:----------------------------------------------------------------|
| Generic     | Converts numbers to values such as 1st, 2nd, 3rd, 4th, 5th etc. |

<br/>

#### Time
| Type                      | Description                                                               |
|:--------------------------|:--------------------------------------------------------------------------|
| Stopwatch simple          | Converts numbers to values such as 01:23, 42:89.                          |
| Day/Hour/Min/Sec simple   | Converts numbers to values such as 1d 23hr, 3hr 23m, 23m 12s.             |
| Day/Hour/Min/Sec detailed | Converts numbers to values such as 1d 23hr 12m 01s, 3hr 23m 42s, 23m 12s. |

<br/>

#### Usage
The main way to use formatters is through extension methods. The method is the same for any formatter type:

```csharp
private void OnEnable()
{
    // Formats the number to a generic currency. So 123456 = 123K
    var myFormattedNumber = 123456.Format<MoneyFormatterGeneric>();
    
    // Formats the number to a generic currency. So 123456 = ¥123,456
    var myFormattedNumberYen = 123456.Format<MoneyFormatterJapaneseYen>();
}
```

Note that `TimeFormatters` have an extra extension to pass through a timespan as well as the usual numbers. 
You may find this more useful than passing in a raw number in these instances to get the desired result.

```csharp
private void OnEnable()
{
    // Formats the number to time (converted to seconds).
    var myFormattedTime = 123456.Format<TimeFormatterStopwatchSimple>();
    
    // Formats the number to time from the span assigned.
    var span = TimeSpan.FromSeconds(123456);
    var myFormattedNumberYen = span.Format<MoneyFormatterJapaneseYen>();
}
```

You can alternatively reference formatters in the inspector using the `FormatterComponent` class.
This class is a MonoBehaviour you can add to GameObjects. From its inspector you can select the desired formatter
to use with that component instance. Call its public methods to use the format:

```csharp
// Your formatter component for reference.
public FormatterComponent formatter;

private void OnEnable()
{
    // Formats the number to with the formatter assigned.
    var myFormattedTime = formatter.Format(123456);
    
    // Formats the number to time  with the formatter assigned.
    // Only works for time formatter types, other will error.
    var span = TimeSpan.FromSeconds(123456);
    var myFormattedNumberYen = formatter.Format(span);
}
```

<br/>

### Extending
You can add your own formatter type by making a class that inherit from the `Formatter` abstract class.
If you want to make a time formatter specifically you should inherit from the `TimeFormatter` class instead.

To assign which category to formatter shows under, override the `Category` property and enter your desired category.

Example:
```csharp
// Outs the class in the "Money" category.
public override string Category => "Money";
```

Implement the `Format()` method to assign your formatting style to any inputted value.

Example:
```csharp
public override string Format(double value)
{
    // Returns the entered number in JPY.
    return value.ToString("C2", CultureInfo.GetCultureInfo("ja-JP"));
}
```