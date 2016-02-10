/*
 * Vector2.cs
 * RVO2 Library C#
 *
 * Copyright (c) 2008-2015 University of North Carolina at Chapel Hill.
 * All rights reserved.
 *
 * Permission to use, copy, modify, and distribute this software and its
 * documentation for educational, research, and non-profit purposes, without
 * fee, and without a written agreement is hereby granted, provided that the
 * above copyright notice, this paragraph, and the following four paragraphs
 * appear in all copies.
 *
 * Permission to incorporate this software into commercial products may be
 * obtained by contacting the Office of Technology Development at the University
 * of North Carolina at Chapel Hill <otd@unc.edu>.
 *
 * This software program and documentation are copyrighted by the University of
 * North Carolina at Chapel Hill. The software program and documentation are
 * supplied "as is," without any accompanying services from the University of
 * North Carolina at Chapel Hill or the authors. The University of North
 * Carolina at Chapel Hill and the authors do not warrant that the operation of
 * the program will be uninterrupted or error-free. The end-user understands
 * that the program was developed for research purposes and is advised not to
 * rely exclusively on the program for any reason.
 *
 * IN NO EVENT SHALL THE UNIVERSITY OF NORTH CAROLINA AT CHAPEL HILL OR THE
 * AUTHORS BE LIABLE TO ANY PARTY FOR DIRECT, INDIRECT, SPECIAL, INCIDENTAL, OR
 * CONSEQUENTIAL DAMAGES, INCLUDING LOST PROFITS, ARISING OUT OF THE USE OF THIS
 * SOFTWARE AND ITS DOCUMENTATION, EVEN IF THE UNIVERSITY OF NORTH CAROLINA AT
 * CHAPEL HILL OR THE AUTHORS HAVE BEEN ADVISED OF THE POSSIBILITY OF SUCH
 * DAMAGE.
 *
 * THE UNIVERSITY OF NORTH CAROLINA AT CHAPEL HILL AND THE AUTHORS SPECIFICALLY
 * DISCLAIM ANY WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE IMPLIED
 * WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE AND ANY
 * STATUTORY WARRANTY OF NON-INFRINGEMENT. THE SOFTWARE PROVIDED HEREUNDER IS ON
 * AN "AS IS" BASIS, AND THE UNIVERSITY OF NORTH CAROLINA AT CHAPEL HILL AND THE
 * AUTHORS HAVE NO OBLIGATIONS TO PROVIDE MAINTENANCE, SUPPORT, UPDATES,
 * ENHANCEMENTS, OR MODIFICATIONS.
 *
 * Please send all bug reports to <geom@cs.unc.edu>.
 *
 * The authors may be contacted via:
 *
 * Jur van den Berg, Stephen J. Guy, Jamie Snape, Ming C. Lin, Dinesh Manocha
 * Dept. of Computer Science
 * 201 S. Columbia St.
 * Frederick P. Brooks, Jr. Computer Science Bldg.
 * Chapel Hill, N.C. 27599-3175
 * United States of America
 *
 * <http://gamma.cs.unc.edu/RVO2/>
 */

using System;
using System.Globalization;

namespace UnitySteer.RVO
{
    /**
     * <summary>Defines a two-dimensional vector.</summary>
     */
    [Serializable]
    public struct Vector2
    {
        public float x;
        public float y;

        /**
         * <summary>Constructs and initializes a two-dimensional vector from the
         * specified xy-coordinates.</summary>
         *
         * <param name="x">The x-coordinate of the two-dimensional vector.
         * </param>
         * <param name="y">The y-coordinate of the two-dimensional vector.
         * </param>
         */
        public Vector2(float x, float y)
        {
            this.x = x;
            this.y = y;
        }

        /**
         * <summary>Returns the string representation of this vector.</summary>
         *
         * <returns>The string representation of this vector.</returns>
         */
        public override string ToString()
        {
            return "(" + x.ToString(new CultureInfo("").NumberFormat) + "," + y.ToString(new CultureInfo("").NumberFormat) + ")";
        }

        
        /**
         * <summary>Computes the dot product of the two specified
         * two-dimensional vectors.</summary>
         *
         * <returns>The dot product of the two specified two-dimensional
         * vectors.</returns>
         *
         * <param name="vector1">The first two-dimensional vector.</param>
         * <param name="vector2">The second two-dimensional vector.</param>
         */
        public static float operator *(Vector2 vector1, Vector2 vector2)
        {
            return vector1.x * vector2.x + vector1.y * vector2.y;
        }

        /**
         * <summary>Computes the scalar multiplication of the specified
         * two-dimensional vector with the specified scalar value.</summary>
         *
         * <returns>The scalar multiplication of the specified two-dimensional
         * vector with the specified scalar value.</returns>
         *
         * <param name="scalar">The scalar value.</param>
         * <param name="vector">The two-dimensional vector.</param>
         */
        public static Vector2 operator *(float scalar, Vector2 vector)
        {
            return vector * scalar;
        }

        /**
         * <summary>Computes the scalar multiplication of the specified
         * two-dimensional vector with the specified scalar value.</summary>
         *
         * <returns>The scalar multiplication of the specified two-dimensional
         * vector with the specified scalar value.</returns>
         *
         * <param name="vector">The two-dimensional vector.</param>
         * <param name="scalar">The scalar value.</param>
         */
        public static Vector2 operator *(Vector2 vector, float scalar)
        {
            return new Vector2(vector.x * scalar, vector.y * scalar);
        }

        /**
         * <summary>Computes the scalar division of the specified
         * two-dimensional vector with the specified scalar value.</summary>
         *
         * <returns>The scalar division of the specified two-dimensional vector
         * with the specified scalar value.</returns>
         *
         * <param name="vector">The two-dimensional vector.</param>
         * <param name="scalar">The scalar value.</param>
         */
        public static Vector2 operator /(Vector2 vector, float scalar)
        {
            return new Vector2(vector.x / scalar, vector.y / scalar);
        }

        /**
         * <summary>Computes the vector sum of the two specified two-dimensional
         * vectors.</summary>
         *
         * <returns>The vector sum of the two specified two-dimensional vectors.
         * </returns>
         *
         * <param name="vector1">The first two-dimensional vector.</param>
         * <param name="vector2">The second two-dimensional vector.</param>
         */
        public static Vector2 operator +(Vector2 vector1, Vector2 vector2)
        {
            return new Vector2(vector1.x + vector2.x, vector1.y + vector2.y);
        }

        /**
         * <summary>Computes the vector difference of the two specified
         * two-dimensional vectors</summary>
         *
         * <returns>The vector difference of the two specified two-dimensional
         * vectors.</returns>
         *
         * <param name="vector1">The first two-dimensional vector.</param>
         * <param name="vector2">The second two-dimensional vector.</param>
         */
        public static Vector2 operator -(Vector2 vector1, Vector2 vector2)
        {
            return new Vector2(vector1.x - vector2.x, vector1.y - vector2.y);
        }

        /**
         * <summary>Computes the negation of the specified two-dimensional
         * vector.</summary>
         *
         * <returns>The negation of the specified two-dimensional vector.
         * </returns>
         *
         * <param name="vector">The two-dimensional vector.</param>
         */
        public static Vector2 operator -(Vector2 vector)
        {
            return new Vector2(-vector.x, -vector.y);
        }
    }
}
