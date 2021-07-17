using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.DataStructures;
using Terraria.Graphics.Shaders;
using Terraria.ModLoader;

namespace JunkoAndFriends.Items.BerserkerVanity
{
    public class BerserkerVanityExtra
    {
        public static readonly PlayerLayer BerserkerHelmetBerserk = new PlayerLayer("JunkoAndFriends", "BerserkerHelmetBerserk", PlayerLayer.Head, delegate (PlayerDrawInfo drawInfo)
        {
            if (drawInfo.shadow != 0 || drawInfo.drawPlayer.dead)
                return;

            Player drawPlayer = drawInfo.drawPlayer;
            Mod mod = ModLoader.GetMod("JunkoAndFriends");

            if (drawPlayer.head != mod.GetEquipSlot("BerserkerHead", EquipType.Head))
                return;

            Texture2D helmetTexture = mod.GetTexture("ExtraTextures/BerserkerHelmet");
            Texture2D glowmaskTexture = mod.GetTexture("ExtraTextures/BerserkerHelmet_Glowmask");
            Texture2D scarfTexture = mod.GetTexture("ExtraTextures/BerserkerScarf");

            float drawHelmetX = (int)drawInfo.position.X + drawPlayer.width / 2 + 2;
            float drawHelmetY = (int)drawInfo.position.Y + drawPlayer.height + 92;

            float drawScarfX = (int)drawInfo.position.X + drawPlayer.width / 2 + 8;
            float drawScarfY = (int)drawInfo.position.Y + drawPlayer.height + 114;

            SpriteEffects spriteEffects = drawInfo.spriteEffects;

            switch ((int)spriteEffects)
            {
                case 1:
                case 3:
                    drawHelmetX -= 4;
                    break;
            }

            bool flag = ((int)spriteEffects == 0 || (int)spriteEffects == 1);
            if (JunkoAndFriends.TerrarianUpFrames.Contains(drawPlayer.bodyFrame.Y) && flag)
            {
                drawHelmetY -= 2;
                drawScarfY -= 2;
            }
            else if (JunkoAndFriends.TerrarianUpFrames.Contains(drawPlayer.bodyFrame.Y))
            {
                drawHelmetY += 2;
                drawScarfY += 2;
            }

            if ((int)spriteEffects == 2 || (int)spriteEffects == 3)
            {
                drawHelmetY += 10;
            }
            Vector2 origin = drawInfo.headOrigin;

            Vector2 helmetPosition = new Vector2(drawHelmetX, drawHelmetY) + drawPlayer.headPosition - Main.screenPosition;
            Vector2 scarfPosition = new Vector2(drawScarfX, drawScarfY) + drawPlayer.headPosition - Main.screenPosition;

            float helmetRotation = drawPlayer.headRotation;

            Rectangle helmetFrame = new Rectangle(0, drawPlayer.Friends().berserkerHelmetFrame * (helmetTexture.Height / 8), helmetTexture.Width, helmetTexture.Height / 8);
            Rectangle scarfFrame = new Rectangle(0, 0, scarfTexture.Width, scarfTexture.Height);
            DrawData helmetDrawData = new DrawData(helmetTexture, helmetPosition, helmetFrame, drawInfo.upperArmorColor * (drawPlayer.Friends().berserkerHelmetFrame > 0 ? 1f : 0f), helmetRotation, new Vector2(helmetTexture.Width / 2f, helmetTexture.Height / 2f), 1f, spriteEffects, 0)
            {
                shader = drawInfo.headArmorShader
            };

            DrawData glowmaskDrawData = new DrawData(glowmaskTexture, helmetPosition, helmetFrame, new Color(255, 255, 255, 0) * (drawPlayer.Friends().berserkerHelmetFrame > 0 ? 1f : 0f), helmetRotation, new Vector2(helmetTexture.Width / 2f, helmetTexture.Height / 2f), 1f, spriteEffects, 0)
            {
                shader = drawInfo.headArmorShader
            };

            DrawData scarfDrawData = new DrawData(scarfTexture, scarfPosition, scarfFrame, drawInfo.middleArmorColor * (drawPlayer.Friends().berserkerHelmetFrame == 0 ? 1f : 0f), helmetRotation, new Vector2(helmetTexture.Width / 2f, helmetTexture.Height / 2f), 1f, spriteEffects, 0)
            {
                shader = drawInfo.headArmorShader
            };

            Main.playerDrawData.Add(helmetDrawData);
            Main.playerDrawData.Add(glowmaskDrawData);
            Main.playerDrawData.Add(scarfDrawData);
        });

