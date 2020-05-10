; example1.nsi
;
; This script is perhaps one of the simplest NSIs you can make. All of the
; optional settings are left to their default settings. The installer simply 
; prompts the user asking them where to install, and drops a copy of example1.nsi
; there. 

; expected arg - BUILD_CONFIG_NAME_ARG

!define OUT_DIR "bin\${BUILD_CONFIG_NAME_ARG}"
!define ARTIFACTS_DIR "${OUT_DIR}\Artifacts"
;--------------------------------

; The name of the installer
Name "Audio Reminder"

; The file to write
OutFile "${OUT_DIR}\AudioReminderInstaller.exe"

; Request application privileges for Windows Vista
RequestExecutionLevel user

; Build Unicode installer
Unicode True

; The default installation directory
InstallDir $DESKTOP\Example1

;--------------------------------

; Pages

Page directory
Page instfiles

;--------------------------------

; The stuff to install
Section "" ;No components page, name is not important

  ; Set output path to the installation directory.
  SetOutPath $INSTDIR
  
  ; Put file there
  File /r ${ARTIFACTS_DIR}\*"
  
SectionEnd ; end the section
