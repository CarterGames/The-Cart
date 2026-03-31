# Dev Environments (API)

| [Usage](Docs_DevEnvironments.md) | [API](API_DevEnvironments.md) |

Dev environments is designed to give you a way to define different build types in code and limit some functionality to those environments.


|             |                     |
|-------------|:--------------------|
| Author      | `J, (Carter Games)` |
| Revision    | `1`                 |
| Last update | `2025-08-10`        |

<br/>

|                  |                                           |
|------------------|:------------------------------------------|
| Assembly         | `CarterGames.Cart.Crates`                 |
| Namespace        | `CarterGames.Cart.Crates.DevEnvironments` |
| Scripting Define | `CARTERGAMES_CART_CRATE_DEVENVIRONMENTS`  |

<br/>

### `Environment detection.cs`
Use the static ```EnvironmentDetection``` class to get which environment is currently in use in code.

<br/>

### Properties

#### `CurrentEnvironment {get}`
Gets the environment the project is currently targeting.

```csharp
EnvironmentDetection.CurrentEnvironment { get; }
```

```csharp
private void OnEnable()
{
    Debug.Log(EnvironmentDetection.CurrentEnvironment);
}
```
