using Microsoft.Xna.Framework;
using StardewValley;


namespace RightClickUntillSoil
{
    public partial class ModEntry
    {
        public static void Game1pressActionButton_postfix(ref bool __result)
        {

            if (!__result || !IsHoldingHoe() ||!Game1.didPlayerJustRightClick() || !IsWithinRadius || !IsHoeDirt(UseToolLocation))
                return;

            Game1.currentLocation.playSound("woodyHit");
            Game1.player.stopJittering();
            var dir = Utility.getDirectionFromChange(UseToolLocation, Game1.player.getTileLocation());
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
            Game1.currentLocation.terrainFeatures.Remove(UseToolLocation);

            __result = false;
        }

        public static bool IsWithinRadius
        {
            get => Utility.tileWithinRadiusOfPlayer((int)UseToolLocation.X, (int)UseToolLocation.Y, Config.ToolRadius, Game1.player);
        }

        public static Vector2 UseToolLocation 
        {
            get => Config.UseToolLocation ? ToolTile : Game1.currentCursorTile;
        }

        public static Vector2 ToolTile
        {
            get => new Vector2((int)(Game1.player.GetToolLocation().X / Game1.tileSize), (int)(Game1.player.GetToolLocation().Y / Game1.tileSize));
        }
        
    }
}
