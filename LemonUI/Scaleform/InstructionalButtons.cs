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
using System;
using System.Collections.Generic;

namespace LemonUI.Scaleform
{
    /// <summary>
    /// An individual instructional button.
    /// </summary>
    public struct InstructionalButton
    {
        #region Private Fields
#if RAGE
        private GameControl control;
#else
        private Control control;
#endif
        private string raw;

#endregion

#region Public Properties

        /// <summary>
        /// The description of this button.
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// The Control used by this button.
        /// </summary>
        public
#if RAGE
            GameControl
#else
            Control 
#endif
            Control
        {
            get => control;
            set
            {
                control = value;
#if FIVEM
                raw = API.GetControlInstructionalButton(2, (int)value, 1);
#elif SHVDN2
                raw = Function.Call<string>(Hash._0x0499D7B09FC9B407, 2, (int)value, 1);
#elif SHVDN3
                raw = Function.Call<string>(Hash.GET_CONTROL_INSTRUCTIONAL_BUTTON, 2, (int)value, 1);
#elif RAGE
                raw = NativeFunction.Natives.GET_CONTROL_INSTRUCTIONAL_BUTTON<string>(2, (int)value, 1);
#endif
            }
        }
        /// <summary>
        /// The Raw Control sent to the Scaleform.
        /// </summary>
        public string Raw
        {
            get => raw;
            set
            {
                raw = value;
#if RAGE
                control = (GameControl)(-1);
#else
                control = (Control)(-1);
#endif
            }
        }

#endregion

#region Constructor

        /// <summary>
        /// Creates an instructional button for a Control.
        /// </summary>
        /// <param name="description">The text for the description.</param>
        /// <param name="control">The control to use.</param>
        public InstructionalButton(string description,
#if RAGE
            GameControl
#else
            Control 
#endif
            control)
        {
            Description = description;
            this.control = control;
#if FIVEM
            raw = API.GetControlInstructionalButton(2, (int)control, 1);
#elif SHVDN2
            raw = Function.Call<string>(Hash._0x0499D7B09FC9B407, 2, (int)control, 1);
#elif SHVDN3
            raw = Function.Call<string>(Hash.GET_CONTROL_INSTRUCTIONAL_BUTTON, 2, (int)control, 1);
#elif RAGE
            raw = NativeFunction.Natives.GET_CONTROL_INSTRUCTIONAL_BUTTON<string>(2, (int)control, 1);
#endif
        }

        /// <summary>
        /// Creates an instructional button for a raw control.
        /// </summary>
        /// <param name="description">The text for the description.</param>
        /// <param name="raw">The raw value of the control.</param>
        public InstructionalButton(string description, string raw)
        {
            Description = description;
#if RAGE
            control = (GameControl)(-1);
#else
            control = (Control)(-1);
#endif
            this.raw = raw;
        }

#endregion
    }

    /// <summary>
    /// Buttons shown on the bottom right of the screen.
    /// </summary>
    public class InstructionalButtons : BaseScaleform
    {
#region Public Fields

        /// <summary>
        /// The buttons used in this Scaleform.
        /// </summary>
        private readonly List<InstructionalButton> buttons = new List<InstructionalButton>();

#endregion

#region Constructors

        /// <summary>
        /// Creates a new set of Instructional Buttons.
        /// </summary>
        /// <param name="buttons">The buttons to add into this menu.</param>
        public InstructionalButtons(params InstructionalButton[] buttons) : base("INSTRUCTIONAL_BUTTONS")
        {
            this.buttons.AddRange(buttons);
        }

#endregion

#region Public Functions

        /// <summary>
        /// Adds an Instructional Button.
        /// </summary>
        /// <param name="button">The button to add.</param>
        public void Add(InstructionalButton button)
        {
            // If the item is already in the list, raise an exception
            if (buttons.Contains(button))
            {
                throw new InvalidOperationException("The button is already in the Scaleform.");
            }

            // Otherwise, add it to the list of items
            buttons.Add(button);
        }
        /// <summary>
        /// Removes an Instructional Button.
        /// </summary>
        /// <param name="button">The button to remove.</param>
        public void Remove(InstructionalButton button)
        {
            // If the button is not in the list, return
            if (!buttons.Contains(button))
            {
                return;
            }

            // Otherwise, remove it
            buttons.Remove(button);
        }
        /// <summary>
        /// Removes all of the instructional buttons.
        /// </summary>
        public void Clear()
        {
            buttons.Clear();
        }
        /// <summary>
        /// Refreshes the items shown in the Instructional buttons.
        /// </summary>
        public override void Update()
        {
            // Clear all of the existing items
            scaleform.CallFunction("CLEAR_ALL");
            scaleform.CallFunction("TOGGLE_MOUSE_BUTTONS", 0);
            scaleform.CallFunction("CREATE_CONTAINER");

            // And add them again
            for (int i = 0; i < buttons.Count; i++)
            {
                InstructionalButton button = buttons[i];
                scaleform.CallFunction("SET_DATA_SLOT", i, button.Raw, button.Description);
            }

            scaleform.CallFunction("DRAW_INSTRUCTIONAL_BUTTONS", -1);
        }

#endregion
    }
}
