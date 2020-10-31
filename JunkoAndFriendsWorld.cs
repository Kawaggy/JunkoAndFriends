using Terraria;
using System.IO;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;

namespace JunkoAndFriends
{
    public class JunkoAndFriendsWorld : ModWorld
    {
        public static bool eclipsePassed = false;

        public override void Initialize()
        {
            eclipsePassed = false;
        }

        public override void PostUpdate()
        {
            if (Main.eclipse) eclipsePassed = true;
        }

        public override TagCompound Save() => new TagCompound
        {
            ["eclipsePassed"] = eclipsePassed
        };

        public override void Load(TagCompound tag)
        {
            eclipsePassed = tag.GetBool("eclipsePassed");
        }

        public override void NetSend(BinaryWriter writer)
        {
            var flags = new BitsByte();
            flags[0] = eclipsePassed;

            writer.Write(flags);
        }

        public override void NetReceive(BinaryReader reader)
        {
            BitsByte flags = reader.ReadByte();
            eclipsePassed = flags[0];
        }
    }
}
