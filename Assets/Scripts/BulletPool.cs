using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletPool : MonoBehaviour
{
    public static BulletPool SharedInstance;
    public List<GameObject> pooledBullet;
    public GameObject objectToPool, targetPos, bulletPos;
    public int amountToPool;
    public float rotationSpeed;
    public bool reloadAgain;

    private void Awake()
    {
        SharedInstance = this;
    }
    private void Start()
    {
        pooledBullet = new List<GameObject>();

        for (int i = 0; i < amountToPool; i++)
        {
            GameObject tmp = Instantiate(objectToPool, targetPos.transform.position, targetPos.transform.rotation);
            tmp.SetActive(false);
            pooledBullet.Add(tmp);
        }
    }
    private void Update()
    {
        if (reloadAgain == true)
        {
            foreach (GameObject i in pooledBullet)
            {
                if (i.activeInHierarchy == true)
                {
                    i.transform.position = targetPos.transform.position;
                    i.SetActive(false);
                }
            }
        }
        for (int i = 0; i < pooledBullet.Count; i++)
        {
            if (!pooledBullet[i].activeInHierarchy)
            {
                pooledBullet[i].transform.position = targetPos.transform.position;
                pooledBullet[i].transform.rotation = targetPos.transform.rotation;
            }
        }
    }
    public void GetPooledBullet()
    {
        for (int i = 0; i < pooledBullet.Count; i++)
        {
            if (!pooledBullet[i].activeInHierarchy)
            {
                pooledBullet[i].transform.position = targetPos.transform.position;
                pooledBullet[i].transform.rotation = targetPos.transform.rotation;

                if (pooledBullet[i].transform.position == targetPos.transform.position)
                {
                    pooledBullet[i].GetComponent<TrailRenderer>().Clear();
                    pooledBullet[i].SetActive(true);
                }
                break;
            }
        }
    }
}
