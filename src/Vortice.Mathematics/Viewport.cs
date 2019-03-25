﻿// Copyright (c) Amer Koleci and contributors.
// Distributed under the MIT license. See the LICENSE file in the project root for more information.

using System;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace Vortice.Mathematics
{
    /// <summary>
    /// Represents a integer viewport struct.
    /// </summary>
    [Serializable]
    [DataContract]
    [StructLayout(LayoutKind.Sequential, Pack = 4)]
    public struct Viewport : IEquatable<Viewport>
    {
        /// <summary>
        /// The size in bytes of the <see cref="Viewport"/> type.
        /// </summary>
        public static readonly int SizeInBytes = Unsafe.SizeOf<Viewport>();

        /// <summary>
        /// Position of the pixel coordinate of the upper-left corner of the viewport.
        /// </summary>
        public int X;

        /// <summary>
        /// Position of the pixel coordinate of the upper-left corner of the viewport.
        /// </summary>
        public int Y;

        /// <summary>
        /// Width dimension of the viewport.
        /// </summary>
        public int Width;

        /// <summary>
        /// Height dimension of the viewport.
        /// </summary>
        public int Height;

        /// <summary>
        /// Gets or sets the minimum depth of the clip volume.
        /// </summary>
        public float MinDepth;

        /// <summary>
        /// Gets or sets the maximum depth of the clip volume.
        /// </summary>
        public float MaxDepth;

        /// <summary>
        /// Initializes a new instance of the <see cref="Viewport"/> struct.
        /// </summary>
        /// <param name="width">The width of the viewport in pixels.</param>
        /// <param name="height">The height of the viewport in pixels.</param>
        public Viewport(int width, int height)
        {
            X = 0;
            Y = 0;
            Width = width;
            Height = height;
            MinDepth = 0.0f;
            MaxDepth = 1.0f;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Viewport"/> struct.
        /// </summary>
        /// <param name="x">The x coordinate of the upper-left corner of the viewport in pixels.</param>
        /// <param name="y">The y coordinate of the upper-left corner of the viewport in pixels.</param>
        /// <param name="width">The width of the viewport in pixels.</param>
        /// <param name="height">The height of the viewport in pixels.</param>
        public Viewport(int x, int y, int width, int height)
        {
            X = x;
            Y = y;
            Width = width;
            Height = height;
            MinDepth = 0.0f;
            MaxDepth = 1.0f;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Viewport"/> struct.
        /// </summary>
        /// <param name="x">The x coordinate of the upper-left corner of the viewport in pixels.</param>
        /// <param name="y">The y coordinate of the upper-left corner of the viewport in pixels.</param>
        /// <param name="width">The width of the viewport in pixels.</param>
        /// <param name="height">The height of the viewport in pixels.</param>
        /// <param name="minDepth">The minimum depth of the clip volume.</param>
        /// <param name="maxDepth">The maximum depth of the clip volume.</param>
        public Viewport(int x, int y, int width, int height, float minDepth, float maxDepth)
        {
            X = x;
            Y = y;
            Width = width;
            Height = height;
            MinDepth = minDepth;
            MaxDepth = maxDepth;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Viewport"/> struct.
        /// </summary>
        /// <param name="bounds">A <see cref="Viewport"/> that defines the location and size of the viewport in a render target.</param>
        public Viewport(Rectangle bounds)
        {
            X = bounds.X;
            Y = bounds.Y;
            Width = bounds.Width;
            Height = bounds.Height;
            MinDepth = 0.0f;
            MaxDepth = 1.0f;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Viewport"/> struct.
        /// </summary>
        /// <param name="bounds">A <see cref="Vector4"/> that defines the location and size of the viewport in a render target.</param>
        public Viewport(Vector4 bounds)
        {
            X = (int)bounds.X;
            Y = (int)bounds.Y;
            Width = (int)bounds.Z;
            Height = (int)bounds.W;
            MinDepth = 0.0f;
            MaxDepth = 1.0f;
        }

        /// <summary>
        /// Gets or sets the bounds of the viewport.
        /// </summary>
        public Rectangle Bounds
        {
            get => new Rectangle(X, Y, Width, Height);
            set
            {
                X = value.X;
                Y = value.Y;
                Width = value.Width;
                Height = value.Height;
            }
        }
        
        /// <summary>
        /// Gets the aspect ratio used by the viewport.
        /// </summary>
        /// <value>The aspect ratio.</value>
        public float AspectRatio
        {
            get
            {
                if (Height != 0)
                {
                    return Width / (float)Height;
                }
                return 0f;
            }
        }

        /// <inheritdoc/>
		public override bool Equals(object obj) => obj is Viewport value && Equals(ref value);

        /// <summary>
        /// Determines whether the specified <see cref="Viewport"/> is equal to this instance.
        /// </summary>
        /// <param name="other">The <see cref="Viewport"/> to compare with this instance.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool Equals(Viewport other) => Equals(ref other);

        /// <summary>
		/// Determines whether the specified <see cref="Viewport"/> is equal to this instance.
		/// </summary>
		/// <param name="other">The <see cref="Viewport"/> to compare with this instance.</param>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool Equals(ref Viewport other)
        {
            return X == other.X
                && Y == other.Y
                && Width == other.Width
                && Height == other.Height
                && MathHelper.NearEqual(MinDepth, other.MinDepth)
                && MathHelper.NearEqual(MaxDepth, other.MaxDepth);
        }

        /// <summary>
        /// Compares two <see cref="Viewport"/> objects for equality.
        /// </summary>
        /// <param name="left">The <see cref="Viewport"/> on the left hand of the operand.</param>
        /// <param name="right">The <see cref="Viewport"/> on the right hand of the operand.</param>
        /// <returns>
        /// True if the current left is equal to the <paramref name="right"/> parameter; otherwise, false.
        /// </returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator ==(Viewport left, Viewport right) => left.Equals(ref right);

        /// <summary>
        /// Compares two <see cref="Viewport"/> objects for inequality.
        /// </summary>
        /// <param name="left">The <see cref="Viewport"/> on the left hand of the operand.</param>
        /// <param name="right">The <see cref="Viewport"/> on the right hand of the operand.</param>
        /// <returns>
        /// True if the current left is unequal to the <paramref name="right"/> parameter; otherwise, false.
        /// </returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator !=(Viewport left, Viewport right) => !left.Equals(ref right);

        /// <inheritdoc/>
		public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = X.GetHashCode();
                hashCode = (hashCode * 397) ^ Y.GetHashCode();
                hashCode = (hashCode * 397) ^ Width.GetHashCode();
                hashCode = (hashCode * 397) ^ Height.GetHashCode();
                hashCode = (hashCode * 397) ^ MinDepth.GetHashCode();
                hashCode = (hashCode * 397) ^ MaxDepth.GetHashCode();
                return hashCode;
            }
        }

        /// <inheritdoc/>
        public override string ToString()
        {
            return $"{nameof(X)}: {X}, {nameof(Y)}: {Y}, {nameof(Width)}: {Width}, {nameof(Height)}: {Height}, {nameof(MinDepth)}: {MinDepth}, {nameof(MaxDepth)}: {MaxDepth}";
        }
    }
}