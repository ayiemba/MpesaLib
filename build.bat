@echo Off
set config=%1
if "%config%" == "" (
   set config=Debug
)
 
set version=1.0.0
if not "%PackageVersion%" == "" (
   set version=%PackageVersion%
)

set nuget=
if "%nuget%" == "" (
	set nuget=nuget
)

%ProgramFiles(x86)%\Microsoft Visual Studio\Preview\Community\MSBuild MpesaLibrary.sln /p:Configuration="%config%" /m /v:M /fl /flp:LogFile=msbuild.log;Verbosity=diag /nr:false

mkdir Build
mkdir Build\lib
mkdir Build\lib\netstandard2.0

%nuget% pack "src\MpesaLib\MpesaLib.nuspec" -NoPackageAnalysis -verbosity detailed -o Build -Version %version% -p Configuration="%config%"
