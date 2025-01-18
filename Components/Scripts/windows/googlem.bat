@echo off

REM Get the adb path from the first argument
SET ADB_EXECUTABLE=%1

REM Check if adb path was provided
IF "%ADB_EXECUTABLE%"=="" (
    echo Error: ADB executable path not provided.
    exit /b 1
)

@REM ==================================
@REM Add app package names below...
@REM ==================================
for %%X in (
"com.android.hotwordenrollment.okgoogle"
"com.google.android.apps.googleassistant"
"com.google.android.apps.magazines"
"com.google.android.inputmethod.latin"
"com.google.android.inputmethod.pinyin"
"com.google.android.inputmethod.japanese"
"com.google.android.inputmethod.korean"
"com.google.android.apps.docs"
"com.google.android.apps.photos"
"com.google.android.apps.tachyon"
"com.google.android.apps.translate"
"com.google.android.apps.youtube"
"com.google.android.apps.books"
"com.google.android.feedback"
"com.google.android.apps.magazines"
"com.google.android.apps.maps"
"com.google.android.projection.gearhead"
"com.google.android.apps.messaging"
"com.google.android.apps.walletnfcrel"
"com.google.android.apps.wallet"
"com.google.android.apps.youtube.music"
"com.google.android.apps.nbu.paisa.user"
"com.google.android.googlequicksearchbox"
"com.google.android.printservice.recommendation"
"com.google.android.apps.subscriptions.red"
"com.google.android.apps.podcasts"
"com.google.android.music"
"com.google.ar.lens"
"com.google.android.apps.wellbeing"
) do (
    echo Uninstalling %%X...
    "%ADB_EXECUTABLE%" shell pm uninstall %%X
    IF ERRORLEVEL 1 (
        echo Failed to uninstall %%X
    ) ELSE (
        echo Successfully uninstalled %%X
    )
    "%ADB_EXECUTABLE%" shell pm uninstall --user 0 %%X
    IF ERRORLEVEL 1 (
        echo Failed to uninstall for user 0 %%X
    ) ELSE (
        echo Successfully uninstalled for user 0 %%X
    )
)
