![3x-Template-Logo-160](https://github.com/user-attachments/assets/b90480f8-4947-49b4-a25e-88ef1229f800)

A code library of tools that I use in conjunction with game-specific code to make my game projects.

## Badges
![CodeFactor](https://www.codefactor.io/repository/github/cartergames/the-cart/badge?style=for-the-badge)
![GitHub all releases](https://img.shields.io/github/downloads/cartergames/the-cart/total?style=for-the-badge&color=8d6ca1)
![GitHub release (latest by date)](https://img.shields.io/github/v/release/cartergames/the-cart?style=for-the-badge)
![GitHub repo size](https://img.shields.io/github/repo-size/cartergames/the-cart?style=for-the-badge)
![Unity](https://img.shields.io/badge/Unity-2020.3.x_or_higher-critical?style=for-the-badge)


## How To Install

### Unity Package Manager (Git URL) [Recommended]
<b>Latest:</b>
<br>
<i>The most up-to-date version of the repo that is considered stable enough for public use.</i>

```
https://github.com/CarterGames/The-Cart.git
```
<br>
<b>Specific branch:</b>
<br>
<i>You can also pull any public branch with the following structure.</i>

```
https://github.com/CarterGames/The-Cart.git#[BRANCH NAME HERE]
```

<br>
<b>Unity Package:</b>
<br>
<i>You can download the .unitypackage from each release of the repo to get a importable package of that version. You do sacrifice the ease of updating if you go this route. See the latest releases <a href="https://github.com/CarterGames/The-Cart/releases">here</a></i>
<br><br>

## Docs
The docs for the project are still a work in progress. Each script is fairly well commented as is but it will all be documented over time before a full 1.x release. More info on how documentation will be handled coming soon (0.14.x most likely). 

## Crates
Crates are scripts or systems that are optional. 
These may not be needed for every project and therefore can be toggled at will. 
Crates can be managed from the crate window under: ```Tools/Carter Games/The Cart/Crate Manager```
<br><br>

![Screenshot_20260303_082439](https://github.com/user-attachments/assets/86f5701b-25db-427f-ab9b-8bd9687c099d)

The crate management window looks like the above image. All crates are displayed by auther and then by name. You can see the toggle status of a crate from small icon next to its button. Pressing the button for any crate will show its details on the right hand side. 

Crates can be internal or external. Internal crates are crates that have their contents in the repo under the ```Crates/``` directory. While external crates are imported from other repositories at the users own risk. Some of these include older crates that became standalone projects. All Carter Games authored crates are safe and heavily reviewed, though bugs may still be present. User generated crates are managed by their authors outside of this repo and are their responsibility. Use at your own risk.
<br><br>
### Can I make my own crate?
Of-course, the library is intended that users can add their own logic to it.

Quick-guide:
- To define a crate that the system will pick up by making a class that inherits from the ``Crate`` class. Crates that are a wrapper for a package such as Notion Data is in the library should inherit from the ``ExternalCrate`` class.
  - Crate technical names are auto generated and follow the style of ``crate.author.name``
  - You will need to define the crates name, description and author before it'll appear in the ``Crate Manager`` for use.
  - You can choose to have other crates be required or optional to your crate. Just override and add their technical names to their respective arrays from the base class.
  - You can add links for docs etc. by overriding the ``CrateLinks`` property with entries.
  - If making an ``ExternalCrate`` you will need to provide the git url package info as well for it to function.
- To define a settings provider for the crate by making a class that implements the ``ISettingsProvider`` interface.
- If you need to store data, use ``DataAsset`` or ``CartSaveHandler`` API. If using ``DataAsset`` you implement classes to auto-make them by inheriting from the ``AutoMakeDataAssetDefineBase`` class. Auto-make classes should be in editor space only.
- All code in your crate, with the exception of the class that inherits from the ``Crate`` class, should have a script define around the entire class matching the crate itself. This will automatically set to ``{CRATEAUTHOR}_CART_CRATE_{CRATENAME}``. 
- Crates should be self contained with a folder structure similar to this (obmit folders that are not used):
```
/Crate Name
-- /Art
-- /Code/Editor
-- /Code/Runtime
-- /Data
-- /Prefabs
-- /Scenes
-- /~Documentation
-- /~Samples
```


## Contributing
See the CONTRIBUTING tab for more details. 
```
https://github.com/CarterGames/The-Cart?tab=contributing-ov-file
```

## Authors
- Jonathan Carter

## Licence
GNU V3

