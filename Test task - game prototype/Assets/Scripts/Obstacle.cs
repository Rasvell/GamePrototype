using System.Collections;
using UnityEngine;

public class Obstacle : MonoBehaviour
{

    public GameObject ExplosionEffect;

    private Transform _transform;
    private float minusScale= 0.01f;
    private bool Idestroyed;
     public void Explosion()
    {
        GetComponent<Transform>().GetChild(0).GetComponent<MeshRenderer>().material.color = Color.yellow;
        GetComponent<CapsuleCollider>().enabled = false;
        _transform = GetComponent<Transform>().GetChild(0).GetComponent<Transform>(); 
        StartCoroutine(BeginExplosion());
  
    }

    public IEnumerator BeginExplosion()
    {
        while (_transform.localScale.x>0)
        {
            yield return new WaitForSeconds(0.01f);
            Vector3 nowScale = _transform.localScale;
            Vector3 newScale = new Vector3(
                   nowScale.x - minusScale,
                   nowScale.y - minusScale,
                   nowScale.z - minusScale
                   );

            _transform.localScale = newScale;

        }

        if(!Idestroyed) FindObjectOfType<GameManager>().obstacles = -1;
        Idestroyed = true;
        ExplosionEffect.SetActive(true);
        Destroy(gameObject, 1f);
    }
 
}
