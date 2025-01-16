@echo off
for %%X in (
"com.android.printspooler"
"com.android.bookmarkprovider"
"com.android.filemanager"
"com.android.notes"
"com.android.providers.partnerbookmarks"
"com.android.stk"
"com.android.stk2"
"com.android.VideoPlayer"
) do (
adb shell pm uninstall %%X
adb shell pm uninstall --user 0 %%X
)