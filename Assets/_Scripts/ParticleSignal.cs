//Original code by Albert Gushiken
//https://www.dropbox.com/s/vf8ohhd9w2bi5qk/ParticleToTarget.cs?dl=0 
//Modified by Jared Slawski for use in this project
using UnityEngine;
using System.Collections;

public class ParticleSignal : MonoBehaviour
{
    public GameObject targetObject;

    private ParticleSystem system;

    private static ParticleSystem.Particle[] particles = new ParticleSystem.Particle[1000];

    int count;

    float particleSystemDurationInSeconds = 1f;

    public void SetupParticleSignal(GameObject paddleTarget, Player owningPlayer)
    {
        this.targetObject = paddleTarget;
        this.system = GetComponent<ParticleSystem>();

        ParticleSystem.MainModule mainSystem = this.system.main;

        switch (owningPlayer)
        {
            case Player.Player1:
                mainSystem.startColor = GameManager.instance.playerPaddles[(int)Player.Player1].GetComponent<MeshRenderer>().material.color;
                break;
            case Player.Player2:
                mainSystem.startColor = GameManager.instance.playerPaddles[(int)Player.Player2].GetComponent<MeshRenderer>().material.color;
                break;
            default:
                Debug.LogError("Unknown player ID: " + owningPlayer + " Unable to set particle color.");
                break;
        }
    }

    void Start()
    {        
        system.Play();
        StartCoroutine(UpdateParticleSystem());
    }

    private IEnumerator UpdateParticleSystem()
    {
        float currentTime = 0f;

        while (currentTime < this.particleSystemDurationInSeconds)
        {
            this.count = this.system.GetParticles(particles);

            for (int i = 0; i < count; i++)
            {
                ParticleSystem.Particle particle = particles[i];

                Vector3 origin = this.system.transform.TransformPoint(particle.position);
                Vector3 destination = targetObject.transform.position;

                Vector3 targetPosition = (destination - origin) * (particle.remainingLifetime / particle.startLifetime);
                particle.position = this.system.transform.InverseTransformPoint(destination - targetPosition);
                particles[i] = particle;
            }

            this.system.SetParticles(particles, count);

            currentTime += Time.deltaTime;

            yield return null;
        }

        Destroy(this.gameObject);
    }
}