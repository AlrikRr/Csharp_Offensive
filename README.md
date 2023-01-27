# Csharp_Offensive
My C# code from Offensive C# course



## Requirements

You need to install the references for `system.management.automation`. You can find them from Nuget module installer inside visualstudio.

- Right Click on `ClientSocket` Sultion (Right part of the screen)
- Manage NuGet Packages 
- Select nuget.org Package Source
- Install :
	- Microsoft.NETFramwork.ReferenceAssemblies

If NuGet package source is not here, just add it like this :
- CLick on the setting button next to Microsoft Visual Studio Offline Packages
- Add new Package Source
	- Name : `nuhet.org`
	- Source : `https://api.nuget.org/v3/index.json`

Once its done, install the new reference :
- Still in Solution menu on the right
- Click on `Add` 
- Click on `Reference`
- Browse a new reference form your local machine
- Add `C:\Windows\assembly\GAC_MSIL\System.Management.Automation\1.0.0.0__31bf3856ad364e35\System.Management.Automation.dll`


