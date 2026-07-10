# Search

An set-up to handle making search providers for data to make it easier to select data outside of using enums.

|             |                     |
|-------------|:--------------------|
| Author      | `J, (Carter Games)` |
| Revision    | `2`                 |
| Last update | `2026-07-10`        |

<br/>




## Search Provider Manager
`SearchProviderManager.cs`

This is mainly an access class. 
It's handy when you want to call a provider from your own custom editors or similar.
Otherwise, it is recommended to use the built-in `SearchAttribute` to access the providers on their relevant fields.

<details>
  <summary>API Reference</summary>

### Methods

#### `GetProvider()`
Gets the provider of the entered type. 
If not currently in the cache it adds it to it when requested.

```csharp
public static T GetProvider<T>();
```

```csharp
private void OnEnable()
{
    var myProvider = SearchProviderManager.GetProvider<MySearchProvider>();
}
```

<br/>

#### `TryGetProvider()`
Tries to get the provider of the entered type. 
If not currently in the cache it adds it to it when requested.

```csharp
public static bool TryGetProvider<T>(out T result);
```

```csharp
private void OnEnable()
{
    if (SearchProviderManager.TryGetProvider<MySearchProvider>(out var myProvider))
    {
        // Logic using myProvider here.
    }
}
```

</details>

``` csharp
private void OnEnable()
{
    // Safe reference
    if (SearchProviderManager.TryGetProvider<MySearchProvider>(out var myProvider))
    {
        // Logic using myProvider here.
    }

    // Direct reference
    var myProvider = SearchProviderManager.GetProvider<MySearchProvider>();
}
```

<br/>




## Search Provider
`SearchProvider.cs`

The search provider class is the main handler that handles the data that is displayed in a provider.
The actual provider side of things is all handled by a Unity API. We're just using it.
Due to some technical setup limitations, you'll need to use
a child class dependent on the base type your provider returns. 

The library provides the following set-ups already.
- String `SearchProviderString`
- Int `SearchProviderInt`
- AssemblyClassDef _(use for class or interface referencing)_ `SearchProviderAssemblyClassDef`
- GameObject `SearchProviderGameObject`
- ScriptableObject `SearchProviderScriptableObject`

You can also make your own child class to base from when needed for unique setups.

<details>
  <summary>API Reference</summary>

### Properties

#### `ProviderTitle`
Gets the title shown on the search provider and on the button when using the `SearchAttribute`

```csharp
public abstract string ProviderTitle { get; }
```

<br/>

#### `ToExclude`
A list of entries to exclude from the search. Add entries to have them be ignored if in the list.

```csharp
protected List<T> ToExclude { get; set; }
```

<br/>

#### `HasOptions`
Gets if the provider has any entries to show. If false any `SearchAttribute` 
select buttons will show this as an issue and block the provider from opening.

```csharp
public abstract bool HasOptions { get; }
```

<br/>

### Events

#### `SelectionMadeEvt`
Raised when a selection is made. Use this to apply the selected option when 
making custom editors with a search setup.

```csharp
public readonly Evt<SearchTreeEntry> SelectionMadeEvt;
```

<br/>

#### `SelectionMadeCtxEvt`
Raised when a selection is made. Use this to apply the selected option when
making custom editors with a search setup. The only difference from `SelectionMadeEvt` is
that this runs a tad later and it gives the search window context back as well as the entry selected.

```csharp
public readonly Evt<SearchTreeEntry, SearchWindowContext> SelectionMadeCtxEvt;
```

<br/>


### Methods

#### `Open()`
Opens the search provider window when called. If you enter a value into the parameter it will
omit that value from the provider automatically. 

```csharp
public void Open();
public void Open(T currentValue);
```

```csharp
private void OnEnable()
{
    SearchProviderManager.GetProvider<MySearchProvider>().Open();
}
```

<br/>

#### `OpenCustom()`
Opens the search provider window when called. The difference from `Open()` is that you 
manually choose the position and width of the provider in the call.

```csharp
public void OpenCustom(Vector2 pos, float width);
```

```csharp
private void OnEnable()
{
    SearchProviderManager.GetProvider<MySearchProvider>().OpenCustom(Vector2.Zero, 100f);
}
```

<br/>

#### `GetEntriesToDisplay()`
Defines the entries the search provider can display.

```csharp
protected abstract List<SearchGroup<T>> GetEntriesToDisplay();
```

```csharp
protected override List<SearchGroup<string>> GetEntriesToDisplay()
{
	var list = new List<SearchGroup<string>>();
	var items = new List<SearchItem<string>>();
	
	foreach (var asset in Conditions)
	{
		if (ToExclude.Contains(asset.Id)) continue;
		items.Add(SearchItem<string>.Set(asset.Id, asset.VariantId));
	}

	list.Add(new SearchGroup<string>(items));
	
	return list;
}
```

</details>

<br/>




## Search Attribute
`SearchAttribute.cs`

