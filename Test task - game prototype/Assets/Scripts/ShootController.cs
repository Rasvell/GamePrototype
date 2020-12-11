using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
public class ShootController : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public Transform ball; 
    public Transform road;
    public GameObject bullet; 
    public GameObject particles;
    public GameManager gameManager;


    private Transform bulletTransform;
    private Coroutine coroutine;
    private float AddScale = 0.01f;
    private GameObject newBullet;
    [HideInInspector]
    public bool canCreate = true;



    public void OnPointerDown(PointerEventData eventData)
    {
        if (ball.transform.localScale.x > 1f && canCreate)
        {
            canCreate = false;
            Vector3 position = new Vector3(ball.position.x, ball.position.y, ball.position.z + 2);
            newBullet = Instantiate(bullet, position, Quaternion.identity);
            bulletTransform = newBullet.GetComponent<Transform>();
            particles.SetActive(true);

            StartCoroutine(GrowthOnCreate());

            if (coroutine == null)
            {
                coroutine = StartCoroutine(Growth());
            }
        }

    }

    public void OnPointerUp(PointerEventData eventData)
    {
      
        if (coroutine != null)
        {
            StopCoroutine(coroutine);
            coroutine = null;
            newBullet.GetComponent<Rigidbody>().AddForce(Vector3.forward*100);
            particles.SetActive(false);
        }

    }

   public IEnumerator Growth()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.01f);
            if (ball.transform.localScale.x > 1f && canCreate)
            {

                Scaler(ball, -AddScale*0.5f);
                Scaler(bulletTransform, AddScale); 
                road.transform.localScale = new Vector3(ball.transform.localScale.x * 0.1f, road.transform.localScale.y, road.transform.localScale.z);
            } 
            else if (ball.transform.localScale.x < 1f)
            {
                gameManager.Lose();
            }
        }
    }

    public IEnumerator GrowthOnCreate()
    {   int i = 0;
        while (i < 25)
        {
            i++; 
            Scaler(ball, -AddScale);
            road.transform.localScale = new Vector3(ball.transform.localScale.x * 0.1f, road.transform.localScale.y, road.transform.localScale.z);
            yield return new WaitForSeconds(0.01f);
        }
        canCreate = true;
    }

     

    public void Scaler( Transform obj, float AddScale) 
    {
        Vector3 nowScale = obj.transform.localScale;
        Vector3 newScale = new Vector3(
               nowScale.x + AddScale,
               nowScale.y + AddScale,
               nowScale.z + AddScale
               );

        obj.transform.localScale = newScale;
        if (obj.transform.localScale.x < 1f) gameManager.Lose();
    }
}

