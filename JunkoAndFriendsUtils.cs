using System.Collections.Generic;
using System.Text;
using Terraria;
using Terraria.ModLoader;

namespace JunkoAndFriends
{
    public static class JunkoAndFriendsUtils
    {
        public static JunkoAndFriendsPlayer Friends(this Player player) => player.GetModPlayer<JunkoAndFriendsPlayer>();

        public static string UsedKeys(this ModHotKey hotkey)
        {
            if (Main.dedServ || hotkey == null)
                return "";

            List<string> assinedKeys = hotkey.GetAssignedKeys();
            if (assinedKeys.Count == 0) //there are none
                return "[NONE]";

            string keys = assinedKeys[0];
            for (int i = 1; i < assinedKeys.Count; i++)
                keys += " | " + assinedKeys[i];

            return keys;
        }
    }
}
