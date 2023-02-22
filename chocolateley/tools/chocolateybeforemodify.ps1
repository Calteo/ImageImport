$ErrorActionPreference = 'Stop'; # stop on all errors
Get-Process "ImageImport" -ErrorAction SilentlyContinue | Stop-Process
