﻿using NUnit.Framework;

namespace Epicycle.Math.Geometry
{
    using System;
    
    [TestFixture]
    public sealed class Line2Test : AssertionHelper
    {
        private const double _tolerance = 1e-10;

        [Test]
        public void ProjectOrigin_returns_vector_orthogonal_to_line_Direction_with_0_distance_to_line()
        {
            var line = new Line2(3.14, new Vector2(2.88, 0.57));

            var result = line.ProjectOrigin();

            Expect(result * line.Direction, Is.EqualTo(0).Within(_tolerance));
            Expect(result.DistanceTo(line), Is.EqualTo(0).Within(_tolerance));
        }

        [Test]
        public void Project_of_Vector2_returns_Vector2_satisfying_the_relevant_equations()
        {
            var line = new Line2(3.14, new Vector2(2.88, 0.57));
            var point = new Vector2(-0.44, 1.23);

            var result = line.Project(point);

            Expect((result - point) * line.Direction, Is.EqualTo(0).Within(_tolerance));
            Expect(result.DistanceTo(line), Is.EqualTo(0).Within(_tolerance));
        }
    }
}
