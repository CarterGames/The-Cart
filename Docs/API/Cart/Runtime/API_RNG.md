# Random (API)

| [Usage](../../../../Docs/Usage/Cart/Runtime/Docs_RNG.md) | [API](API_RNG.md) |

Rng is one of the many core systems which handles generating random numbers, string etc. The set-up lets you choose which provider is used to produce the random results. Some are true random while others are seeded random (where a seed can produce the same result each time run).

|             |              |
|-------------|:-------------|
| Revision    | `1`          |
| Last update | `2026-05-11` |

<br/>

|             |                            |
|-------------|:---------------------------|
| Assembly    | `CarterGames.Cart.Runtime` |
| Namespace   | `CarterGames.Cart.Random`  |

<br/>

### `Rng`
The main API class for the random system of the library.

<br/>

### Properties

#### `Seed`
Gets the seed the setup is currently using as a string, if applicable.

```csharp
public static string Seed { get; }
```

```csharp
private void OnEnable()
{
    Debug.Log(Rng.Seed);
}
```

<br/>

### Methods

#### `Bool`
Gets a random boolean result.

```csharp
public static bool Bool();
```

```csharp
private void OnEnable()
{
    var myBool = Rng.Bool();
    Debug.Log(myBool);
}
```

<br/>

#### `String`
Generates a random string of the desired length. 
`useSymbols` Defines if common symbols such as brackets or similar are included in the random string or not.

```csharp
public static string String(int length, bool useSymbols = false);
```

```csharp
private void OnEnable()
{
    var myString = Rng.String(16);
    Debug.Log(myString);
}
```

<br/>

#### `Int`
Gets a random int from the entered min and max values. If no min is entered it will assume 0 as the min value.

```csharp
public static int Int(int max);
public static int Int(int min, int max);
```

```csharp
private void OnEnable()
{
    // Gets a random number between 1 - 10
    var myInt = Rng.Int(1, 10);
    
    Debug.Log(myInt);
}
```

<br/>

#### `IntVariance`
Gets a random int with a variance of +/- the entered variance from the starting value.

```csharp
public static int IntVariance(int startingValue, int variance);
```

```csharp
private void OnEnable()
{
    // Gets a random int between 2 - 8
    var myInt = Rng.IntVariance(5, 3);
    
    Debug.Log(myInt);
}
```

<br/>

#### `Float`
Gets a random float from the entered min and max values. If no min is entered it will assume 0f as the min value.

```csharp
public static float Float(float max);
public static float Float(float min, float max);
```

```csharp
private void OnEnable()
{
    // Gets a float between 2.5f - 7.5f
    var myFloat = Rng.Float(2.5f, 7.5f);
    
    Debug.Log(myFloat);
}
```

<br/>

#### `Float01`
Gets a random float of between 0 - 1.

```csharp
public static float Float01();
```

```csharp
private void OnEnable()
{
    // Gets a float between 0 - 1, so could be 0.3425f for example.
    var myFloat = Rng.Float01();
    
    Debug.Log(myFloat);
}
```

<br/>

#### `FloatVariance`
Gets a random float with a variance of +/- the entered variance from the starting value.

```csharp
public static float FloatVariance(float startingValue, float variance);
```

```csharp
private void OnEnable()
{
    // Gets a random float between 2.5f - 7.5f
    var myFloat = Rng.FloatVariance(5f, 2.5f);
    
    Debug.Log(myFloat);
}
```

<br/>

#### `Double`
Gets a random double from the entered min and max values. If no min is entered it will assume 0d as the min value.

```csharp
public static double Double(double max);
public static double Double(double min, double max);
```

```csharp
private void OnEnable()
{
    // Gets a double between 2d - 8.25d
    var myDouble = Rng.Double(2d, 8.25d);
    
    Debug.Log(myDouble);
}
```

<br/>

#### `Double01`
Gets a random double of between 0 - 1.

```csharp
public static float Double01();
```

```csharp
private void OnEnable()
{
    // Gets a double between 0 - 1, so could be 0.3425d for example.
    var myDouble = Rng.Double01();
    
    Debug.Log(myDouble);
}
```

<br/>

#### `DoubleVariance`
Gets a random double with a variance of +/- the entered variance from the starting value.

```csharp
public static float DoubleVariance(double startingValue, double variance);
```

```csharp
private void OnEnable()
{
    // Gets a random double between 2.5d - 7.5d
    var myDouble = Rng.DoubleVariance(5d, 2.5d);
    
    Debug.Log(myDouble);
}
```

<br/>

#### `Vector2`
Gets a random Vector2 with each axis being random from 0 - the entered max value. 0 is assumed the min values when not defined.

```csharp
public static Vector2 Vector2(float max);
public static Vector2 Vector2(float min, float max);
public static Vector2 Vector2(float minX, float maxX, float minY, float maxY);
```

```csharp
private void OnEnable()
{
    // Gets a Vector2 with both x & y betwen 2 - 8.
    var myVec = Rng.Vector2(2, 8);
    
    Debug.Log(myVec);
    
    // Gets a Vector2 with x between 1 - 3 & y between 5 - 7
    var myVec = Rng.Vector2(1, 3, 5, 7);
}
```

