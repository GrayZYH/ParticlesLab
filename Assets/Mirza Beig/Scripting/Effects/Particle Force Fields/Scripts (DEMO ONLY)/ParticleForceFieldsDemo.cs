using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using UnityEngine.UI;
using UnityEngine.SceneManagement;

using MirzaBeig.Scripting.Effects;

public class ParticleForceFieldsDemo : MonoBehaviour
{
    [Header("Overview")]

    public Text FPSText;
    public Text particleCountText;

    public Toggle postProcessingToggle;
    public MonoBehaviour postProcessing;

    [Header("Particle System Settings")]

    new public ParticleSystem particleSystem;

    ParticleSystem.MainModule particleSystemMainModule;
    ParticleSystem.EmissionModule particleSystemEmissionModule;

    public Text maxParticlesText;
    public Text particlesPerSecondText;

    public Slider maxParticlesSlider;
    public Slider particlesPerSecondSlider;

    [Header("Attraction Particle Force Field Settings")]

    public AttractionParticleForceField attractionParticleForceField;

    public Text attractionParticleForceFieldRadiusText;
    public Text attractionParticleForceFieldMaxForceText;

    public Text attractionParticleForceFieldArrivalRadiusText;
    public Text attractionParticleForceFieldArrivedRadiusText;

    public Text attractionParticleForceFieldPositionTextX;
    public Text attractionParticleForceFieldPositionTextY;
    public Text attractionParticleForceFieldPositionTextZ;

    public Slider attractionParticleForceFieldRadiusSlider;
    public Slider attractionParticleForceFieldMaxForceSlider;

    public Slider attractionParticleForceFieldArrivalRadiusSlider;
    public Slider attractionParticleForceFieldArrivedRadiusSlider;

    public Slider attractionParticleForceFieldPositionSliderX;
    public Slider attractionParticleForceFieldPositionSliderY;
    public Slider attractionParticleForceFieldPositionSliderZ;

    [Header("Vortex Particle Force Field Settings")]

    public VortexParticleForceField vortexParticleForceField;

    public Text vortexParticleForceFieldRadiusText;
    public Text vortexParticleForceFieldMaxForceText;

    public Text vortexParticleForceFieldRotationTextX;
    public Text vortexParticleForceFieldRotationTextY;
    public Text vortexParticleForceFieldRotationTextZ;

    public Text vortexParticleForceFieldPositionTextX;
    public Text vortexParticleForceFieldPositionTextY;
    public Text vortexParticleForceFieldPositionTextZ;

    public Slider vortexParticleForceFieldRadiusSlider;
    public Slider vortexParticleForceFieldMaxForceSlider;

    public Slider vortexParticleForceFieldRotationSliderX;
    public Slider vortexParticleForceFieldRotationSliderY;
    public Slider vortexParticleForceFieldRotationSliderZ;

    public Slider vortexParticleForceFieldPositionSliderX;
    public Slider vortexParticleForceFieldPositionSliderY;
    public Slider vortexParticleForceFieldPositionSliderZ;

