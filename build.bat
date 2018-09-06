@echo Off
set config=%1
if "%config%" == "" (
   set config=Debug
)
 

set version=1.1.9


if not "%PackageVersion%" == "" (
   set version=%PackageVersion%
)

set nuget= "./../lib/nuget2.exe"
if "%nuget%" == "" (
	set nuget=nuget
)

Set msBuildLocation="C:\Program Files (x86)\Microsoft Visual Studio\2017\Community\MSBuild\15.0\Bin\msbuild.exe"


%msBuildLocation% MpesaLibrary.sln /t:restore /t:pack /p:IncludeSymbols=true /p:Configuration="%config%" /m /v:M /fl /flp:LogFile=msbuild.log;Verbosity=diag /nr:false

mkdir Build
mkdir Build\lib
mkdir Build\lib\netstandard2.0






