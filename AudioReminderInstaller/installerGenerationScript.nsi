; example1.nsi
;
; This script is perhaps one of the simplest NSIs you can make. All of the
; optional settings are left to their default settings. The installer simply 
; prompts the user asking them where to install, and drops a copy of example1.nsi
; there. 

!define BUILD_CONFIG "Debug"
!define OUT_DIR "bin\${BUILD_CONFIG}"
!define INSTALLER_NAME "AudioReminderInstaller"
!define ARTIFACTS_DIR "${OUT_DIR}\Artifacts"
;--------------------------------

; The name of the installer
Name "${INSTALLER_NAME} name"

; The file to write
OutFile "${OUT_DIR}\${INSTALLER_NAME}.exe"

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
