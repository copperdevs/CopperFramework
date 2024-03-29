﻿using System.Reflection;
using CopperFramework.Utility;

namespace CopperFramework.Rendering.DearImGui.ReflectionRenderers;

public class Texture2DFieldRenderer : ImGuiReflection.FieldRenderer
{
    public override void ReflectionRenderer(FieldInfo fieldInfo, object component, int id)
    {
        var value = (Texture2D)(fieldInfo.GetValue(component) ?? new Texture2D());

        TextureRenderer($"{fieldInfo.Name.ToTitleCase()}##{fieldInfo.Name}{id}", value);
    }

    public override void ValueRenderer(ref object value, int id)
    {
        TextureRenderer($"{value.GetType().Name.ToTitleCase()}##{id}", (Texture2D)value);
    }

    private static void TextureRenderer(string title, Texture2D textureValue)
    {
        CopperImGui.CollapsingHeader(title,
            () =>
            {
                CopperImGui.HorizontalGroup(() => { rlImGui.ImageSize(textureValue, 64, 64); },
                    () =>
                    {
                        CopperImGui.Text(
                            $"Size: <{textureValue.Width},{textureValue.Height}> \nFormat: {textureValue.Format} \nOpenGL id: {textureValue.Id} \nMipmap level: {textureValue.Mipmaps}");
                    });
            });
    }
}