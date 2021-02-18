#if RAGE

using System;
using System.Drawing;
using System.Security;
using Rage;
using Rage.Native;

namespace LemonUI.Scaleform
{
    public sealed class RageScaleform : IDisposable, IHandleable, INamedAsset
    {
        public RageScaleform(string id)
        {
            Name = id;
            Load();
        }

        public PoolHandle Handle { get; set; }

        public bool IsLoaded => NativeFunction.Natives.HAS_SCALEFORM_MOVIE_LOADED<bool>(Handle);

        public string Name { get; }

        public void Dismiss()
        {
            if (IsLoaded)
            {
                uint handleValue = Handle.Value;
                NativeFunction.Natives.SET_SCALEFORM_MOVIE_AS_NO_LONGER_NEEDED(ref handleValue);
                Handle = new PoolHandle(handleValue);
            }
        }

        public void Dispose() => Dismiss();

        public bool Equals(IHandleable other)
        {
            return Handle == other.Handle;
        }

        public bool IsValid()
        {
            return IsLoaded;
        }

        public void Load()
        {
            Handle = new PoolHandle(NativeFunction.Natives.REQUEST_SCALEFORM_MOVIE<uint>(Name));
        }

        public void LoadAndWait()
        {
            Load();
            if (!IsLoaded) GameFiber.Yield();
        }

        public void Render2D()
        {
            NativeFunction.Natives.DRAW_SCALEFORM_MOVIE_FULLSCREEN(Handle.Value, 255, 255, 255, 255, 0);
        }

        public void Render2DScreenSpace(PointF location, PointF size)
        {
            float x = location.X / RageScreen.Width;
            float y = location.Y / RageScreen.Height;
            float width = size.X / RageScreen.Width;
            float height = size.Y / RageScreen.Height;

            NativeFunction.Natives.DRAW_SCALEFORM_MOVIE(Handle.Value, x + (width / 2.0f), y + (height / 2.0f), width, height, 255, 255, 255, 255);
        }

        public void CallFunction(string function, params object[] arguments)
        {
            CallFunctionHead(function, arguments);
            NativeFunction.Natives.END_SCALEFORM_MOVIE_METHOD();
        }

        public int CallFunctionWithReturn(string function, params object[] arguments)
        {
            CallFunctionHead(function, arguments);
            return NativeFunction.Natives.END_SCALEFORM_MOVIE_METHOD_RETURN_VALUE<int>();
        }

        private void CallFunctionHead(string function, params object[] arguments)
        {
            NativeFunction.Natives.BEGIN_SCALEFORM_MOVIE_METHOD(Handle.Value, function);

            foreach (var argument in arguments)
            {
                if (argument is int argInt)
                {
                    NativeFunction.Natives.SCALEFORM_MOVIE_METHOD_ADD_PARAM_INT(argInt);
                }
                else if (argument is string argString)
                {
                    NativeFunction.Natives.BEGIN_TEXT_COMMAND_SCALEFORM_STRING("STRING");
                    NativeFunction.Natives.ADD_TEXT_COMPONENT_SUBSTRING_PLAYER_NAME(argString);
                    NativeFunction.Natives.END_TEXT_COMMAND_SCALEFORM_STRING();
                }
                else if (argument is char argChar)
                {
                    NativeFunction.Natives.BEGIN_TEXT_COMMAND_SCALEFORM_STRING("STRING");
                    NativeFunction.Natives.ADD_TEXT_COMPONENT_SUBSTRING_PLAYER_NAME(argChar.ToString());
                    NativeFunction.Natives.END_TEXT_COMMAND_SCALEFORM_STRING();
                }
                else if (argument is float argFloat)
                {
                    NativeFunction.Natives.SCALEFORM_MOVIE_METHOD_ADD_PARAM_FLOAT(argFloat);
                }
                else if (argument is double argDouble)
                {
                    NativeFunction.Natives.SCALEFORM_MOVIE_METHOD_ADD_PARAM_FLOAT((float)argDouble);
                }
                else if (argument is bool argBool)
                {
                    NativeFunction.Natives.SCALEFORM_MOVIE_METHOD_ADD_PARAM_BOOL((bool)argBool);
                }
                else if (argument is RageTxdString argTxd)
                {
                    NativeFunction.Natives.SCALEFORM_MOVIE_METHOD_ADD_PARAM_TEXTURE_NAME_STRING(argTxd);
                }
                else
                {
                    throw new ArgumentException(string.Format("Unknown argument type {0} passed to scaleform with handle {1}.", argument.GetType().Name, Handle.Value), "arguments");
                }
            }
        }
    }

    public static class RageScreen
    {
        public const float Height = 720f;
        public const float Width = 1280f;
    }

    public sealed class RageTxdString
    {
        public RageTxdString(string name)
        {
            Text = name;
        }

        public string Text { get; }
    }
}

#endif