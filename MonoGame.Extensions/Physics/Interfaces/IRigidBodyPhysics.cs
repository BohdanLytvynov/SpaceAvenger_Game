using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;

namespace MonoGame.Extensions.Physics.Interfaces
{
    public interface IRigidBodyPhysics : IPhysicsBehavior
    {
        #region Properties

        public Vector2 LinearAcceleration { get; }

        public Vector2 LinearVelocity { get; }

        #endregion

        Vector2 GetCenterOfMass(params IRigidBodyObject[] rigidBodyObjects);

        Vector2 GetCenterOfMass(IEnumerable<IRigidBodyObject> rigidBodyObjects);

        Vector2 GetDisplacement(Vector2 Force, float mass, GameTime time);

        float GetMOI(Func<float> expression);

        float GetMOI(IMOICalculator MOICalculator);
    }
}
