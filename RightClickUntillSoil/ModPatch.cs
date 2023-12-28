using System;
using Microsoft.Xna.Framework;
using StardewModdingAPI;
using StardewModdingAPI.Events;
using StardewModdingAPI.Utilities;
using StardewValley.TerrainFeatures;
using StardewValley.Tools;
using StardewValley;
using System.Reflection;
using StardewValley.BellsAndWhistles;
using StardewValley.Locations;
using HarmonyLib;
using Netcode;

namespace RightClickUntillSoil
{
    public partial class ModEntry
    {
        public static void Game1pressActionButton_postfix(ref bool __result)
        {
            
            if (!__result || !IsHoldingHoe() || !IsHoeDirt(Game1.currentCursorTile) || !Game1.didPlayerJustRightClick(true))
                return;

            Game1.currentLocation.playSound("woodyHit");
            Game1.player.stopJittering();
            var dir = Utility.getDirectionFromChange(Game1.currentCursorTile, Game1.player.getTileLocation());
            if (dir == -1) {
                dir = Game1.player.FacingDirection;
            }
            Game1.player.faceDirection(dir);
            Game1.player.UsingTool = true;
            Game1.player.CanMove = false;
            Game1.player.freezePause = 500;
            AnimatedSprite.endOfAnimationBehavior endOfBehaviorFunction = new AnimatedSprite.endOfAnimationBehavior((who) => {
                who.UsingTool = false;
                who.CanMove = true;
            });
            Game1.player.FarmerSprite.animateOnce(295 + dir, 100, 0, endOfBehaviorFunction);
            Game1.currentLocation.terrainFeatures.Remove(Game1.currentCursorTile);

            __result = false;
        }
        
    }
}
