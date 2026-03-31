# Random
| [Usage](Docs_RNG.md) | [API](../../../../Docs/API/Cart/Runtime/API_RNG.md) |

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

