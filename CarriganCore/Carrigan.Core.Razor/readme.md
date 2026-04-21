# Carrigan.Core.Razor

**Carrigan.Core.Razor** is a small Razor Class Library for ASP.NET that provides a limited set of custom view components, partial views, and related UI assets for sharing a few common presentation components across applications.

## Features

- Reusable **View Components**
- Reusable **Partial Views**
- A small set of shared Razor UI building blocks
- Packaged for reuse across multiple projects

## Purpose

This library is intended to provide a small collection of shared Razor-based presentation elements in a single reusable package.

At this stage, the library is intentionally limited in scope and only contains a modest set of reusable UI elements.

Typical use cases include:

- Shared status badges
- Reusable cards and layout fragments
- Common display components

## Installation

Add the NuGet package to your ASP.NET project:

```bash
dotnet add package Carrigan.Core.Razor
```

Or reference the project directly during development.

## Project Type

This library is designed as a **Razor Class Library** with support for **pages and views**, allowing consumers to use packaged Razor assets such as partial views and view component views.

## Usage

After referencing the package, applications can use the included Razor assets according to normal ASP.NET conventions.

Examples may include:

- Rendering a partial view
- Invoking a view component
- Reusing shared UI fragments across multiple pages

## Notes

To avoid naming conflicts in consuming applications, library partials and view components should use clear, library-specific naming conventions where practical.

## Package Tags

`razor`, `aspnetcore`, `mvc`, `view-components`, `partial-views`, `razor-class-library`, `ui`, `web`

---

## Links

* NuGet: [https://www.nuget.org/packages/Carrigan.Core](https://www.nuget.org/packages/Carrigan.Core)
* Repo:  [https://github.com/CarriganSoftwareSolutions/Carrigan.Core](https://github.com/CarriganSoftwareSolutions/Carrigan.Core)

---

## License

Apache‑2.0 
[https://www.apache.org/licenses](https://www.apache.org/licenses)
