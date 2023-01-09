$ErrorActionPreference = 'Stop'; # stop on all errors
$toolsDir   = "$(Split-Path -parent $MyInvocation.MyCommand.Definition)"

$target = Join-Path $toolsDir "ImageImport.exe"
$shortCutFile = Join-Path $([Environment]::GetFolderPath("CommonStartMenu")) "Calteo\Image Import.lnk"
Install-ChocolateyShortcut -shortcutFilePath $shortCutFile -targetPath $target -workDirectory $toolsDir -description "Image Import"

. $target register