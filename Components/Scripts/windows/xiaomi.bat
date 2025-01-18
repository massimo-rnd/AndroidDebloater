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
"cn.wps.xiaomi.abroad.lite"
"cn.wps.moffice_eng"
"com.duokan.phone.remotecontroller"
"com.miui.klo.bugreport"
"com.miui.virtualsim"
"com.mi.global.bbs"
"com.mi.globalbrowser"
"com.micredit.in"
"com.miui.notes"
"com.miui.player"
"com.miui.videoplayer"
"com.miui.weather2"
"com.miui.weather"
"com.tencent.igxiaomi"
"com.xiaomi.midrop"
"com.xiaomi.payment"
"com.xiaomi.hm.health"
"com.xiaomi.shop"
"com.xiaomi.shop2"
"com.miui.analytics"
"com.xiaomi.mipicks"
"com.miui.msa.global"
"com.xiaomi.glgm"
"com.xiaomi.gamecenter"
"com.xiaomi.payment"
"com.mipay.wallet.in"
"com.tencent.soter.soterserver"
"com.miui.videoplayer"
"com.miui.player"
"com.miui.yellowpage"
"com.miui.bugreport"
"com.miui.miservice"
"com.miui.weather2"
"com.mi.android.globalFileexplorer"
"com.miui.weather"
"com.miui.hybrid"
"com.miui.hybrid.accessory"
"com.xiaomi.joyose"
"com.mi.globalTrendNews"
"com.miui.compass"
"com.miui.enbbs"
"com.miui.fmservice"
"com.miui.huanji"
"com.miui.nextpay"
"com.miui.translation.kingsoft"
"com.miui.userguide"
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
