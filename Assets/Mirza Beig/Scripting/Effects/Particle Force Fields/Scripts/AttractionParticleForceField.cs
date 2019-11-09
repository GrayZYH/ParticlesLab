
// =================================	
// Namespaces.
// =================================

using UnityEngine;

// =================================	
// Define namespace.
// =================================

namespace MirzaBeig
{

    namespace Scripting
    {

        namespace Effects
        {

            // =================================	
            // Classes.
            // =================================

            [AddComponentMenu("Effects/Particle Force Fields/Attraction Particle Force Field")]

            public class AttractionParticleForceField : ParticleForceField
            {
                // =================================	
                // Nested classes and structures.
                // =================================

                // ...

                // =================================	
                // Variables.
                // =================================

                // ...

                [Header("ForceField Controls")]

                [Tooltip("Tether force based on linear inverse particle distance to force field center.")]
                public float arrivalRadius = 1.0f;
                [Tooltip("Dead zone from force field center in which no additional force is applied.")]
                public float arrivedRadius = 0.5f;

                float arrivalRadiusSqr;
                float arrivedRadiusSqr;

                // =================================	
                // Functions.
                // =================================

                // ...

                protected override void Awake()
                {
                    base.Awake();
                }

                // ...

                protected override void Start()
                {
                    base.Start();
                }

                // ...

                protected override void Update()
                {
                    base.Update();
                }

                // ...

                protected override void LateUpdate()
                {
                    float uniformTransformScale = transform.lossyScale.x;

                    arrivalRadiusSqr = (arrivalRadius * arrivalRadius) * uniformTransformScale;
                    arrivedRadiusSqr = (arrivedRadius * arrivedRadius) * uniformTransformScale;

                    // ...

                    base.LateUpdate();
                }

                // ...

                protected override Vector3 GetForce()
                {
                    Vector3 force;

                    if (parameters.distanceToForceFieldCenterSqr < arrivedRadiusSqr)
                    {
                        force.x = 0.0f;
                        force.y = 0.0f;
                        force.z = 0.0f;
                    }
                    else if (parameters.distanceToForceFieldCenterSqr < arrivalRadiusSqr)
                    {
                        float inverseArrivalScaleNormalized = 1.0f - (parameters.distanceToForceFieldCenterSqr / arrivalRadiusSqr);
                        force = Vector3.Normalize(parameters.scaledDirectionToForceFieldCenter) * inverseArrivalScaleNormalized;
                    }
                    else
                    {
                        force = Vector3.Normalize(parameters.scaledDirectionToForceFieldCenter);
                    }

                    return force;
                }

                // ...

                protected override void OnDrawGizmosSelected()
                {
                    if (enabled)
                    {
                        base.OnDrawGizmosSelected();

                        // ...

                        float uniformTransformScale = transform.lossyScale.x;

                        float arrivalRadius = this.arrivalRadius * uniformTransformScale;
                        float arrivedRadius = this.arrivedRadius * uniformTransformScale;

                        Vector3 offsetCenter = transform.position + center;

                        Gizmos.color = Color.yellow;
                        Gizmos.DrawWireSphere(offsetCenter, arrivalRadius);

                        Gizmos.color = Color.red;
                        Gizmos.DrawWireSphere(offsetCenter, arrivedRadius);

                        //Gizmos.color = Color.white;
                        //Gizmos.DrawLine(currentParticleSystem.transform.position, offsetCenter);
                    }
                }

                // =================================	
                // End functions.
                // =================================

            }

            // =================================	
            // End namespace.
            // =================================

        }

    }

}

// =================================	
// --END-- //
// =================================
