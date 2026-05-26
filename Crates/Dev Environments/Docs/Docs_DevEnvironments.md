# Dev Environments

| [Usage](Docs_DevEnvironments.md) | [API](API_DevEnvironments.md) |

Dev environments is designed to give you a way to define different build types in code and limit some functionality to those environments.


|             |                     |
|-------------|:--------------------|
| Author      | `J, (Carter Games)` |
| Revision    | `1`                 |
| Last update | `2025-08-10`        |


<br/>

### Defined environments
All environments in the crate are pre-defined. It is not intended to add extra in the design of this crate.

| Environment  | Description  |
|--------------|:------------|
| `Release` | Intended for release builds of the projects. 
| `Development` | Intended for builds when making the project and developing new features. 
| `Test` | Indented for builds that are similar to release, but with testing tools or settings enabled to check things over.


<br/>

### Switching environments

You can switch environment from the toolbar in the editor next to the scene switching button. You will be prompted to confirm switching environment when doing so. The button will display the current environment as well.

![Dev environments options toolbar img](img/docs_crate_devencironments_01.png)