using System;
using OpenTK.Mathematics;

namespace Yasai.Graphics
{
    public readonly struct Transform : ITransform
    {
       public Vector2 Position { get; }
       public Vector2 Size     { get; }
       public float Rotation   { get; }

       public Anchor Anchor    { get; }
       public Anchor Origin    { get; }
       public Vector2 Offset   { get; }
       
       public Transform(Vector2 position, Vector2 size, float rotation, Anchor anchor, Anchor origin, Vector2 offset)
       {
           Position = position;
           Size = size;
           Rotation = rotation;
           Anchor = anchor;
           Origin = origin;
           Offset = offset;
       }
    }
}