# Changelog
All notable changes to the package will be documented in this file.

The format is based on [Keep a Changelog](http://keepachangelog.com/en/1.0.0/)
and this project adheres to [Semantic Versioning](http://semver.org/spec/v2.0.0.html).

<br/>

# Example
Use the template below to add new releases to the changelog, these are also used in the release notes on the repository as well. Please obmit any categories that do not apply to the release. 

```
## [{Version number}] - {Release Date (Year-Month-Day)}

### 🟩 Added
Any new additions to the project.

### 🟨 Changed
Any changes to existing elements of the project.

### 🟪 Removed
Any elements of the project that have been removed.

### 🟥 Fixed
Any fixes for bugs or issues with the library.

### ⬜ Deprecated
Any notices of deprecation in the code.

### 🟦 Security
Any security related chnges to the project.

<br/>
```

<br/>

# Entries

## [0.14.0] - 2026-??-??

### 🟩 Added

- Initial documentation added to core library.
- Some crates documented, more to follow in the 0.14.x lifespan. The library won’t go to it's next version until all current are ones are documented.
- Conditions crate: added summary method to return the full status of a condition for debugging.

### 🟨 Changed

- Updated the placement of the HtmlStringToColor() method for colors from the ColorExtensions class to a new ColorHelper class.
- Conditions crate: API improved to allow for checking refresh and valid status easily instead of having the user check in listeners.

### 🟥 Fixed

- Conditions crate: Fixed an issue where the constants assembly reference would reference the wrong GUID when generated.

<br/>

## [0.13.1] - 2026-03-28

### 🟩 Added

- Added new API for runtime timers to allow changing the time remaining & duration after creation.

### 🟥 Fixed

- Fixed built-in crate define classes stopping builds from completing due to missing scripting defines.

<br/>

## [0.13.0] - 2026-03-06

### 🟩 Added

- Added Multi Scene asset as a crate in the library. Not functionally different yet but wil be worked on in the future.

### 🟨 Changed

- New logo
- Modules renamed to Crates. Its more thematic and unique.
- Updated crate setup to be more in-depth
    - Now allows for links to be shown
    - Updated API to allow crates to be organised by author.
    - Updated so user made crates will show up in the crate window.
    - Reworked how crates are defined to make it easier to define them.
    - Updated settings provider setup to nest crate settings in their own tabs instead of making the settings be one large page.
- ScriptableRef renamed to AutoMake. Same API but a new name.
    - Updated Auto data asset making API in the editor to not require all fields to function.
- Updated AssemblyHelper to check internally only when defined.
- Updated library settings provider to match Save Manager 3.x style.
- Updated readme with more info (further updates to come).
- Added changelog to repo.
- Added contributing guide to repo.
- Licence updated to GNU V3 over MIT.

### 🟪 Removed

- PlayerInfo setup removed as you’d likely use a remote uid instead for a player to ensure they are uniuqe.

### 🟥 Fixed

- Fixed RNG strings causing an error with an index out of range issue on some calls.
- Fixed some editor icons not working on Linux editor.
