using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionLaser : ActionBase
{
    public GameObject LaserObject;      // 雷射特效
    public GameObject LaserRealObject;    
    public GameObject ExplosionObject;  // 噴發特效
    public GameObject ShootPosition;
    public float LaserKeepTime;
    public float Damage;

    private GameObject target;
    private GameObject laserObject;
    private GameObject laserRealObject;
    private GameObject explosionObject;
    private ParticleSystem laserParticleSystem;
    private ParticleSystem laserRealParticleSystem;
    private bool isFinished;

    public override void Init()
    {
        target = GameObject.FindWithTag("Player");
        laserObject = Instantiate(LaserObject, ShootPosition.transform.position, Quaternion.identity);
        laserRealObject = Instantiate(LaserRealObject, ShootPosition.transform.position, Quaternion.identity);
        laserRealObject.AddComponent<ParticleDamager>().SetValue(gameObject, Damage * monsterInfo.AttackBuffRate);
        explosionObject = Instantiate(ExplosionObject, ShootPosition.transform.position, Quaternion.identity, transform);
        laserParticleSystem = laserObject.GetComponent<ParticleSystem>();
        laserRealParticleSystem = laserRealObject.GetComponent<ParticleSystem>();
        isFinished = false;
        StartCoroutine(LaserDestroy());
    }

    public override void Process()
    {
        if(!isFinished)
            ShootPlayer();
    }

    public override void Exit()
    {
        
    }

    private void ShootPlayer()
    {
        Vector3 newDirection = Vector3.RotateTowards(transform.forward, target.transform.position - transform.position, monsterInfo.RotateSpeed * Time.deltaTime, 0.0f);
        newDirection.y = 0;
        transform.rotation = Quaternion.LookRotation(newDirection);
        // 雷射物件方向控制
        laserObject.transform.position = ShootPosition.transform.position;
        laserRealObject.transform.position = ShootPosition.transform.position;
        ParticleSystem.Particle[] particles = new ParticleSystem.Particle[laserParticleSystem.particleCount];
        ParticleSystem.Particle[] realParticles = new ParticleSystem.Particle[laserRealParticleSystem.particleCount];

        int count = laserParticleSystem.GetParticles(particles);
        int realCount = laserRealParticleSystem.GetParticles(realParticles);
        for (int i = 0; i < count; i++)
        {
            particles[i].velocity = Quaternion.AngleAxis(transform.rotation.eulerAngles.y, Vector3.up) * Vector3.forward * 40f;
        }

        for(int i = 0; i < realCount; i++)
        {
            realParticles[i].velocity = Quaternion.AngleAxis(transform.rotation.eulerAngles.y, Vector3.up) * Vector3.forward * 40f;
        }

        laserParticleSystem.SetParticles(particles, count);
        laserRealParticleSystem.SetParticles(realParticles, realCount);
    }

    private IEnumerator LaserDestroy()
    {
        yield return new WaitForSeconds(LaserKeepTime);
        isFinished = true;
        Destroy(laserObject);
        Destroy(laserRealObject);
        Destroy(explosionObject);
    }
}
