using System;
using System.Linq;
using Terraria;

namespace JunkoAndFriends
{
    public static class JunkoAndFriendsUtils
    {
        public static JunkoAndFriendsPlayer Friends(this Player player) => player.GetModPlayer<JunkoAndFriendsPlayer>();
    }
}
