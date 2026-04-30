# Object Pooling

| [Usage](Docs_ObjectPooling.md) | [API](../../../../Docs/API/Cart/Runtime/API_ObjectPooling.md) |

Provides a system to pool objects in expandable object pools in code.

|             |              |
|-------------|:-------------|
| Revision    | `1`          |
| Last update | `2026-04-30` |

<br/>

### Making a pool
To make a new object pool, simply make a field or property of a type of `ObjectPoolBase` from the library.
Currently, there are built-in implementations for:
- `ObjectPoolGameObject` for GameObject related pools.
- `ObjectPoolGeneric` for GameObject related pools where you want a particular component as the reference value.

You will need to initialize the class for the pool to be usable. This can be done through its constructor.
Once ready the pool will handle getting or making new elements of the pool as well as returning them when ready.

<br/>

#### Example
```csharp
public GameObject prefab;
public Transform parent;

private ObjectPoolGameObject pool;


private void OnEnable()
{
    // Initializing the pool for use.
    pool = new ObjectPoolGameObject(prefab, parent);
}
```

<br/>

### Getting elements
To get the next free element of a pool, just call the `Assign()` method of the pool.
This will return the next free element. If none are free, and the pool can expand, the pool
will auto-create a new member and return it. If restricted in size the result will be null.
Check is there are any free members by checking the `FreeMembers` property for any elements.

#### Example
```csharp
public GameObject prefab;
public Transform parent;

private ObjectPoolGameObject pool;


private void OnEnable()
{
    // Initializing the pool for use.
    pool = new ObjectPoolGameObject(prefab, parent);
    
    // Gets the next free element.
    var element = pool.Assign();
    
    // Safely checks to see if there is a free element before using it.
    if (pool.FreeMembers.Count > 0)
    {
        element = pool.Assign();
    }
}
```

<br/>

### Returning elements
Returning elements to the pool is done manually for now. You will need to call the `Return()` method
on pool to return each element when you are finished with it. Alternatively you can reset an entire pool
with the `Reset()` method. 

#### Example
```csharp
public GameObject prefab;
public Transform parent;

private ObjectPoolGameObject pool;


private void OnEnable()
{
    // Initializing the pool for use.
    pool = new ObjectPoolGameObject(prefab, parent);
    
    // Gets the next free element.
    var element = pool.Assign();
    
    // Returns the element to the pool.
    pool.Return(element);
    
    // Resets the entire pool
    pool.Reset();
}
```