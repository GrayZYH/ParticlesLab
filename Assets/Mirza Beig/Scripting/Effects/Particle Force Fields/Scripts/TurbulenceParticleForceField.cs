
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

            [AddComponentMenu("Effects/Particle Force Fields/Turbulence Particle Force Field")]

            public class TurbulenceParticleForceField : ParticleForceField
            {
                // =================================	
                // Nested classes and structures.
                // =================================

                // ...

                public enum NoiseType
                {
                    PseudoPerlin,

                    Perlin,

                    Simplex,

                    OctavePerlin,
                    OctaveSimplex
                }

                // =================================	
                // Variables.
                // =================================

                // ...

                [Header("ForceField Controls")]

                [Tooltip("Noise texture mutation speed.")]

                public float scrollSpeed = 1.0f;

                [Range(0.0f, 8.0f)]
                [Tooltip("Noise texture detail amplifier.")]

                public float frequency = 1.0f;

                public NoiseType noiseType = NoiseType.Perlin;

                // ...

                [Header("Octave Variant-Only Controls")]

                [Range(1, 8)]
                [Tooltip("Overlapping noise iterations. 1 = no additional iterations.")]

                public int octaves = 1;

                [Range(0.0f, 4.0f)]
                [Tooltip("Frequency scale per-octave. Can be used to change the overlap every iteration.")]

                public float octaveMultiplier = 0.5f;

                [Range(0.0f, 1.0f)]
                [Tooltip("Amplitude scale per-octave. Can be used to change the overlap every iteration.")]

                public float octaveScale = 2.0f;

                float time;

                // Noise2 start offsets.

                float randomX;
                float randomY;
                float randomZ;

                // Final offset.

                float offsetX;
                float offsetY;
                float offsetZ;

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

                    // ...

                    randomX = Random.Range(-32.0f, 32.0f);
                    randomY = Random.Range(-32.0f, 32.0f);
                    randomZ = Random.Range(-32.0f, 32.0f);
                }

                // ...

                protected override void Update()
                {
                    time = Time.time;

                    // ...

                    base.Update();
                }

                // ...

                protected override void LateUpdate()
                {
                    offsetX = (time * scrollSpeed) + randomX;
                    offsetY = (time * scrollSpeed) + randomY;
                    offsetZ = (time * scrollSpeed) + randomZ;

                    // ...

                    base.LateUpdate();
                }

                // ...

                protected override Vector3 GetForce()
                {
                    // I could also pre-multiply the frequency, but
                    // all the octave variants also use frequency
                    // within themselves, so it would cause redundant
                    // multiplication.

                    float xX = parameters.particlePosition.x + offsetX;
                    float yX = parameters.particlePosition.y + offsetX;
                    float zX = parameters.particlePosition.z + offsetX;

                    float xY = parameters.particlePosition.x + offsetY;
                    float yY = parameters.particlePosition.y + offsetY;
                    float zY = parameters.particlePosition.z + offsetY;

                    float xZ = parameters.particlePosition.x + offsetZ;
                    float yZ = parameters.particlePosition.y + offsetZ;
                    float zZ = parameters.particlePosition.z + offsetZ;

                    Vector3 force;

                    switch (noiseType)
                    {
                        case NoiseType.PseudoPerlin:
                            {
                                // This isn't really right, but... it gives believable-enough results. 
                                // It's also much faster than real perlin noise.

                                // It works well where you don't have to animate a large field where
                                // the repeating pattern would otherwise be easily seen. 

                                // Examples of good uses: smoke trail particle turbulence.
                                // Example of bad uses: particle box simulating waves or something...

                                float noiseX = Mathf.PerlinNoise(xX * frequency, yY * frequency);
                                float noiseY = Mathf.PerlinNoise(xX * frequency, zY * frequency);
                                float noiseZ = Mathf.PerlinNoise(xX * frequency, xY * frequency);

                                noiseX = Mathf.Lerp(-1.0f, 1.0f, noiseX);
                                noiseY = Mathf.Lerp(-1.0f, 1.0f, noiseY);
                                noiseZ = Mathf.Lerp(-1.0f, 1.0f, noiseZ);

                                Vector3 forceX = (Vector3.right * noiseX);
                                Vector3 forceY = (Vector3.up * noiseY);
                                Vector3 forceZ = (Vector3.forward * noiseZ);

                                force = forceX + forceY + forceZ;

                                break;
                            }

                        // ...

                        default:
                        case NoiseType.Perlin:
                            {
                                force.x = Noise2.perlin(xX * frequency, yX * frequency, zX * frequency);
                                force.y = Noise2.perlin(xY * frequency, yY * frequency, zY * frequency);
                                force.z = Noise2.perlin(xZ * frequency, yZ * frequency, zZ * frequency);

                                return force;
                            }

                        // ...

                        case NoiseType.Simplex:
                            {
                                force.x = Noise2.simplex(xX * frequency, yX * frequency, zX * frequency);
                                force.y = Noise2.simplex(xY * frequency, yY * frequency, zY * frequency);
                                force.z = Noise2.simplex(xZ * frequency, yZ * frequency, zZ * frequency);

                                break;
                            }

                        // ...

                        case NoiseType.OctavePerlin:
                            {
                                force.x = Noise2.octavePerlin(xX, yX, zX, frequency, octaves, octaveMultiplier, octaveScale);
                                force.y = Noise2.octavePerlin(xY, yY, zY, frequency, octaves, octaveMultiplier, octaveScale);
                                force.z = Noise2.octavePerlin(xZ, yZ, zZ, frequency, octaves, octaveMultiplier, octaveScale);

                                break;
                            }

                        case NoiseType.OctaveSimplex:
                            {
                                force.x = Noise2.octaveSimplex(xX, yX, zX, frequency, octaves, octaveMultiplier, octaveScale);
                                force.y = Noise2.octaveSimplex(xY, yY, zY, frequency, octaves, octaveMultiplier, octaveScale);
                                force.z = Noise2.octaveSimplex(xZ, yZ, zZ, frequency, octaves, octaveMultiplier, octaveScale);

                                break;
                            }
                    }

                    return force;
                }

                // ...

                protected override void OnDrawGizmosSelected()
                {
                    if (enabled)
                    {
                        base.OnDrawGizmosSelected();
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
