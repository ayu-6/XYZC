<h1 align="center">
    XYZC — XyzConsole
</h1>
XYZC is a modern C# library for creating styled, aligned, and interactive text-based UI components in the terminal.  
Perfect for tools, dashboards, and text-based games.

---

## ✨ Features

- 🎨 Rich text formatting (colors, background, effects)
- 🧭 Text alignment (left, center, right)
- 🧱 Composable components (labels, boxes, panels, etc.)
- 🧰 Extendable object-based design
- 🖥️ Console area management and rendering

---

## 📦 Installation
you can add XYZC package to your project by:
```bash
# Coming soon to NuGet
```

---

## 🚀 Quick Example

```csharp
using XYZC.Components;

new ConsoleLabel("Hello, XYZC!")
{
    ForegroundColor = Color.Cyan,
    BackgroundColor = Color.Black,
    TextAlign = ConsoleAlign.Center
}.Draw();
```

---

## 📂 Components

| Component        | Description                            |
|------------------|----------------------------------------|
| `ConsoleScene`   | Root canvas and container              |
| `ConsoleStage`   | Manages the active drawing window      |
| `ConsoleBox`     | Renders styled bordered boxes          |
| `ConsoleLabel`   | Displays styled text                   |
| `ConsolePanel`   | Panel container for organized layout   |
| `ConsolePaper`   | Clean drawing surface for UI content   |

---

## 🌐 Supported Platforms

- ✅ Windows
- ✅ Linux
- ✅ macOS

---

## 🛠 Development

This library is open-source and under active development.  
We welcome contributions, feedback, and feature requests!

```bash
git clone https://github.com/ayu-6/XYZC.git
cd XYZC
dotnet build
```
