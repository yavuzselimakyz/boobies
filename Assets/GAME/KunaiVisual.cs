using UnityEngine;

public class KunaiVisual : MonoBehaviour
{
    [SerializeField] private Transform projectileVisual;
    [SerializeField] private Transform projectileShadow;
    [SerializeField] private Kunai projectile;


    private Transform target;
    private Vector3 trajectoryStartPosition;


    private float shadowPositionDivider = 6f;


    private void Start()
    {
        trajectoryStartPosition = transform.position;
    }


    private void Update()
    {
        UpdateProjectileRotation();
        UpdateShadowPosition();

        float trajectoryProgressMagnitude = (transform.position - trajectoryStartPosition).magnitude;
        float trajectoryMagnitude = (target.position - trajectoryStartPosition).magnitude;


        float trajectoryProgressNormalized = trajectoryProgressMagnitude / trajectoryMagnitude;


        if (trajectoryProgressNormalized < .7f)
        {
            UpdateProjectileShadowRotation();
        }


    }


    private void UpdateShadowPosition()
    {
        if (projectile == null || projectileShadow == null || target == null)
        {
            Debug.LogWarning("KunaiVisual: Missing reference(s) in UpdateShadowPosition");
            return;
        }

        Vector3 newPosition = transform.position;
        Vector3 trajectoryRange = target.position - trajectoryStartPosition;

        if (Mathf.Abs(trajectoryRange.normalized.x) < Mathf.Abs(trajectoryRange.normalized.y))
        {
            newPosition.x = trajectoryStartPosition.x + projectile.GetNextXTrajectoryPosition() / shadowPositionDivider + projectile.GetNextPositionXCorrectionAbsolute();
        }
        else
        {
            newPosition.y = trajectoryStartPosition.y + projectile.GetNextYTrajectoryPosition() / shadowPositionDivider + projectile.GetNextPositionYCorrectionAbsolute();
        }

        projectileShadow.position = newPosition;
    }


    private void UpdateProjectileRotation()
    {
        Vector3 projectileMoveDir = projectile.GetProjectileMoveDir();


        projectileVisual.transform.rotation = Quaternion.Euler(0, 0, Mathf.Atan2(projectileMoveDir.y, projectileMoveDir.x) * Mathf.Rad2Deg);
    }


    private void UpdateProjectileShadowRotation()
    {
        Vector3 projectileMoveDir = projectile.GetProjectileMoveDir();


        projectileShadow.transform.rotation = Quaternion.Euler(0, 0, Mathf.Atan2(projectileMoveDir.y, projectileMoveDir.x) * Mathf.Rad2Deg);
    }


    public void SetTarget(Transform target)
    {
        this.target = target;
    }

}
