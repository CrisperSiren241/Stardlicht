using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretScript : MonoBehaviour
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
            Vector3 direction = _Player.position - head.position; // Направление к игроку
            Quaternion targetRotation = Quaternion.LookRotation(direction); // Целевой поворот
            head.rotation = Quaternion.Slerp(head.rotation, targetRotation, Time.deltaTime * 5f); // Плавное вращение
            Debug.DrawRay(_Barrel.position, head.forward * 5, Color.red, 2f);

            if (Time.time >= nextFire)
            {
                nextFire = Time.time + 1f / fireRate;
                Shoot();
            }
        }
    }

    void Shoot()
    {
        GameObject clone = Instantiate(_projectile, _Barrel.position, Quaternion.identity);
        Rigidbody rb = clone.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.AddForce(head.forward * 5000);
        }
        Destroy(clone, 10f);
    }



}
