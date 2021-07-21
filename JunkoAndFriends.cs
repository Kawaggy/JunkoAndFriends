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
                GameShaders.Misc["WaveShader"] = new MiscShaderData(new Ref<Effect>(GetEffect("Effects/WaveShader")), "WaveShaderPass");
                JunkoAndFriendsRenderTargets.Initialize();
            }
            
            On.Terraria.Main.Draw += Main_Draw;

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

        private void Main_Draw(On.Terraria.Main.orig_Draw orig, Main self, Microsoft.Xna.Framework.GameTime gameTime)
        {
            if (!Main.dedServ)
                JunkoAndFriendsRenderTargets.Draw();
            orig(self, gameTime);
        }

        public override void Unload()
        {
            guraGawrDownFrames = null;
            SpecialEffectKey = null;
            TerrarianUpFrames = null;
            JunkoAndFriendsRenderTargets.Unload();
            Instance = null;
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
                    player.berserkerDoTransformation = reader.ReadBoolean();
                    player.guraGawrDoA = reader.ReadBoolean();
                    player.pekoraSmoll = reader.ReadBoolean();
                    player.berserkerHelmetFrame = reader.ReadByte();
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

                case MessageType.SyncBerserkerDoTransformation:
                    playerNumber = reader.ReadByte();
                    player = Main.player[playerNumber].GetModPlayer<JunkoAndFriendsPlayer>();
                    player.berserkerDoTransformation = reader.ReadBoolean();
                    if (Main.netMode == NetmodeID.Server)
                    {
                        var packet = GetPacket();
                        packet.Write((byte)MessageType.SyncBerserkerDoTransformation);
                        packet.Write(playerNumber);
                        packet.Write(player.berserkerDoTransformation);
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

                case MessageType.SyncPekoraSmoll:
                    playerNumber = reader.ReadByte();
                    player = Main.player[playerNumber].GetModPlayer<JunkoAndFriendsPlayer>();
                    player.pekoraSmoll = reader.ReadBoolean();
                    if (Main.netMode == NetmodeID.Server)
                    {
                        var packet = GetPacket();
                        packet.Write((byte)MessageType.SyncPekoraSmoll);
                        packet.Write(playerNumber);
                        packet.Write(player.pekoraSmoll);
                        packet.Send(-1, playerNumber);
                    }
                    break;

                case MessageType.SyncBerserkerHelmetFrame:
                    playerNumber = reader.ReadByte();
                    player = Main.player[playerNumber].GetModPlayer<JunkoAndFriendsPlayer>();
                    player.berserkerHelmetFrame = reader.ReadByte();
                    if (Main.netMode == NetmodeID.Server)
                    {
                        var packet = GetPacket();
                        packet.Write((byte)MessageType.SyncBerserkerHelmetFrame);
                        packet.Write(playerNumber);
                        packet.Write((byte)player.berserkerHelmetFrame);
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
        SyncBerserkerDoTransformation = 3,
        SyncGuraGawrDoA = 4,
        SyncPekoraSmoll = 5,
        SyncBerserkerHelmetFrame = 6
    }
}