<br/>

#### `Vector201`
Gets a random Vector2 with each axis of the vector set between 0 - 1

```csharp
public static Vector2 Vector201();
```

```csharp
private void OnEnable()
{
    // Gets a random vector2 within a 0-1 range of each axis.
    var myVec = Rng.Vector201();
    
    Debug.Log(myVec);
}
```

<br/>

#### `Vector2Variance`
Gets a random Vector2 with a variance of +/- the entered variance for every vector value from the starting value.

```csharp
public static Vector2 Vector2Variance(Vector2 startingValue, float variance);
public static Vector2 Vector2Variance(Vector2 startingValue, Vector2 variance);
```

```csharp
private void OnEnable()
{
    // Gets a random vector2 with axis adjusted by 1 so 0 - 2 on each axis.
    var myVec = Rng.Vector2Variance(Vector2.One, 1);
    
    Debug.Log(myVec);
    
    // Gets a random vector2 adjusted by the 2nd vectors axis values.
    myVec = Rng.Vector2Variance(Vector2.One, new Vector2(2, 5));
    
    Debug.Log(myVec);
}
```

<br/>

#### `Vector3`
Gets a random Vector3 with each axis being random from 0 - the entered max value. 0 is assumed the min values when not defined.

```csharp
public static Vector3 Vector3(float max);
public static Vector3 Vector3(float min, float max);
public static Vector3 Vector3(float minX, float maxX, float minY, float maxY, float minZ, float maxZ);
```

```csharp
private void OnEnable()
{
    // Gets a Vector3 with both x, y & z betwen 2 - 8.
    var myVec = Rng.Vector3(2, 8);
    
    Debug.Log(myVec);
    
    // Gets a Vector3 with x between 1 - 3, y between 5 - 7 & z between 2 - 6
    var myVec = Rng.Vector3(1, 3, 5, 7, 2, 6);
}
```

<br/>

#### `Vector301`
Gets a random Vector3 with each axis of the vector set between 0 - 1

```csharp
public static Vector3 Vector301();
```

```csharp
private void OnEnable()
{
    // Gets a random vector3 within a 0-1 range of each axis.
    var myVec = Rng.Vector301();
    
    Debug.Log(myVec);
}
```

<br/>

#### `Vector3Variance`
Gets a random Vector3 with a variance of +/- the entered variance for every vector value from the starting value.

```csharp
public static Vector3 Vector3Variance(Vector3 startingValue, float variance);
public static Vector3 Vector3Variance(Vector3 startingValue, Vector3 variance);
```

```csharp
private void OnEnable()
{
    // Gets a random vector3 with axis adjusted by 1 so 0 - 2 on each axis.
    var myVec = Rng.Vector3Variance(Vector3.One, 1);
    
    Debug.Log(myVec);
    
    // Gets a random vector3 adjusted by the 2nd vectors axis values.
    myVec = Rng.Vector3Variance(Vector3.One, new Vector3(2, 5, 3));
    
    Debug.Log(myVec);
}
```

<br/>

#### `Vector4`
Gets a random Vector4 with each axis being random from 0 - the entered max value. 0 is assumed the min values when not defined.

```csharp
public static Vector4 Vector4(float max);
public static Vector4 Vector4(float min, float max);
public static Vector4 Vector4(float minX, float maxX, float minY, float maxY, float minZ, float maxZ, float minW, float maxW);
```

```csharp
private void OnEnable()
{
    // Gets a Vector4 with both x, y & z betwen 2 - 8.
    var myVec = Rng.Vector4(2, 8);
    
    Debug.Log(myVec);
    
    // Gets a Vector3 with x between 1 - 3, y between 5 - 7
    // z between 2 - 6 & w between 4 - 10
    var myVec = Rng.Vector4(1, 3, 5, 7, 2, 6, 4, 10);
}
```

<br/>

#### `Vector401`
Gets a random Vector4 with each axis of the vector set between 0 - 1

```csharp
public static Vector4 Vector401();
```

```csharp
private void OnEnable()
{
    // Gets a random vector4 within a 0-1 range of each axis.
    var myVec = Rng.Vector401();
    
    Debug.Log(myVec);
}
```

<br/>

#### `Vector4Variance`
Gets a random Vector4 with a variance of +/- the entered variance for every vector value from the starting value.

```csharp
public static Vector4 Vector4Variance(Vector4 startingValue, float variance);
public static Vector4 Vector4Variance(Vector4 startingValue, Vector4 variance);
```

```csharp
private void OnEnable()
{
    // Gets a random vector4 with axis adjusted by 1 so 0 - 2 on each axis.
    var myVec = Rng.Vector4Variance(Vector4.One, 1);
    
    Debug.Log(myVec);
    
    // Gets a random vector4 adjusted by the 2nd vectors axis values.
    myVec = Rng.Vector4Variance(Vector4.One, new Vector4(2, 5, 3, 4));
    
    Debug.Log(myVec);
}
```

<br/>