using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    public bool canFire;
    public float speed = 100;
    public int damage;
    Vector3 transformPos;
    void Start()
    {
        //transformPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        StartCoroutine(Destroy());
        //transformPos = transform.position;
        transform.Translate(0, 0, speed * Time.deltaTime);

        //RaycastHit[] hits = Physics.RaycastAll(new Ray(transformPos, (transform.position - transformPos).normalized), (transform.position - transformPos).magnitude);
        Ray ray = Camera.main.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, Mathf.Infinity))
        {
            if (hit.collider.tag == "Zombie")
            {
                if (Input.GetMouseButton(0))
                {
                    transform.position = Vector3.MoveTowards(transform.position, hit.collider.transform.position, speed * Time.deltaTime);
                }
            }

            if (hit.collider.tag == "Head")
            {
                if (Input.GetMouseButton(0))
                {
                    transform.position = Vector3.MoveTowards(transform.position, hit.collider.transform.position, speed * Time.deltaTime);
                }
            }
        }
    }
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Zombie")
        {
            other.gameObject.GetComponent<EnemyController>().health -= damage;
            print(other.gameObject.tag);
            gameObject.SetActive(false);
        }

        if (other.gameObject.tag == "Head")
        {
            other.gameObject.GetComponent<EnemyController>().health = 0;
            print(other.gameObject.tag);
            gameObject.SetActive(false);
        }
    }
    IEnumerator Destroy()
    {
        yield return new WaitForSeconds(0.3f);
        gameObject.SetActive(false);
    }
}
