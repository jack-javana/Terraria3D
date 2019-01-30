﻿using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria.GameInput;
using Terraria.ModLoader;
using Terraria.UI;

namespace Terraria3D
{
    public static class UITerraria3D
    {
        public static bool Visible { get; private set; } = false;
        public static bool CameraContolsEnabled { get; private set; } = true;

        private static UserInterface _interface;
        private static UIState _state;
        private static UISettingsWindow _settingsWindow;
        private static ModHotKey _settingsKeyBinding;
        private static ModHotKey _toggleCameraControlsKeyBinding;
        private static ModHotKey _toggle3DKeyBinding;


        public static void Load()
        {
            _interface = new UserInterface();
            _state = new UIState();
            _settingsWindow = new UISettingsWindow("Settings");

            _interface.SetState(_state);
            _state.Append(_settingsWindow);
            _state.Activate();

            // Why 'L'? No clue, I hope it's not already bound to something xD
            _settingsKeyBinding = Terraria3D.Instance.RegisterHotKey("Toggle 3D Settings", "L");
            _toggleCameraControlsKeyBinding = Terraria3D.Instance.RegisterHotKey("Toggle Camera Controls", "Multiply");
            _toggle3DKeyBinding = Terraria3D.Instance.RegisterHotKey("Toggle 3D", "K");
        }

        public static void Unload()
        {
            _interface.SetState(null);
            _state.Deactivate();

            _settingsWindow = null;
            _state = null;
            _interface = null;

            _settingsKeyBinding = null;
        }

        public static void Update(GameTime gameTime)
        {
            _interface.Update(gameTime);
            // Hack to fix scroll bug.
            PlayerInput.ScrollWheelDeltaForUI = 0;
        }

        public static void Draw()
            => _interface.Draw(Terraria.Main.spriteBatch, new GameTime());

        public static void ModifyInterfaceLayers(List<GameInterfaceLayer> layers)
        {
            int inventoryIndex = layers.FindIndex(layer => layer.Name.Equals("Vanilla: Mouse Text"));
            if (inventoryIndex != -1)
            {
                layers.Insert(inventoryIndex + 1, new LegacyGameInterfaceLayer(
                    "ExampleMod: Example Person UI", () =>
                    {
                        if (Visible) Draw();
                        return true;
                    }, InterfaceScaleType.UI)
                );
            }
        }
        public static void ProcessInput()
        {
            if (_settingsKeyBinding.JustPressed)
                Visible = !Visible;
            if (_toggleCameraControlsKeyBinding.JustPressed)
                CameraContolsEnabled = !CameraContolsEnabled;
            if (_toggle3DKeyBinding.JustPressed)
                Terraria3D.Enabled = !Terraria3D.Enabled;
        }
    }
}