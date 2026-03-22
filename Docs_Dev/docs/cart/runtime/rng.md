---
id: cart_runtime_rng
displayed_sidebar: cart
sidebar_position: 2
sidebar_label: 'Random (Rng)'
tags:
  - Random
---

# Random

Rng is one of the many core systems which handles generating random numbers, string etc. The set-up lets you choose which provider is used to produce the random results. Some are true random while others are seeded random (where a seed can produce the same result each time run). 

## Providers
### Settings
You can edit the random system settings in the library settings provider. This can be found under:
```
Edit/Project Settings/Carter Games/The Cart
```

There is currently only the option to choose your provider in the settings. To select a provider, just press the select provider button and choose the option you want from the search box that opens. Once you select an option the setting will update to use that provider.

![random_settings](img/random_settings.png)

If you choose a provider that uses a seed. The option to change the seed will be shown under the provider.

<br/>
### Types
There are two main random types supported. ``Seeded``, where the random result is consistent based on a seed input or ``True`` which is random each time and does not have a seed to re-produce results with. You can set the random provider used in the settings provider for the library.

<br/>
### Built-in
The default providers in the project are:

| Provider | Type | Description |
| ----- | ----- | :----- |
| Unity | True | Uses the Unity API ``Random.Range`` to produce random results.
| System | Seeded | Uses C# system namespace random setup to generate random results.
| Alea | Seeded | A Unity C# clone of Alea (PRNG) which can be produced in other code languages as well. 

<br/>
### Custom
If you want to implement your own provider of random you can make a non-static class implementing the `IRngProvider` interface and implement its methods.

If your random provider is a seeded one, please use the `ISeededRngProvider` interface instead.

You’ll be able to select your new provider in the asset settings provider when you implement the interface. The interface only has a couple of methods to implement, the rest of the Rng API is made from these methods:

Common:

| Property | Description |
| --- | :--- |
| ``Bool {get}`` | Should return a random bool result. |

| Method | Description |
| --- | :--- |
| ``Int()`` | Should return a random int between the min and max values inclusive.. |
| ``Float()`` | Should return a random float between the min and max values inclusive. |
| ``Double()`` | Should return a random double between the min and max values inclusive. |

Seeded Only:

| Method | Description |
| --- | :--- |
| ``GenerateSeed()`` | Should return a new seed for the provider to use. |
<br/>
## Usage
The ``Rng`` class is the static class intended to be used to access the random provider in use. This should be used when interacting with this system of the library.

```csharp
private void OnEnable()
{
    Debug.Log(Rng.String(16)); // Generates a random string of 16 characters in lenght.
    Debug.Log(Rng.Int(1, 10)); // Generates a random number between 1 - 10.
}
```
<br/>
### Anonymous subscriptions
---
You can also subscribe to events without matching the parameters required using the anonymous setup. This setup has the same add and remove methods but taking in a string key for the anonymous subscription to be identified as. On removal we just pass in the key we assigned on creation. Some examples: 

```csharp
private void OnEnable()
{
    OnGameOver.AddAnonymous("MySub", () => MyIntMethod(100));
}

private void OnDisable()
{
    OnGameOver.RemoveAnonymous("MySub");
}

private void MyIntMethod(int amount)
{
    // Some code here...
}
```
<br/>
### Event raising/invoking
---
Raising events is essentially invoking the action. It is done by just calling the `Raise()` method on the event you want to call. You pass in any params the event requires in the call to pass them to all listeners. 

```csharp
private void OnSomeGameStateChange()
{
    OnGameOver.Raise();
    OnMoneyCollected.Raise(money);
}
```

<br/>
## Evt Listener

The event listener class provides a method to listen to events based on if a boolean state at the time of executing. If false it subscribes to the event, if true it instantly continues the desired action. There is an extension method for the evt class itself or static methods to be called without the event itself.



<br/>
## Scripting API

---

Assembly: ```CarterGames.Cart.Runtime```

Namespace: ```CarterGames.Cart.Events```

<br/>
### Definition

---

```csharp
private readonly Evt MyEvent = new Evt();
```

As Evt is a class you’ll need to create an instance of it for use, otherwise it’ll return null and do nothing. 

<br/>

---

### Add()

---

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

---

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

### AddAnonymous()

---

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

---

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

### Remove()

---

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

---

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

### RemoveAnonymous()

---

Removes an anonymous listener to the evt instance. You only need to pass the key you assigned to the anonymous event listener to remove it.

---

```csharp
public void RemoveAnonymous(string id);
```

---

```csharp
Evt MyEvent = new Evt();

private void OnEnable()
{
	MyEvent.RemoveAnonymous("MyMethod");
}
```

### Raise()

---

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

---

```csharp
Evt MyEvent = new Evt();

private void OnEnable()
{
	MyEvent.Raise();
}
```

### Clear()

---

Clears all the listeners from this evt. 

---

```csharp
public void Clear();
```

---

```csharp
Evt MyEvent = new Evt();

private void OnEnable()
{
	MyEvent.Clear();
}
```
