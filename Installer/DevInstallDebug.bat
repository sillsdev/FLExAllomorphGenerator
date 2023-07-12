@echo off
copy "..\AllomorphGeneratorDll\bin\x64\Debug\AllomorphGenerator.dll" "c:\fwrepo\fw\Output\Debug" > nul
copy "..\AllomorphGeneratorDll\bin\x64\Debug\AllomorphGenerator.pdb" "c:\fwrepo\fw\Output\Debug" > nul
REM copy "..\AllomorphGeneratorDll\doc\UserDocumentation.pdf" "c:\fwrepo\fw\Output\Debug\doc"
copy "..\AlloGenModel\bin\x64\Debug\AlloGenModel.dll" "c:\fwrepo\fw\Output\Debug" > nul
copy "..\AlloGenModel\bin\x64\Debug\AlloGenModel.pdb" "c:\fwrepo\fw\Output\Debug" > nul
copy "..\AlloGenService\bin\x64\Debug\AlloGenService.dll" "c:\fwrepo\fw\Output\Debug" > nul
copy "..\AlloGenService\bin\x64\Debug\AlloGenService.pdb" "c:\fwrepo\fw\Output\Debug" > nul
copy "..\VarGenService\bin\x64\Debug\VarGenService.dll" "c:\fwrepo\fw\Output\Debug" > nul
copy "..\VarGenService\bin\x64\Debug\VarGenService.pdb" "c:\fwrepo\fw\Output\Debug" > nul
copy "..\VariantGeneratorDll\bin\x64\Debug\VariantGenerator.dll" "c:\fwrepo\fw\Output\Debug" > nul
copy "..\VariantGeneratorDll\bin\x64\Debug\VariantGenerator.pdb" "c:\fwrepo\fw\Output\Debug" > nul
echo Copying done
REM copy "C:\fwrepo\fw\DistFiles\Language Explorer\Configuration\UtilityCatalogInclude.xml" "C:\fwrepo\fw\DistFiles\Language Explorer\Configuration\UtilityCatalogIncludeOrig.xml"
REM copy UtilityCatalogLine.txt >> "C:\fwrepo\fw\DistFiles\Language Explorer\Configuration\UtilityCatalogInclude.xml"
xcopy /i /y "..\AlloGenService\AlloGenDataMigrations" "c:\fwrepo\fw\Output\Debug\AlloGenDataMigrations"