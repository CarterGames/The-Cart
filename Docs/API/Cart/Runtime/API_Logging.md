# Logging (API)

| [Usage](../../../../Docs/Usage/Cart/Runtime/Docs_Logging.md) | [API](API_Logging.md) |

Provides a logging system that can be toggled and modularized to show only certain categories of logs at a time.

|             |              |
|-------------|:-------------|
| Revision    | `1`          |
| Last update | `2026-04-28` |

<br/>

|                  |     |
|------------------|:----|
| Assembly         | `CarterGames.Cart.Runtime`  |
| Namespace        | `CarterGames.Cart`  |

<br/>

### `CartLogger`
The logger class that handles sending logs out to the console.

<br/>

### Methods

#### `Log`
Displays a normal debug message.

```csharp
public static void Log<LogCategory>(object message, UnityEngine.Object ctx = null, bool editorOnlyLog = false)
public static void Log<LogCategory>(object message, Type additionalContext, UnityEngine.Object ctx = null, bool editorOnlyLog = false)
```

```csharp
private void OnEnable()
{
    CartLogger.Log<MyLogs>("Hello");
}
```

<br/>

#### `LogWarning`
Displays a warning debug message.

```csharp
public static void LogWarning<LogCategory>(object message, UnityEngine.Object ctx = null, bool editorOnlyLog = false)
public static void LogWarning<LogCategory>(object message, Type additionalContext, UnityEngine.Object ctx = null, bool editorOnlyLog = false)
```

```csharp
private void OnEnable()
{
    CartLogger.LogWarning<MyLogs>("Hello");
}
```

<br/>

#### `LogError`
Displays a error debug message.

```csharp
public static void LogError<LogCategory>(object message, UnityEngine.Object ctx = null, bool editorOnlyLog = false)
public static void LogError<LogCategory>(object message, Type additionalContext, UnityEngine.Object ctx = null, bool editorOnlyLog = false)
```

```csharp
private void OnEnable()
{
    CartLogger.LogError<MyLogs>("Hello");
}
```

<br/>


### `LogCategory`
A base class to inherit from to create custom categories for the logs system to use.

```csharp
public class MyLogs : LogCategory { }
```

<br/>
