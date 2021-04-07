# Installation

### Manually

[Download](https://github.com/chamalowmoelleux/MonoGame-LDtk-Importer/releases) the latest version of the `MonoGame_LDtk_Importer.dll` file and place it in your project (or somewhere you can find it easily).

Add the  `MonoGame_LDtk_Importer.dll` to your project references, and to your `Content.mgcb` file ([here](#add-the-importer-to-the-pipeline) is an explanation of how to add references to your `Content.mgcb` file).

### With NuGet

Add the package to your project with the Package Manager Console using this command:

```txt
Install-Package MonoGame_LDtk_Importer
```

 Or with the .NET CLI using this command:

```txt
dotnet add package MonoGame_LDtk_Importer
```

Or just search for the `MonoGame_LDtk_Importer` package in the Package Manager GUI and install it to your project.

### Compiling

This importer need the following packages:
- `MonoGame.Framework.Content.Pipeline v3.8.0.*`
- `MonoGame.Framework.DesktopGL v3.8.0.*`
- `System.Drawing.Common v5.*`
- `System.Text.Json v4.7.2` ***Don't install v5 !!! MonoGame Pipeline don't support it***

Also, let the project in the `netcoreapp3.1` framework target, don't change it.

Compile the project with Visual Studio and do the steps listed in the [Manually](#manually) installation but with your compiled version.

## Add the importer to the Pipeline

You can do it manually by editing the file, or with the MGCB Editor

#### Manually

Simply add this line in your file under the **References** tab:

```txt
/reference:path\to\the\ddl\file.dll
```

It should look like this:

```txt
#----------------------------- Global Properties ----------------------------#
some stuff

#-------------------------------- References --------------------------------#
/reference:path\to\the\ddl\file.dll

#---------------------------------- Content ---------------------------------#
some stuff

```

You can add your `.ldtk` files to you `Content.mgcb` file, the MGCB should use the custom importer automatically.

#### Using the MGCB Editor

Open your `Content.mgcb` file and select the root of your project (usually called Content), and in the Properties go all way down to `References`. Click on it and a window should open:

<img src="..\images\mgcb_editor_example.png" alt="Example" style="zoom:80%;" />

Click on the `Add` button and find the `MonoGame_LDtk_Importer.ddl` file of the Importer (if you installed it with NuGet, you should find it) in this folder in your project folder `packages/MonoGame_LDtk_Importer.1.0.0/lib/net5/MonoGame_LDtk_Importer.dll`.

And that's it, you can now add your LDtk projects files to your `Content.mgcb` file.

