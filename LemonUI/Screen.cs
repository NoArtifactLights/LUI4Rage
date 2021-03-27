#if FIVEM
using CitizenFX.Core;
using CitizenFX.Core.Native;
#elif SHVDN2 || SHVDN3
using GTA;
using GTA.Native;
#elif RAGE
using Rage;
using Rage.Native;
#endif
using LemonUI.Extensions;
using System.Drawing;
using System.Security;

namespace LemonUI
{
    /// <summary>
    /// Represents the internal alignment of screen elements.
    /// </summary>
    public enum GFXAlignment
    {
        /// <summary>f
        /// Vertical Alignment to the Bottom.
        /// </summary>
        Bottom = 66,
        /// <summary>
        /// Vertical Alignment to the Top.
        /// </summary>
        Top = 84,
        /// <summary>
        /// Centered Vertically or Horizontally.
        /// </summary>
        Center = 67,
        /// <summary>
        /// Horizontal Alignment to the Left.
        /// </summary>
        Left = 76,
        /// <summary>
        /// Horizontal Alignment to the Right.
        /// </summary>
        Right = 82,
    }

    /// <summary>
    /// Contains a set of tools to work with the screen information.
    /// </summary>
    public static class Screen
    {
        /// <summary>
        /// The Aspect Ratio of the screen resolution.
        /// </summary>
        public static float AspectRatio
        {
            get
            {
#if FIVEM
                return API.GetAspectRatio(false);
#elif SHVDN2
                return Function.Call<float>(Hash._0xF1307EF624A80D87, false);
#elif SHVDN3
                return Function.Call<float>(Hash._GET_ASPECT_RATIO, false);
#elif RAGE
                return NativeFunction.Natives.xF1307EF624A80D87<float>(false);
#endif
            }
        }

