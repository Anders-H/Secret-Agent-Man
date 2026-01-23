#define MyAppName "Secret Agent Man"
#define MyAppVersion "1.0"
#define MyAppPublisher "Havet Software"
#define MyAppURL "https://secretagentman.80tal.se/"
#define MyAppExeName "SecretAgentManLoader.exe"

[Setup]
AppId={{F24FEF15-2160-4FA4-90EC-839C688F7EBB}
AppName={#MyAppName}
AppVersion={#MyAppVersion}
AppVerName={#MyAppName} {#MyAppVersion}
AppPublisher={#MyAppPublisher}
AppPublisherURL={#MyAppURL}
AppSupportURL={#MyAppURL}
AppUpdatesURL={#MyAppURL}
DefaultDirName={autopf}\{#MyAppName}
DisableProgramGroupPage=yes
OutputDir=D:\SecretAgentManSetup
OutputBaseFilename=InstallSecretAgentMan
SetupIconFile=D:\GitRepos\Secret-Agent-Man\SecretAgentMan\SecretAgentMan\secretagentman.ico
Compression=lzma
SolidCompression=yes
WizardStyle=modern

[Languages]
Name: "english"; MessagesFile: "compiler:Default.isl"

[Tasks]
Name: "desktopicon"; Description: "{cm:CreateDesktopIcon}"; GroupDescription: "{cm:AdditionalIcons}"; Flags: unchecked

[Files]
Source: "D:\GitRepos\Secret-Agent-Man\SecretAgentMan\PublishedProgram\{#MyAppExeName}"; DestDir: "{app}"; Flags: ignoreversion
Source: "D:\GitRepos\Secret-Agent-Man\SecretAgentMan\PublishedProgram\api-ms-win-core-console-l1-1-0.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "D:\GitRepos\Secret-Agent-Man\SecretAgentMan\PublishedProgram\api-ms-win-core-datetime-l1-1-0.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "D:\GitRepos\Secret-Agent-Man\SecretAgentMan\PublishedProgram\api-ms-win-core-debug-l1-1-0.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "D:\GitRepos\Secret-Agent-Man\SecretAgentMan\PublishedProgram\api-ms-win-core-errorhandling-l1-1-0.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "D:\GitRepos\Secret-Agent-Man\SecretAgentMan\PublishedProgram\api-ms-win-core-file-l1-1-0.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "D:\GitRepos\Secret-Agent-Man\SecretAgentMan\PublishedProgram\api-ms-win-core-file-l1-2-0.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "D:\GitRepos\Secret-Agent-Man\SecretAgentMan\PublishedProgram\api-ms-win-core-file-l2-1-0.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "D:\GitRepos\Secret-Agent-Man\SecretAgentMan\PublishedProgram\api-ms-win-core-handle-l1-1-0.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "D:\GitRepos\Secret-Agent-Man\SecretAgentMan\PublishedProgram\api-ms-win-core-heap-l1-1-0.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "D:\GitRepos\Secret-Agent-Man\SecretAgentMan\PublishedProgram\api-ms-win-core-interlocked-l1-1-0.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "D:\GitRepos\Secret-Agent-Man\SecretAgentMan\PublishedProgram\api-ms-win-core-libraryloader-l1-1-0.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "D:\GitRepos\Secret-Agent-Man\SecretAgentMan\PublishedProgram\api-ms-win-core-localization-l1-2-0.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "D:\GitRepos\Secret-Agent-Man\SecretAgentMan\PublishedProgram\api-ms-win-core-memory-l1-1-0.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "D:\GitRepos\Secret-Agent-Man\SecretAgentMan\PublishedProgram\api-ms-win-core-namedpipe-l1-1-0.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "D:\GitRepos\Secret-Agent-Man\SecretAgentMan\PublishedProgram\api-ms-win-core-processenvironment-l1-1-0.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "D:\GitRepos\Secret-Agent-Man\SecretAgentMan\PublishedProgram\api-ms-win-core-processthreads-l1-1-0.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "D:\GitRepos\Secret-Agent-Man\SecretAgentMan\PublishedProgram\api-ms-win-core-processthreads-l1-1-1.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "D:\GitRepos\Secret-Agent-Man\SecretAgentMan\PublishedProgram\api-ms-win-core-profile-l1-1-0.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "D:\GitRepos\Secret-Agent-Man\SecretAgentMan\PublishedProgram\api-ms-win-core-rtlsupport-l1-1-0.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "D:\GitRepos\Secret-Agent-Man\SecretAgentMan\PublishedProgram\api-ms-win-core-string-l1-1-0.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "D:\GitRepos\Secret-Agent-Man\SecretAgentMan\PublishedProgram\api-ms-win-core-synch-l1-1-0.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "D:\GitRepos\Secret-Agent-Man\SecretAgentMan\PublishedProgram\api-ms-win-core-synch-l1-2-0.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "D:\GitRepos\Secret-Agent-Man\SecretAgentMan\PublishedProgram\api-ms-win-core-sysinfo-l1-1-0.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "D:\GitRepos\Secret-Agent-Man\SecretAgentMan\PublishedProgram\api-ms-win-core-timezone-l1-1-0.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "D:\GitRepos\Secret-Agent-Man\SecretAgentMan\PublishedProgram\api-ms-win-core-util-l1-1-0.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "D:\GitRepos\Secret-Agent-Man\SecretAgentMan\PublishedProgram\api-ms-win-crt-conio-l1-1-0.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "D:\GitRepos\Secret-Agent-Man\SecretAgentMan\PublishedProgram\api-ms-win-crt-convert-l1-1-0.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "D:\GitRepos\Secret-Agent-Man\SecretAgentMan\PublishedProgram\api-ms-win-crt-environment-l1-1-0.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "D:\GitRepos\Secret-Agent-Man\SecretAgentMan\PublishedProgram\api-ms-win-crt-filesystem-l1-1-0.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "D:\GitRepos\Secret-Agent-Man\SecretAgentMan\PublishedProgram\api-ms-win-crt-heap-l1-1-0.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "D:\GitRepos\Secret-Agent-Man\SecretAgentMan\PublishedProgram\api-ms-win-crt-locale-l1-1-0.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "D:\GitRepos\Secret-Agent-Man\SecretAgentMan\PublishedProgram\api-ms-win-crt-math-l1-1-0.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "D:\GitRepos\Secret-Agent-Man\SecretAgentMan\PublishedProgram\api-ms-win-crt-multibyte-l1-1-0.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "D:\GitRepos\Secret-Agent-Man\SecretAgentMan\PublishedProgram\api-ms-win-crt-private-l1-1-0.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "D:\GitRepos\Secret-Agent-Man\SecretAgentMan\PublishedProgram\api-ms-win-crt-process-l1-1-0.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "D:\GitRepos\Secret-Agent-Man\SecretAgentMan\PublishedProgram\api-ms-win-crt-runtime-l1-1-0.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "D:\GitRepos\Secret-Agent-Man\SecretAgentMan\PublishedProgram\api-ms-win-crt-stdio-l1-1-0.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "D:\GitRepos\Secret-Agent-Man\SecretAgentMan\PublishedProgram\api-ms-win-crt-string-l1-1-0.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "D:\GitRepos\Secret-Agent-Man\SecretAgentMan\PublishedProgram\api-ms-win-crt-time-l1-1-0.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "D:\GitRepos\Secret-Agent-Man\SecretAgentMan\PublishedProgram\api-ms-win-crt-utility-l1-1-0.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "D:\GitRepos\Secret-Agent-Man\SecretAgentMan\PublishedProgram\cheat.dat"; DestDir: "{app}"; Flags: ignoreversion
Source: "D:\GitRepos\Secret-Agent-Man\SecretAgentMan\PublishedProgram\clrcompression.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "D:\GitRepos\Secret-Agent-Man\SecretAgentMan\PublishedProgram\config.bronco"; DestDir: "{app}"; Flags: ignoreversion
Source: "D:\GitRepos\Secret-Agent-Man\SecretAgentMan\PublishedProgram\D3DCompiler_47_cor3.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "D:\GitRepos\Secret-Agent-Man\SecretAgentMan\PublishedProgram\dbgshim.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "D:\GitRepos\Secret-Agent-Man\SecretAgentMan\PublishedProgram\havet-logo.mp4"; DestDir: "{app}"; Flags: ignoreversion
Source: "D:\GitRepos\Secret-Agent-Man\SecretAgentMan\PublishedProgram\mscordaccore_amd64_amd64_4.6.30411.01.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "D:\GitRepos\Secret-Agent-Man\SecretAgentMan\PublishedProgram\mscorrc.debug.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "D:\GitRepos\Secret-Agent-Man\SecretAgentMan\PublishedProgram\PenImc_cor3.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "D:\GitRepos\Secret-Agent-Man\SecretAgentMan\PublishedProgram\PresentationNative_cor3.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "D:\GitRepos\Secret-Agent-Man\SecretAgentMan\PublishedProgram\sos.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "D:\GitRepos\Secret-Agent-Man\SecretAgentMan\PublishedProgram\sos_amd64_amd64_4.6.30411.01.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "D:\GitRepos\Secret-Agent-Man\SecretAgentMan\PublishedProgram\ucrtbase.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "D:\GitRepos\Secret-Agent-Man\SecretAgentMan\PublishedProgram\vcruntime140_cor3.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "D:\GitRepos\Secret-Agent-Man\SecretAgentMan\PublishedProgram\wpfgfx_cor3.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "D:\GitRepos\Secret-Agent-Man\SecretAgentMan\PublishedProgram\api-ms-win-core-console-l1-1-0.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "D:\GitRepos\Secret-Agent-Man\SecretAgentMan\PublishedProgram\api-ms-win-core-datetime-l1-1-0.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "D:\GitRepos\Secret-Agent-Man\SecretAgentMan\PublishedProgram\api-ms-win-core-debug-l1-1-0.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "D:\GitRepos\Secret-Agent-Man\SecretAgentMan\PublishedProgram\api-ms-win-core-errorhandling-l1-1-0.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "D:\GitRepos\Secret-Agent-Man\SecretAgentMan\PublishedProgram\api-ms-win-core-file-l1-1-0.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "D:\GitRepos\Secret-Agent-Man\SecretAgentMan\PublishedProgram\api-ms-win-core-file-l1-2-0.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "D:\GitRepos\Secret-Agent-Man\SecretAgentMan\PublishedProgram\api-ms-win-core-file-l2-1-0.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "D:\GitRepos\Secret-Agent-Man\SecretAgentMan\PublishedProgram\api-ms-win-core-handle-l1-1-0.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "D:\GitRepos\Secret-Agent-Man\SecretAgentMan\PublishedProgram\api-ms-win-core-heap-l1-1-0.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "D:\GitRepos\Secret-Agent-Man\SecretAgentMan\PublishedProgram\api-ms-win-core-interlocked-l1-1-0.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "D:\GitRepos\Secret-Agent-Man\SecretAgentMan\PublishedProgram\api-ms-win-core-libraryloader-l1-1-0.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "D:\GitRepos\Secret-Agent-Man\SecretAgentMan\PublishedProgram\api-ms-win-core-localization-l1-2-0.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "D:\GitRepos\Secret-Agent-Man\SecretAgentMan\PublishedProgram\api-ms-win-core-memory-l1-1-0.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "D:\GitRepos\Secret-Agent-Man\SecretAgentMan\PublishedProgram\api-ms-win-core-namedpipe-l1-1-0.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "D:\GitRepos\Secret-Agent-Man\SecretAgentMan\PublishedProgram\api-ms-win-core-processenvironment-l1-1-0.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "D:\GitRepos\Secret-Agent-Man\SecretAgentMan\PublishedProgram\api-ms-win-core-processthreads-l1-1-0.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "D:\GitRepos\Secret-Agent-Man\SecretAgentMan\PublishedProgram\api-ms-win-core-processthreads-l1-1-1.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "D:\GitRepos\Secret-Agent-Man\SecretAgentMan\PublishedProgram\api-ms-win-core-profile-l1-1-0.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "D:\GitRepos\Secret-Agent-Man\SecretAgentMan\PublishedProgram\api-ms-win-core-rtlsupport-l1-1-0.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "D:\GitRepos\Secret-Agent-Man\SecretAgentMan\PublishedProgram\api-ms-win-core-string-l1-1-0.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "D:\GitRepos\Secret-Agent-Man\SecretAgentMan\PublishedProgram\api-ms-win-core-synch-l1-1-0.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "D:\GitRepos\Secret-Agent-Man\SecretAgentMan\PublishedProgram\api-ms-win-core-synch-l1-2-0.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "D:\GitRepos\Secret-Agent-Man\SecretAgentMan\PublishedProgram\api-ms-win-core-sysinfo-l1-1-0.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "D:\GitRepos\Secret-Agent-Man\SecretAgentMan\PublishedProgram\api-ms-win-core-timezone-l1-1-0.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "D:\GitRepos\Secret-Agent-Man\SecretAgentMan\PublishedProgram\api-ms-win-core-util-l1-1-0.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "D:\GitRepos\Secret-Agent-Man\SecretAgentMan\PublishedProgram\api-ms-win-crt-conio-l1-1-0.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "D:\GitRepos\Secret-Agent-Man\SecretAgentMan\PublishedProgram\api-ms-win-crt-convert-l1-1-0.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "D:\GitRepos\Secret-Agent-Man\SecretAgentMan\PublishedProgram\api-ms-win-crt-environment-l1-1-0.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "D:\GitRepos\Secret-Agent-Man\SecretAgentMan\PublishedProgram\api-ms-win-crt-filesystem-l1-1-0.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "D:\GitRepos\Secret-Agent-Man\SecretAgentMan\PublishedProgram\api-ms-win-crt-heap-l1-1-0.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "D:\GitRepos\Secret-Agent-Man\SecretAgentMan\PublishedProgram\api-ms-win-crt-locale-l1-1-0.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "D:\GitRepos\Secret-Agent-Man\SecretAgentMan\PublishedProgram\api-ms-win-crt-math-l1-1-0.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "D:\GitRepos\Secret-Agent-Man\SecretAgentMan\PublishedProgram\api-ms-win-crt-multibyte-l1-1-0.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "D:\GitRepos\Secret-Agent-Man\SecretAgentMan\PublishedProgram\api-ms-win-crt-private-l1-1-0.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "D:\GitRepos\Secret-Agent-Man\SecretAgentMan\PublishedProgram\api-ms-win-crt-process-l1-1-0.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "D:\GitRepos\Secret-Agent-Man\SecretAgentMan\PublishedProgram\api-ms-win-crt-runtime-l1-1-0.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "D:\GitRepos\Secret-Agent-Man\SecretAgentMan\PublishedProgram\api-ms-win-crt-stdio-l1-1-0.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "D:\GitRepos\Secret-Agent-Man\SecretAgentMan\PublishedProgram\api-ms-win-crt-string-l1-1-0.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "D:\GitRepos\Secret-Agent-Man\SecretAgentMan\PublishedProgram\api-ms-win-crt-time-l1-1-0.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "D:\GitRepos\Secret-Agent-Man\SecretAgentMan\PublishedProgram\api-ms-win-crt-utility-l1-1-0.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "D:\GitRepos\Secret-Agent-Man\SecretAgentMan\PublishedProgram\apphost.exe"; DestDir: "{app}"; Flags: ignoreversion
Source: "D:\GitRepos\Secret-Agent-Man\SecretAgentMan\PublishedProgram\clrcompression.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "D:\GitRepos\Secret-Agent-Man\SecretAgentMan\PublishedProgram\config.bronco"; DestDir: "{app}"; Flags: ignoreversion
Source: "D:\GitRepos\Secret-Agent-Man\SecretAgentMan\PublishedProgram\D3DCompiler_47_cor3.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "D:\GitRepos\Secret-Agent-Man\SecretAgentMan\PublishedProgram\dbgshim.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "D:\GitRepos\Secret-Agent-Man\SecretAgentMan\PublishedProgram\havet-logo.mp4"; DestDir: "{app}"; Flags: ignoreversion
Source: "D:\GitRepos\Secret-Agent-Man\SecretAgentMan\PublishedProgram\mscordaccore_amd64_amd64_4.6.30411.01.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "D:\GitRepos\Secret-Agent-Man\SecretAgentMan\PublishedProgram\mscorrc.debug.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "D:\GitRepos\Secret-Agent-Man\SecretAgentMan\PublishedProgram\PenImc_cor3.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "D:\GitRepos\Secret-Agent-Man\SecretAgentMan\PublishedProgram\PresentationNative_cor3.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "D:\GitRepos\Secret-Agent-Man\SecretAgentMan\PublishedProgram\SecretAgentMan.exe"; DestDir: "{app}"; Flags: ignoreversion
Source: "D:\GitRepos\Secret-Agent-Man\SecretAgentMan\PublishedProgram\SecretAgentManLoader.exe"; DestDir: "{app}"; Flags: ignoreversion
Source: "D:\GitRepos\Secret-Agent-Man\SecretAgentMan\PublishedProgram\sos.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "D:\GitRepos\Secret-Agent-Man\SecretAgentMan\PublishedProgram\sos_amd64_amd64_4.6.30411.01.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "D:\GitRepos\Secret-Agent-Man\SecretAgentMan\PublishedProgram\ucrtbase.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "D:\GitRepos\Secret-Agent-Man\SecretAgentMan\PublishedProgram\vcruntime140_cor3.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "D:\GitRepos\Secret-Agent-Man\SecretAgentMan\PublishedProgram\wpfgfx_cor3.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "D:\GitRepos\Secret-Agent-Man\SecretAgentMan\PublishedProgram\Content\ammo4x7.xnb"; DestDir: "{app}\Content"; Flags: ignoreversion
Source: "D:\GitRepos\Secret-Agent-Man\SecretAgentMan\PublishedProgram\Content\ammobox16x11.xnb"; DestDir: "{app}\Content"; Flags: ignoreversion
Source: "D:\GitRepos\Secret-Agent-Man\SecretAgentMan\PublishedProgram\Content\bomb40x31.xnb"; DestDir: "{app}\Content"; Flags: ignoreversion
Source: "D:\GitRepos\Secret-Agent-Man\SecretAgentMan\PublishedProgram\Content\bonus-meter.xnb"; DestDir: "{app}\Content"; Flags: ignoreversion
Source: "D:\GitRepos\Secret-Agent-Man\SecretAgentMan\PublishedProgram\Content\bonus-stars-640x360.xnb"; DestDir: "{app}\Content"; Flags: ignoreversion
Source: "D:\GitRepos\Secret-Agent-Man\SecretAgentMan\PublishedProgram\Content\bonuslevel.wma"; DestDir: "{app}\Content"; Flags: ignoreversion
Source: "D:\GitRepos\Secret-Agent-Man\SecretAgentMan\PublishedProgram\Content\bonuslevel.xnb"; DestDir: "{app}\Content"; Flags: ignoreversion
Source: "D:\GitRepos\Secret-Agent-Man\SecretAgentMan\PublishedProgram\Content\briefcase13x10.xnb"; DestDir: "{app}\Content"; Flags: ignoreversion
Source: "D:\GitRepos\Secret-Agent-Man\SecretAgentMan\PublishedProgram\Content\c64fontswe.xnb"; DestDir: "{app}\Content"; Flags: ignoreversion
Source: "D:\GitRepos\Secret-Agent-Man\SecretAgentMan\PublishedProgram\Content\coin10x10.xnb"; DestDir: "{app}\Content"; Flags: ignoreversion
Source: "D:\GitRepos\Secret-Agent-Man\SecretAgentMan\PublishedProgram\Content\crt.xnb"; DestDir: "{app}\Content"; Flags: ignoreversion
Source: "D:\GitRepos\Secret-Agent-Man\SecretAgentMan\PublishedProgram\Content\cutscene.wma"; DestDir: "{app}\Content"; Flags: ignoreversion
Source: "D:\GitRepos\Secret-Agent-Man\SecretAgentMan\PublishedProgram\Content\cutscene.xnb"; DestDir: "{app}\Content"; Flags: ignoreversion
Source: "D:\GitRepos\Secret-Agent-Man\SecretAgentMan\PublishedProgram\Content\cutsceneframe.xnb"; DestDir: "{app}\Content"; Flags: ignoreversion
Source: "D:\GitRepos\Secret-Agent-Man\SecretAgentMan\PublishedProgram\Content\enemy_sfx_coin_1.xnb"; DestDir: "{app}\Content"; Flags: ignoreversion
Source: "D:\GitRepos\Secret-Agent-Man\SecretAgentMan\PublishedProgram\Content\enemy_sfx_coin_2.xnb"; DestDir: "{app}\Content"; Flags: ignoreversion
Source: "D:\GitRepos\Secret-Agent-Man\SecretAgentMan\PublishedProgram\Content\enemy_sfx_coin_3.xnb"; DestDir: "{app}\Content"; Flags: ignoreversion
Source: "D:\GitRepos\Secret-Agent-Man\SecretAgentMan\PublishedProgram\Content\floppy.xnb"; DestDir: "{app}\Content"; Flags: ignoreversion
Source: "D:\GitRepos\Secret-Agent-Man\SecretAgentMan\PublishedProgram\Content\frame.xnb"; DestDir: "{app}\Content"; Flags: ignoreversion
Source: "D:\GitRepos\Secret-Agent-Man\SecretAgentMan\PublishedProgram\Content\game-over.wma"; DestDir: "{app}\Content"; Flags: ignoreversion
Source: "D:\GitRepos\Secret-Agent-Man\SecretAgentMan\PublishedProgram\Content\game-over.xnb"; DestDir: "{app}\Content"; Flags: ignoreversion
Source: "D:\GitRepos\Secret-Agent-Man\SecretAgentMan\PublishedProgram\Content\gameover1.xnb"; DestDir: "{app}\Content"; Flags: ignoreversion
Source: "D:\GitRepos\Secret-Agent-Man\SecretAgentMan\PublishedProgram\Content\gameover2.xnb"; DestDir: "{app}\Content"; Flags: ignoreversion
Source: "D:\GitRepos\Secret-Agent-Man\SecretAgentMan\PublishedProgram\Content\gameover3.xnb"; DestDir: "{app}\Content"; Flags: ignoreversion
Source: "D:\GitRepos\Secret-Agent-Man\SecretAgentMan\PublishedProgram\Content\gameover4.xnb"; DestDir: "{app}\Content"; Flags: ignoreversion
Source: "D:\GitRepos\Secret-Agent-Man\SecretAgentMan\PublishedProgram\Content\hiscore.wma"; DestDir: "{app}\Content"; Flags: ignoreversion
Source: "D:\GitRepos\Secret-Agent-Man\SecretAgentMan\PublishedProgram\Content\hiscore.xnb"; DestDir: "{app}\Content"; Flags: ignoreversion
Source: "D:\GitRepos\Secret-Agent-Man\SecretAgentMan\PublishedProgram\Content\hud.xnb"; DestDir: "{app}\Content"; Flags: ignoreversion
Source: "D:\GitRepos\Secret-Agent-Man\SecretAgentMan\PublishedProgram\Content\ingame.wma"; DestDir: "{app}\Content"; Flags: ignoreversion
Source: "D:\GitRepos\Secret-Agent-Man\SecretAgentMan\PublishedProgram\Content\ingame.xnb"; DestDir: "{app}\Content"; Flags: ignoreversion
Source: "D:\GitRepos\Secret-Agent-Man\SecretAgentMan\PublishedProgram\Content\lives11x7.xnb"; DestDir: "{app}\Content"; Flags: ignoreversion
Source: "D:\GitRepos\Secret-Agent-Man\SecretAgentMan\PublishedProgram\Content\load-screen-360p-nofilter.xnb"; DestDir: "{app}\Content"; Flags: ignoreversion
Source: "D:\GitRepos\Secret-Agent-Man\SecretAgentMan\PublishedProgram\Content\loader.wma"; DestDir: "{app}\Content"; Flags: ignoreversion
Source: "D:\GitRepos\Secret-Agent-Man\SecretAgentMan\PublishedProgram\Content\loader.xnb"; DestDir: "{app}\Content"; Flags: ignoreversion
Source: "D:\GitRepos\Secret-Agent-Man\SecretAgentMan\PublishedProgram\Content\mayor50x50.xnb"; DestDir: "{app}\Content"; Flags: ignoreversion
Source: "D:\GitRepos\Secret-Agent-Man\SecretAgentMan\PublishedProgram\Content\plane5x3.xnb"; DestDir: "{app}\Content"; Flags: ignoreversion
Source: "D:\GitRepos\Secret-Agent-Man\SecretAgentMan\PublishedProgram\Content\planeflipped5x3.xnb"; DestDir: "{app}\Content"; Flags: ignoreversion
Source: "D:\GitRepos\Secret-Agent-Man\SecretAgentMan\PublishedProgram\Content\player25x25.xnb"; DestDir: "{app}\Content"; Flags: ignoreversion
Source: "D:\GitRepos\Secret-Agent-Man\SecretAgentMan\PublishedProgram\Content\player_sfx_coin_1.xnb"; DestDir: "{app}\Content"; Flags: ignoreversion
Source: "D:\GitRepos\Secret-Agent-Man\SecretAgentMan\PublishedProgram\Content\player_sfx_coin_2.xnb"; DestDir: "{app}\Content"; Flags: ignoreversion
Source: "D:\GitRepos\Secret-Agent-Man\SecretAgentMan\PublishedProgram\Content\rip25x25.xnb"; DestDir: "{app}\Content"; Flags: ignoreversion
Source: "D:\GitRepos\Secret-Agent-Man\SecretAgentMan\PublishedProgram\Content\sfx_enemydeath1.xnb"; DestDir: "{app}\Content"; Flags: ignoreversion
Source: "D:\GitRepos\Secret-Agent-Man\SecretAgentMan\PublishedProgram\Content\sfx_enemydeath2.xnb"; DestDir: "{app}\Content"; Flags: ignoreversion
Source: "D:\GitRepos\Secret-Agent-Man\SecretAgentMan\PublishedProgram\Content\sfx_enemydeath3.xnb"; DestDir: "{app}\Content"; Flags: ignoreversion
Source: "D:\GitRepos\Secret-Agent-Man\SecretAgentMan\PublishedProgram\Content\sfx_explosion.xnb"; DestDir: "{app}\Content"; Flags: ignoreversion
Source: "D:\GitRepos\Secret-Agent-Man\SecretAgentMan\PublishedProgram\Content\sfx_explosion_small.xnb"; DestDir: "{app}\Content"; Flags: ignoreversion
Source: "D:\GitRepos\Secret-Agent-Man\SecretAgentMan\PublishedProgram\Content\sfx_gun1.xnb"; DestDir: "{app}\Content"; Flags: ignoreversion
Source: "D:\GitRepos\Secret-Agent-Man\SecretAgentMan\PublishedProgram\Content\sfx_gun10.xnb"; DestDir: "{app}\Content"; Flags: ignoreversion
Source: "D:\GitRepos\Secret-Agent-Man\SecretAgentMan\PublishedProgram\Content\sfx_gun2.xnb"; DestDir: "{app}\Content"; Flags: ignoreversion
Source: "D:\GitRepos\Secret-Agent-Man\SecretAgentMan\PublishedProgram\Content\sfx_gun3.xnb"; DestDir: "{app}\Content"; Flags: ignoreversion
Source: "D:\GitRepos\Secret-Agent-Man\SecretAgentMan\PublishedProgram\Content\sfx_gun4.xnb"; DestDir: "{app}\Content"; Flags: ignoreversion
Source: "D:\GitRepos\Secret-Agent-Man\SecretAgentMan\PublishedProgram\Content\sfx_gun5.xnb"; DestDir: "{app}\Content"; Flags: ignoreversion
Source: "D:\GitRepos\Secret-Agent-Man\SecretAgentMan\PublishedProgram\Content\sfx_gun6.xnb"; DestDir: "{app}\Content"; Flags: ignoreversion
Source: "D:\GitRepos\Secret-Agent-Man\SecretAgentMan\PublishedProgram\Content\sfx_gun7.xnb"; DestDir: "{app}\Content"; Flags: ignoreversion
Source: "D:\GitRepos\Secret-Agent-Man\SecretAgentMan\PublishedProgram\Content\sfx_gun8.xnb"; DestDir: "{app}\Content"; Flags: ignoreversion
Source: "D:\GitRepos\Secret-Agent-Man\SecretAgentMan\PublishedProgram\Content\sfx_gun9.xnb"; DestDir: "{app}\Content"; Flags: ignoreversion
Source: "D:\GitRepos\Secret-Agent-Man\SecretAgentMan\PublishedProgram\Content\sfx_noammo2.xnb"; DestDir: "{app}\Content"; Flags: ignoreversion
Source: "D:\GitRepos\Secret-Agent-Man\SecretAgentMan\PublishedProgram\Content\sfx_pickup-ammo.xnb"; DestDir: "{app}\Content"; Flags: ignoreversion
Source: "D:\GitRepos\Secret-Agent-Man\SecretAgentMan\PublishedProgram\Content\sfx_playerdeath.xnb"; DestDir: "{app}\Content"; Flags: ignoreversion
Source: "D:\GitRepos\Secret-Agent-Man\SecretAgentMan\PublishedProgram\Content\start-screen-frame.xnb"; DestDir: "{app}\Content"; Flags: ignoreversion
Source: "D:\GitRepos\Secret-Agent-Man\SecretAgentMan\PublishedProgram\Content\start-screen-gun.xnb"; DestDir: "{app}\Content"; Flags: ignoreversion
Source: "D:\GitRepos\Secret-Agent-Man\SecretAgentMan\PublishedProgram\Content\start-screen-logo433x54.xnb"; DestDir: "{app}\Content"; Flags: ignoreversion
Source: "D:\GitRepos\Secret-Agent-Man\SecretAgentMan\PublishedProgram\Content\start-screen-portraits.xnb"; DestDir: "{app}\Content"; Flags: ignoreversion
Source: "D:\GitRepos\Secret-Agent-Man\SecretAgentMan\PublishedProgram\Content\Static50x50.xnb"; DestDir: "{app}\Content"; Flags: ignoreversion
Source: "D:\GitRepos\Secret-Agent-Man\SecretAgentMan\PublishedProgram\Content\static_mayor.xnb"; DestDir: "{app}\Content"; Flags: ignoreversion
Source: "D:\GitRepos\Secret-Agent-Man\SecretAgentMan\PublishedProgram\Content\title_anim-465x205x5x17.xnb"; DestDir: "{app}\Content"; Flags: ignoreversion
Source: "D:\GitRepos\Secret-Agent-Man\SecretAgentMan\PublishedProgram\Content\water640x30.xnb"; DestDir: "{app}\Content"; Flags: ignoreversion

[Icons]
Name: "{autoprograms}\{#MyAppName}"; Filename: "{app}\{#MyAppExeName}"
Name: "{autodesktop}\{#MyAppName}"; Filename: "{app}\{#MyAppExeName}"; Tasks: desktopicon

[Run]
Filename: "{app}\{#MyAppExeName}"; Description: "{cm:LaunchProgram,{#StringChange(MyAppName, '&', '&&')}}"; Flags: nowait postinstall skipifsilent
