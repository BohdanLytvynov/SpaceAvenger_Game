using Microsoft.Xna.Framework;
using MonoGame.Extensions.Physics.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MonoGame.Extensions.Physics.Realizations
{
    public class RigidBodyPhysics : IRigidBodyPhysics
    {
        #region Fields

        private Vector2 m_linearAcceleration;

        private Vector2 m_linearVelocity;

        public Vector2 LinearAcceleration => m_linearAcceleration;

        public Vector2 LinearVelocity => m_linearVelocity;

        #endregion

        public Vector2 GetCenterOfMass(params IRigidBodyObject[] rigidBodyObjects)
        {
            if (rigidBodyObjects.Length == 1)
                return rigidBodyObjects.First().Transform.Origin;

            float total_mass = 0f;
            Vector2 r = Vector2.Zero;

            foreach (var obj in rigidBodyObjects)
            {
                total_mass += obj.Mass;

                r += obj.Transform.Position * obj.Mass;
            }

            return r / total_mass;
        }

        public Vector2 GetCenterOfMass(IEnumerable<IRigidBodyObject> rigidBodyObjects)
        {
            if (rigidBodyObjects.Count() == 1)
                return rigidBodyObjects.First().Transform.Origin;

            float total_mass = 0f;
            Vector2 r = Vector2.Zero;

            foreach (var obj in rigidBodyObjects)
            {
                total_mass += obj.Mass;

                r += obj.Transform.Position * obj.Mass;
            }

            return r / total_mass;
        }

        public Vector2 GetDisplacement(Vector2 Force, float mass, GameTime time)
        {
            m_linearAcceleration = Force / mass;

            m_linearVelocity += LinearAcceleration * (float)time.ElapsedGameTime.TotalSeconds;

            return LinearVelocity * (float)time.ElapsedGameTime.TotalSeconds;
        }

        public float GetMOI(Func<float> expression)
        {
            if (expression is null)
                throw new ArgumentNullException(nameof(expression));

            return expression.Invoke();
        }

        public float GetMOI(IMOICalculator MOICalculator)
        {
            if (MOICalculator is null)
                throw new ArgumentNullException(nameof(MOICalculator));

            return MOICalculator.Calculate();
        }
    }
}
