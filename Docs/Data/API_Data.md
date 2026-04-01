# Data (API)

| [Usage]() | [API]() |

The data asset system is designed to allow you to access game data without the need for hard references in an inspector. So you can access your data directly in code. 

|             |            |
|-------------|:-----------|
| Revision    | `1`         |
| Last update | `2026-04-01`         |

<br/>

|                  |     |
|------------------|:----|
| Assembly         | `CarterGames.Cart.Runtime`  |
| Namespace        | `CarterGames.Cart.Data`  |

<br/>

### `DataAccess`
Provides API to get data assets in the project.

<br/>

### Methods

#### `GetAsset()`
Gets the data asset of the defined type. If there is more than one and you don’t define the id it will pick the first one it finds. Will return null if none are found.


```csharp
public static T GetAsset<T>();
public static T GetAsset<T>(string variantId);
```

```csharp
DataAssetMyData dataAsset;

private void OnEnable()
{
    // Gets the first one found.
    dataAsset = DataAccess.GetAsset<DataAssetMyData>();
    
    // Gets the data asset with the variant id of "MyVariantId".
    dataAsset = DataAccess.GetAsset<DataAssetMyData>("MyVariantId");
}
```

<br/>

#### `GetAssets()`
Gets all the data assets of the defined type that are found in the project. Will return null if none are found.


```csharp
public static List<T> GetAssets<T>();
```

```csharp
List<DataAssetMyData> dataAssets;

private void OnEnable()
{
    // Gets all the found assets of the entered type.
    dataAssets = DataAccess.GetAssets<DataAssetMyData>();
}
```

<br/>

#### `GetAllAssets()`
Gets all the data assets defined in the index should you need to look through them all.


```csharp
public static List<DataAsset> GetAllAssets();
```

```csharp
List<DataAsset> dataAssets;

private void OnEnable()
{
    // Gets all the assets in the index.
    dataAssets = DataAccess.GetAllAssets();
}
```

<br/>

### `DataAsset`
The class to inherit from the make a scriptable object the system will track.

<br/>

### Properties

#### VariantId
A unique Id that can be used to identify the data asset for use with the data access class. By default a random Guid will be used to populate the field. This can be changed in the `DataAsset` inspector in the editor or via your `DataAsset` class in the code.

```csharp
public virtual string VariantId { get; }
```

<br/>

#### ExcludeFromAssetIndex
Defines if the data asset is excluded from being stored in the asset index. If true then the `DataAccess` API will not find the asset in the project. This is assigned from the `DataAsset` inspector in the editor and is default set to false. Please only change this setting in the inspector of the `DataAsset` to avoid confusion.

```csharp
public bool ExcludeFromAssetIndex { get; }
```

<br/>

### Methods

#### OnInitialize()
Override to add logic to a data asset once it has been initialized. Use to run logic when the scriptable object has been received the Unity `OnEnable()` callback essentially. 

```csharp
protected virtual void OnInitialize();
```
