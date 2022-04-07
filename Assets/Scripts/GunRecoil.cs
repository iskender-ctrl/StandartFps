using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunRecoil : MonoBehaviour
{
    Vector3 currentRotation, targetRotation;
    [SerializeField]
    private float recoilX, recoilY, recoilZ, snappiness, returnSpeed, duration, magnitude;
    [SerializeField]
    Vector3 firstRotation;

    // Update is called once per frame
    void Update()
    {
        targetRotation = Vector3.Lerp(targetRotation, firstRotation, returnSpeed * Time.deltaTime);
        currentRotation = Vector3.Slerp(currentRotation, targetRotation, snappiness * Time.fixedDeltaTime);
        transform.localRotation = Quaternion.Euler(currentRotation);
    }
    public void RecoilFire()
    {
        targetRotation += new Vector3(recoilX, Random.Range(-recoilY, recoilY), Random.Range(-recoilZ, recoilZ));
        StartCoroutine(Shake(duration, magnitude));
    }
    public IEnumerator Shake(float duration, float magnitude)
    {
        Vector3 cameraPos = Camera.main.transform.localPosition;

        float elapsed = 0.0f;

        while (elapsed < duration)
        {
            float x = Random.Range(-0.1f, 0.1f) * magnitude;
            float y = Random.Range(-0.1f, 0.0f) * magnitude;

            Camera.main.transform.localPosition = new Vector3(x, cameraPos.y, cameraPos.z);

            elapsed += Time.deltaTime;

            yield return null;
        }

        Camera.main.transform.localPosition = cameraPos;
    }
}
