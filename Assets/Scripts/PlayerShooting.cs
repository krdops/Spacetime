// =====================================================================================
// Script: PlayerShooting.cs
// Copyright (c) 2016, Golden Iota Gaming - All rights reserved.
//    Unauthorized copying of this file via any medium is strictly prohibited.
//    All contents within are closed-source, i.e. proprietary and confidential.
// Author(s): Kody Dibble
// Date Created: 26 July 2016
// Description: Event handler for shooting (bullet collision with enemy)
// =====================================================================================

using UnityEngine;

public class PlayerShooting : MonoBehaviour
{

    #region VARS

    // === GUN STATISTICS ===============================================================
    public int damagePerShot = 20;              // Damage amount for shooting.
    public float timeBetweenBullets = 0.15f;    // Fire delay between shots (in seconds).
    public float range = 100f;                  // Maximum firing range.
    public float effectsDisplayTime = 0.2f;     // Timing duration for displaying effects.

    // == GUN EFFECTS ===================================================================
    public ParticleSystem gunParticles;         // Particle effects from gunfire.
    public LineRenderer gunLine;                // Draw a line for bullet tracing/vapor trails.
    public AudioSource gunAudio;                // Gunfire sound effect.
    public Light gunLight;                      // Lighting effect of gunfire.

    // == GUN MECHANICS =================================================================
    private float timer;                        // Timer variable (for spacing between fired shots)
    private Ray shootRay;                       // Projectile trajectory ray.
    private RaycastHit shootHit;                // Projectile landing point.
    private int shootableMask;                  // For selectively "ignoring" colliders during raycasting.
    
    // == ENEMY =========================================================================
    entity_health enemyHealth = null;           // Component link to collided enemy.

    #endregion

    #region UNITY_FUNCS
    // DESC: Awake ===========================================================================================
    // Name  : Awake
    // Params: n/a
    // Descr : Unity function. This function is called only ONCE (during lifetime of script instance).
    //         Use for initializing any variables or game states before the game starts.
    //         This function is called AFTER all objects are initialized.
    // =======================================================================================================
    void Awake ()
    {
        shootableMask = LayerMask.GetMask ("Shootable");
        gunParticles = GetComponent<ParticleSystem> ();
        gunLine = GetComponent <LineRenderer> ();
        gunAudio = GetComponent<AudioSource> ();
        gunLight = GetComponent<Light> ();
    }

    // DESC: Update ==========================================================================================
    // Name  : Update
    // Params: n/a
    // Descr : Unity function. This is called once per frame.
    // =======================================================================================================
    void Update ()
    {
        // 1. Increment timer.
        timer += Time.deltaTime;

        // 2. Properly space a time-delay between gunfire. Shoot if the time is "right".
		if(Input.GetButton ("Fire1") && timer >= timeBetweenBullets && Time.timeScale != 0)
        {
            Shoot ();
        }

        // 3. Timing of muzzle flash from gun.
        if(timer >= timeBetweenBullets * effectsDisplayTime)
        {
            DisableEffects ();
        }
    }


    #endregion

    #region MISC_FUNCS
    // DESC: DisableEffects ==================================================================================
    // Name  : DisableEffects
    // Params: n/a
    // Descr : Only disables display of muzzle flash.
    // =======================================================================================================
    public void DisableEffects ()
    {
        gunLine.enabled = false;
        gunLight.enabled = false;
    }


    // DESC: Shoot ===========================================================================================
    // Name  : Shoot
    // Params: n/a
    // Descr : Event handler for firing/collision mechanism.
    // =======================================================================================================
    void Shoot ()
    {
        // 1. Reset timer.
        timer = 0f;

        // 2. Play audio sound of gunfire.
        gunAudio.Play ();

        // 3. Display muzzle flash.
        gunLight.enabled = true;

        // 4. Toggle particle effects of gunfire.
        gunParticles.Stop ();
        gunParticles.Play ();

        // 5. Initialize tracing of trajectory of projectile.
        gunLine.enabled = true;
        gunLine.SetPosition (0, transform.position);

        // 6. Determine origin and direction of projectile trajectory.
        shootRay.origin = transform.position;
        shootRay.direction = transform.forward;

        // 7. Collision handling of projectile.
        if(Physics.Raycast (shootRay, out shootHit, range, shootableMask))
        {
            #region OLD_CODE
            /*
            EnemyHealth enemyHealth = shootHit.collider.GetComponent <EnemyHealth> ();
            if(enemyHealth != null)
            {
                enemyHealth.TakeDamage (damagePerShot, shootHit.point);
            }
            */
            #endregion

            // 7a-1. Initialize connection to enemy's health component.
            enemyHealth = shootHit.collider.GetComponent<entity_health>();

            // 7a-2. Apply damage as long as enemy is NOT considered "dead".
            if (!enemyHealth.hasDied())
            {
                // 7a-2a. Give damage amount (determined by "damagePerShot") and subtract that from enemy's current Health.
                enemyHealth.damage(damagePerShot);
            }
            // 7a-3. Uninitialize connection to enemy's health component.
            enemyHealth = null;

            // 7a-4. Finialize position of projectile landing.
            gunLine.SetPosition (1, shootHit.point);

        }
        else
        {
            // 7b-1. Finialize position of projectile landing.
            gunLine.SetPosition (1, shootRay.origin + shootRay.direction * range);
        }
    }


    #endregion
}
