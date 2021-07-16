using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.DataStructures;
using Terraria.ModLoader;

namespace JunkoAndFriends.Items.GuraGawrVanity
{
    public class GuraGawrVanityExtra
    {
        public static readonly PlayerLayer GuraGawrA = new PlayerLayer("JunkoAndFriends", "GuraGawrA", PlayerLayer.Head, delegate (PlayerDrawInfo drawInfo)
        {
            if (drawInfo.shadow != 0 || drawInfo.drawPlayer.dead)
                return;

            Player drawPlayer = drawInfo.drawPlayer;
            Mod mod = ModLoader.GetMod("JunkoAndFriends");

            if (drawPlayer.head != mod.GetEquipSlot("GuraGawrHeadHair", EquipType.Head) && drawPlayer.head != mod.GetEquipSlot("GuraGawrHeadHoodie", EquipType.Head))
                return;

            Texture2D faceTexture = mod.GetTexture("ExtraTextures/GuraGawrAFace");

            float drawFaceX = (int)drawInfo.position.X + drawPlayer.width / 2 + 4;
            float drawFaceY = (int)drawInfo.position.Y + drawPlayer.height + 3;

            SpriteEffects spriteEffects = drawInfo.spriteEffects;

            switch ((int)spriteEffects)
            {
                case 1:
                case 3:
                    drawFaceX -= 8;
                    break;
            }

            bool flag = ((int)spriteEffects == 0 || (int)spriteEffects == 1);
            if (JunkoAndFriends.TerrarianUpFrames.Contains(drawPlayer.bodyFrame.Y) && flag)
            {
                drawFaceY -= 2;
            }
            else if (JunkoAndFriends.TerrarianUpFrames.Contains(drawPlayer.bodyFrame.Y))
            {
                drawFaceY += 2;
            }

            if ((int)spriteEffects == 2 || (int)spriteEffects == 3)
            {
                drawFaceY += 6;
            }
            Vector2 origin = drawInfo.headOrigin;

            Vector2 facePosition = new Vector2(drawFaceX, drawFaceY) + drawPlayer.headPosition - Main.screenPosition;

            float faceRotation = drawPlayer.headRotation;

            Rectangle faceFrame = new Rectangle(0, drawPlayer.Friends().guraGawrAFrame * (faceTexture.Height / 7), faceTexture.Width, faceTexture.Height / 7);

            DrawData faceDrawData = new DrawData(faceTexture, facePosition, faceFrame, drawInfo.upperArmorColor * (drawPlayer.Friends().guraGawrDoA ? 1f : 0f), faceRotation, new Vector2(faceTexture.Width / 2f, faceTexture.Height / 2f), 1f, spriteEffects, 0)
            {
                shader = drawInfo.headArmorShader
            };

            Main.playerDrawData.Add(faceDrawData);
        });

        public static readonly PlayerLayer GuraGawrTail = new PlayerLayer("JunkoAndFriends", "GuraGawrTail", PlayerLayer.MiscEffectsBack, delegate (PlayerDrawInfo drawInfo)
        {
            if (drawInfo.shadow != 0 || drawInfo.drawPlayer.dead)
                return;

            Player drawPlayer = drawInfo.drawPlayer;
            Mod mod = ModLoader.GetMod("JunkoAndFriends");

            if (drawPlayer.body != mod.GetEquipSlot("GuraGawrBody", EquipType.Body) || drawPlayer.legs != mod.GetEquipSlot("GuraGawrLeg", EquipType.Legs))
                return;

            Texture2D tailTexture = mod.GetTexture("ExtraTextures/GuraGawrTail");

            float drawX = (int)drawInfo.position.X + 2;
            float drawY = (int)drawInfo.position.Y + drawPlayer.height + 12;

            SpriteEffects spriteEffects = drawInfo.spriteEffects;

            if (spriteEffects == SpriteEffects.FlipHorizontally && spriteEffects != SpriteEffects.FlipVertically)
            {
                drawX = (int)drawInfo.position.X + drawPlayer.width + 14;
            }
            if (spriteEffects == SpriteEffects.FlipVertically)
            {
                drawX = (int)drawInfo.position.X + 2;
                drawY = (int)drawInfo.position.Y + 20;
            }
            if ((int)spriteEffects == 3)
            {
                drawY = (int)drawInfo.position.Y + 20;
                drawX = (int)drawInfo.position.X + drawPlayer.width + 14;
            }

            Vector2 origin = drawInfo.bodyOrigin;

            Vector2 tailPosition = new Vector2(drawX, drawY) + drawPlayer.bodyPosition - Main.screenPosition;

            float tailRotation = drawPlayer.bodyRotation;

            Rectangle tailFrame = new Rectangle(0, drawPlayer.Friends().guraGawrTailFrame * (tailTexture.Height / 4), tailTexture.Width, tailTexture.Height / 4);

            DrawData tailDrawData = new DrawData(tailTexture, tailPosition, tailFrame, drawInfo.middleArmorColor, tailRotation, origin, 1f, spriteEffects, 0)
            {
                shader = drawInfo.bodyArmorShader
            };

            Main.playerDrawData.Add(tailDrawData);
        });

        public static readonly PlayerLayer GuraGawrTrident = new PlayerLayer("JunkoAndFriends", "GuraGawrTrident", PlayerLayer.MiscEffectsBack, delegate (PlayerDrawInfo drawInfo)
        {
            if (drawInfo.shadow != 0 || drawInfo.drawPlayer.dead)
                return;

            Player drawPlayer = drawInfo.drawPlayer;
            Mod mod = ModLoader.GetMod("JunkoAndFriends");

            if (drawPlayer.body != mod.GetEquipSlot("GuraGawrBody", EquipType.Body) ||
            drawPlayer.legs != mod.GetEquipSlot("GuraGawrLeg", EquipType.Legs) ||
            (drawPlayer.head != mod.GetEquipSlot("GuraGawrHeadHair", EquipType.Head) && drawPlayer.head != mod.GetEquipSlot("GuraGawrHeadHoodie", EquipType.Head)))
                return;
            Texture2D tridentTexture = mod.GetTexture("ExtraTextures/GuraGawrTrident");

            float drawX = drawInfo.position.X + drawPlayer.width / 2f;
            float drawY = drawInfo.position.Y + drawPlayer.height / 2f;

            SpriteEffects spriteEffects = drawInfo.spriteEffects;

            if ((int)spriteEffects == 0 || (int)spriteEffects == 2)
                drawX += 6;
            else
                drawX -= 6;

            if ((int)spriteEffects == 2 || (int)spriteEffects == 3)
                drawY -= 6;

            Vector2 origin = drawInfo.bodyOrigin;

            Vector2 tridentPosition = new Vector2(drawX, drawY) + drawPlayer.bodyPosition - Main.screenPosition;

            float rotation = 0f;
            if ((int)spriteEffects == 0 || (int)spriteEffects == 3)
                rotation = 1f;
            else
                rotation = -1f;

            float tridentRotation = drawPlayer.bodyRotation + rotation + drawPlayer.Friends().guraGawrTridentRotation;

            DrawData tridentDrawData = new DrawData(tridentTexture, tridentPosition, null, drawInfo.middleArmorColor, tridentRotation, new Vector2(tridentTexture.Width / 2f, tridentTexture.Height / 2f), 1f, spriteEffects, 0)
            {
                shader = drawInfo.bodyArmorShader
            };

            Main.playerDrawData.Add(tridentDrawData);
        });

    }
}
