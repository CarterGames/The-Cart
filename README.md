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

Crates can be internal or external. Internal crates are crates that have their contents in the repo under the ```Crates/``` directory. While external crates are imported from other repositories at the users own risk. Some of these include older crates that became standalone projects. All Carter Games authored crates are safe and heavily reviewed. User generated crates are managed by their authors outside of this repo. 

### Can I make my own crate?
Of-course, the library is intended that users can add their own logic to it. Please see the crate template repo for a base to make your crates from. That repo also has tools to generate the required setups for a new crate per minor release version. 
```
https://github.com/CarterGames/Cart_Crate_Template
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
