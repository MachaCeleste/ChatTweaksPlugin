using ChatTweaksPlugin;
using HarmonyLib;
using System;
using Util;

[HarmonyPatch]
public class ChatGuildPatch
{
    [HarmonyPatch(typeof(ChatGuild), "FormatNickName")]
    class FormatNickNamePatch
    {
        static bool Prefix(ChatGuild __instance, ref string rawNickName, ref Roles role, ref string __result)
        {
            string result;
            string color = "";
            if (role >= Roles.MODERATOR)
            {
                color = "yellow";
                if (DataUtils.TryParseColor(Plugin.modColor.Value, out _))
                    color = Plugin.modColor.Value;
            }
            else if (rawNickName.Equals(PlayerClient.Singleton.player.clientChatName))
            {
                color = "#00FFFF";
                if (DataUtils.TryParseColor(Plugin.selfColor.Value, out _))
                    color = Plugin.selfColor.Value;
            }
            else
            {
                color = "#5CF300";
                if (DataUtils.TryParseColor(Plugin.pcColor.Value, out _))
                    color = Plugin.pcColor.Value;
            }
            result = $"<color={color}>{rawNickName}</color>";
            if (Plugin.timeStamp.Value)
            {
                var dt = DateTime.Now;
                var time = dt.ToString("hh:mm tt");
                if (Plugin.milTime.Value)
                {
                    time = dt.ToString("HH:mm");
                }
                result += $" <size=80%>- {time}</size>";
            }
            __result = result;
            return false;
        }
    }
}