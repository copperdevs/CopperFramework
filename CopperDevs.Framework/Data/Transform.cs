﻿namespace CopperDevs.Framework.Data;

public struct Transform
{
    public TransformVector3 Position = default;
    public float Rotation = 0;
    public Vector2 Scale = Vector2.One;

    public Action Updated = null!;
    public Action<Vector2> PositionUpdated = null!;
    public Action<float> RotationUpdated = null!;
    public Action<Vector2> ScaleUpdated = null!;

    public Transform()
    {
        Position = default;
        Rotation = 0;
        Scale = Vector2.One;
    }

    public void LookAt(TransformVector3 point)
    {
        Rotation = -MathF.Atan2(Position.Y - point.Y, Position.X - point.X) * (180 / MathF.PI) + 180;
    }

    public float Distance(Vector2 position)
    {
        return Vector2.Distance(Position, position);
    }

    public static bool operator ==(Transform left, Transform right)
    {
        return left.Equals(right);
    }

    public static bool operator !=(Transform left, Transform right)
    {
        return !(left == right);
    }

    public bool Equals(Transform other)
    {
        return Position.Equals(other.Position) && Rotation.Equals(other.Rotation) && Scale.Equals(other.Scale);
    }

    public override bool Equals(object? obj)
    {
        return obj is Transform other && Equals(other);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Position, Rotation, Scale);
    }
}