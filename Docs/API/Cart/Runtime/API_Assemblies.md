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

### Constructor

Creates a new definition when called.

```csharp
public AssemblyClassDef(string assembly, string type)
```

```csharp
private void OnEnable()
{
    AssemblyClassDef classDef = new AssemblyClassDef(myType.Assembly.FullName, myType.FullName)); // Makes a new define.
}
```

<br/>

### Properties

#### `IsValid`
Gets if the class in the define is valid or not.

```csharp
public bool IsValid { get; }
```

```csharp
[SerializeField] private AssemblyClassDef classDef;

private void OnEnable()
{
    Debug.Log(classDef.IsValid); // Logs out if the define is valid.
}
```

<br/>

#### `StoredAssembly`
Gets the assembly string stored in the define.

```csharp
public string StoredAssembly { get; }
```

```csharp
[SerializeField] private AssemblyClassDef classDef;

private void OnEnable()
{
    // Logs out if the assembly in the define.
    Debug.Log(classDef.StoredAssembly);
}
```

<br/>

#### `StoredType`
Gets the type string stored in the define.

```csharp
public string StoredType { get; }
```

```csharp
[SerializeField] private AssemblyClassDef classDef;

private void OnEnable()
{
    // Logs out if the type in the define.
    Debug.Log(classDef.StoredType);
}
```

<br/>

#### `StoredAssemblyQualified`
The assembly qualified string stored.

```csharp
public string StoredAssemblyQualified { get; }
```

```csharp
[SerializeField] private AssemblyClassDef classDef;

private void OnEnable()
{
    // Logs out if the assembly qualified type in the define.
    Debug.Log(classDef.StoredAssemblyQualified);
}
```

<br/>

### Methods

#### `TryGetType()`
Tries to get the type stored.

```csharp
public bool TryGetType(out Type typeStored)
```

```csharp
private void OnEnable()
{
    AssemblyClassDef classDef = new AssemblyClassDef(myType.Assembly.FullName, myType.FullName));
    classDef.TryGetType(out var type);
    Debug.Log(type);
}
```

<br/>

#### `GetTypeInstance<T>()`
Gets the type stored in the define for use as an instance for use.

```csharp
public T GetTypeInstance<T>()
```

```csharp
private void OnEnable()
{
    AssemblyClassDef classDef = new AssemblyClassDef(myType.Assembly.FullName, myType.FullName));
    MyType typeFromDefine = classDef.GetTypeInstance<MyType>();
}
```

<br/>

#### `IsDefineType()`
Gets if a type is the same as this assembly class define.

```csharp
public bool IsDefineType(Type type)
```

```csharp
private void OnEnable()
{
    AssemblyClassDef classDef = new AssemblyClassDef(myType.Assembly.FullName, myType.FullName));
    
    if (classDef.IsDefineType(typeOf(myType)))
    {
        // Your logic here...    
    }
}
```

<br/>

#### `InheritsFrom()`
Gets if the type entered is a base class of the stored value.

```csharp
public bool InheritsFrom(Type type)
```

```csharp
private void OnEnable()
{
    AssemblyClassDef classDef = new AssemblyClassDef(myType.Assembly.FullName, myType.FullName));
    
    if (classDef.InheritsFrom(typeOf(myOtherType)))
    {
        // Your logic here...    
    }
}
```

<br/>


### Operators

```csharp
public static implicit operator AssemblyClassDef(Type type)
```

```csharp
private void OnEnable()
{
    Type myType;
    AssemblyClassDef classDef = myType;
}
```

<br/>