using ChatTweaksPlugin;
using HarmonyLib;
using System.Reflection;
using TMPro;
using UnityEngine;
using Util;

[HarmonyPatch]
public class UserChatListPatch
{
    [HarmonyPatch(typeof(UserChatList), "Configure")]
    class ConfigurePatch
    {
        static void Postfix(UserChatList __instance, ref GlobalChat.UserChat userChat)
        {
            FieldInfo fieldInfo = AccessTools.Field(typeof(UserChatList), "tmpNickName");
            TMP_Text tmpNickName = fieldInfo.GetValue(__instance) as TMP_Text;
            Color color = default;
            if (userChat.role > Roles.EVERYONE)
            {
                DataUtils.TryParseColor(Plugin.modColor.Value, out color);
            }
            else if (userChat.nickName.Equals(PlayerClient.Singleton.player.clientChatName))
            {
                DataUtils.TryParseColor(Plugin.selfColor.Value, out color);
            }
            else
            {
                DataUtils.TryParseColor(Plugin.pcColor.Value, out color);
            }
            if (color != default)
                tmpNickName.color = color;
        }
    }
}