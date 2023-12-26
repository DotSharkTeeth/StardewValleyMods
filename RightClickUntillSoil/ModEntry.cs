using System;
using Microsoft.Xna.Framework;
using StardewModdingAPI;
using StardewModdingAPI.Events;
using StardewModdingAPI.Utilities;
using StardewValley.TerrainFeatures;
using StardewValley.Tools;
using StardewValley;
using System.Reflection;

namespace RightClickUntillSoil
{
    /// <summary>The mod entry point.</summary>
    public partial class ModEntry : Mod
    {
        /*********
        ** Public methods
        *********/
        /// <summary>The mod entry point, called after the mod is first loaded.</summary>
        /// <param name="helper">Provides simplified APIs for writing mods.</param>
        public override void Entry(IModHelper helper)
        {
            helper.Events.Input.ButtonPressed += this.OnButtonPressed;
        }
        

        /*********
        ** Private methods
        *********/
        /// <summary>Raised after the player presses a button on the keyboard, controller, or mouse.</summary>
        /// <param name="sender">The event sender.</param>
        /// <param name="e">The event data.</param>
        private void OnButtonPressed(object sender, ButtonPressedEventArgs e)
        {
            // ignore if player hasn't loaded a save yet
            if (!Context.IsWorldReady)
                return;
                
            if (e.Button == SButton.MouseRight && IsHoldingHoe() && !Game1.player.UsingTool)
            {

                //this.Monitor.Log($"{Game1.player.getTileLocation()}:{Game1.currentCursorTile}:{IsHoeDirt(Game1.currentCursorTile)}", LogLevel.Debug);
                
                if(IsHoeDirt(Game1.currentCursorTile)) {
                    Game1.currentLocation.playSound("woodyHit");
                    Game1.player.stopJittering();
                    var dir = Utility.getDirectionFromChange(Game1.currentCursorTile, Game1.player.getTileLocation());
                    Game1.player.faceDirection(dir);
                    Game1.player.UsingTool = true;
                    AnimatedSprite.endOfAnimationBehavior endOfBehaviorFunction = new AnimatedSprite.endOfAnimationBehavior((who) => {
                        who.UsingTool = false;
                    });
                    Game1.player.FarmerSprite.animateOnce(295 + dir, 100, 0, endOfBehaviorFunction);
                    Game1.currentLocation.terrainFeatures.Remove(Game1.currentCursorTile);
                }
            }
        }
        private static bool IsHoldingHoe()
        {
            return Game1.player.CurrentTool is Hoe;
        }
        private static bool IsHoeDirt(Vector2 tile)
        {
            return Game1.currentLocation.terrainFeatures.ContainsKey(tile)
                && Game1.currentLocation.terrainFeatures[tile] is HoeDirt dirt
                && dirt.crop == null
                && !Game1.currentLocation.objects.ContainsKey(tile);
        }
    }
}
