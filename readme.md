# PDF Signature Removal and Processing Tool

A Nutrient .NET console application that loads a PDF, detects and removes all digital signatures using the GdPicture SDK, and saves a processed copy without signatures.

---

## Table of Contents

1. [Prerequisites](#prerequisites)
2. [Installation](#installation)
3. [Configuration](#configuration)
4. [Usage](#usage)
5. [Project Structure](#project-structure)
6. [Contributing](#contributing)
7. [License](#license)

---

## Prerequisites

- **.NET 8.0+ or .NET Framework 4.7+** (Windows)
- **GdPicture14 SDK (14.3.10)** (installed and licensed)
- **NuGet Packages**:
  - `GdPicture14`
  - `System.Drawing.Common`
- **IDE (optional)**: Visual Studio 2022 / Visual Studio Code

## Installation

1. **Clone this repository**

   ```bash
   git clone <your-repo-url>
   cd remove-digital-signature-console-app
   ```

2. **Restore dependencies**

   ```bash
   dotnet restore
   ```

3. **Install required NuGet packages** (if not already referenced in the project)

   ```bash
   dotnet add package GdPicture14
   dotnet add package System.Drawing.Common
   ```

4. **Update project file** (`.csproj`) to target Windows and enable GDI+ interop:

   ```xml
   <PropertyGroup>
     <OutputType>Exe</OutputType>
     <TargetFramework>net8.0-windows</TargetFramework>
     <UseWindowsForms>true</UseWindowsForms>
   </PropertyGroup>
   ```

---

## Configuration

- Open `Program.cs`
- Locate the license registration line:
  ```csharp
  lm.RegisterKEY("........ARHVoez6xrhe24gbHQ=");
  ```
- Replace with your actual GdPicture license key (or a demo key provided by GdPicture).

---

## Usage

1. **Build & Run**

   ```bash
   dotnet build
   dotnet run
   ```

2. **Follow the prompts**:

   - **Input PDF path**: Enter the full path to the source PDF with double quotes
   - **Output PDF path**: Accept the default or specify a custom path with double quotes.

3. **Processing**:

   - The app will load the PDF.
   - Detect and remove all signatures.
   - Save the processed PDF (default suffix: `_ProcessedNoSignatures.pdf`).

4. **Example**:

   ```text
   Enter the path to your PDF file:
   "C:\Docs\signed-document.pdf"
   Output will be saved as: "C:\Docs\signed-document_ProcessedNoSignatures.pdf"
   ```

---

## Project Structure

```
remove-digital-signature-console-app/
├── Program.cs
├── remove-digital-signature-console-app.csproj
└── README.md
```

---

## Author
[Narashiman Krishnamurthy](https://www.linkedin.com/in/narashimank/)

---

## License
Contact Nutrient sales for License.

