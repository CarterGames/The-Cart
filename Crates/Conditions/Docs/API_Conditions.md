# Conditions (API)

| [Usage](Docs_Conditions.md) | [API](API_Conditions.md) |

The conditions system is designed to let you define boolean checks based on runtime criteria. Useful for abstracting checks from their systems and making them easier to debug.


|             |                     |
|-------------|:--------------------|
| Author      | `J, (Carter Games)` |
| Revision    | `3`                 |
| Last update | `2026-??-??`        |

<br/>

|                  |                                           |
|------------------|:------------------------------------------|
| Assembly         | `CarterGames.Cart.Crates`                 |
| Namespace        | `CarterGames.Cart.Crates.Conditions` |
| Scripting Define | `CARTERGAMES_CART_CRATE_CONDITIONS`  |

<br/>

### `ConditionManager.cs`
Use as your main access to the conditions crate. Almost all of your code will call this class when accessing this system. 

<br/>

### Properties

#### `IsInitialized`
Gets is the conditions setup is ready for use. Make sure you query this before calling any of the conditions API at runtime to avoid issues. 

```csharp
public static bool IsInitialized { get; }
```

```csharp
private void OnEnable()
{
    Debug.Log(ConditionManager.IsInitialized);  // Logs out if the system has initialized yet.
}
```

### Events

#### `InitializedEvt`
An `Evt` instance that is raised when the conditions setup is initialized successfully.

```csharp
public static Evt InitializedEvt;
```

```csharp
private void OnEnable()
{
    if (ConditionManager.IsInitialized)
    {
        OnConditonsInit(); // Conditions already initialized, so no need to listen for it.
    }
    else
    {
        ConditionManager.InitializedEvt.Add(OnConditonsInit);
    }
    
    return;
    
    void OnConditonsInit()
    {
        ConditionManager.InitializedEvt.Remove(OnConditonsInit);
        // Your logic to run on conditions init here.
    }
}
```
