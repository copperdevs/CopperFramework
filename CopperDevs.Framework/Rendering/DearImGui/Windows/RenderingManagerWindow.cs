﻿using CopperDevs.DearImGui;
using CopperDevs.DearImGui.Attributes;
using ImGuiNET;

namespace CopperDevs.Framework.Rendering.DearImGui.Windows;

[Window("Rendering Manager", WindowOpen = true)]
public class RenderingManagerWindow : BaseWindow
{
    public override void WindowStart()
    {
        CopperImGui.RegisterPopup("BuiltInShaderPopup", BuiltInShaderPopup);
    }

    public override void WindowStop()
    {
        CopperImGui.DeregisterPopup("BuiltInShaderPopup");
    }

    public override void WindowUpdate()
    {
        CopperImGui.TabGroup("rendering_manager_window_tab_group",
            ("General Info", GeneralInfo),
            ("Shader Info", ShaderInfo),
            ("Window Render Texture", WindowRenderTexture),
            ("Font Info", FontInfo),
            ("Reflection Renderers Info", ImGuiReflectionRenderersInfo));
    }

    private static void GeneralInfo()
    {
        CopperImGui.Text(Time.Fps, "Fps");
    }

    private static void ShaderInfo()
    {
        CopperImGui.Checkbox("Engine Window Screen Shader Enabled", ref Engine.Instance.ScreenShaderEnabled);

        CopperImGui.Separator();

        CopperImGui.ForceRenderPopup("BuiltInShaderPopup");
        CopperImGui.Button("Load Built In Shader", () => CopperImGui.ShowPopup("BuiltInShaderPopup"));

        CopperImGui.Separator();

        CopperImGui.CollapsingHeader("Loaded Shaders", () =>
        {
            foreach (var shader in RenderingSystem.Instance.GetRenderableItems<Shader>())
            {
                CopperImGui.CollapsingHeader(shader.Name, () =>
                {
                    CopperImGui.Selectable($"Set to engine screen shader###button{shader.Name}", () => Engine.Instance.SetScreenShader(shader));

                    if (!string.IsNullOrEmpty(shader.VertexShaderData))
                        CopperImGui.CollapsingHeader($"Vertex Shader Data###vertex{shader.Name}", () => { CopperImGui.Text(shader.VertexShaderData); });

                    if (!string.IsNullOrEmpty(shader.FragmentShaderData))
                        CopperImGui.CollapsingHeader($"Fragment Shader Data###fragment{shader.Name}", () => { CopperImGui.Text(shader.FragmentShaderData); });
                });
            }
        });
    }

    private static void WindowRenderTexture()
    {
        var renderTexture = (object)Engine.Instance.GameRenderTexture;
        CopperImGui.RenderObjectValues(ref renderTexture);
    }

    private static void FontInfo()
    {
        CopperImGui.CollapsingHeader("Loaded Fonts", () =>
        {
            var list = RenderingSystem.Instance.GetRenderableItems<Font>();
            for (var i = 0; i < list.Count; i++)
            {
                var font = list[i];
                CopperImGui.CollapsingHeader(font.Name, () =>
                {
                    CopperImGui.Text(font.BaseSize, "Base Size");
                    CopperImGui.Text(font.GlyphCount, "Glyph Count");
                    CopperImGui.Text(font.GlyphPadding, "Glyph Padding");

                    var targetObject = font.Texture;
                    CopperImGui.RenderObjectValues(ref targetObject, i);
                });
            }
        });
    }

    private static void BuiltInShaderPopup()
    {
        foreach (var includedShader in Enum.GetValues(typeof(Shader.IncludedShaders)))
        {
            CopperImGui.Selectable(includedShader, () => { Shader.Load((Shader.IncludedShaders)includedShader); });
        }
    }

    private static void ImGuiReflectionRenderersInfo()
    {
        foreach (var renderer in CopperImGui.GetAllImGuiRenderers())
        {
            CopperImGui.Text($"{renderer.Value}", $"{renderer.Key.Name}");
        }
    }
}