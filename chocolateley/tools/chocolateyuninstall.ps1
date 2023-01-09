$ErrorActionPreference = 'Stop'; 

$toolsDir   = "$(Split-Path -parent $MyInvocation.MyCommand.Definition)"
$target = Join-Path $toolsDir "ImageImport.exe"
. $target unregister

$shortCutFile = Join-Path $([Environment]::GetFolderPath("CommonStartMenu")) "Calteo\Image Import.lnk"
Remove-Item $shortCutFile -Force
