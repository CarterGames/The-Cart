# Checks

| [Usage](Docs_Checks.md) | [API](../../../../Docs/API/Cart/Runtime/API_Checks.md) |

Checks are a smaller setup to help with ensuring certain references are not null or that 
booleans are in the right state when intended. If not then the setup will throw an exception 
for you before any other logic is run.

|             |              |
|-------------|:-------------|
| Revision    | `1`          |
| Last update | `2026-05-26` |

<br/>


### PreReq
This is the main class for the system. You can currently use it just for null reference 
checks and boolean checks. More may come in the future. You use it like you would any
return statements, but you don't need to add a return in this instance as it'll throw
an exception instead.

```csharp
public GameObject myReference;
public GameObject myOtherReference;

private void OnEnable()
{
    PreReq.DisallowIfNull(myReference, "Cannot process as null ref.");
    PreReq.DisallowIfNull(myOtherReference, "Cannot process as null ref.");
    
    // All good at this point to use either reference as they are not null.
}
```

For boolean checks, you can use the same class. If you boolean check has a lot of criteria
you want to try out the `Conditions` crate in the library. It provides a more modular setup
for checking multiple criteria from the editor instead.

```csharp
public GameObject myReference;
public bool myBool;
public bool myOtherBool;

private void OnEnable()
{
    PreReq.DisallowIfNull(myReference, "Cannot process as null ref.");
    PreReq.DisallowIfFalse(myBool, "Cannot procees as bool is false, should be true.");
    PreReq.DisallowIfTrue(myOtherBool, "Cannot procees as bool is true, should be false.");
    
    // All good at this point to use either reference as they are not null.
}
```