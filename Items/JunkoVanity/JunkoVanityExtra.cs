using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.DataStructures;
using Terraria.Graphics.Shaders;
using Terraria.ModLoader;

namespace JunkoAndFriends.Items.JunkoVanity
{
    public class JunkoVanityExtra
    {
        public static readonly PlayerLayer JunkoAura = new PlayerLayer("JunkoAndFriends", "JunkoAura", PlayerLayer.MiscEffectsBack, delegate (PlayerDrawInfo drawInfo)
        {
            if (drawInfo.shadow != 0 || drawInfo.drawPlayer.dead)
                return;

            Player drawPlayer = drawInfo.drawPlayer;
            Mod mod = ModLoader.GetMod("JunkoAndFriends");

            if (drawPlayer.head != mod.GetEquipSlot("JunkoHead", EquipType.Head) || drawPlayer.body != mod.GetEquipSlot("JunkoBody", EquipType.Body))
                return;

            float drawX = (int)drawInfo.position.X + drawPlayer.width / 2;
            float drawY = (int)drawInfo.position.Y + drawPlayer.height / 2;

            Vector2 origin = drawInfo.bodyOrigin;

            Vector2 position = new Vector2(drawX, drawY) + drawPlayer.bodyPosition - Main.screenPosition;

            Color color = Color.White;

            float rotation = drawPlayer.bodyRotation;

            SpriteEffects spriteEffects = drawInfo.spriteEffects;

            DrawData drawData = new DrawData(JunkoAndFriendsRenderTargets.Junko.junkoAura, position, null, color, rotation, new Vector2(JunkoAndFriendsRenderTargets.Junko.junkoAura.Width / 2f, JunkoAndFriendsRenderTargets.Junko.junkoAura.Height / 2f), 1f, spriteEffects, 0);
            drawData.shader = drawInfo.bodyArmorShader;

            Main.playerDrawData.Add(drawData);

            Lighting.AddLight(drawPlayer.position, new Vector3(0.870588235f, 0.396078431f, 0.611764706f));
        });
    }
}
