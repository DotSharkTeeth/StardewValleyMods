using System;
using Microsoft.Xna.Framework;
using StardewModdingAPI;
using StardewModdingAPI.Events;
using StardewModdingAPI.Utilities;
using StardewValley.TerrainFeatures;
using StardewValley.Tools;
using StardewValley;
using System.Reflection;
using HarmonyLib;

namespace RightClickUntillSoil
{
    /// <summary>The mod entry point.</summary>
    public partial class ModEntry : Mod
    {

        public static IMonitor SMonitor;
        public static IModHelper SHelper;

        public override void Entry(IModHelper helper)
        {
            SMonitor = Monitor;
            SHelper = Helper;
            var harmony = new Harmony(ModManifest.UniqueID);

            harmony.Patch(
               original: AccessTools.Method(typeof(Game1), nameof(Game1.pressActionButton)),
               postfix: new HarmonyMethod(typeof(ModEntry), nameof(ModEntry.Game1pressActionButton_postfix))
            );
        }
        
        private static bool IsHoldingHoe()
        {
            return !Game1.player.UsingTool && Game1.player.CurrentTool is Hoe;
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
