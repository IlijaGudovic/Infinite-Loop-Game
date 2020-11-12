using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ParticleSystem))]
public class effectScript : MonoBehaviour
{


    ParticleSystem efekat;
    ParticleSystem.Particle[] particles;

    public Color mainColor;

    public bool colorBool;

    private void LateUpdate()
    {

        if (colorBool == true)
        {
            InitializeIfNeeded();

            // GetParticles is allocation free because we reuse the m_Particles buffer between updates
            int numParticlesAlive = efekat.GetParticles(particles);

            //Menja boju
            changeColor();

            // Apply the particle changes to the Particle System
            efekat.SetParticles(particles, numParticlesAlive);
        }
        
    }

    void InitializeIfNeeded()
    {
        if (efekat == null)
            efekat = GetComponent<ParticleSystem>();

        if (particles == null || particles.Length < efekat.main.maxParticles)
            particles = new ParticleSystem.Particle[efekat.main.maxParticles];
    }



    private bool oneTimeLerp;

    private float lerpTime;

    public float lerpDuration = 5;



    public void changeColor()
    {

        int numParticlesAlive = efekat.GetParticles(particles);

        if (oneTimeLerp == false) //Da bi moglo da se lerpuje, odnosno da lerpTime ne bi uvek bio 0
        {

            oneTimeLerp = true;

            lerpTime = 0;
            Invoke("returnLerp", 2);

        }

        // Change only the particles that are alive
        for (int i = 0; i < numParticlesAlive; i++) 
        {

            particles[i].startColor = Color.Lerp(particles[i].startColor, mainColor, lerpTime); //Menja boju

        }

        if (lerpTime < 1)
        {
            lerpTime += Time.deltaTime / lerpDuration;
        }

    }

    private void returnLerp()
    {
        oneTimeLerp = false;
    }







}
