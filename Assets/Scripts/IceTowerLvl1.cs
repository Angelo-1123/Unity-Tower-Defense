using UnityEngine;
using System.Collections;

public class IceTowerLvl1 : MonoBehaviour
{
    public Transform target;
    private Enemy targetEnemy;

    [Header("General")]
    public float range = 15f; // Check TowerBlueprint script to make sure both share the same range.

    [Header("Use Projectiles (default)")]
    public float fireRate = 1f;
    private float fireCountdown = 0f;
    public GameObject arrowPrefab;

    [Header("Use Laser")]
    public bool useLaser = false;
    public LineRenderer lineRenderer;
    public LineRenderer circleRenderer;
    public ParticleSystem impactEffect;
    public Light impactLight;
    public int damageOverTime = 10;
    public float slowAmount = 0.3f;

    public float explosionRadius = 50.0f;

    [Header("Unity Setup Fields")]
    public string enemyTag = "Enemy";
    public Transform firePoint;

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating ("UpdateTarget", 0f, 0.5f);
    }

    void UpdateTarget ()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemyTag);
        float shortestDistance = Mathf.Infinity;
        GameObject nearestEnemy = null;

        foreach (GameObject enemy in enemies)
        {
            float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);
            if (distanceToEnemy < shortestDistance)
            {
                shortestDistance = distanceToEnemy;
                nearestEnemy = enemy;
            }
        }

        if(nearestEnemy != null && shortestDistance <= range)
        {
            target = nearestEnemy.transform;
            targetEnemy = nearestEnemy.GetComponent<Enemy>();
        }
        else
        {
            target = null;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(target == null)
        {
            if(useLaser)
            {
                if(lineRenderer.enabled)
                {
                    lineRenderer.enabled = false;
                    impactEffect.Stop();
                    impactLight.enabled = false;
                }
            }

            return;
        }

        if(useLaser)
        {
            Laser();
        }
        else
        {
            if(fireCountdown <= 0f)
            {
                Shoot();
                fireCountdown = 1f / fireRate;
            }

            fireCountdown -= Time.deltaTime;
        }
    }

    void Laser()
    {   
        targetEnemy.TakeDamage(damageOverTime * Time.deltaTime, true);
        //targetEnemy.Slow(slowAmount);

        Collider[] colliders = Physics.OverlapSphere(target.position, explosionRadius);
        foreach (Collider collider in colliders)
        {
            if(collider.tag == "Enemy")
            {
                Enemy e = collider.GetComponent<Enemy>();
                e.Slow(slowAmount);
            }
        }


        if(!lineRenderer.enabled)
        {
            lineRenderer.enabled = true;
            impactEffect.Play();
            impactLight.enabled = true;
        }
        lineRenderer.SetPosition(0, firePoint.position);
        lineRenderer.SetPosition(1, target.position);
        
        Vector3 dir = firePoint.position - target.position;

        impactEffect.transform.position = target.position + dir.normalized;
        
        impactEffect.transform.rotation = Quaternion.LookRotation(dir);

        
    }

    void Shoot()
    {
        GameObject arrowGO = (GameObject)Instantiate (arrowPrefab, firePoint.position, firePoint.rotation);
        Arrow arrow = arrowGO.GetComponent<Arrow>();

        if (arrow != null)
        {
            arrow.Seek(target);
        }
    }

    void OnDrawGizmosSelected ()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
    }
}
