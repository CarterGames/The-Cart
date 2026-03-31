# TEMPLATE (API)

| [Usage]() | [API]() |

Summary of system here.

|             |            |
|-------------|:-----------|
| Author      | ``         |
| Revision    | ``         |
| Last update | ``         |

<br/>

|                  |     |
|------------------|:----|
| Assembly         | ``  |
| Namespace        | ``  |
| Scripting Define | ``  | // This entry is optional, remove if none are used.

<br/>

### `Class name`
class desc

<br/>

### Fields
### Properties
### Events
### Methods

Example Entry, always space with a br between each chunk.

#### `Add()`
Adds an listener to the evt instance. Note that it must have matching parameters to correctly subscribe.


```csharp
public void Add(Action listener);
public void Add(Action<T> listener);
public void Add(Action<T1,T2> listener);
public void Add(Action<T1,T2,T3> listener);
public void Add(Action<T1,T2,T3,T4> listener);
public void Add(Action<T1,T2,T3,T4,T5> listener);
public void Add(Action<T1,T2,T3,T4,T5,T6> listener);
public void Add(Action<T1,T2,T3,T4,T5,T6,T7> listener);
public void Add(Action<T1,T2,T3,T4,T5,T6,T7,T8> listener);
```

```csharp
Evt MyEvent = new Evt();

private void OnEnable()
{
    MyEvent.Add(MyMethod);
}

private void MyMethod()
{
    // My logic here...
}
```

<br/>