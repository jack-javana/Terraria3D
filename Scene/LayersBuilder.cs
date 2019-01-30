﻿
namespace Terraria3D
{
    public static class LayerBuilder
    {
        public static void PopulateLayers(ref Layer3D[] layers)
        {
            if (layers != null)
            {
                foreach (var layer in layers)
                    layer?.Dispose();
            }
            layers = new Layer3D[]
            {
                new Layer3D()
                {
                    Name = "Background",
                    ZPos = 4,
                    Depth = 4,
                    RenderFunction = () =>
                    {
                        Rendering.DrawBlack();
                        Rendering.DrawBackgroundWater();
                        Rendering.DrawSceneBackground();
                        Rendering.DrawWalls();
                    }
                },
                // Solid tiles
                new Layer3D()
                {
                    Name = "Solid Tiles",
                    Depth = 32,
                    InputPlane = Layer3D.InputPlaneType.SolidTiles,
                    RenderFunction = () =>
                    {
                        Rendering.DrawSolidTiles();
                        Reflection.PostDrawTiles();
                    }
                },
                // Non Solid tiles
                new Layer3D()
                {
                    Name = "Non Solid Tiles",
                    Depth = 8,
                    InputPlane = Layer3D.InputPlaneType.NoneSolidTiles,
                    RenderFunction = () =>
                    {
                        Rendering.DrawNonSolidTiles();
                        Rendering.DrawWaterFalls();

                    }
                },
                //Player
                new Layer3D()
                {
                    Name = "Characters",
                    ZPos = -18,
                    Depth = 6,
                    NoiseAmount = 0,
                    RenderFunction = () =>
                    {
                        Rendering.DrawPlayers();
                        Rendering.DrawNPCsBehindTiles();
                        Rendering.DrawNPCsBehindNonSoldTiles();
                        Rendering.DrawNPCsInfrontOfTiles();
                        Rendering.DrawNPCsOverPlayer();
                        Rendering.DrawWallOfFlesh();
                        Rendering.SortDrawCacheWorm();
                    }
                },
                // Proj
                new Layer3D()
                {
                    Name = "Projectiles",
                    ZPos = -20,
                    Depth = 2,
                    NoiseAmount = 0,
                    RenderFunction = () =>
                    {
                        Rendering.DrawProjectiles();
                        Rendering.DrawProjsBehindNPCsAndTiles();
                        Rendering.DrawProjsBehindProjectiles();
                        Rendering.DrawProjsOverWireUI();
                        Rendering.DrawNPCProjectiles();
                        Rendering.DrawInfernoRings();

                    }
                },
                // Items Gore
                new Layer3D()
                {
                    Name = "Gore",
                    ZPos = -12,
                    Depth = 6,
                    NoiseAmount = 0,
                    RenderFunction = () =>
                    {
                        Rendering.DrawGore();
                        Rendering.DrawBackGore();
                        Rendering.DrawDust();
                        Rendering.DrawRain();
                        Rendering.DrawSandstorm();
                        Rendering.DrawMoonLordDeath();
                        Rendering.DrawMoonlordDeathFront();
                        Rendering.DrawItems();
                        Rendering.DrawHitTileAnimation();
                        Rendering.DrawItemText();
                        Rendering.DrawCombatText();
                        Rendering.DrawChatOverPlayerHeads();
                    }
                },
                new Layer3D()
                {
                    Name = "Water Foreground",
                    Depth = 32,
                    RenderFunction = () =>
                    {
                        Rendering.DrawForegroundWater();
                    }
                },
                new Layer3D()
                {
                    Name = "Wires",
                    ZPos = -32,
                    Depth = 4,
                    RenderFunction = () => Rendering.DrawWires()
                }
            };
        }
    }
}
