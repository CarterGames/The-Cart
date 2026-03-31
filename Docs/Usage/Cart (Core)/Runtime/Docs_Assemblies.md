# Assemblies

| [Usage](Docs_Assemblies.md) | [API](../../../../Docs/API/Cart%20(Core)/Runtime/API_Assemblies.md) |

Mainly adds a load of API to make accessing classes through assemblies a tad easier. Results of using this API should be cached to avoid massive performance hits.

|             |                     |
|-------------|:--------------------|
| Author      | `J, (Carter Games)` |
| Revision    | `2`                 |
| Last update | `2026-03-06`        |

<br/>

### Assembly Helper
The main class to use. It provides the API for getting classes through assemblies. 

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