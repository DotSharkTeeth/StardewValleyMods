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
using HarmonyLib;

namespace FasterTransition
{
    public partial class ModEntry
    {
        public static void ScreenFadeUpdateFadeAlpha_prefix(ScreenFade __instance, GameTime time)
        {
            if (!Config.Enable)
                return;

            if (__instance.fadeIn)
                __instance.fadeToBlackAlpha += Config.Speed * time.ElapsedGameTime.Milliseconds;
            else
                __instance.fadeToBlackAlpha -= Config.Speed * time.ElapsedGameTime.Milliseconds;
        }

        public static bool FadeScreenToBlack_prefix(ref ScreenFade __instance, float startAlpha = 0.0f, bool stopMovement = true)
        {
            if (
                !Config.Enable || 
                !Config.NoTransition || 
                __instance.globalFade || 
                Game1.nonWarpFade || 
                Game1.delayedActions.Count > 0 ||
                Game1.IsFakedBlackScreen() ||
                !(Game1.pauseTime == 0.0 || Game1.eventUp) ||
                Game1.viewportFreeze
            //(Game1.viewport.X == -1000 && Game1.viewport.Y == -1000)
            )
                return true;

            //IReflectedField<Game1.afterFadeFunction> afterFade = SHelper.Reflection.GetField<Game1.afterFadeFunction>(__instance, "afterFade");
            //if (afterFade != null)
            //    return true;
            //SMonitor.Log($"Global fade:{__instance.globalFade}, Fadein{__instance.fadeIn}, afterfade{bol}", LogLevel.Debug);
            //SMonitor.Log($"Delay action:{Game1.delayedActions.Count}", LogLevel.Debug);

            //__instance.globalFade = false;
            //__instance.fadeToBlack = true;
            //__instance.fadeIn = false;
            //__instance.fadeToBlackAlpha = 1.3f;
            //if (stopMovement)
            //    Game1.player.CanMove = false;
            //__instance.UpdateFade(Game1.currentGameTime);
            //__instance.fadeToBlackAlpha = -0.3f;
            //__instance.UpdateFade(Game1.currentGameTime);
            //return false;

            __instance.fadeToBlack = false;
            __instance.fadeIn = false;
            if (stopMovement)
                Game1.player.CanMove = false;
            __instance.fadeToBlackAlpha = 0f;

            IReflectedField<Func<bool>> onFadeToBlackComplete = SHelper.Reflection.GetField<Func<bool>>(__instance, "onFadeToBlackComplete");
            onFadeToBlackComplete.GetValue()?.Invoke();
            IReflectedField<Game1.afterFadeFunction> afterFade = SHelper.Reflection.GetField<Game1.afterFadeFunction>(__instance, "afterFade");
            afterFade.GetValue()?.Invoke();
            afterFade.SetValue(null);
            IReflectedField<Action> onFadedBackInComplete = SHelper.Reflection.GetField<Action>(__instance, "onFadedBackInComplete");
            onFadedBackInComplete.GetValue()?.Invoke();
            return false;


        }

    }
}