        /// <summary>
        /// Converts a relative resolution into one scaled to 1080p.
        /// </summary>
        /// <param name="relativeX">The relative value of X.</param>
        /// <param name="relativeY">The relative value of Y.</param>
        /// <param name="absoluteX">The value of X scaled to 1080p.</param>
        /// <param name="absoluteY">The value of Y scaled to 1080p.</param>
        public static void ToAbsolute(float relativeX, float relativeY, out float absoluteX, out float absoluteY)
        {
            // Get the real width based on the aspect ratio
            float width = 1080f * AspectRatio;
            // And save the correct values
            absoluteX = width * relativeX;
            absoluteY = 1080f * relativeY;
        }
        /// <summary>
        /// Converts a 1080p-based resolution into relative values.
        /// </summary>
        /// <param name="absoluteX">The 1080p based X coord.</param>
        /// <param name="absoluteY">The 1080p based Y coord.</param>
        /// <param name="relativeX">The value of X converted to relative.</param>
        /// <param name="relativeY">The value of Y converted to relative.</param>
        public static void ToRelative(float absoluteX, float absoluteY, out float relativeX, out float relativeY)
        {
            // Get the real width based on the aspect ratio
            float width = 1080f * AspectRatio;
            // And save the correct values
            relativeX = absoluteX / width;
            relativeY = absoluteY / 1080f;
        }
        /// <summary>
        /// Checks if the cursor is inside of the specified area.
        /// </summary>
        /// <remarks>
        /// This function takes values scaled to 1080p and is aware of the alignment set via SET_SCRIPT_GFX_ALIGN.
        /// </remarks>
        /// <param name="pos">The start of the area.</param>
        /// <param name="size">The size of the area to check.</param>
        /// <returns><see langword="true"/> if the cursor is in the specified bounds, <see langword="false"/> otherwise.</returns>
        public static bool IsCursorInArea(PointF pos, SizeF size) => IsCursorInArea(pos.X, pos.Y, size.Width, size.Height);
        /// <summary>
        /// Checks if the cursor is inside of the specified area.
        /// </summary>
        /// <remarks>
        /// This function takes values scaled to 1080p and is aware of the alignment set via SET_SCRIPT_GFX_ALIGN.
        /// </remarks>
        /// <param name="x">The start X position.</param>
        /// <param name="y">The start Y position.</param>
        /// <param name="width">The height of the search area from X.</param>
        /// <param name="height">The height of the search area from Y.</param>
        /// <returns><see langword="true"/> if the cursor is in the specified bounds, <see langword="false"/> otherwise.</returns>
        public static bool IsCursorInArea(float x, float y, float width, float height)
        {
            // Get the current location of the cursor
#if FIVEM
            float cursorX = API.GetControlNormal(0, (int)Control.CursorX);
            float cursorY = API.GetControlNormal(0, (int)Control.CursorY);
#elif SHVDN2 || SHVDN3
            float cursorX = Function.Call<float>(Hash.GET_CONTROL_NORMAL, 0, (int)Control.CursorX);
            float cursorY = Function.Call<float>(Hash.GET_CONTROL_NORMAL, 0, (int)Control.CursorY);
#elif RAGE
            float cursorX = NativeFunction.Natives.GET_CONTROL_NORMAL<float>(0, (int)GameControl.CursorX);
            float cursorY = NativeFunction.Natives.GET_CONTROL_NORMAL<float>(0, (int)GameControl.CursorY);
#endif
            // Convert the search area values to relative
            ToRelative(width, height, out float realWidth, out float realHeight);
            // And get the correct on screen positions based on the GFX Alignment
            PointF realPos = GetRealPosition(x, y).ToRelative();
            // Check if the values are in the correct positions
            bool isX = cursorX >= realPos.X && cursorX <= realPos.X + realWidth;
            bool isY = cursorY > realPos.Y && cursorY < realPos.Y + realHeight;
            // And return the result of those checks
            return isX && isY;
        }
        /// <summary>
        /// Converts the specified position into one that is aware of <see cref="SetElementAlignment(GFXAlignment, GFXAlignment)"/>.
        /// </summary>
        /// <param name="og">The original 1080p based position.</param>
        /// <returns>A new 1080p based position that is aware of the the Alignment.</returns>
        public static PointF GetRealPosition(PointF og) => GetRealPosition(og.X, og.Y);
        /// <summary>
        /// Converts the specified position into one that is aware of <see cref="SetElementAlignment(GFXAlignment, GFXAlignment)"/>.
        /// </summary>
        /// <param name="x">The 1080p based X position.</param>
        /// <param name="y">The 1080p based Y position.</param>
        /// <returns>A new 1080p based position that is aware of the the Alignment.</returns>
        public static PointF GetRealPosition(float x, float y)
        {
            // Convert the resolution to relative
            ToRelative(x, y, out float relativeX, out float relativeY);
            // Request the real location of the position
            float realX = 0, realY = 0;
#if FIVEM
            API.GetScriptGfxPosition(relativeX, relativeY, ref realX, ref realY);
#elif SHVDN2 || SHVDN3
            OutputArgument argX = new OutputArgument();
            OutputArgument argY = new OutputArgument();
            Function.Call((Hash)0x6DD8F5AA635EB4B2, relativeX, relativeY, argX, argY); // _GET_SCRIPT_GFX_POSITION
            realX = argX.GetResult<float>();
            realY = argY.GetResult<float>();
#elif RAGE
            NativeFunction.Natives.x6DD8F5AA635EB4B2(relativeX, relativeY, ref realX, ref realY);
#endif
            // And return it converted to absolute
            ToAbsolute(realX, realY, out float absoluteX, out float absoluteY);
            return new PointF(absoluteX, absoluteY);
        }
        /// <summary>
        /// Shows the cursor during the current game frame.
        /// </summary>
        public static void ShowCursorThisFrame()
        {
#if FIVEM
            API.SetMouseCursorActiveThisFrame();
#elif SHVDN2
            Function.Call(Hash._SHOW_CURSOR_THIS_FRAME);
#elif SHVDN3
            Function.Call(Hash._SET_MOUSE_CURSOR_ACTIVE_THIS_FRAME);
#elif RAGE
            NativeFunction.Natives.xAAE7CE1D63167423();
#endif
        }
        /// <summary>
        /// Sets the alignment of game elements like <see cref="Elements.ScaledRectangle"/>, <see cref="Elements.ScaledText"/> and <see cref="Elements.ScaledTexture"/>.
        /// </summary>
        /// <param name="horizontal">The Horizontal alignment of the items.</param>
        /// <param name="vertical">The vertical alignment of the items.</param>
        public static void SetElementAlignment(GFXAlignment horizontal, GFXAlignment vertical)
        {
#if FIVEM
            API.SetScriptGfxAlign((int)horizontal, (int)vertical);
            API.SetScriptGfxAlignParams(0, 0, 0, 0);
#elif SHVDN2
            Function.Call(Hash._SET_SCREEN_DRAW_POSITION, (int)horizontal, (int)vertical);
            Function.Call(Hash._0xF5A2C681787E579D, 0, 0, 0, 0);
#elif SHVDN3
            Function.Call(Hash.SET_SCRIPT_GFX_ALIGN, (int)horizontal, (int)vertical);
            Function.Call(Hash.SET_SCRIPT_GFX_ALIGN_PARAMS, 0, 0, 0, 0);
#elif RAGE
            NativeFunction.Natives.SET_SCRIPT_GFX_ALIGN((int)horizontal, (int)vertical);
            NativeFunction.Natives.SET_SCRIPT_GFX_ALIGN_PARAMS(0, 0, 0, 0);
#endif
        }
        /// <summary>
        /// Resets the alignment of the game elements.
        /// </summary>
        public static void ResetElementAlignment()
        {
#if FIVEM
            API.ResetScriptGfxAlign();
#elif SHVDN2
            Function.Call(Hash._0xE3A3DB414A373DAB);
#elif SHVDN3
            Function.Call(Hash.RESET_SCRIPT_GFX_ALIGN);
#elif RAGE
            NativeFunction.Natives.xE3A3DB414A373DAB();
#endif
        }
    }
}
