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
 
## How to: Thumb Image Basic

Thumb is common use in most of scenario, here provide a simple way to accomplish this goal.
Combine [CiConfig](https://github.com/lettucebo/Creatidea.Library.Configs) and [CiResult](https://github.com/lettucebo/Creatidea.Library.Results) for better user experience.

CiImage is a static calss, so no need to new an instance, just call it.
The return type is **CiResult<Image>**, call CiResult<Image>.Data for Image object.

The following example demonstrates how to thumb an image:
```csharp
var result1 = CiImage.ThumbImage(path);
if (!result1.Success)
{
    Console.WriteLine("發生錯誤：{0}", result1.Message);
}
else
{
    var link = SaveImage(result1.Data, ImageFormat.Jpeg);
    Console.WriteLine("Show result1: {0}", link);
}
```
- This method use CiConfig read size config, first look for "Size", if not exist then find for "FitSize".
- If Size is found, then it will stop to find FitSize and use Size for size setting.
- Use Size will maintain image ratio，FitSize will not maintain image ratio.

## How to: Thumb Image By Given Specific Parameter #1

The following example demonstrates how to thumb an image:
```csharp
var result2 = CiImage.ThumbImage(path, 300);
if (!result2.Success)
{
    Console.WriteLine("發生錯誤：{0}", result2.Message);
}
else
{
    var link = SaveImage(result2.Data, ImageFormat.Jpeg);
    Console.WriteLine("Show result2: {0}", link);
}
```
- This method use given size to thumb image and thumbnail will maintain image ratio

## How to: Thumb Image By Given Specific Parameter #2
