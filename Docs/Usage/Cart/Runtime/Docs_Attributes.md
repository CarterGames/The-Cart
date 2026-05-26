# Attributes

The library has some attributes you can apply in your project for quality of life. See more below.

|             |            |
|-------------|:-----------|
| Revision    | `1`         |
| Last update | `2026-04-01`         |

<br/>


### `[ReadOnly]`
Disables the GUI of fields or serialized properties in the inspector so you cannot edit them at runtime. 

> <b>Note:</b> this is a little limited and will not fully restrict edits.

```csharp
[SerializedField] [Readonly] private bool exampleField;
```
