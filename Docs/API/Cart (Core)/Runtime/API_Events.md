# Events System (API)

---
| [Usage](../../../../Docs/Usage/Cart%20(Core)/Runtime/Docs_Events.md) | [API](API_Events.md) |

The events system is a extension to system actions that makes sure no method is over-subscribed to. It also supports anonymous subscriptions which you can unsubscribe just like a normal subscription.

---

## Details

|   |                            |
|---|:---------------------------|
| Assembly | `CarterGames.Cart.Runtime` |
| Namespace | `CarterGames.Cart.Events`  |

---
<br/>

---

## Definition
As Evt is a class you’ll need to create an instance of it for use, otherwise it’ll return null and do nothing.

---

```csharp
private readonly Evt MyEvent = new Evt();
```

---
<br/>


### Methods

---

#### Add()
Adds an listener to the evt instance. Note that it must have matching parameters to correctly subscribe. 

---

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

---
<br/>

---

#### AddAnonymous()
Adds an anonymous listener to the evt instance. The parameters do not need to match when adding anonymous actions to run on an event. Though it is still advised to match them where possible. 

---

```csharp
public void AddAnonymous(string id, Action listener);
public void AddAnonymous(string id, Action<T> listener);
public void AddAnonymous(string id, Action<T1,T2> listener);
public void AddAnonymous(string id, Action<T1,T2,T3> listener);
public void AddAnonymous(string id, Action<T1,T2,T3,T4> listener);
public void AddAnonymous(string id, Action<T1,T2,T3,T4,T5> listener);
public void AddAnonymous(string id, Action<T1,T2,T3,T4,T5,T6> listener);
public void AddAnonymous(string id, Action<T1,T2,T3,T4,T5,T6,T7> listener);
public void AddAnonymous(string id, Action<T1,T2,T3,T4,T5,T6,T7,T8> listener);
```

```csharp
Evt MyEvent = new Evt();

private void OnEnable()
{
    MyEvent.AddAnonymous("MyMethod", MyMisMatchedMethod(100));
}

private void MyMisMatchedMethod(int health)
{
    // My logic here...
}
```

---
<br/>

---

#### Remove()
Removes a listener to the evt instance. Note that it must have matching parameters to correctly unsubscribe. 

---

```csharp
public void Remove(Action listener);
public void Remove(Action<T> listener);
public void Remove(Action<T1,T2> listener);
public void Remove(Action<T1,T2,T3> listener);
public void Remove(Action<T1,T2,T3,T4> listener);
public void Remove(Action<T1,T2,T3,T4,T5> listener);
public void Remove(Action<T1,T2,T3,T4,T5,T6> listener);
public void Remove(Action<T1,T2,T3,T4,T5,T6,T7> listener);
public void Remove(Action<T1,T2,T3,T4,T5,T6,T7,T8> listener);
```

```csharp
Evt MyEvent = new Evt();

private void OnEnable()
{
    MyEvent.Remove(MyMethod);
}

private void MyMethod()
{
    // My logic here...
}
```

---
<br/>

---

#### RemoveAnonymous()
Removes an anonymous listener to the evt instance. You only need to pass the key you assigned to the anonymous event listener to remove it.

---

```csharp
public void RemoveAnonymous(string id);
```

```csharp
Evt MyEvent = new Evt();

private void OnEnable()
{
    MyEvent.RemoveAnonymous("MyMethod");
}
```

---
<br/>

---

#### Raise()
Raises/Invokes the evt which will run all the listeners currently subscribed to it. If your event takes parameters you’ll need to pass through their values here.

---

```csharp
public void Raise();
public void Raise(T param);
public void Raise(T1 param1, T2 param2);
public void Raise(T1 param1, T2 param2, T3 param3);
public void Raise(T1 param1, T2 param2, T3 param3, T4 param4);
public void Raise(T1 param1, T2 param2, T3 param3, T4 param4, T5 param5);
public void Raise(T1 param1, T2 param2, T3 param3, T4 param4, T5 param5, T6 param6);
public void Raise(T1 param1, T2 param2, T3 param3, T4 param4, T5 param5, T6 param6, T7 param7);
public void Raise(T1 param1, T2 param2, T3 param3, T4 param4, T5 param5, T6 param6, T7 param7, T8 param8);
```

```csharp
Evt MyEvent = new Evt();

private void OnEnable()
{
    MyEvent.Raise();
}
```

---
<br/>

---

#### Clear()
Clears all the listeners from this evt. 

---

```csharp
public void Clear();
```

```csharp
Evt MyEvent = new Evt();

private void OnEnable()
{
    MyEvent.Clear();
}
```

---
<br/>