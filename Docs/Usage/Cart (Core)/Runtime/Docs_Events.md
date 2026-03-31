# Events System

| [Usage](Docs_Events.md) | [API](../../../../Docs/API/Cart%20(Core)/Runtime/API_Events.md) |

The events system is a extension to system actions that makes sure no method is over-subscribed to. It also supports anonymous subscriptions which you can unsubscribe just like a normal subscription.

|             |                     |
|-------------|:--------------------|
| Author      | `J, (Carter Games)` |
| Revision    | `5`                 |
| Last update | `2026-03-06`        |

<br/>

### Event creation
To get started with the events system you'll need to first create an event. It's recommended to use a static class or make the event itself static where possible for ease of access. To define an event, write something like the example below:

```csharp
public static readonly Evt OnGameOver = new Evt();
```

Note that the Evt class is a class for you’ll need to make an instance of it before using it, this is the only main difference between this setup and the normal system action flow. Making the event read-only just helps us not edit it accidentally and defining it as a new event is just cleaner as it won't need to be initialized anywhere else before use.

For events with parameters there are 8 extra classes in the `Evt.cs` file that allow for it. If you need more parameters, you can make additional ones as they all follow the same setup, just with an extra generic field to use in the class. To define a parameter event, use something like the following:

```csharp
public static readonly Evt<int> OnMoneyCollected = new Evt<int>();
```
<br/>

### Event subscribing
Subscribing to events is as easy as it is to do with actions. Instead of using `+=` or `-=` to subscribe, the events system has 2 simple methods, being `Add()` & `Remove()`. Note that any method subscribing to an event will need to have the same number of parameters of the same type to subscribe correctly. Some examples:

```csharp
private void OnEnable()
{
    OnGameOver.Add(MyVoidMethod);
}

private void OnDisable()
{
    OnGameOver.Remove(MyVoidMethod);
}

private void MyVoidMethod()
{
    // Some code here...
}
```

```csharp
private void OnEnable()
{
    OnMoneyCollect.Add(MyIntMethod);
}

private void OnDisable()
{
    OnMoneyCollected.Remove(MyIntMethod);
}

private void MyIntMethod(int amount)
{
    // Some Code Here...
}
```
<br/>

### Anonymous subscriptions
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
Raising events is essentially invoking the action. It is done by just calling the `Raise()` method on the event you want to call. You pass in any params the event requires in the call to pass them to all listeners. 

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