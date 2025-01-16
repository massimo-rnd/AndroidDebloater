#!/bin/bash

ADB_EXECUTABLE="$1"

# Check if adb path was provided
if [ -z "$ADB_EXECUTABLE" ]; then
    echo "Error: ADB executable path not provided."
    exit 1
fi

# Declare the package array
declare -a arr=(
    "com.android.printspooler"
    "com.android.bookmarkprovider"
    "com.android.filemanager"
    "com.android.notes"
    "com.android.providers.partnerbookmarks"
    "com.android.stk"
    "com.android.stk2"
     "com.android.VideoPlayer"
)

# Command to remove the package
rm_pkg="shell pm uninstall --user 0"

# Loop through the packages and uninstall them
for i in "${arr[@]}"
do
    echo "Uninstalling $i..."
    "$ADB_EXECUTABLE" $rm_pkg "$i"
    if [ $? -ne 0 ]; then
        echo "Failed to uninstall $i"
    else
        echo "Successfully uninstalled $i"
    fi
done