The search attribute is used to make a field use a search provider property drawer
for the applied field. Due to some technical setup limitations, you'll need to use 
a child class dependent on the type the field is. The library provides the following set-ups already.
- String `[SearchString]`
- Int `[SearchInt]`
- AssemblyClassDef _(use for class or interface referencing)_ `[SearchClassDef]`
- GameObject `[SearchGameObject]`
- ScriptableObject `[SearchScriptableObject]`

You pass in the search provider type that you want to use in the attribute
constructor when assigning it.

<details>
  <summary>API Reference</summary>

#### Constructors

```csharp
protected SearchAttribute(Type searchType);
```

```csharp
[SerializedField] 
[SearchString(typeof(MyStringSearchProvider))] 
private string myStringField;
```

<br/>
</details>

<br/>




## Custom Search Providers

### Make a new search provider base type
To make a new search provider base type you'll need to make 3 classes.
- 2 Runtime classes.
- 1 Editor class.

<br/>

#### Search Provider (Runtime class)
Here you just need to make a new abstract class that inherits from the `SearchProvider<T>` generic class.
You don't need to implement anything in it, it is purely a pass-through. 
The ideal naming scheme is `SearchProvider` followed by the type, say `double`. it would result in 
`SearchProviderDouble` in this example. 

<br/>

#### Search Attribute (Runtime class)
Here you make class that inherits from `SearchAttribute`. Again you don't need to add any logic to your
new attribute class. But it should be limited to fields only and have the required constructor 
implementation to compile.

Example:
```csharp
[AttributeUsage(AttributeTargets.Field)]
public class SearchIntAttribute : SearchAttribute
{
    public SearchIntAttribute(Type searchType) : base(searchType)
    { }
}
```

<br/>

#### Property Drawer (Editor class)
This should be placed in an editor folder in the project or wrapped in a `#if` `UNITY_EDITOR` define.
You need to inherit from the `PropertyDrawerSearchProviderSelectable` class and implement its requirements.
Most of the requirements are just getting info or setting the value to the property the attribute is on. 
The drawer should be a custom property drawer of the attribute class you just made above. 

Example:
```csharp
[CustomPropertyDrawer(typeof(SearchIntAttribute), true)]
public class PropertyDrawerSearchProviderInt : PropertyDrawerSearchProviderSelectable<SearchProviderInt, int>
{
    protected override bool IsValid(SerializedProperty property)
    {
        return int.TryParse(property.stringValue, out _);
    }

    protected override bool GetHasValue(SerializedProperty property)
    {
        return IsValid(property);
    }

    protected override int GetCurrentValue(SerializedProperty property)
    {
        return property.intValue;
    }

    protected override string GetCurrentValueString(SerializedProperty property)
    {
        return property.intValue.ToString();
    }

    protected override void OnSelectionMade(SerializedProperty property, int selectedEntry)
    {
        property.intValue = selectedEntry;
        property.serializedObject.ApplyModifiedProperties();
        property.serializedObject.Update();
    }

    protected override void ClearValue(SerializedProperty property)
    {
        property.intValue = 0;
        property.serializedObject.ApplyModifiedProperties();
        property.serializedObject.Update();
    }
}
```

<br/>

#### Simple usage
For the simple implementation, you just need to make a class inheriting from one of the base classes provided.
These being:
`SearchProviderString`, `SearchProviderInt`, `SearchProviderAssemblyClassDef`, `SearchProviderGameObject`
, `SearchProviderScriptableObject`.

To apply the search GUI to a field, just add the matching type search attribute to the field in question, 
passing in the search provider you wish to use. like so:
```csharp
// SearchProviderLocalizationIds inherits from SearchProviderString in this example.
[SerializeField] [SearchString(typeof(SearchProviderLocalizationIds))] private string locTextUnselected;
```

The setup is the same for any other types. A table below of all the matching setups that are built in. 

| Type               | Provider base class              | Attribute |
|--------------------|----------------------------------| -- |
| `String`           | `SearchProviderString`           | `[SearchString]`
| `Int`              | `SearchProviderInt`              | `[SearchInt]`
| `AssemblyClassDef` | `SearchProviderAssemblyClassDef` | `[SearchClassDef]`
| `GameObject`       | `SearchProviderGameObject`       | `[SearchGameObject]`
| `ScriptableObject` | `SearchProviderScriptableObject` | `[SearchScriptableObject]`

<br/>

#### Manual usage
If you want to use a provider in your own editor GUI or similar, you can do. 
You just have to use the setup manually instead, as the property drawer 
does some of the work for you in the simple setup.

- **Getting a provider type**
  - To get a provider, use the `SearchProviderManager` API to get your provider type.
- **Opening a provider**
  - Call `Open()` on the provider you wish to use to open it at that point.
  - You can use `OpenCustom()` for more fine control on the GUI size & placement when opened.
- **Excluding entries**
  - To exclude an entry, you can pass it in when opening the provider. 
  Just pass in the entry you want to exclude when calling `Open()` 
- **Get the selected entry**
  - Listen to the `SelectionMadeEvt` evt before calling to open the provider. 
  Then implement a method to process the result. 
  The selected value will be in a `object` under `SearchTreeEntry.userData`. 
  You can cast to the required type from there.