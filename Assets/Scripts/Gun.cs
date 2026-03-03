using UnityEngine;
using System.Collections;
using JetBrains.Annotations;


public class Gun : MonoBehaviour
{
    public float reloadTime = 1f;
    public float fireRate = 0.2f;
    public int magazineSize = 20;

    public GameObject bullet;
    public Transform bulletSpawnPoint;

    public float recoilDistance = 0.1f;
    public float recoilSpeed = 15f;

    private int currentAmmo;
    private bool isReloading = false;
    private float nextTimeToFire = 0f;

    private Quaternion initialRotation;
    private Vector3 initialPosition;
    private Vector3 recoilOffset = new Vector3(66, 50, 50);
    void Start()
    {
        currentAmmo = magazineSize;
        initialRotation = transform.localRotation;
        initialPosition = transform.localPosition;
    }
    public void Shoot()
    {
        if (isReloading || Time.time < nextTimeToFire)

            return;
        if (currentAmmo <= 0)
        {
            StartCoroutine(Reload());
            return;

        }
        currentAmmo--;
        Instantiate(bullet, bulletSpawnPoint.position, bulletSpawnPoint.rotation);
    }
        IEnumerator Reload()
        {
            isReloading = true;

            Quaternion targetRotation = initialRotation * Quaternion.Euler(recoilOffset);
            float halfReload = reloadTime / 2f;
            float t = 0f;

            while (t < halfReload)
            {
                transform.localRotation = Quaternion.Slerp(initialRotation, targetRotation, t / halfReload);
                t += Time.deltaTime;
                yield return null;
            }
            t = 0f;
            while (t < halfReload)
            {
                transform.localRotation = Quaternion.Slerp(initialRotation, targetRotation, t / halfReload);
                t += Time.deltaTime;
                yield return null;
            }
            currentAmmo = magazineSize;
            isReloading = false;
        } 
        public void TryReload()
        {
            if (isReloading) return;
            if (currentAmmo == magazineSize) return;

        StartCoroutine(Reload());
        }
    public 
        
    }

       


