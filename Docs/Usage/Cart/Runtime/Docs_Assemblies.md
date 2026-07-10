# Assemblies

| [Usage](Docs_Assemblies.md) | [API](../../../../Docs/API/Cart/Runtime/API_Assemblies.md) |

Mainly adds a load of API to make accessing classes through assemblies a tad easier.

|             |                     |
|-------------|:--------------------|
| Author      | `J, (Carter Games)` |
| Revision    | `3`                 |
| Last update | `2026-07-04`        |

<br/>

### Assembly Helper
The main class to use. It provides the API for getting classes through assemblies.

> 🚧 It is highly recommended that you cache the result of any queries to avoid performance issues.

Example, getting all classes of type `MyClassType`

``` csharp
private void OnEnable()
{
    var myClasses = AssemblyHelper.GetClassesOfType<MyClassType>();
}
```

<br/>

### Assembly Class Def
Is a wrapper setup that lets you store a `class` reference based on its assembly / class name in a serializable state. Its not a perfect solution as if the assembly/class name referenced changes it will lose its reference.