using Terraria.ModLoader;

namespace JunkoAndFriends
{
    public partial class JunkoAndFriendsPlayer : ModPlayer
    {
        public override void clientClone(ModPlayer clientClone)
        {
            JunkoAndFriendsPlayer clone = clientClone as JunkoAndFriendsPlayer;

            clone.vanitySpecialEffect = vanitySpecialEffect;
            clone.berserkerIsBerserk = berserkerIsBerserk;
            clone.berserkerDoTransformation = berserkerDoTransformation;
            clone.guraGawrDoA = guraGawrDoA;
            clone.pekoraSmoll = pekoraSmoll;
            clone.berserkerHelmetFrame = berserkerHelmetFrame;
        }

        public override void SyncPlayer(int toWho, int fromWho, bool newPlayer)
        {
            ModPacket packet = mod.GetPacket();
            packet.Write((byte)MessageType.SyncPlayer);
            packet.Write((byte)player.whoAmI);
            packet.Write(vanitySpecialEffect);
            packet.Write(berserkerIsBerserk);
            packet.Write(berserkerDoTransformation);
            packet.Write(guraGawrDoA);
            packet.Write(pekoraSmoll);
            packet.Write((byte)berserkerHelmetFrame);
            packet.Send(toWho, fromWho);
        }

        public override void SendClientChanges(ModPlayer clientPlayer)
        {
            JunkoAndFriendsPlayer clone = clientPlayer as JunkoAndFriendsPlayer;
            if (clone.vanitySpecialEffect != vanitySpecialEffect)
            {
                var packet = mod.GetPacket();
                packet.Write((byte)MessageType.SyncVanitySpecialEffect);
                packet.Write((byte)player.whoAmI);
                packet.Write(vanitySpecialEffect);
                packet.Send();
            }

            if (clone.berserkerIsBerserk != berserkerIsBerserk)
            {
                var packet = mod.GetPacket();
                packet.Write((byte)MessageType.SyncBerserkerIsBerserk);
                packet.Write((byte)player.whoAmI);
                packet.Write(berserkerIsBerserk);
                packet.Send();
            }

            if (clone.berserkerDoTransformation != berserkerDoTransformation)
            {
                var packet = mod.GetPacket();
                packet.Write((byte)MessageType.SyncBerserkerDoTransformation);
                packet.Write((byte)player.whoAmI);
                packet.Write(berserkerDoTransformation);
                packet.Send();
            }

            if (clone.guraGawrDoA != guraGawrDoA)
            {
                var packet = mod.GetPacket();
                packet.Write((byte)MessageType.SyncGuraGawrDoA);
                packet.Write((byte)player.whoAmI);
                packet.Write(guraGawrDoA);
                packet.Send();
            }

            if (clone.pekoraSmoll != pekoraSmoll)
            {
                var packet = mod.GetPacket();
                packet.Write((byte)MessageType.SyncPekoraSmoll);
                packet.Write((byte)player.whoAmI);
                packet.Write(pekoraSmoll);
                packet.Send();
            }

            if (clone.berserkerHelmetFrame != berserkerHelmetFrame)
            {
                var packet = mod.GetPacket();
                packet.Write((byte)MessageType.SyncBerserkerHelmetFrame);
                packet.Write((byte)player.whoAmI);
                packet.Write((byte)berserkerHelmetFrame);
                packet.Send();
            }
        }

    }
}
