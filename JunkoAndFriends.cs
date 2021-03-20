using JunkoAndFriends.Items;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Terraria;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.ModLoader;

namespace JunkoAndFriends
{
    public class JunkoAndFriends : Mod
    {
        internal static JunkoAndFriends Instance;
        public static ModHotKey SpecialEffectKey; //will make vanities do special things!

        public override void Load()
        {
            Instance = this;
            if (!Main.dedServ)
            {
                AddEquipTexture(null, EquipType.Legs, "Junko_Legs", "JunkoAndFriends/Items/JunkoVanity/JunkoLeg_Legs");
                AddEquipTexture(null, EquipType.Legs, "MoriLeg_Legs", "JunkoAndFriends/Items/MoriVanity/MoriLeg_Legs");
                AddEquipTexture(null, EquipType.Legs, "AmeliaLeg_Legs", "JunkoAndFriends/Items/AmeliaVanity/AmeliaLeg_Legs");
                GameShaders.Armor.BindShader(ModContent.ItemType<WaveShaderDye>(), new ArmorShaderData(new Ref<Effect>(GetEffect("Effects/WaveShader")), "WaveShaderPass"));
            }

            SpecialEffectKey = RegisterHotKey("Special Vanity Effect", "NumPad5");

            guraGawrDownFrames = new List<int>
            {
                56,
                112,
                168,
                224,
                336,
                560,
                616,
                672,
                728
            };

            TerrarianUpFrames = new List<int>
            {
                392,
                448,
                504,
                784,
                840,
                896
            };
        }

        public override void Unload()
        {
            Instance = null;
            guraGawrDownFrames = null;
            SpecialEffectKey = null;
            TerrarianUpFrames = null;
        }

        public static List<int> guraGawrDownFrames;
        public static List<int> TerrarianUpFrames;

        public override void HandlePacket(BinaryReader reader, int whoAmI)
        {
            MessageType msgType = (MessageType)reader.ReadByte();
            switch(msgType)
            {
                case MessageType.SyncPlayer:
                    byte playerNumber = reader.ReadByte();
                    JunkoAndFriendsPlayer player = Main.player[playerNumber].GetModPlayer<JunkoAndFriendsPlayer>();
                    player.vanitySpecialEffect = reader.ReadBoolean();
                    player.berserkerIsBerserk = reader.ReadBoolean();
                    player.guraGawrDoA = reader.ReadBoolean();
                    break;

                case MessageType.SyncVanitySpecialEffect:
                    playerNumber = reader.ReadByte();
                    player = Main.player[playerNumber].GetModPlayer<JunkoAndFriendsPlayer>();
                    player.vanitySpecialEffect = reader.ReadBoolean();
                    if (Main.netMode == NetmodeID.Server)
                    {
                        var packet = GetPacket();
                        packet.Write((byte)MessageType.SyncVanitySpecialEffect);
                        packet.Write(playerNumber);
                        packet.Write(player.vanitySpecialEffect);
                        packet.Send(-1, playerNumber);
                    }
                    break;

                case MessageType.SyncBerserkerIsBerserk:
                    playerNumber = reader.ReadByte();
                    player = Main.player[playerNumber].GetModPlayer<JunkoAndFriendsPlayer>();
                    player.berserkerIsBerserk = reader.ReadBoolean();
                    if (Main.netMode == NetmodeID.Server)
                    {
                        var packet = GetPacket();
                        packet.Write((byte)MessageType.SyncBerserkerIsBerserk);
                        packet.Write(playerNumber);
                        packet.Write(player.berserkerIsBerserk);
                        packet.Send(-1, playerNumber);
                    }
                    break;

                case MessageType.SyncGuraGawrDoA:
                    playerNumber = reader.ReadByte();
                    player = Main.player[playerNumber].GetModPlayer<JunkoAndFriendsPlayer>();
                    player.guraGawrDoA = reader.ReadBoolean();
                    if (Main.netMode == NetmodeID.Server)
                    {
                        var packet = GetPacket();
                        packet.Write((byte)MessageType.SyncGuraGawrDoA);
                        packet.Write(playerNumber);
                        packet.Write(player.guraGawrDoA);
                        packet.Send(-1, playerNumber);
                    }
                    break;
            }
        }
    }

    public enum MessageType
    {
        SyncPlayer = 0,
        SyncVanitySpecialEffect = 1,
        SyncBerserkerIsBerserk = 2,
        SyncGuraGawrDoA = 3
    }
}