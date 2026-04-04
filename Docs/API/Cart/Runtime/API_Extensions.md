# Extensions (API)

| [Usage](../../../../Docs/Usage/Cart/Runtime/Docs_Extensions.md) | [API](API_Extensions.md) |

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

## Classes
- [Array Extensions](#array-extensions)
- [Canvas Group Extensions](#canvas-group-extensions)

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

---

<br/>

[//]: #
[//]: #
[//]: # (Canvas group extensions)
[//]: #
[//]: #

### `CanvasGroupExtensions`
Any extensions for canvas groups.

<br/>

### Methods

#### `SetActive()`
Checks to see if the array is empty or null.

```csharp
public static void SetActive(this CanvasGroup canvasGroup, bool active, bool? interactable = null, bool? blocksRaycasts = null, bool? ignoreParentGroups = null);
```

```csharp
public CanvasGroup group;

private void OnEnable()
{
    group.SetActive(true); 
    // Will set alpha to 1 
    // interactable to true 
    // blocksRaycasts to true
    // ignoreParentGroups to false
    // Unless overriden by optional parameters.
}
```

<br/>

---

<br/>

[//]: #
[//]: #
[//]: # (Color extensions)
[//]: #
[//]: #

### `Color Extensions`
Any extensions for color's.

<br/>

### Methods

#### `Equals()`
Compares the two colours and see's if they match.

```csharp
public static bool Equals(this Color col, Color b, bool compareAlpha = false);
```

```csharp
public Color col01;
public Color col02;

private void OnEnable()
{
    Debug.Log(col01.Equals(col02));
}
```

<br/>

#### `SetAlpha()`
Changes the alpha of the color without effect the other values.

```csharp
public static Color SetAlpha(this Color col, float alpha);
```

```csharp
public Color col01;

private void OnEnable()
{
    col01.SetAlpha(.5f); // Sets the color to 50% opacity.
}
```

<br/>

#### `With()`
Changes any value of the color to the new values entered.

```csharp
public static Color With(this Color col, float? r = null, float? g = null, float? b = null, float? a = null);
```

```csharp
private void OnEnable()
{
    Color.Blue.With(a: .5f); // Sets the color to 50% opacity while keeping the r,g,b values as is.
}
```

<br/>

#### `Adjust()`
Increments any value of the color by the new values entered.

```csharp
public static Color Adjust(this Color col, float? r = null, float? g = null, float? b = null, float? a = null);
```

```csharp
private void OnEnable()
{
    Color.Blue.Adjust(a: -.5f); // Sets the color to 50% opacity while keeping the r,g,b values as is.
}
```

<br/>

#### `Invert()`
Inverts the colour entered.

```csharp
public static Color Invert(this Color col);
```

```csharp
private void OnEnable()
{
    Color.Blue.Invert(); // Inverts the color (so orange in this case).
}
```

<br/>

---

<br/>

[//]: #
[//]: #
[//]: # (DateTime extensions)
[//]: #
[//]: #

### `DateTime Extensions`
A set of extension methods for DateTime.

<br/>

### Methods

#### `SetDate()`
Sets the date specifically in a datetime.

```csharp
public static DateTime SetDate(this DateTime dateTime, int? year, int? month, int? day);
```

```csharp
DateTime myDate = new DateTime(1990, 12, 30, 15, 30, 0);

private void OnEnable()
{
    myDate.SetDate(2020, 11, 14);   // myDate adjusted to: 2020/11/14 | 15:30:00
}
```

<br/>

#### `SetTime()`
Set the time specifically in a datetime.

```csharp
public static DateTime SetTime(this DateTime dateTime, int? hour, int? minute, int? second, int? millisecond = 0);
public static DateTime SetTime(this DateTime dateTime, TimeSpan timeSpan);
```

```csharp
DateTime myDate = new DateTime(1990, 12, 30, 15, 30, 0);

private void OnEnable()
{
    myDate.SetTime(10, 54, 28);   // myDate adjusted to: 1990/12/30 | 10:54:28
}
```

<br/>

#### `ToUnixEpoch()`
Converts the date time entered into a Utc Unix Epoch timestamp, as a long to avoid the 2038 issue.

```csharp
public static long ToUnixEpoch(this DateTime dateTime);
```

```csharp
DateTime myDate = new DateTime(1990, 12, 30, 15, 30, 0);

private void OnEnable()
{
    long unixEpoch = myDate.ToUnixEpoch();
}
```

<br/>

---

<br/>

[//]: #
[//]: #
[//]: # (Dictionary extensions)
[//]: #
[//]: #

### `Dictionary Extensions`
A collection of extension methods for dictionaries.

<br/>

### Methods

#### `IsEmptyOrNull()`
Checks to see if the dictionary is empty or null.

```csharp
public static bool IsEmptyOrNull<TKey,TValue>(this Dictionary<TKey,TValue> dictionary);
```

```csharp
Dictionary<string, int> dic = new Dictionary<string, int>();

private void OnEnable()
{
    Debug.Log(dic.IsEmptyOrNull()); // Returns true as it has no entries, but isn't null.
    dic.Add("Test", 1);
    Debug.Log(dic.IsEmptyOrNull()); // Returns false as it has an entry and isn't null.
}
```

<br/>

#### `IsEmpty()`
Checks to see if the dictionary is empty.

```csharp
public static bool IsEmpty<TKey,TValue>(this Dictionary<TKey,TValue> dictionary);
```

```csharp
Dictionary<string, int> dic = new Dictionary<string, int>();

private void OnEnable()
{
    Debug.Log(dic.IsEmpty()); // Returns true as it has no entries.
    dic.Add("Test", 1);
    Debug.Log(dic.IsEmpty()); // Returns false as it has an entry.
}
```

<br/>

#### `RandomKey()`
Gets a random element from the dictionary and returns the key of the random entry it selected.

```csharp
public static TKey RandomKey<TKey, TValue>(this Dictionary<TKey,TValue> dictionary);
```

```csharp
Dictionary<string, int> dic = new Dictionary<string, int>();

private void OnEnable()
{
    dic.Add("Test", 1);
    dic.Add("Test2", 2);
    dic.Add("TesT3", 3);
    Debug.Log(dic.RandomKey()); // Could return "Test", "Test2", or "Test3".
}
```

<br/>

---

<br/>

[//]: #
[//]: #
[//]: # (EnumFlags extensions)
[//]: #
[//]: #

### `EnumFlags Extensions`
A extension class for enum flags specifically.

<br/>

### Methods

#### `EnumFlagsCount()`
Gets the total number of flags selected.

```csharp
public static int EnumFlagsCount<T>(this T value);
```

```csharp
private void OnEnable()
{
    Debug.Log(MyEnum.EnumFlagsCount());
}
```

<br/>

#### `EnumFlagsToArray()`
Converts the entered value into its enum flagged type as an array for use.

```csharp
public static T[] EnumFlagsToArray<T>(this T value);
```

```csharp
private void OnEnable()
{
    MyEnum[] flagsArray = MyEnum.EnumFlagsToArray();
}
```

<br/>

---

<br/>

[//]: #
[//]: #
[//]: # (Int extensions)
[//]: #
[//]: #

### `Int Extensions`
An extension class for normal ints.

<br/>

### Methods

#### `Invert()`
Inverts the value called from.

```csharp
public static int Invert(this int original);
```

```csharp
private void OnEnable()
{
    Debug.Log(100.Invert()); // Returns -100.
}
```

<br/>

#### `Invert01()`
Inverts the value called from, clamped between 0-1.

```csharp
public static int Invert01(this int original);
```

```csharp
private void OnEnable()
{
    Debug.Log(1.Invert()); // Returns -1.
}
```

<br/>

#### `ParseAsBool()`
Converts an int to a bool anchored around 0.

```csharp
public static bool ParseAsBool(this int original);
```

```csharp
private void OnEnable()
{
    Debug.Log(0.ParseAsBool()); // Returns false.
    Debug.Log(1.ParseAsBool()); // Returns true.
}
```

<br/>

---

<br/>

[//]: #
[//]: #
[//]: # (List extensions)
[//]: #
[//]: #

### `List Extensions`
Extensions for Lists.

<br/>

### Methods

#### `IsEmptyOrNull()`
Checks to see if the list is empty or null.

```csharp
public static bool IsEmptyOrNull<T>(this IList<T> list);
```

```csharp
List<int> myList = new List<int>() { 1,2,3 };

private void OnEnable()
{
    if (myList.IsEmptyOrNull()) return;
    // Logic here with the list now its confirmed as not empty or null.
}
```

<br/>

#### `IsEmpty()`
Checks to see if the list is empty.

```csharp
public static bool IsEmpty<T>(this IList<T> list);
```

```csharp
List<int> myList = new List<int>() { 1,2,3 };

private void OnEnable()
{
    if (myList.IsEmpty()) return;
    // Logic here with the list now its confirmed as not empty.
}
```

<br/>

#### `RandomElement()`
Returns a random element from the array called from.

```csharp
public static T RandomElement<T>(this IList<T> list);
```

```csharp
List<int> myList = new List<int>() { 1,2,3 };

private void OnEnable()
{
    // Returns a random entry from the list (so 1, 2 or 3);
    Debug.Log(myList.RandomElement());
}
```

<br/>

#### `Shuffle()`
Shuffles the list into a random order.

```csharp
public static bool Shuffle<T>(this IList<T> list);
```

```csharp
List<int> myList = new List<int>() { 1,2,3 };

private void OnEnable()
{
    // Returns the list in a random order (so 1,2,3 coult return as 3,1,2 / 2,3,1 etc.);
    Debug.Log(myList.Shuffle());
}
```

<br/>

#### `HasNullEntries()`
Gets if there are any null entries in the list (missing or blank).

```csharp
public static bool HasNullEntries<T>(this IList<T> list);
```

```csharp
List<int> myList = new List<int>() { 1,2,3 };

private void OnEnable()
{
    Debug.Log(myList.HasNullEntries()); // Would return false as the list is all valid.
}
```

<br/>

#### `RemoveMissing()`
Removes missing entries in a list when called.

```csharp
public static IList<T> RemoveMissing<T>(this IList<T> list);
```

```csharp
List<int> myList = new List<int>() { 1,2,3 };

private void OnEnable()
{
    Debug.Log(myList.RemoveMissing()); // Would remove any entries if any were missing or blank.
}
```

<br/>

#### `RemoveDuplicates()`
Removed any duplicate entries from the list entered.

```csharp
public static IList<T> RemoveDuplicates<T>(this IList<T> list);
```

```csharp
List<int> myList = new List<int>() { 1,2,3,1 };

private void OnEnable()
{
    Debug.Log(myList.RemoveDuplicates()); // Would return 1,2,3 removing the extra 1 in the list.
}
```

<br/>

#### `IsWithinIndex()`
Gets if the index is within the lists current bounds.

```csharp
public static bool IsWithinIndex<T>(this IList<T> list, int index);
```

```csharp
List<int> myList = new List<int>() { 1,2,3 };

private void OnEnable()
{
    Debug.Log(myList.IsWithinIndex(2)); // Would return true
    Debug.Log(myList.IsWithinIndex(4)); // Would return false
}
```

<br/>

#### `IndexCount()`
Gets the total indexes in a list (so the index count -1).

```csharp
public static int IndexCount<T>(this IList<T> list);
```

```csharp
List<int> myList = new List<int>() { 1,2,3 };

private void OnEnable()
{
    Debug.Log(myList.IndexCount()); // Would return 2
}
```

<br/>

---

<br/>

[//]: #
[//]: #
[//]: # (Material extensions)
[//]: #
[//]: #

### `Material Extensions`
An extension class for materials.

<br/>

### Methods

#### `GetBool()`
Get a named bool value.

For the name ID of the property retrieved from the `Shader.PropertyToID()` method.

```csharp
public static bool GetBool(this Material material, int nameId);
public static bool GetBool(this Material material, string name);
```

```csharp
Material mat;

private void OnEnable()
{
    // Returns the value of "m_myBool".
    Debug.Log(mat.GetBool("m_myBool"));
}
```

<br/>

#### `SetBool()`
Set a named bool value.

For the name ID of the property retrieved from the `Shader.PropertyToID()` method.

```csharp
public static void SetBool(this Material material, int nameId, bool value);
public static void SetBool(this Material material, string name, bool value);
```

```csharp
Material mat;

private void OnEnable()
{
    // Sets the value of "m_myBool" to false.
    Debug.Log(mat.SetBool("m_myBool", false));
}
```

<br/>

---

<br/>

[//]: #
[//]: #
[//]: # (Math extensions)
[//]: #
[//]: #

### `Math Extensions`
General mathematics extensions. 

<br/>

### Methods

#### `ToNumberOnly()`
Gets just the whole numbers & avoids the remainder.

```csharp
public static float ToNumberOnly(this float value);
public static double ToNumberOnly(this double value);
```

```csharp
private void OnEnable()
{
    // Returns 12
    Debug.Log(12.345f.ToNumberOnly());
}
```

<br/>

#### `ToDecimalOnly()`
Gets just the decimal points and removed the whole numbers.

```csharp
public static float ToDecimalOnly(this float value);
public static double ToDecimalOnly(this double value);
```

```csharp
private void OnEnable()
{
    // Returns 0.345
    Debug.Log(12.345f.ToDecimalOnly());
}
```

<br/>

#### `ToPositive()`
Converts the number from a negative value to a positive one if it is negative to start with.

```csharp
public static float ToPositive(this float value);
public static double ToPositive(this double value);
```

```csharp
private void OnEnable()
{
    // Returns 123
    Debug.Log(-123.ToPositive());
}
```

<br/>

#### `Approximately()`
Gets if the number "a" is within the threshold of the target "b".

```csharp
public static bool Approximately(this float a, float b, float threshold);
public static bool Approximately(this double a, double b, double threshold);
```

```csharp
private void OnEnable()
{
    // Returns true
    Debug.Log(123.456f.Approximately(123.457f, .2f));
}
```

<br/>

#### `FloatEquals()`
Handy to check if a float equals a value.

```csharp
public static bool FloatEquals(this float a, float b);
```

```csharp
private void OnEnable()
{
    // Returns true
    Debug.Log(123.456f.FloatEquals(123.456f));
    
    // Returns false
    Debug.Log(123.456f.FloatEquals(123.458f));
}
```

<br/>

#### `DoubleEquals()`
Handy to check if a double equals a value.

```csharp
public static bool DoubleEquals(this float a, float b);
```

```csharp
private void OnEnable()
{
    // Returns true
    Debug.Log(123.456d.DoubleEquals(123.456d));
    
    // Returns false
    Debug.Log(123.456d.DoubleEquals(123.458d));
}
```

<br/>

#### `FloorToPower()`
Floors the input to the nearest power.

```csharp
public static int FloorToPower(this int a, int p, int iterations = 1000);
```

```csharp
private void OnEnable()
{
    // Returns 512
    Debug.Log(1020.FloorToPower(2));
}
```

<br/>

#### `RoundToPower()`
Rounds the input to the nearest power.

```csharp
public static int RoundToPower(this int a, int p, int iterations = 1000);
```

```csharp
private void OnEnable()
{
    // Returns 1024
    Debug.Log(1020.RoundToPower(2));
}
```

<br/>

#### `CeilToPower()`
Ceiling the input to the nearest power.

```csharp
public static int CeilToPower(this int a, int p, int iterations = 1000);
```

```csharp
private void OnEnable()
{
    // Returns 1024
    Debug.Log(1020.CeilToPower(2));
}
```

<br/>

---

<br/>

[//]: #
[//]: #
[//]: # (Object extensions)
[//]: #
[//]: #

### `Object Extensions`
Some extensions for object/UnityEngine.Object's

<br/>

### Methods

#### `IsMissingOrNull()`
Gets if the object reference is missing or null.

```csharp
public static bool IsMissingOrNull(this UnityEngine.Object obj);
public static bool IsMissingOrNull(this object obj);
```

```csharp
GameObject obj;

private void OnEnable()
{
    // Returns true (as not assigned).
    Debug.Log(obj.IsMissingOrNull());
}
```

<br/>

---

<br/>

[//]: #
[//]: #
[//]: # (Quaternion extensions)
[//]: #
[//]: #

### `Quaternion Extensions`
A collection of extension methods for Quaternions

<br/>

### Methods

#### `With()`
Returns a vector with the modified w, x, y, z value.

```csharp
public static Quaternion With(this Quaternion original, float? w = null, float? x = null, float? y = null, float? z = null);
```

```csharp
Quaternion rot = 

private void OnEnable()
{
    rot = Quaternion.Euler(1,2,3);
    rot.With(x: 4);
    // rot = x - 4, y - 2, z - 3, z - 0
}
```

<br/>

#### `Adjust()`
Returns a vector with the modified w, x, y, z value.

```csharp
public static Quaternion Adjust(this Quaternion original, float? w = null, float? x = null, float? y = null, float? z = null);
```

```csharp
Quaternion rot = 

private void OnEnable()
{
    rot = Quaternion.Euler(1,2,3);
    rot.Adjust(x: 4);
    // rot = x - 5, y - 2, z - 3, z - 0
}
```

<br/>

---

<br/>

[//]: #
[//]: #
[//]: # (ScrollRect extensions)
[//]: #
[//]: #

### `ScrollRect Extensions`
An extension class for scroll rects.

<br/>

### Methods

#### `GetSnapToPositionToBringChildIntoView()`
Snaps the scroll rect to a rect transform in the scroll rect so you can scroll to a point.

```csharp
public static Vector2 GetSnapToPositionToBringChildIntoView(this ScrollRect instance, RectTransform child);
```

```csharp
ScrollRect scroll;
RectTransform target; 

private void OnEnable()
{
    // Snaps to the target that is under the scroll rect.
    scroll.GetSnapToPositionToBringChildIntoView(target);
}
```

<br/>

---

<br/>

[//]: #
[//]: #
[//]: # (String extensions)
[//]: #
[//]: #

### `String Extensions`
An extensions class for strings.

<br/>

### Methods

#### `TrimSpaces()`
Replaces any " " with "" instead.

```csharp
public static string TrimSpaces(this string entry);
```

```csharp
private void OnEnable()
{
    // Logs "Hello"
    Debug.Log("He ll o  ".TrimSpaces());
}
```

<br/>

#### `SplitCapitalsWithSpace()`
Inserts a space after every capital letter in the string.

```csharp
public static string SplitCapitalsWithSpace(this string entry);
```

```csharp
private void OnEnable()
{
    // Logs "Hello World"
    Debug.Log("HelloWorld".SplitCapitalsWithSpace());
}
```

<br/>

#### `SplitAndGetLastElement()`
Splits the string into characters and gets the last element.

```csharp
public static string SplitAndGetLastElement(this string input, char character);
```

```csharp
private void OnEnable()
{
    // Logs 'd'
    Debug.Log("HelloWorld".SplitAndGetLastElement());
}
```

<br/>

---

<br/>

[//]: #
[//]: #
[//]: # (Transform extensions)
[//]: #
[//]: #

### `Transform Extensions`
A collection of extensions for the transform component.

<br/>

### Methods

#### `GetTopParentNotRoot()`
Gets the top most parent that isn't the root object.

```csharp
public static Transform GetTopParentNotRoot(this Transform transform);
```

```csharp
Transform transform;

private void OnEnable()
{
    transform.GetTopParentNotRoot();
}
```

<br/>

#### `GetParentCount()`
Gets the total number of parent transform to the entered transform.

```csharp
public static int GetParentCount(this Transform transform);
```

```csharp
Transform transform;

private void OnEnable()
{
    transform.GetParentCount();
}
```

<br/>

#### `SetParentAndFirstIndex()`
Sets the parent of the transform as well as setting the transform at the first in the index list of said parent.

```csharp
public static void SetParentAndFirstIndex(this Transform transform, Transform target);
```

```csharp
Transform transform;
Transform parent;

private void OnEnable()
{
    transform.SetParentAndFirstIndex(parent);
}
```

<br/>

#### `SetParentAndLastIndex()`
Sets the parent of the transform as well as setting the transform at the last in the index list of said parent.

```csharp
public static void SetParentAndLastIndex(this Transform transform, Transform target);
```

```csharp
Transform transform;
Transform parent;

private void OnEnable()
{
    transform.SetParentAndLastIndex(parent);
}
```

<br/>

#### `SetPosAndRot()`
Sets the position & rotation but allows for local only edits as well.

```csharp
public static Transform SetPosAndRot(this Transform transform, Transform target, bool isLocal = false);
```

```csharp
Transform transform;
Transform target;

private void OnEnable()
{
    transform.SetPosAndRot(target);
}
```

<br/>

#### `SetParent()`
Sets the parent to the parent requested.

```csharp
public static Transform SetParent(this Transform transform, Transform parent);
```

```csharp
Transform transform;
Transform parent;

private void OnEnable()
{
    transform.SetParent(parent);
}
```

<br/>

#### `SetScale()`
Sets the scale of the transform when called.

```csharp
public static Transform SetScale(this Transform transform, Transform parent);
```

```csharp
Transform transform;
Transform parent;

private void OnEnable()
{
    transform.SetScale(new Vector3(1,2,3));
}
```

<br/>

#### `AllChildren()`
Gets all the children transforms of the entered transform.

```csharp
public static IEnumerable<Transform> AllChildren(this Transform parent);
```

```csharp
Transform transform;

private void OnEnable()
{
    var children = transform.AllChildren();
}
```

<br/>

---

<br/>

[//]: #
[//]: #
[//]: # (TryGet extensions)
[//]: #
[//]: #

### `TryGet Extensions`
Just like the TryGetComponent, but with options to get from parents and children.

<br/>

### Methods

#### `TryGetComponents()`
Tries to get the component requested on the GameObject entered.

```csharp
public static bool TryGetComponents<T>(this Component target, out T[] component);
public static bool TryGetComponents(this Component target, Type type, out Component[] component);
public static bool TryGetComponents<T>(this GameObject target, out T[] component);
public static bool TryGetComponents(this GameObject target, Type type, out Component[] component);
```

```csharp
private void OnEnable()
{
    if (gameObject.TryGetComponents<MyComponent>(out var result))
    {
        // Logic to use the result here.    
    }
    else
    {
        // Logic to handle not getting any of `MyComponent` here.
    }
}
```

<br/>

#### `TryGetComponentInParent()`
Tries to get the component requested in the parent(s) of the GameObject entered.

```csharp
public static bool TryGetComponentInParent<T>(this Component target, out T component);
public static bool TryGetComponentInParent(this Component target, Type type, out Component component);
public static bool TryGetComponentInParent<T>(this GameObject target, out T component);
public static bool TryGetComponentInParent(this GameObject target, Type type, out Component component)
```

```csharp
private void OnEnable()
{
    if (gameObject.TryGetComponentInParent<MyComponent>(out var result))
    {
        // Logic to use the result here.    
    }
    else
    {
        // Logic to handle not getting any of `MyComponent` here.
    }
}
```

<br/>

#### `TryGetComponentsInParent()`
Tries to get the components requested in the parent(s) of the GameObject entered.

```csharp
public static bool TryGetComponentsInParent<T>(this Component target, out T[] component);
public static bool TryGetComponentsInParent(this Component target, Type type, out Component[] component);
public static bool TryGetComponentsInParent<T>(this GameObject target, out T[] component);
public static bool TryGetComponentsInParent(this GameObject target, Type type, out Component[] component);
```

```csharp
private void OnEnable()
{
    if (gameObject.TryGetComponentsInParent<MyComponent>(out var result))
    {
        // Logic to use the result here.    
    }
    else
    {
        // Logic to handle not getting any of `MyComponent` here.
    }
}
```

<br/>

#### `TryGetComponentInChildren()`
Tries to get the component requested in the children(s) of the GameObject entered.

```csharp
public static bool TryGetComponentInChildren<T>(this Component target, out T component);
public static bool TryGetComponentInChildren(this Component target, Type type, out Component component);
public static bool TryGetComponentInChildren<T>(this GameObject target, out T component);
public static bool TryGetComponentInChildren(this GameObject target, Type type, out Component component);
```

```csharp
private void OnEnable()
{
    if (gameObject.TryGetComponentInChildren<MyComponent>(out var result))
    {
        // Logic to use the result here.    
    }
    else
    {
        // Logic to handle not getting any of `MyComponent` here.
    }
}
```

<br/>

#### `TryGetComponentsInChildren()`
Tries to get the components requested in the children(s) of the GameObject entered.

```csharp
public static bool TryGetComponentsInChildren<T>(this Component target, out T[] component);
public static bool TryGetComponentsInChildren(this Component target, Type type, out Component[] component);
public static bool TryGetComponentsInChildren<T>(this GameObject target, out T[] component);
public static bool TryGetComponentsInChildren(this GameObject target, Type type, out Component[] component);
```

```csharp
private void OnEnable()
{
    if (gameObject.TryGetComponentsInChildren<MyComponent>(out var result))
    {
        // Logic to use the result here.    
    }
    else
    {
        // Logic to handle not getting any of `MyComponent` here.
    }
}
```

<br/>

---

<br/>

[//]: #
[//]: #
[//]: # (Vector extensions)
[//]: #
[//]: #

### `Vector Extensions`
A load of extensions for Unity Vector2/Vector3/Vector4.

<br/>

### Methods

#### `Add()`
Adds the two vectors together returning the result.

```csharp
public static Vector2 Add(this Vector2 vec2, Vector2 value);
public static Vector3 Add(this Vector3 vec3, Vector3 value);
public static Vector4 Add(this Vector4 vec4, Vector4 value);
```

```csharp
private void OnEnable()
{
    // newVec2 = (3,4)
    var newVec2 = Vector2.One.Add(new Vector2(2,3));
}
```

<br/>

#### `AddF()`
Adds the values to the input vector and returns the result.

```csharp
public static Vector2 Add2F(this Vector2 vec2, float? x = null, float? y = null);
public static Vector3 Add3F(this Vector3 vec3, float? x = null, float? y = null, float? z = null);
public static Vector4 Add4F(this Vector4 vec4, float? x = null, float? y = null, float? z = null, float? w = null);
```

```csharp
private void OnEnable()
{
    // newVec2 = (3,4)
    var newVec2 = Vector2.One.Add2F(2, 3));
}
```

<br/>

#### `Clamp()`
Clamps the vector to the entered vectors.

```csharp
public static Vector2 Clamp(this Vector2 vec2, Vector2 min, Vector2 max);
public static Vector3 Clamp(this Vector3 vec3, Vector3 min, Vector3 max);
public static Vector4 Clamp(this Vector4 vec4, Vector4 min, Vector4 max);
```

```csharp
private void OnEnable()
{
    // newVec2 = (2,3)
    var newVec2 = new Vector2(5,4).Clamp(Vector2.Zero, new Vector2(2,3));
}
```

<br/>

#### `ClampF()`
Clamps the vector to the entered values.

```csharp
public static Vector2 Clamp2F(this Vector2 vec2, float min, float max);
public static Vector3 Clamp3F(this Vector3 vec3, float min, float max);
public static Vector4 Clamp4F(this Vector4 vec4, float min, float max);
```

```csharp
private void OnEnable()
{
    // newVec2 = (2,3)
    var newVec2 = new Vector2(5,4).Clamp2F(2,3));
}
```

<br/>

#### `Copy()`
Copies the vector into a new instance.

```csharp
public static Vector2 Copy(this Vector2 vec2);
public static Vector3 Copy(this Vector3 vec3);
public static Vector4 Copy(this Vector4 vec4);
```

```csharp
private void OnEnable()
{
    var newVec2 = new Vector2(5,4);
    var copyOfVec2 = newVec2.Copy();
}
```

<br/>

#### `DirectionTo()`
Gets a line from the entered vectors.

```csharp
public static Vector2 DirectionTo(this Vector2 vec2, Vector2 target);
public static Vector3 DirectionTo(this Vector3 vec3, Vector3 target);
public static Vector4 DirectionTo(this Vector4 vec4, Vector4 target);
```

```csharp
private void OnEnable()
{
    var lineVec2 = new Vector2(5,4).DirectionTo(Vector2.Zero);
}
```

<br/>

#### `Divide()`
Divides the vector by another vector.

```csharp
public static Vector2 Divide(this Vector2 vec2, Vector2 value);
public static Vector3 Divide(this Vector3 vec3, Vector3 value);
public static Vector4 Divide(this Vector4 vec4, Vector4 value);
```

```csharp
private void OnEnable()
{
    // Returns (2.5, 2)
    var vec2 = new Vector2(5,4).Divide(Vector2.One * 2);
}
```

<br/>

#### `DivideF()`
Divides the vector by the entered x/y values.

```csharp
public static Vector2 Divide2F(this Vector2 vec2, float? x = null, float? y = null);
public static Vector3 Divide3F(this Vector3 vec3, float? x = null, float? y = null, float? z = null);
public static Vector4 Divide4F(this Vector4 vec4, float? x = null, float? y = null, float? z = null, float? w = null);
```

```csharp
private void OnEnable()
{
    // Returns (2.5, 2)
    var vec2 = new Vector2(5,4).Divide2F(2, 2);
}
```

<br/>

#### `EqualsF()`
Gets if the vector has equalling x/y values.

```csharp
public static bool Equals2F(this Vector2 vec2, float? x = null, float? y = null);
public static bool Equals3F(this Vector3 vec3, float? x = null, float? y = null, float? z = null);
public static bool Equals4F(this Vector4 vec4, float? x = null, float? y = null, float? z = null, float? w = null);
```

```csharp
private void OnEnable()
{
    // Returns false
    var matches = new Vector2(5,4).Equals2F(1,2);
    
    // Returns true
    matches = new Vector2(5,4).Equals2F(5,4);
}
```

<br/>

#### `Line()`
Gets a line from a to b.

```csharp
public static Vector2 Line(this Vector2 vec2, Vector2 target);
public static Vector2 Line(this Vector2 vec2, Vector2 target, float distance);
public static Vector3 Line(this Vector3 vec3, Vector3 target);
public static Vector3 Line(this Vector3 vec3, Vector3 target, float distance);
public static Vector4 Line(this Vector4 vec4, Vector4 target);
public static Vector4 Line(this Vector4 vec4, Vector4 target, float distance);
```

```csharp
private void OnEnable()
{
    // Returns (2.5, 2)
    var vec2 = new Vector2(5,4).Line(new Vector2(2, 2));
}
```

<br/>

#### `MidPoint()`
Gets a middle point of a line between a & b.

```csharp
public static Vector2 MidPoint(this Vector2 vec2, Vector2 target);
public static Vector3 MidPoint(this Vector3 vec3, Vector3 target);
public static Vector4 MidPoint(this Vector4 vec4, Vector4 target);
```

```csharp
private void OnEnable()
{
    // Returns (2.5, 2)
    var vec2 = new Vector2(5,4).Line(new Vector2(2, 2));
}
```

<br/>

#### `Multiply()`
Multiplies the vector by the entered vector.

```csharp
public static Vector2 Multiply(this Vector2 vec2, Vector2 value);
public static Vector3 Multiply(this Vector3 vec3, Vector3 value);
public static Vector4 Multiply(this Vector4 vec4, Vector4 value);
```

```csharp
private void OnEnable()
{
    // Returns (10, 8)
    var vec2 = new Vector2(5,4).Multiply(new Vector2(2, 2));
}
```

<br/>

#### `MultiplyScalar()`
Multiplies the vector by the entered float scale.

```csharp
public static Vector2 MultiplyScalar(this Vector2 vec2, float value);
public static Vector3 MultiplyScalar(this Vector3 vec3, float value);
public static Vector4 MultiplyScalar(this Vector4 vec4, float value);
```

```csharp
private void OnEnable()
{
    // Returns (10, 8)
    var vec2 = new Vector2(5,4).Multiply(2));
}
```

<br/>

#### `MultiplyF()`
Multiplies the vector by the entered x/y scale.

```csharp
public static Vector2 Multiply2F(this Vector2 vec2, float? x = null, float? y = null);
public static Vector3 Multiply3F(this Vector3 vec3, float? x = null, float? y = null, float? z = null);
public static Vector4 Multiply4F(this Vector4 vec4, float? x = null, float? y = null, float? z = null, float? w = null);
```

```csharp
private void OnEnable()
{
    // Returns (10, 8)
    var vec2 = new Vector2(5,4).Multiply2F(2, 2);
}
```

<br/>

#### `Set()`
Multiplies the vector by the entered x/y scale.

```csharp
public static Vector2 Set(this Vector2 vec2, float? x = null, float? y = null);
public static Vector3 Set(this Vector3 vec3, float? x = null, float? y = null, float? z = null);
public static Vector4 Set(this Vector4 vec4, float? x = null, float? y = null, float? z = null, float? w = null);
```

```csharp
private void OnEnable()
{
    // Returns (6, 6)
    var vec2 = new Vector2(5,4).Set(1, 2);
}
```

<br/>