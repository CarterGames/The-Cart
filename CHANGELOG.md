# Changelog
All notable changes to the webrtc package will be documented in this file.

The format is based on [Keep a Changelog](http://keepachangelog.com/en/1.0.0/)
and this project adheres to [Semantic Versioning](http://semver.org/spec/v2.0.0.html).

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
