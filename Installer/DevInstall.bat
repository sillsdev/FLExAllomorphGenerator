@echo off
copy "..\AllomorphGeneratorDll\bin\x64\Release\AllomorphGenerator.dll" "c:\fwrepo\fw\Output\Release"
copy "..\AllomorphGeneratorDll\bin\x64\Release\AllomorphGenerator.pdb" "c:\fwrepo\fw\Output\Release"
REM copy "..\AllomorphGeneratorDll\doc\UserDocumentation.pdf" "c:\fwrepo\fw\Output\Release\doc"
copy "..\AlloGenModel\bin\x64\Release\AlloGenModel.dll" "c:\fwrepo\fw\Output\Release"
copy "..\AlloGenModel\bin\x64\Release\AlloGenModel.pdb" "c:\fwrepo\fw\Output\Release"
copy "..\AlloGenService\bin\x64\Release\AlloGenService.dll" "c:\fwrepo\fw\Output\Release"
copy "..\AlloGenService\bin\x64\Release\AlloGenService.pdb" "c:\fwrepo\fw\Output\Release"

REM copy "C:\fwrepo\fw\DistFiles\Language Explorer\Configuration\UtilityCatalogInclude.xml" "C:\fwrepo\fw\DistFiles\Language Explorer\Configuration\UtilityCatalogIncludeOrig.xml"
REM copy UtilityCatalogLine.txt >> "C:\fwrepo\fw\DistFiles\Language Explorer\Configuration\UtilityCatalogInclude.xml"

