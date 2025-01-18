#!/bin/bash

ADB_EXECUTABLE="$1"

# Check if adb path was provided
if [ -z "$ADB_EXECUTABLE" ]; then
    echo "Error: ADB executable path not provided."
    exit 1
fi

# Declare the package array
declare -a arr=(
    "com.baidu.input_huawei"
    "com.huawei.appmarket"
    "com.huawei.android.hwpay"
    "com.huawei.android.karaoke"
    "com.huawei.android.tips"
    "com.huawei.android.totemweather"
    "com.huawei.browser"
    "com.huawei.compass"
    "com.huawei.gameassistant"
    "com.huawei.himovie.overseas"
    "com.huawei.phoneservice"
    "com.huawei.vassistant"
    "com.huawei.videoeditor"
    "com.huawei.wallet"
    "com.iflytek.speechsuite"
    "com.huawei.music"
    "com.huawei.browser"
    "com.huawei.hitouch"
    "com.huawei.fastapp"
    "com.huawei.magazine"
    "com.huawei.hwpanpayservice"
    "com.huawei.contactscamcard"
    "com.huawei.android.instantshare"
    "com.huawei.intelligent"
    "com.huawei.hwstartupguide"
    "com.huawei.hwdetectrepair"
    "com.huawei.featurelayer.sharedfeature.map"
    "com.huawei.scanner"
    "com.huawei.notepad"
    "com.huawei.health"
    "com.huawei.email"
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
    "com.android.printspooler"
    "com.android.bookmarkprovider"
    "com.android.filemanager"
    "com.android.notes"
    "com.android.providers.partnerbookmarks"
    "com.android.stk"
    "com.android.stk2"
    "com.android.VideoPlayer"
    "com.amazon.mShop.android.shopping"
    "com.netflix.partner.activation"
    "ru.yandex.searchplugin"
    "com.ebay.mobile"
    "com.ebay.carrier"
    "com.alibaba.aliexpresshd"
    "sg.bigo.live"
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
