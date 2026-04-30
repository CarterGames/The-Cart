# Object Pooling (API)

| [Usage](../../../../Docs/Usage/Cart/Runtime/Docs_ObjectPooling.md) | [API](API_ObjectPooling.md) |

Provides a system to pool objects in expandable object pools in code.

|             |              |
|-------------|:-------------|
| Revision    | `1`          |
| Last update | `2026-04-30` |

<br/>

|                  |     |
|------------------|:----|
| Assembly         | `CarterGames.Cart.Runtime`  |
| Namespace        | `CarterGames.Cart`  |

<br/>

### `ObjectPoolBase`
The base class for object pool. All variants use the same API below:

<br/>

### Properties

#### `IsInitialized`
Gets if the pool is ready for use.

```csharp
public bool IsInitialized { get; }
```

```csharp
public GameObject prefab;
public Transform parent;

private ObjectPoolGameObject pool;


private void OnEnable()
{
    // Initializing the pool for use.
    pool = new ObjectPoolGameObject(prefab, parent);
    
    Debug.Log(pool.IsInitialized);
}
```

<br/>

#### `ShouldExpand`
Defines if the pool should auto-expand. By default, this is true.

```csharp
public bool ShouldExpand { get; set; } = true;
```

```csharp
public GameObject prefab;
public Transform parent;

private ObjectPoolGameObject pool;


private void OnEnable()
{
    // Initializing the pool for use.
    pool = new ObjectPoolGameObject(prefab, parent);
    
    Debug.Log(pool.ShouldExpand);
    
    // Disables auto-expansion.
    pool.ShouldExpand = false;
}
```

<br/>

#### `AllMembers`
Gets a collection of all the members of the pool.

```csharp
public IReadOnlyCollection<T> AllMembers { get; }
```

```csharp
public GameObject prefab;
public Transform parent;

private ObjectPoolGameObject pool;


private void OnEnable()
{
    // Initializing the pool for use.
    pool = new ObjectPoolGameObject(prefab, parent);
    
    // Logs out the total number of members.
    Debug.Log(pool.AllMembers.Count);
}
```

<br/>

#### `AllInUse`
Gets all the in use members of the pool.

```csharp
public IReadOnlyCollection<T> AllInUse { get; }
```

```csharp
public GameObject prefab;
public Transform parent;

private ObjectPoolGameObject pool;


private void OnEnable()
{
    // Initializing the pool for use.
    pool = new ObjectPoolGameObject(prefab, parent);
    
    // Logs out the total number of members in use currently.
    Debug.Log(pool.AllInUse.Count);
}
```

<br/>

#### `FreeMembers`
Gets all the free members of the pool.

```csharp
public IReadOnlyCollection<T> FreeMembers { get; }
```

```csharp
public GameObject prefab;
public Transform parent;

private ObjectPoolGameObject pool;


private void OnEnable()
{
    // Initializing the pool for use.
    pool = new ObjectPoolGameObject(prefab, parent);
    
    // Logs out the total number of members not in use currently.
    Debug.Log(pool.FreeMembers.Count);
}
```

<br/>

### Methods

#### `Assign`
Gets the next free element of the pool, or creates a new entry in the pool and returns it
if the pool is allowed to expand.

```csharp
public virtual T Assign()
```

```csharp
public GameObject prefab;
public Transform parent;

private ObjectPoolGameObject pool;


private void OnEnable()
{
    // Initializing the pool for use.
    pool = new ObjectPoolGameObject(prefab, parent);
    
    // Gets the next free element for use.
    var element = pool.Assign();
}
```

<br/>

#### `Return`
Returns an element to the pool when called so it can be re-used again.

```csharp
public virtual void Return(T member)
```

```csharp
public GameObject prefab;
public Transform parent;

private ObjectPoolGameObject pool;


private void OnEnable()
{
    // Initializing the pool for use.
    pool = new ObjectPoolGameObject(prefab, parent);
    
    // Gets the next free element for use.
    var element = pool.Assign();
    
    // Returns an element to the pool.
    pool.Return(element);
}
```

<br/>

#### `Reset`
Resets all pool members to be inactive by returning any active.

```csharp
public void Reset()
```

```csharp
public GameObject prefab;
public Transform parent;

private ObjectPoolGameObject pool;


private void OnEnable()
{
    // Initializing the pool for use.
    pool = new ObjectPoolGameObject(prefab, parent);
    
    // Gets the next free element for use.
    var element = pool.Assign();
}


private void OnDisable()
{
    // Reset the pool.
    pool.Reset();
}
```