    void Start()
    {
        // Overview.

        if (postProcessing)
        {
            postProcessingToggle.isOn = postProcessing.enabled;
        }

        // Particle system settings.

        particleSystemMainModule = particleSystem.main;
        particleSystemEmissionModule = particleSystem.emission;

        maxParticlesSlider.value = particleSystemMainModule.maxParticles;
        particlesPerSecondSlider.value = particleSystemEmissionModule.rateOverTime.constant;

        maxParticlesText.text = "Max Particles: " + maxParticlesSlider.value;
        particlesPerSecondText.text = "Particles Per Second: " + particlesPerSecondSlider.value;

        // Attraction particle force field settings.

        attractionParticleForceFieldRadiusSlider.value = attractionParticleForceField.radius;
        attractionParticleForceFieldMaxForceSlider.value = attractionParticleForceField.force;

        attractionParticleForceFieldArrivalRadiusSlider.value = attractionParticleForceField.arrivalRadius;
        attractionParticleForceFieldArrivedRadiusSlider.value = attractionParticleForceField.arrivedRadius;

        Vector3 attractionParticleForceFieldPosition = attractionParticleForceField.transform.position;

        attractionParticleForceFieldPositionSliderX.value = attractionParticleForceFieldPosition.x;
        attractionParticleForceFieldPositionSliderY.value = attractionParticleForceFieldPosition.y;
        attractionParticleForceFieldPositionSliderZ.value = attractionParticleForceFieldPosition.z;

        attractionParticleForceFieldRadiusText.text = "Radius: " + attractionParticleForceFieldRadiusSlider.value;
        attractionParticleForceFieldMaxForceText.text = "Max Force: " + attractionParticleForceFieldMaxForceSlider.value;

        attractionParticleForceFieldArrivalRadiusText.text = "Arrival Radius: " + attractionParticleForceFieldArrivalRadiusSlider.value;
        attractionParticleForceFieldArrivedRadiusText.text = "Arrived Radius: " + attractionParticleForceFieldArrivedRadiusSlider.value;

        attractionParticleForceFieldPositionTextX.text = "Position X: " + attractionParticleForceFieldPositionSliderX.value;
        attractionParticleForceFieldPositionTextY.text = "Position Y: " + attractionParticleForceFieldPositionSliderY.value;
        attractionParticleForceFieldPositionTextZ.text = "Position Z: " + attractionParticleForceFieldPositionSliderZ.value;

        // Vortex particle force field settings.

        vortexParticleForceFieldRadiusSlider.value = vortexParticleForceField.radius;
        vortexParticleForceFieldMaxForceSlider.value = vortexParticleForceField.force;

        Vector3 vortexParticleForceFieldRotation = vortexParticleForceField.transform.eulerAngles;

        vortexParticleForceFieldRotationSliderX.value = vortexParticleForceFieldRotation.x;
        vortexParticleForceFieldRotationSliderY.value = vortexParticleForceFieldRotation.y;
        vortexParticleForceFieldRotationSliderZ.value = vortexParticleForceFieldRotation.z;

        Vector3 vortexParticleForceFieldPosition = vortexParticleForceField.transform.position;

        vortexParticleForceFieldPositionSliderX.value = vortexParticleForceFieldPosition.x;
        vortexParticleForceFieldPositionSliderY.value = vortexParticleForceFieldPosition.y;
        vortexParticleForceFieldPositionSliderZ.value = vortexParticleForceFieldPosition.z;

        vortexParticleForceFieldRadiusText.text = "Radius: " + vortexParticleForceFieldRadiusSlider.value;
        vortexParticleForceFieldMaxForceText.text = "Max Force: " + vortexParticleForceFieldMaxForceSlider.value;

        vortexParticleForceFieldRotationTextX.text = "Rotation X: " + vortexParticleForceFieldRotationSliderX.value;
        vortexParticleForceFieldRotationTextY.text = "Rotation Y: " + vortexParticleForceFieldRotationSliderY.value;
        vortexParticleForceFieldRotationTextZ.text = "Rotation Z: " + vortexParticleForceFieldRotationSliderZ.value;

        vortexParticleForceFieldPositionTextX.text = "Position X: " + vortexParticleForceFieldPositionSliderX.value;
        vortexParticleForceFieldPositionTextY.text = "Position Y: " + vortexParticleForceFieldPositionSliderY.value;
        vortexParticleForceFieldPositionTextZ.text = "Position Z: " + vortexParticleForceFieldPositionSliderZ.value;
    }

    void Update()
    {
        // Overview.

        FPSText.text = "FPS: " + 1.0f / Time.deltaTime;
        particleCountText.text = "Particle Count: " + particleSystem.particleCount;
    }

    public void ReloadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    // Particle system settings.

    public void SetMaxParticles(float value)
    {
        particleSystemMainModule.maxParticles = (int)value;
        maxParticlesText.text = "Max Particles: " + value;
    }
    public void SetParticleEmissionPerSecond(float value)
    {
        particleSystemEmissionModule.rateOverTime = value;
        particlesPerSecondText.text = "Particles Per Second: " + value;
    }

