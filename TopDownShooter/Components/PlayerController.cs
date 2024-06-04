﻿using Raylib_CSharp.Collision;
using Raylib_CSharp.Interact;

namespace TopDownShooter.Components;

public class PlayerController : GameComponent
{
    [Exposed] [Range(1, 32)] private int moveSpeed = 10;
    [Exposed] [ReadOnly] private Vector2 moveInput;
    [Exposed] [Range(0, 16)] private float inputSmoothTime = 12;

    public override void Start()
    {
        Transform.Scale = 24;
    }

    public override void Update()
    {
        moveInput = MathUtil.Lerp(moveInput, PlayerMoveInput(), Time.DeltaTime * inputSmoothTime);

        Transform.Position += moveInput * moveSpeed;
        Transform.LookAt(Input.MousePosition);

        rlGraphics.DrawCircleV(Vector2.Zero, 1, Color.Red);
    }

    public override void FixedUpdate()
    {
        EnemyManager.Instance.EnemyTargetPosition = Transform.Position;
    }

    private Vector2 PlayerMoveInput()
    {
        var value = Vector2.Normalize(
            new Vector2(
                Input.IsKeyDown(KeyboardKey.D, KeyboardKey.A),
                Input.IsKeyDown(KeyboardKey.S, KeyboardKey.W)
            ));

        if (float.IsNaN(value.X))
            value.X = 0;
        if (float.IsNaN(value.Y))
            value.Y = 0;

        return value;
    }

    public override void DebugUpdate()
    {
        var playerPos = Transform.Position;

        rlGraphics.DrawLineV(playerPos, playerPos + (moveInput * Transform.Scale), Color.Green);
        rlGraphics.DrawLineV(playerPos, playerPos + (PlayerMoveInput() * Transform.Scale), Color.Blue);

        var directionRay = new Ray(new Vector3(playerPos.X, playerPos.Y, 0), Transform.Rotation.ToRotatedUnitVector().ToVector3());
        rlGraphics.DrawRay(directionRay, Color.Red);
    }
}