; This is an Inno Setup configuration file
; http://www.jrsoftware.org/isinfo.php

#define ApplicationVersion GetFileVersion('..\SpectrumLook\bin\Debug\SpectrumLook.exe')

[CustomMessages]
AppName=SpectrumLook

[Messages]
WelcomeLabel2=This will install [name/ver] on your computer.
; Example with multiple lines:
; WelcomeLabel2=Welcome message%n%nAdditional sentence

[Files]
Source: ..\SpectrumLook\bin\Debug\SpectrumLook.exe                                     ; DestDir: {app}
Source: ..\SpectrumLook\bin\Debug\SpectrumLook.pdb                                     ; DestDir: {app}
Source: ..\SpectrumLook\bin\Debug\MolecularWeightCalculator.dll                        ; DestDir: {app}
Source: ..\SpectrumLook\bin\Debug\MolecularWeightCalculator.pdb                        ; DestDir: {app}
Source: ..\SpectrumLook\bin\Debug\MSDataFileReader.dll                                 ; DestDir: {app}
Source: ..\SpectrumLook\bin\Debug\MSDataFileReader.pdb                                 ; DestDir: {app}
Source: ..\SpectrumLook\bin\Debug\PHRPReader.dll                                       ; DestDir: {app}
Source: ..\SpectrumLook\bin\Debug\PHRPReader.pdb                                       ; DestDir: {app}
Source: ..\SpectrumLook\bin\Debug\PRISM.dll                                            ; DestDir: {app}
Source: ..\SpectrumLook\bin\Debug\PSI_Interface.dll                                    ; DestDir: {app}
Source: ..\SpectrumLook\bin\Debug\ThermoFisher.CommonCore.BackgroundSubtraction.dll    ; DestDir: {app}
Source: ..\SpectrumLook\bin\Debug\ThermoFisher.CommonCore.Data.dll                     ; DestDir: {app}
Source: ..\SpectrumLook\bin\Debug\ThermoFisher.CommonCore.MassPrecisionEstimator.dll   ; DestDir: {app}
Source: ..\SpectrumLook\bin\Debug\ThermoFisher.CommonCore.RawFileReader.dll            ; DestDir: {app}
Source: ..\SpectrumLook\bin\Debug\ThermoRawFileReader.dll                              ; DestDir: {app}
Source: ..\SpectrumLook\bin\Debug\ThermoRawFileReader.pdb                              ; DestDir: {app}
Source: ..\SpectrumLook\bin\Debug\ZedGraph.dll                                         ; DestDir: {app}
Source: ..\SpectrumLook\SpectrumLookIcon.ico                                           ; DestDir: {app}

Source: ..\..\README.md                                                                ; DestDir: {app}
Source: ..\..\TestData\MSGFPlus\QC_Mam_19_01_excerpt.mzML                              ; DestDir: {app}
Source: ..\..\TestData\MSGFPlus\MSGFPlus_Tryp_MetOx_StatCysAlk_20ppmParTol.txt         ; DestDir: {app}
Source: ..\..\TestData\MSGFPlus\MSGFPlus_Tryp_MetOx_StatCysAlk_20ppmParTol_ModDefs.txt ; DestDir: {app}

[Dirs]
Name: {commonappdata}\SpectrumLook; Flags: uninsalwaysuninstall

[Tasks]
Name: desktopicon; Description: {cm:CreateDesktopIcon}; GroupDescription: {cm:AdditionalIcons}; Flags: unchecked
; Name: quicklaunchicon; Description: {cm:CreateQuickLaunchIcon}; GroupDescription: {cm:AdditionalIcons}; Flags: unchecked

[Icons]
Name: {commondesktop}\SpectrumLook; Filename: {app}\SpectrumLookIcon.ico; Tasks: desktopicon; Comment: SpectrumLook
Name: {group}\SpectrumLook;         Filename: {app}\SpectrumLookIcon.ico; Comment: SpectrumLook

[Setup]
AppName=SpectrumLook
AppVersion={#ApplicationVersion}
;AppVerName=ProteinDigestionSimulator
AppID=SpectrumLookId
AppPublisher=Pacific Northwest National Laboratory
AppPublisherURL=http://omics.pnl.gov/software
AppSupportURL=http://omics.pnl.gov/software
AppUpdatesURL=http://omics.pnl.gov/software
ArchitecturesAllowed=x64 x86
ArchitecturesInstallIn64BitMode=x64
DefaultDirName={autopf}\SpectrumLook
DefaultGroupName=PAST Toolkit
AppCopyright=© PNNL
;LicenseFile=.\License.rtf
PrivilegesRequired=poweruser
OutputBaseFilename=SpectrumLook_Installer
VersionInfoVersion={#ApplicationVersion}
VersionInfoCompany=PNNL
VersionInfoDescription=SpectrumLook
VersionInfoCopyright=PNNL
DisableFinishedPage=true
ShowLanguageDialog=no
ChangesAssociations=false
EnableDirDoesntExistWarning=false
AlwaysShowDirOnReadyPage=true
;UninstallDisplayIcon={app}\delete_16x.ico
ShowTasksTreeLines=true
OutputDir=.\Output

[Registry]
;Root: HKCR; Subkey: MyAppFile; ValueType: string; ValueName: ; ValueDataMyApp File; Flags: uninsdeletekey
;Root: HKCR; Subkey: MyAppSetting\DefaultIcon; ValueType: string; ValueData: {app}\wand.ico,0; Flags: uninsdeletevalue

[UninstallDelete]
Name: {app}; Type: filesandordirs
