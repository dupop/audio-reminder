; example2.nsi
;
; This script is based on example1.nsi, but it remember the directory, 
; has uninstall support and (optionally) installs start menu shortcuts.
;
; It will install example2.nsi into a directory that the user selects,

;--------------------------------

; expected arg - BUILD_CONFIG_NAME_ARG

!define OUT_DIR "bin\${BUILD_CONFIG_NAME_ARG}"
!define ARTIFACTS_DIR "${OUT_DIR}\Artifacts"

;--------------------------------

; The name of the installer
Name "Audio Reminder"

; The file to write
OutFile "${OUT_DIR}\AudioReminderInstaller.exe"

; Request application privileges for Windows Vista
RequestExecutionLevel admin

; Build Unicode installer
Unicode True

; The default installation directory
InstallDir "$PROGRAMFILES\Audio Reminder"

; Registry key to check for directory (so if you install again, it will 
; overwrite the old one automatically)
InstallDirRegKey HKLM "Software\AudioReminder" "Install_Dir"

;--------------------------------

; Pages

Page components
Page directory
Page instfiles

UninstPage uninstConfirm
UninstPage instfiles

;--------------------------------

; The stuff to install
Section "Audio Reminder (required)"

  SectionIn RO
  
  ; Set output path to the installation directory.
  SetOutPath "$INSTDIR\bin"
  
  ; Put file there
  File /r "${ARTIFACTS_DIR}\*"
 
  ; Add Ringer Listener to startup folder 
  CreateShortcut "$SMSTARTUP\Audio Reminder Ringer Listener.lnk" "$INSTDIR\bin\AudioReminderRingerListener\AudioReminderRingerListener.exe" "" "$INSTDIR\bin\AudioReminderRingerListener\AudioReminderRingerListener.exe" 0

  ; Write the installation path into the registry
  WriteRegStr HKLM SOFTWARE\AudioReminder "Install_Dir" "$INSTDIR"
  
  ; Write the uninstall keys for Windows
  WriteRegStr HKLM "Software\Microsoft\Windows\CurrentVersion\Uninstall\AudioReminder" "DisplayName" "Audio Reminder"
  WriteRegStr HKLM "Software\Microsoft\Windows\CurrentVersion\Uninstall\AudioReminder" "UninstallString" '"$INSTDIR\uninstall.exe"'
  WriteRegDWORD HKLM "Software\Microsoft\Windows\CurrentVersion\Uninstall\AudioReminder" "NoModify" 1
  WriteRegDWORD HKLM "Software\Microsoft\Windows\CurrentVersion\Uninstall\AudioReminder" "NoRepair" 1
  WriteUninstaller "$INSTDIR\uninstall.exe"
  
SectionEnd

; Optional section (can be disabled by the user)
Section "Start Menu Shortcuts"

  CreateDirectory "$SMPROGRAMS\Audio Reminder"
  CreateShortcut "$SMPROGRAMS\Audio Reminder\Uninstall.lnk" "$INSTDIR\uninstall.exe" "" "$INSTDIR\uninstall.exe" 0
  CreateShortcut "$SMPROGRAMS\Audio Reminder\Audio Reminder.lnk" "$INSTDIR\bin\AudioReminderUI\AudioReminderUI.exe" "" "$INSTDIR\bin\AudioReminderUI\AudioReminderUI.exe" 0
  
SectionEnd

; Optional section (can be disabled by the user)
Section "Desktop Shortcut"

  CreateShortcut "$DESKTOP\Audio Reminder.lnk" "$INSTDIR\bin\AudioReminderUI\AudioReminderUI.exe" "" "$INSTDIR\bin\AudioReminderUI\AudioReminderUI.exe" 0
  
SectionEnd

;--------------------------------

; Uninstaller

Section "Uninstall"
  
  ; Remove registry keys
  DeleteRegKey HKLM "Software\Microsoft\Windows\CurrentVersion\Uninstall\AudioReminder"
  DeleteRegKey HKLM SOFTWARE\AudioReminder

  ; Remove files and uninstaller
  Delete $INSTDIR\example2.nsi
  RMDir /r "$INSTDIR\bin"

  ; Remove shortcuts, if any
  Delete "$SMPROGRAMS\Audio Reminder\*.*"
  Delete "$DESKTOP\Audio Reminder.lnk"

  ; Remove ringer Listener from startup folder 
  Delete "$SMSTARTUP\Audio Reminder Ringer Listener.lnk"

  ; Remove directories used
  RMDir "$SMPROGRAMS\Audio Reminder"
  RMDir "$INSTDIR"

SectionEnd
