﻿namespace TopDownShooter.Components;

public class MouseDrawer : GameComponent
{
    [Exposed] private float smoothingTime = 16;
    [Exposed] private Color color = Color.White;

    public override void Start()
    {
        Transform.Scale = 8;
    }

    public override void Update()
    {
        Transform.Position = MathUtil.Lerp(Transform.Position, Input.MousePosition, Time.DeltaTime * smoothingTime);
        rlGraphics.DrawCircleV(Vector2.Zero, 1, color);
    }

    public override void DebugUpdate()
    {
        rlGraphics.DrawCircleV(Input.MousePosition.FlipY(), 8, Color.Red);
    }
}