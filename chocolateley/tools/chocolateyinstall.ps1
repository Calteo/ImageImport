$ErrorActionPreference = 'Stop'; # stop on all errors
$toolsDir   = "$(Split-Path -parent $MyInvocation.MyCommand.Definition)"

$packageDir = Join-Path $toolsDir "package"

Expand-Archive package.zip $packageDir

$target = Join-Path $packageDir "ImageImport.exe"
$shortCutFile = Join-Path $([Environment]::GetFolderPath("CommonStartMenu")) "Calteo\Image Import.lnk"
Install-ChocolateyShortcut -shortcutFilePath $shortCutFile -targetPath $target -workDirectory $packageDir -description "Image Import"

. $target register