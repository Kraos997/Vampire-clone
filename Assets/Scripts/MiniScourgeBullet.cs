using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class MiniScourgeBullet : MonoBehaviour
{
    [SerializeField] private int _bulletCount;
    void Start()
    {
        StartCoroutine(Couroutine());
    }

    IEnumerator Couroutine()
    {
        yield return new WaitForSeconds(0.5f);
        for (int i = 1; i < _bulletCount + 1; i++)
        {
            float angle = 45 * i;
            Quaternion rotation = Quaternion.Euler(0,0, angle);
            Transform bulletTransform = Instantiate(GameAssets.I.pfBullet, transform.position, rotation);

            bulletTransform.localScale = new(0.5f,0.5f,0.5f);
            Vector3 shootDir =  rotation * Vector3.right;
            bulletTransform.GetComponent<BulletScript>().Setup(shootDir);
        }
        Destroy(gameObject);
    }
}
