# SolidCore for Unity

Unity integration package for the [SolidCore](https://github.com/ASGrincewicz/SolidCore) engine.

SolidCore itself is an engine-agnostic, data-oriented .NET runtime. This package is the **Unity authoring layer** on top of it: *Unity becomes the authoring layer; SolidCore stays the runtime engine.*

## What's inside

```
Runtime/
  Plugins/SolidCore.dll   ← the standalone SolidCore engine (compiled, dropped in)
  Assets/                 ← ScriptableObject asset types (SolidSystemSO, SolidEntitySO, …)
  Bridge/                 ← MonoBehaviour bootstrap + Entity↔GameObject runtime
  Extensions/             ← SolidCore.Math ⇄ UnityEngine conversions
  Systems/                ← TransformSyncSystem and friends
Editor/
  Importers/ Inspectors/ Wizards/   ← editor tooling for the asset types
```

The core is consumed as a **precompiled assembly** (`Runtime/Plugins/SolidCore.dll`), not as source — this keeps the engine repo a pure .NET library free of Unity `.meta` files. The bridge code references the DLL's public API (`SolidCore.ECS`, `SolidCore.Math`, `SolidCore.Serialization`, `SolidCore.Collections`, `SolidCore.Conversion`).

## Installing

Add as a Git submodule inside a Unity project's `Packages/` (or reference via the Package Manager Git URL):

```
git submodule add git@github.com:ASGrincewicz/SolidCore_for_Unity.git Packages/com.solidcore.engine
```

## Updating the bundled engine

`Runtime/Plugins/SolidCore.dll` is built from the SolidCore repo:

```
dotnet build SolidCore/SolidCore.csproj -c Release
cp SolidCore/bin/Release/netstandard2.1/SolidCore.dll <this-repo>/Runtime/Plugins/SolidCore.dll
```

The engine multi-targets `net8.0;netstandard2.1`. **Use the `netstandard2.1` build here** — that's the compatibility level Unity loads managed plugins at. (The `net8.0` build is for the standalone tools/tests/benchmarks in the engine repo.)
