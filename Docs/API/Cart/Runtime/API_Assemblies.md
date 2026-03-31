# Assemblies (API)

| [Usage](../../../../Docs/Usage/Cart/Runtime/Docs_Assemblies.md) | [API](API_Assemblies.md) |

Mainly adds a load of API to make accessing classes through assemblies a tad easier. Results of using this API should be cached to avoid massive performance hits.

|             |                     |
|-------------|:--------------------|
| Author      | `J, (Carter Games)` |
| Revision    | `2`                 |
| Last update | `2026-03-06`        |

<br/>

|            |                               |
|------------|:------------------------------|
| Assembly   | `CarterGames.Cart.Runtime`    |
| Namespace  | `CarterGames.Cart`            |

<br/>

### `AssemblyHelper.cs`
The main class to use. It provides the API for getting classes through assemblies. All methods have the option to check internally only, which will only check inside the cart's own assemblies if set to true.

<br/>

### Methods

#### `CountClassesOfType()`
Gets the number of classes of the requested type in the project.

```csharp
public static int CountClassesOfType<T>(bool internalCheckOnly = false);
public static int CountClassesOfType<T>(params Assembly[] assemblies);
```

```csharp
private void OnEnable()
{
    int numberOfClass = AssemblyHelper.CountClassesOfType<MyClassType>();
}
```

<br/>

#### `GetClassesOfType()`
Gets all the classes of the passes in type found in the project. This won't include `abstract` classes.

```csharp
public static IEnumerable<T> GetClassesOfType<T>(bool internalCheckOnly = false);
public static IEnumerable<T> GetClassesOfType<T>(params Assembly[] assemblies);
```

```csharp
private void OnEnable()
{
    var classesOfType = AssemblyHelper.GetClassesOfType<MyClassType>();
}
```

<br/>

#### `GetClassesNamesOfType()`
Gets all the class types that inherit from the entered type.

```csharp
public static IEnumerable<Type> GetClassesNamesOfType<T>(bool internalCheckOnly = false);
```

```csharp
private void OnEnable()
{
    var classTypesOfType = AssemblyHelper.GetClassesNamesOfType<MyClassType>();
}
```

<br/>

#### `GetClassesNamesOfBaseType()`
Gets all the class names of the entered type in the project that use the base type. Similar to `GetClassesNamesOfType()` but doesn't need to know the exact type when called.

```csharp
public static IEnumerable<Type> GetClassesNamesOfBaseType(Type baseType, bool internalCheckOnly = false);
```

```csharp
private void OnEnable()
{
    var classTypesOfType = AssemblyHelper.GetClassesNamesOfType(typeof(MyClassType));
}
```

<br/>

---

<br/>

### `AssemblyClassDef.cs`
The event listener can be used to listen to events if a boolean is not true. Either through the `Evt` instance itself of via the `EvtListener` class.

<br/>