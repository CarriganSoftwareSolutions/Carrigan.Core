# Carrigan.Core

Carrigan.Core is a small collection of reusable .NET libraries published by Carrigan Software Solutions LLC. This is primarily intended to support other projects, and was open sourced for full disclosure of the codebase. However, if you find anything useful, feel free to use it in accordance with the Apache 2.0 license.

## Projects

- `Carrigan.Core` - general-purpose helpers and extension methods
- `Carrigan.Core.Mvc` - ASP.NET Core MVC helpers
- `Carrigan.Core.Razor` - reusable Razor UI components and assets
- `Carrigan.Core.Analyzers` - Roslyn analyzers

## Status

Pre-1.0. APIs may change.

## .NET Framework Support

Carrigan libraries officially support .NET versions that are currently supported by Microsoft.

Although Microsoft ended support for .NET 9 on November 10, 2026, we continue to provide .NET 9 binaries for compatibility with existing applications.

**Binaries targeting unsupported .NET versions are provided on an as-is basis and are not officially supported by Carrigan Software Solutions LLC.** These targets are not guaranteed to receive testing, framework-specific bug fixes, security updates, or technical support.

Support for obsolete target frameworks may be removed in future releases. We strongly recommend upgrading applications to a supported .NET version.

This policy does not affect the `netstandard2.0` target used by our Roslyn analyzers for compiler compatibility.

## Packages

- `Carrigan.Core`
- `Carrigan.Core.Mvc`
- `Carrigan.Core.Razor`
- `Carrigan.Core.Analyzers`

## Build

```bash
dotnet build CarriganCore/CarriganCore.sln
```

## Support Development

If this package helps you, consider supporting development:

https://github.com/sponsors/CarriganSoftwareSolutions