using JunkoAndFriends.Items;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using Terraria;
using Terraria.Graphics.Shaders;
using Terraria.ModLoader;

namespace JunkoAndFriends
{
    public class JunkoAndFriends : Mod
    {
        internal static Mod junkoVanity;
        internal static JunkoAndFriends Instance;

        public override void Load()
        {
            Instance = this;
            junkoVanity = ModLoader.GetMod("JunkoVanity");
            if (!Main.dedServ)
            {
                AddEquipTexture(null, EquipType.Legs, "Junko_Legs", "JunkoAndFriends/Items/JunkoVanity/JunkoLeg_Legs");
                AddEquipTexture(null, EquipType.Legs, "MoriLeg_Legs", "JunkoAndFriends/Items/MoriVanity/MoriLeg_Legs");
                GameShaders.Armor.BindShader(ModContent.ItemType<WaveShaderDye>(), new ArmorShaderData(new Ref<Effect>(GetEffect("Effects/WaveShader")), "WaveShaderPass"));
            }

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
        }

        public override void Unload()
        {
            Instance = null;
            junkoVanity = null;
            guraGawrDownFrames = null;
        }

        public static List<int> guraGawrDownFrames;
    }
}