    // Attraction particle force field settings.

    public void SetAttractionParticleForceFieldRadius(float value)
    {
        attractionParticleForceField.radius = value;
        attractionParticleForceFieldRadiusText.text = "Radius: " + value;
    }
    public void SetAttractionParticleForceFieldMaxForce(float value)
    {
        attractionParticleForceField.force = value;
        attractionParticleForceFieldMaxForceText.text = "Max Force: " + value;
    }
    public void SetAttractionParticleForceFieldArrivalRadius(float value)
    {
        attractionParticleForceField.arrivalRadius = value;
        attractionParticleForceFieldArrivalRadiusText.text = "Arrival Radius: " + value;
    }
    public void SetAttractionParticleForceFieldArrivedRadius(float value)
    {
        attractionParticleForceField.arrivedRadius = value;
        attractionParticleForceFieldArrivedRadiusText.text = "Arrived Radius: " + value;
    }

    public void SetAttractionParticleForceFieldPositionX(float value)
    {
        Vector3 position = attractionParticleForceField.transform.position;

        position.x = value;

        attractionParticleForceField.transform.position = position;
        attractionParticleForceFieldPositionTextX.text = "Position X: " + value;
    }
    public void SetAttractionParticleForceFieldPositionY(float value)
    {
        Vector3 position = attractionParticleForceField.transform.position;

        position.y = value;

        attractionParticleForceField.transform.position = position;
        attractionParticleForceFieldPositionTextY.text = "Position Y: " + value;
    }
    public void SetAttractionParticleForceFieldPositionZ(float value)
    {
        Vector3 position = attractionParticleForceField.transform.position;

        position.z = value;

        attractionParticleForceField.transform.position = position;
        attractionParticleForceFieldPositionTextZ.text = "Position Z: " + value;
    }

    // Vortex particle force field settings.

    public void SetVortexParticleForceFieldRadius(float value)
    {
        vortexParticleForceField.radius = value;
        vortexParticleForceFieldRadiusText.text = "Radius: " + value;
    }
    public void SetVortexParticleForceFieldMaxForce(float value)
    {
        vortexParticleForceField.force = value;
        vortexParticleForceFieldMaxForceText.text = "Max Force: " + value;
    }

    public void SetVortexParticleForceFieldRotationX(float value)
    {
        Vector3 rotation = vortexParticleForceField.transform.eulerAngles;

        rotation.x = value;

        vortexParticleForceField.transform.eulerAngles = rotation;
        vortexParticleForceFieldRotationTextX.text = "Rotation X: " + value;
    }
    public void SetVortexParticleForceFieldRotationY(float value)
    {
        Vector3 rotation = vortexParticleForceField.transform.eulerAngles;

        rotation.y = value;

        vortexParticleForceField.transform.eulerAngles = rotation;
        vortexParticleForceFieldRotationTextY.text = "Rotation Y: " + value;
    }
    public void SetVortexParticleForceFieldRotationZ(float value)
    {
        Vector3 rotation = vortexParticleForceField.transform.eulerAngles;

        rotation.z = value;

        vortexParticleForceField.transform.eulerAngles = rotation;
        vortexParticleForceFieldRotationTextZ.text = "Rotation Z: " + value;
    }

    public void SetVortexParticleForceFieldPositionX(float value)
    {
        Vector3 position = vortexParticleForceField.transform.position;

        position.x = value;

        vortexParticleForceField.transform.position = position;
        vortexParticleForceFieldPositionTextX.text = "Position X: " + value;
    }
    public void SetVortexParticleForceFieldPositionY(float value)
    {
        Vector3 position = vortexParticleForceField.transform.position;

        position.y = value;

        vortexParticleForceField.transform.position = position;
        vortexParticleForceFieldPositionTextY.text = "Position Y: " + value;
    }
    public void SetVortexParticleForceFieldPositionZ(float value)
    {
        Vector3 position = vortexParticleForceField.transform.position;

        position.z = value;

        vortexParticleForceField.transform.position = position;
        vortexParticleForceFieldPositionTextZ.text = "Position Z: " + value;
    }
}
