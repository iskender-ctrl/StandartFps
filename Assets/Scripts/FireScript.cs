using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class FireScript : MonoBehaviour
{
    public GameObject bulletPrefab, bulletPosition, firePngPos, gun, character;
    public TextMeshProUGUI bulletsRemainingText;
    public ParticleSystem[] openFire;
    AudioSource fireSound;
    GunRecoil recoil;
    int fireRandomİndex;
    public int reload, fireLength, bulletsRemaining, damage;
    public float nextFire = 0;
    public float weaponFrequency = 0.5f;
    bool canFire = true;
    private void Awake()
    {
        recoil = gun.GetComponent<GunRecoil>();
        Camera.main.GetComponent<AudioSource>().Stop();
        fireSound = GetComponent<AudioSource>();
        bulletsRemaining = fireLength;
        bulletsRemainingText.text = bulletsRemaining.ToString();

        for (int i = 0; i < openFire.Length; i++)
        {
            openFire[i].Stop();
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        OpenFire();
    }
    void OpenFire()
    {
        if (Input.GetMouseButton(0) && Time.time > nextFire && canFire == true)
        {
            nextFire = Time.time + weaponFrequency;

            if (reload < fireLength)
            {
                reload += 1;

                GetComponent<BulletPool>().GetPooledBullet();
                recoil.RecoilFire();
                gun.GetComponent<Animator>().SetBool("Fire", true);
                fireSound.Play();

                for (int i = 0; i < openFire.Length; i++)
                {
                    fireRandomİndex = Random.Range(0, openFire.Length);
                    openFire[fireRandomİndex].Play();
                }

                if (gun.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("GunShaker"))
                {
                    gun.GetComponent<Animator>().SetBool("Fire", false);
                }

                bulletsRemaining -= 1;
                bulletsRemainingText.text = bulletsRemaining.ToString();
            }

            else
            {
                Camera.main.GetComponent<AudioSource>().Play();
                character.GetComponent<Animator>().SetBool("reloading", true);
                GetComponent<BulletPool>().reloadAgain = true;
                gun.GetComponent<Animator>().SetBool("Fire", false);
                canFire = false;
                StartCoroutine(FinishedReloading());
            }
        }
        else
        {
            fireSound.Stop();
        }
    }
    private void OnEnable()
    {
        bulletsRemaining = fireLength;
        character.GetComponent<Animator>().SetBool("reloading", false);

        GetComponent<BulletPool>().reloadAgain = false;
        canFire = true;
        reload = 0;
        bulletsRemaining = fireLength;
        bulletsRemainingText.text = bulletsRemaining.ToString();
    }
    private void OnDisable()
    {
        bulletsRemaining = fireLength;
        character.GetComponent<Animator>().SetBool("reloading", false);

        GetComponent<BulletPool>().reloadAgain = false;
        canFire = true;
        reload = 0;
        bulletsRemaining = fireLength;
        bulletsRemainingText.text = bulletsRemaining.ToString();
    }
    IEnumerator FinishedReloading()
    {
        yield return new WaitForSeconds(3f);
        bulletsRemaining = fireLength;
        character.GetComponent<Animator>().SetBool("reloading", false);

        GetComponent<BulletPool>().reloadAgain = false;
        canFire = true;
        reload = 0;
        bulletsRemaining = fireLength;
        bulletsRemainingText.text = bulletsRemaining.ToString();
        yield return new WaitForSeconds(15f);
    }
}
