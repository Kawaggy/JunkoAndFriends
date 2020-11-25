using JunkoAndFriends.Items;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System.Text;
using Terraria;
using Terraria.Graphics.Shaders;
using Terraria.ModLoader;

namespace JunkoAndFriends
{
    public class JunkoAndFriends : Mod
    {
        internal static JunkoAndFriends Instance;
        public static ModHotKey SpecialEffectKey; //will make vanities do special things!
        internal static Mod extraSounds;

        public override void Load()
        {
            Instance = this;
            if (!Main.dedServ)
            {
                AddEquipTexture(null, EquipType.Legs, "Junko_Legs", "JunkoAndFriends/Items/JunkoVanity/JunkoLeg_Legs");
                AddEquipTexture(null, EquipType.Legs, "MoriLeg_Legs", "JunkoAndFriends/Items/MoriVanity/MoriLeg_Legs");
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
            extraSounds = null;
        }

        public override void PostSetupContent()
        {
            extraSounds = ModLoader.GetMod("KawaggyModExtraSounds");
        }

        public static List<int> guraGawrDownFrames;
        public static List<int> TerrarianUpFrames;
    }
}