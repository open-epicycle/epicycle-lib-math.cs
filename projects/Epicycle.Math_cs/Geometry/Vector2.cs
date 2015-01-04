﻿using System.Collections.Generic;
using System.Linq;

using Epicycle.Commons;
using Epicycle.Math.LinearAlgebra;

namespace Epicycle.Math.Geometry
{
    using System;

    public struct Vector2 : IEquatable<Vector2>
    {
        public double X 
        {
            get { return _x; }
        }

        public double Y 
        {
            get { return _y; }
        }

        private readonly double _x;
        private readonly double _y;

        public enum Axis
        {
            X = 0,
            Y = 1,
            Count = 2 // used in for loops
        }

        public double this[Axis axis]
        {
            get
            {
                switch (axis)
                {
                    case Axis.X:
                        return _x;

                    case Axis.Y:
                        return _y;

                    default:
                        throw new IndexOutOfRangeException("Invalid 2D axis " + axis.ToString());
                }
            }
        }

        #region creation

        public Vector2(double x, double y)
        {
            _x = x;
            _y = y;
        }

        public Vector2(Vector2i v)
        {
            _x = v.X;
            _y = v.Y;
        }

        public Vector2(OVector v)
        {
            ArgAssert.Equal(v.Dimension, "v.Dimension", 2, "2");

            _x = v[0];
            _y = v[1];
        }

        public static implicit operator Vector2(Vector2i v)
        {
            return new Vector2(v);
        }

        public static explicit operator Vector2(OVector v)
        {
            return new Vector2(v);
        }

        public static implicit operator OVector(Vector2 v)
        {
            return new Vector(v._x, v._y);
        }

        #endregion

        #region equality

        public bool Equals(Vector2 v)
        {
            return X == v.X && Y == v.Y;
        }

        public override bool Equals(object obj)
        {
            var v = obj as Vector2?;

            if(!v.HasValue)
            {
                return false;
            }

            return Equals(v.Value);
        }

        public override int GetHashCode()
        {
            return X.GetHashCode() ^ Y.GetHashCode();
        }

        #endregion

        #region norm

        public double Norm2
        {
            get { return _x * _x + _y * _y; }
        }

        public double Norm
        {
            get { return Math.Sqrt(Norm2); }
        }

        public Vector2 Normalized
        {
            get 
            {
                var norm = this.Norm;

                if (norm < BasicMath.Epsilon)
                {
                    return UnitX;
                }
 
                return this / norm;
            }
        }

        public static double Distance2(Vector2 v, Vector2 w)
        {
            return (v - w).Norm2;
        }

        public static double Distance(Vector2 v, Vector2 w)
        {
            return (v - w).Norm;
        }

        #endregion

        #region algebra

        public static Vector2 operator +(Vector2 v)
        {
            return v;
        }

        public static Vector2 operator -(Vector2 v)
        {
            return new Vector2(-v._x, -v._y);
        }

        public static Vector2 operator *(Vector2 v, double a)
        {
            return new Vector2(v._x * a, v._y * a);
        }

        public static Vector2 operator *(double a, Vector2 v)
        {
            return v * a;
        }

        public static Vector2 operator /(Vector2 v, double a)
        {
            return new Vector2(v._x / a, v._y / a);
        }

        public static Vector2 operator +(Vector2 v, Vector2 w)
        {
            return new Vector2(v._x + w._x, v._y + w._y);
        }

        public static Vector2 operator -(Vector2 v, Vector2 w)
        {
            return new Vector2(v._x - w._x, v._y - w._y);
        }

        public static double operator *(Vector2 v, Vector2 w)
        {
            return v._x * w._x + v._y * w._y;
        }

        public double Cross(Vector2 v)
        {
            return _x * v._y - _y * v._x;
        }

        #endregion

        #region static

        public static readonly Vector2 Zero = new Vector2(0, 0);
        public static readonly Vector2 UnitX = new Vector2(1, 0);
        public static readonly Vector2 UnitY = new Vector2(0, 1);

        public static Vector2 Unit(Axis axis)
        {
            switch (axis)
            {
                case Axis.X:
                    return UnitX;

                case Axis.Y:
                    return UnitY;

                default:
                    throw new IndexOutOfRangeException("Invalid 2D axis " + axis.ToString());
            }
        }

        public static double Angle(Vector2 v, Vector2 w)
        {
            var vwcos = v * w;
            var vwsin = v.Cross(w);

            return Math.Atan2(vwsin, vwcos);
        }

        #endregion

        public override string ToString()
        {
            return string.Format("({0}, {1})", X, Y);
        }
    }
}
