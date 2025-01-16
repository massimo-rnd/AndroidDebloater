@echo off
for %%X in (
"com.amazon.mShop.android.shopping"
"com.netflix.partner.activation"
"ru.yandex.searchplugin"
"com.ebay.mobile"
"com.ebay.carrier"
"com.alibaba.aliexpresshd"
"sg.bigo.live"
) do (
adb shell pm uninstall %%X
adb shell pm uninstall --user 0 %%X
)