# Data

| [Usage](Docs_Data.md) | [API](../../../../Docs/API/Cart/Runtime/API_Data.md) |

The data asset system is designed to allow you to access game data without the need for hard references in an inspector. So you can access your data directly in code. 

|             |            |
|-------------|:-----------|
| Revision    | `1`         |
| Last update | `2026-04-01`         |

<br/>

The data system has two main elements to it. The data assets and the data asset index. The assets are what hold the data you define. With you making classes that implement the class to store data. While the index keeps a reference of all the data assets in the project for use at runtime. 

[//]: # (<br/>)
[//]: # (## Table of Contents {#contents})
[//]: # (1. [Creating data assets]&#40;#create&#41;)
[//]: # (2. [Referencing data assets]&#40;#reference&#41;)
[//]: # (3. [Data asset index]&#40;#asset-index&#41;)
[//]: # (4. [Editor only data]&#40;#editor-only-data-assets&#41;)

<br/>

### Creating data assets {#create}
To create a data asset, either inherit from the `DataAsset` class or use the provided tool that will create the necessary classes for you. You can find the creation tool under: `Tools/Carter Games/The Cart/[Data] Data Asset Creator`

This will open an editor window that’ll let you make a new data asset class. To use the tool, you simple enter the name you want the class to have a press create. A popup will then appear to let you choose where it should be saved. After which you’ll get the chance to open the newly created class for editing in your chosen IDE. 

You can also make the class manually by making a new class that inherit from `DataAsset`. An example below:

```csharp
[CreateAssetMenu] // So you can make an instance in the project from the create asset menu.
public class DataAssetLevels : DataAsset
{
    // Your data here.    
}
```

<br/>

### Referencing data assets {#reference}
You can reference data assets in two main ways. Either a direct reference in the inspector like you normally would with any field. Or by getting the asset from the `DataAccess` class. The data access class.

```csharp
private void OnEnable()
{
    // Gets the first asset of the type found.
    var asset = DataAccess.GetAsset<DataAssetLevels>();
    
    // Gets the asset of the matching variant id.
    asset = DataAccess.GetAsset<DataAssetLevels>("MyAssetVariantId");
    
    // Gets all of the assets of the type found.
    var assets = DataAccess.GetAssets<DataAssetLevels>();
}
```

<br/>

### Data asset index {#asset-index}
The data asset index is a scriptable object that stores a reference to all the data assets. This is used to performantly allow referencing to the assets through the DataAccess class. Avoiding expensive operations like `Resources.Load()`. The system will automatically make the data asset index if it doesn’t exist and update it with any new assets when entering play mode or before a build is made to ensure it is up to date. Should you need to update it manually, you can do so from the following menu item: `Tools/Carter Games/The Cart/[Data] Update Data Asset Index`

<br/>

### Editor only data {#editor-only-data-assets}
You can use the data setup in an editor only context as well. This uses the `EditorOnlyDataAsset` instead of the standard `DataAsset` class. The only difference is that the editor only ones are always excluded from the asset index setup, so cannot be referenced from the `DataAccess` class API. See editor data documentation for more information on editor usage.
