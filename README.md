# Creatidea.Library.Images
創鈺共用類別庫系列

[![BuildStatus](https://travis-ci.org/lettucebo/Creatidea.Library.Images.svg)](https://travis-ci.org/lettucebo/Creatidea.Library.Images)

# Requirements

this library requires .NET 4.5 and above.

# Installation

You can either <a href="https://github.com/lettucebo/Creatidea.Library.Images.git">download</a> the source and build your own dll or, if you have the NuGet package manager installed, you can grab them automatically.
```
PM> Install-Package Creatidea.Library.Images
```

Once you have the libraries properly referenced in your project, you can include calls to them in your code. 
For a sample implementation, check the [Example](https://github.com/lettucebo/Creatidea.Library.Images/tree/master/Creatidea.Library.Images.Examples) folder.

Add the following namespaces to use the library:
```csharp
using System.Drawing;
```

# Functions
  - Text To Image
  - Thumb Image
 
## How to: Thumb Image

Thumb is common use in most of scenario, here provide a simple way to accomplish this goal.

Combine [CiConfig](https://github.com/lettucebo/Creatidea.Library.Configs) and [CiResult](https://github.com/lettucebo/Creatidea.Library.Results) for better user experience.
