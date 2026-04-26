# Helpers (API)

| [Usage](../../../../Docs/Usage/Cart/Runtime/Docs_Helpers.md) | [API](API_Helpers.md) |

Helpers are like extension methods, but not. They are helpful methods for missing API
but you need to call it from the helper class specifically for them to function.

|             |              |
|-------------|:-------------|
| Revision    | `1`          |
| Last update | `2026-04-26` |

<br/>

|                  |     |
|------------------|:----|
| Assembly         | `CarterGames.Cart.Runtime`  |
| Namespace        | `CarterGames.Cart`  |

<br/>

## Navigation
- [Color](#colorhelper)
- [DateTime](#datetimehelper)
- [Math](#mathhelper)
- [Resources](#resourceshelper)
- [Texture](#texturehelper)

<br/>

### `ColorHelper`
A helper class for colour types.

<br/>

### Methods

#### `HtmlStringToColor()`
Converts a html color code to a color.

```csharp
public static Color HtmlStringToColor(string input)
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

#### `ColorToHtmlString()`
Converts a color to a html string color code.

```csharp
public static string ColorToHtmlString(Color input)
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
<br/>

### `DateTimeHelper`
A helper class for DateTime. Mainly for missing API in older versions such as `2020.3.x` which the library is built in.

<br/>

### Properties

#### `UnixEpoch`
Gets the unix epoch value of Jan 1st, 1970 at 00:00:00.

```csharp
public static readonly DateTime UnixEpoch { get; }
```

```csharp
private void OnEnable()
{
    // Logs out the unix epoch.
    Debug.Log(DateTimeHelper.UnixEpoch); 
}
```

<br/>

### Methods

#### `ParseUnixEpochUtc()`
Parses a Unix Epoch Utc timestamp into a date time for use.

```csharp
public static Color ParseUnixEpochUtc(long timestamp)
```

```csharp
private void OnEnable()
{
    // Logs out ¥123,456,789
    Debug.Log(myNumber.Format<MoneyFormatterJapaneseYen>()); 
}
```

<br/>
<br/>

### `MathHelper`
A helper class for common math problems.

<br/>

### Methods

#### `GetSpeed()`
Gets the speed from a distance and time.

```csharp
public static float GetSpeed(float distance, float time)
```

```csharp
private void OnEnable()
{
    Debug.Log(MathHelper.GetSpeed(100f, 0.5f)); 
}
```

<br/>

#### `GetDistance()`
Gets the distance from a speed and time.

```csharp
public static float GetDistance(float speed, float time)
```

```csharp
private void OnEnable()
{
    Debug.Log(MathHelper.GetDistance(500f, 0.5f)); 
}
```

<br/>

#### `GetTime()`
Gets the time from a distance and speed.

```csharp
public static float GetTime(float distance, float speed)
```

```csharp
private void OnEnable()
{
    Debug.Log(MathHelper.GetDistance(100f, 500f)); 
}
```

<br/>

#### `Lerp()`
Lerp functionally but with double. Time clamped between 0-1.

```csharp
public static double Lerp(double a, double b, float t)
```

```csharp
private void OnEnable()
{
    Debug.Log(MathHelper.Lerp(1f, 5f, 0.5f)); 
}
```

<br/>

#### `Percentage()`
Gets the value adjusted by a percentage.

```csharp
public static int Percentage(int value, int percentage)
public static float Percentage(float value, float percentage)
public static double Percentage(double value, double percentage)
```

```csharp
private void OnEnable()
{
    var myNumber = 12345;
    
    // Logs out 2469%
    Debug.Log(MathHelper.Percentage(12345, 200)); 
}
```

<br/>

#### `PercentageDecimal()`
Gets the value adjusted by a percentage.

```csharp
public static int PercentageDecimal(int value, int percentage)
public static float PercentageDecimal(float value, float percentage)
public static double PercentageDecimal(double value, double percentage)
```

```csharp
private void OnEnable()
{
    var myNumber = 12345;
    
    // Logs out 2469%
    Debug.Log(MathHelper.Percentage(12345, 200)); 
}
```

<br/>
<br/>

### `ResourcesHelper`
Helpers for loading assets from the resources' folder.

<br/>

### Methods

#### `Load()`
Loads an asset from the resources folder of the type needed.

```csharp
public static Object Load(string basePath, string resourceName, Type systemTypeInstance)
```

```csharp
private void OnEnable()
{ 
    var asset = ResourcesHelper.Load("Assets/Resources", "MyAsset.prefab", typeof(GameObject));
}
```

<br/>

#### `LoadAll()`
Loads all assets from the resources folder of the type needed.

```csharp
public static T[] LoadAll<T>(string basePath) where T : Object
```

```csharp
private void OnEnable()
{ 
    var assets = ResourcesHelper.LoadAll<GameObject>("Assets/Resources");
}
```

<br/>
<br/>

### `TextureHelper`
Some helpers for textures.

<br/>

### Methods

#### `SolidColorTexture2D()`
Creates a single color texture of the requested size and color.

```csharp
public static Texture2D SolidColorTexture2D(int width, int height, Color col)
```

```csharp
private void OnEnable()
{ 
    // Makes a texture of a 1x1 square that is magenta in color.
    var texture = TextureHelper.SolidColorTexture2D(1, 1, Color.magenta);
}
```

<br/>