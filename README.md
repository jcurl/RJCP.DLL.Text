# RJCP.Core.Text <!-- omit in toc -->

- [1. SPrintF](#1-sprintf)
- [2. Release History](#2-release-history)
  - [2.1. Version 0.5.1](#21-version-051)
  - [2.2. Version 0.5.0](#22-version-050)

## 1. SPrintF

This assembly module contains an implementation of the C function sprintf(),
ported to C#.

## 2. Release History

### 2.1. Version 0.5.1

Quality:

- SPrintF: Add test cases for specific binary representations of double (DOTNET-825)
- SPrintF Double: Clean up code (DOTNET-826)
- SPrintF: Provide documentation on how the algorithm for IEEE754 works
- SPrintF: Add floating point (single) tests (DOTNET-828)
- SPrintF: Generate tables and replace in code, with possibly better precision (DOTNET-829)
- SPrintF: Test the case of negative zero for float and double (DOTNET-834)
- Upgrade from .NET Standard 2.1 to .NET 6.0 (DOTNET-936, DOTNET-941, DOTNET-942, DOTNET-945)

### 2.2. Version 0.5.0

- Initial Version
