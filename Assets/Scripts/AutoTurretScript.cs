using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoTurretScript : MonoBehaviour
{
    // Start is called before the first frame update
    Transform _Player;
    public Transform _Barrel;
    float dist;
    public float howClose;
    public Transform head;
    public GameObject _projectile;
    public float fireRate, nextFire;
    void Start()
    {
        _Player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        dist = Vector3.Distance(_Player.position, transform.position);
        if (dist <= howClose)
        {
            if (Time.time >= nextFire)
            {
                nextFire = Time.time + 1f / fireRate;
                Shoot(); // Передаем точное направление к игроку
            }
        }
    }

    void Shoot()
    {
        // Инстанциируем снаряд
        GameObject clone = Instantiate(_projectile, _Barrel.position, Quaternion.identity);
        Rigidbody rb = clone.GetComponent<Rigidbody>();
        if (rb != null)
        {
            // Применяем силу в направлении к игроку
            rb.AddForce(_Barrel.forward * 100);
        }
        Destroy(clone, 5f);
    }

}
