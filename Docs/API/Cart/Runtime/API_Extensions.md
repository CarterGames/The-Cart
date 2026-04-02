# Extensions (API)

| [Usage]() | [API]() |

Contains a load of extension methods for a load of different classes. This section only contains extension methods and nothing else.

|             |            |
|-------------|:-----------|
| Revision    | `1`         |
| Last update | `2026-04-02`         |

<br/>

|                  |     |
|------------------|:----|
| Assembly         | `CarterGames.Cart.Runtime`  |
| Namespace        | `CarterGames.Cart`  |

<br/>

### `ArrayExtensions`
Any extensions for arrays.

<br/>

### Methods

#### `IsEmptyOrNull()`
Checks to see if the array is empty or null.

```csharp
public static bool IsEmptyOrNull<T>(this T[] array);
```

```csharp
int[] myArray = new int[3] { 1,2,3 };

private void OnEnable()
{
    if (myArray.IsEmptyOrNull()) return;
    // Logic here with the array now its confirmed as not empty or null.
}
```

<br/>

#### `IsEmpty()`
Checks to see if the array is empty.

```csharp
public static bool IsEmpty<T>(this T[] array);
```

```csharp
int[] myArray = new int[3] { 1,2,3 };

private void OnEnable()
{
    if (myArray.IsEmpty()) return;
    // Logic here with the array now its confirmed as not empty.
}
```

<br/>

#### `RandomElement()`
Returns a random element from the array called from.

```csharp
public static bool RandomElement<T>(this T[] array);
```

```csharp
int[] myArray = new int[3] { 1,2,3 };

private void OnEnable()
{
    Debug.Log(myArray.RandomElement()); // Returns a random entry from the array (so 1, 2 or 3);
}
```

<br/>

#### `Shuffle()`
Shuffles the list into a random order.

```csharp
public static bool Shuffle<T>(this T[] array);
```

```csharp
int[] myArray = new int[3] { 1,2,3 };

private void OnEnable()
{
    Debug.Log(myArray.Shuffle()); // Returns the array in a random order (so 1,2,3 coult return as 3,1,2 / 2,3,1 etc.);
}
```

<br/>

#### `HasNullEntries()`
Gets if there are any null entries in the array (missing or blank).

```csharp
public static bool HasNullEntries<T>(this T[] array);
```

```csharp
int[] myArray = new int[3] { 1,2,3 };

private void OnEnable()
{
    Debug.Log(myArray.HasNullEntries()); // Would return false as the array is all valid.
}
```

<br/>

#### `RemoveMissing()`
Removes missing entries in a array when called.

```csharp
public static bool RemoveMissing<T>(this T[] array);
```

```csharp
int[] myArray = new int[3] { 1,2,3 };

private void OnEnable()
{
    Debug.Log(myArray.RemoveMissing()); // Would remove any entries if any were missing or blank.
}
```

<br/>

#### `RemoveDuplicates()`
Removed any duplicate entries from the array entered.

```csharp
public static bool RemoveDuplicates<T>(this T[] array);
```

```csharp
int[] myArray = new int[4] { 1,2,3,1 };

private void OnEnable()
{
    Debug.Log(myArray.RemoveDuplicates()); // Would return 1,2,3 removing the extra 1 in the array.
}
```

<br/>

#### `IsWithinIndex()`
Gets if the index is within the lists bounds.

```csharp
public static bool IsWithinIndex<T>(this T[] array, int index);
```

```csharp
int[] myArray = new int[3] { 1,2,3 };

private void OnEnable()
{
    Debug.Log(myArray.IsWithinIndex(2)); // Would return true
    Debug.Log(myArray.IsWithinIndex(4)); // Would return false
}
```

<br/>

#### `IndexLength()`
Gets the total indexes in an array (so the index length -1).

```csharp
public static bool IndexLength<T>(this T[] array);
```

```csharp
int[] myArray = new int[3] { 1,2,3 };

private void OnEnable()
{
    Debug.Log(myArray.IndexLength()); // Would return 2
}
```

<br/>

#### `Add()`
Inserts an element into an array at the end.

```csharp
public static bool Add<T>(this T[] array, T element);
```

```csharp
int[] myArray = new int[3] { 1,2,3 };

private void OnEnable()
{
    Debug.Log(myArray.Add(4)); // Would make myArray = [1,2,3,4]
}
```

<br/>

#### `Remove()`
Removes the element from the array and re-sizes it.

```csharp
public static bool Remove<T>(this T[] array, T element);
```

```csharp
int[] myArray = new int[4] { 1,2,3,4 };

private void OnEnable()
{
    Debug.Log(myArray.Remove(4)); // Would make myArray = [1,2,3]
}
```

<br/>

#### `Contains()`
Gets if the array contains the requested element.

```csharp
public static bool Contains<T>(this T[] array, T element);
```

```csharp
int[] myArray = new int[4] { 1,2,3,4 };

private void OnEnable()
{
    Debug.Log(myArray.Contains(3)); // Would return true.
    Debug.Log(myArray.Contains(5)); // Would return false.
}
```

<br/>

#### `IndexOf()`
Gets the index of the entered element in the entered array.

```csharp
public static bool IndexOf<T>(this T[] array, T element);
```

```csharp
int[] myArray = new int[4] { 1,2,3,4 };

private void OnEnable()
{
    Debug.Log(myArray.IndexOf(3)); // Would return 2.
}
```

<br/>
