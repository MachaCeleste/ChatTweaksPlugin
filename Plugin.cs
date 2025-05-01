using BepInEx;
using BepInEx.Configuration;
using BepInEx.Logging;
using HarmonyLib;

namespace ChatTweaksPlugin;

[BepInPlugin("com.machaceleste.chattweaksplugin", MyPluginInfo.PLUGIN_NAME, MyPluginInfo.PLUGIN_VERSION)]
public class Plugin : BaseUnityPlugin
{
    internal static new ManualLogSource Logger;

    public static ConfigEntry<bool> timeStamp;
    public static ConfigEntry<bool> milTime;
    public static ConfigEntry<string> modColor;
    public static ConfigEntry<string> selfColor;
    public static ConfigEntry<string> pcColor;

    private void Awake()
    {
        timeStamp = Config.Bind("Main", "1. Timestamp", true, "Enable chat timestamps.");
        milTime = Config.Bind("Main", "2. 24 Hour Time", false, "Enable 24 hour mode for chat timestamps.");
        modColor = Config.Bind("Main", "3. Moderator Color", "yellow", "Select chat name color for moderators.");
        selfColor = Config.Bind("Main", "4. Self Color", "#00FFFF", "Select chat name color for yourself.");
        pcColor = Config.Bind("Main", "5. Player Color", "#5CF300", "Select chat name color for other players.");

        Logger = base.Logger;
        Logger.LogInfo($"Plugin {MyPluginInfo.PLUGIN_GUID} is loaded!");
        var harmony = new Harmony("com.machaceleste.chattweaksplugin");
        harmony.PatchAll();
    }
}