        public static readonly PlayerLayer BerserkerCape = new PlayerLayer("JunkoAndFriends", "BerserkerCape", PlayerLayer.MiscEffectsBack, delegate (PlayerDrawInfo drawInfo)
        {
            if (drawInfo.shadow != 0 || drawInfo.drawPlayer.dead)
                return;

            Player drawPlayer = drawInfo.drawPlayer;
            Mod mod = ModLoader.GetMod("JunkoAndFriends");

            if (drawPlayer.body != mod.GetEquipSlot("BerserkerBody", EquipType.Body))
                return;

            Texture2D capeTexture = mod.GetTexture("ExtraTextures/BerserkerCape");

            float drawX = drawInfo.position.X + drawPlayer.width / 2f;
            float drawY = drawInfo.position.Y + drawPlayer.height / 2f - 4;

            SpriteEffects spriteEffects = drawInfo.spriteEffects;

            if ((int)spriteEffects == 0 || (int)spriteEffects == 2)
                drawX -= 12;
            else
                drawX += 12;

            if ((int)spriteEffects == 2 || (int)spriteEffects == 3)
                drawY -= 6;

            if ((int)spriteEffects == 2)
                spriteEffects = (SpriteEffects)0;

            if ((int)spriteEffects == 3)
                spriteEffects = (SpriteEffects)1;

            Vector2 origin = drawInfo.bodyOrigin;

            Vector2 capePosition = new Vector2(drawX, drawY) + drawPlayer.bodyPosition - Main.screenPosition;

            float capeRotation = drawPlayer.bodyRotation + drawPlayer.Friends().berserkerCapeRotation;

            DrawData capeDrawData = new DrawData(capeTexture, capePosition, null, drawInfo.middleArmorColor, capeRotation, new Vector2(capeTexture.Width / 2f, 0f), 1f, spriteEffects, 0)
            {
                shader = drawInfo.bodyArmorShader
            };

            Main.playerDrawData.Add(capeDrawData);
        });

        public static readonly PlayerHeadLayer BerserkerHeadMap = new PlayerHeadLayer("JunkoAndFriends", "BerserlerHeadLayer", delegate (PlayerHeadDrawInfo drawInfo)
        {
            Player drawPlayer = drawInfo.drawPlayer;
            Mod mod = ModLoader.GetMod("JunkoAndFriends");

            if (drawPlayer.head != mod.GetEquipSlot("BerserkerHead", EquipType.Head))
                return;

            Texture2D helmetTexture = mod.GetTexture("ExtraTextures/BerserkerHelmet");

            float drawX = (int)(drawPlayer.position.X - Main.screenPosition.X - drawPlayer.bodyFrame.Width / 2 +
                                 drawPlayer.width / 2);
            float drawY = (int)(drawPlayer.position.Y - Main.screenPosition.Y + drawPlayer.height -
                drawPlayer.bodyFrame.Height + 4);

            Vector2 position = new Vector2(drawX, drawY) + drawPlayer.headPosition + drawInfo.drawOrigin;

            Rectangle frame = new Rectangle(0, drawPlayer.Friends().berserkerHelmetFrame * (helmetTexture.Height / 8), helmetTexture.Width, helmetTexture.Height / 8); ;

            float rotation = drawPlayer.headRotation;

            Vector2 origin = drawInfo.drawOrigin;

            float scale = drawInfo.scale;

            SpriteEffects spriteEffects = drawInfo.spriteEffects;

            DrawData drawData = new DrawData(helmetTexture, position, frame, drawInfo.armorColor, rotation, origin, scale,
                spriteEffects, 0);

            GameShaders.Armor.Apply(drawInfo.armorShader, drawPlayer, drawData);

            drawData.Draw(Main.spriteBatch);

            Main.pixelShader.CurrentTechnique.Passes[0].Apply();
        });
    }
}
