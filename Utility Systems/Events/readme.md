![Events](https://user-images.githubusercontent.com/33253710/157115872-e3c652ca-1657-47db-95e0-483357371bed.jpg)

<p align=center>The events system is a simple extension to system actions that makes sure no method is over-subscribed to.</p>

# Usage
## Event Creation
To get started with the events system you'll need to first create an event. This can either be in the <code>Events.cs</code> class provided in this library or in any static class of your own. It's recommended to use a static class for ease of access to the events. To define an event, write something like the examples below:

![evt1](https://user-images.githubusercontent.com/33253710/156997440-e86737ad-98ed-43fd-a512-c72343a90104.png)

Making the events readonly just helps us not edit it accidentally and defining it as a new event is just cleaner as it won't need to be initialized anywhere else before use. 

For events with parameters there are 8 extra classes in the <code>Evt.cs</code> file that allow for it. If you need more parameters, you can make additional ones as they all follow the same setup, just with an extra generic field to use in the class. To define a parameter event, use something like the following:

![evt2](https://user-images.githubusercontent.com/33253710/156997458-682286c1-4d1d-428d-afc7-d5caf055a046.png)
  
## Event Subscribing
  Subscribing to events is as easy as it is to do with actions. Instead of using <code>+=</code> or <code>-=</code> to subscribe, the events system has 2 simple methods, being <code>Add()</code> & <code>Remove()</code>. Note that any method subscribing to an event will need to have the same number of parameters of the same type to subscribe correctly. Some examples using the same events defined in the creation section above: 

![Events Example](https://user-images.githubusercontent.com/33253710/156997490-b1e43722-caa3-4387-b5d7-dc46a54edf2a.png)  ![Events Example 2](https://user-images.githubusercontent.com/33253710/157088331-2b9d4020-7ddc-4a8d-9c6d-47acbdc7e008.png)



## Event Raising
Raising events is essentially invoking the action. It is done by just calling the <code>Raise()</code> method on the event you want to call. Using the examples from above again to showcase this:

![Events Example 3](https://user-images.githubusercontent.com/33253710/157101197-7b95ee94-a0d5-4893-8677-7be6e2ca98c0.png)
