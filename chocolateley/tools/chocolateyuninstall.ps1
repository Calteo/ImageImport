$ErrorActionPreference = 'Stop'; 

$shortCutFile = Join-Path $([Environment]::GetFolderPath("CommonStartMenu")) "Calteo\Image Import.lnk"
Remove-Item $shortCutFile -Force
