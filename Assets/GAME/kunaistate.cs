using System.Collections;
using UnityEngine;

public class kunaistate : MonoBehaviour
{
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private Transform target;


    [SerializeField] private float shootRate;
    [SerializeField] private float projectileMaxMoveSpeed;
    [SerializeField] private float projectileMaxHeight;


    [SerializeField] private AnimationCurve trajectoryAnimationCurve;
    [SerializeField] private AnimationCurve axisCorrectionAnimationCurve;
    [SerializeField] private AnimationCurve projectileSpeedAnimationCurve;


    private float shootTimer;

  
    private void Update()
    {
        shootTimer -= Time.deltaTime;


        if (shootTimer <= 0)
        {
            shootTimer = shootRate;
            Kunai projectile = Instantiate(projectilePrefab, transform.position, Quaternion.identity).GetComponent<Kunai>();


            projectile.InitializeProjectile(target, projectileMaxMoveSpeed, projectileMaxHeight);
            projectile.InitializeAnimationCurves(trajectoryAnimationCurve, axisCorrectionAnimationCurve, projectileSpeedAnimationCurve);
        }
    }

}


