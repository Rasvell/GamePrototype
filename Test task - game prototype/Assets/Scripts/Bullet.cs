using UnityEngine;

public class Bullet : MonoBehaviour
{
    public GameObject BulletColorer;


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<Obstacle>())
        {
            Infection();
          Destroy(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void Infection()
    {
        Collider [] obstacle = Physics.OverlapSphere(transform.position, GetComponent<Transform>().localScale.x*2.5f);
         
        for (int i = 0; i < obstacle.Length; i++)
        {
            if (obstacle[i].GetComponent<Obstacle>())
            {
                ShotColorer(obstacle[i].transform);  
            }
        }
    }
    
    public void ShotColorer(Transform TargetTransform)
    { 
        GameObject newColorerBullet = Instantiate(BulletColorer, transform.position, Quaternion.identity);
        ColorerBullet colorerBullet = newColorerBullet.GetComponent<ColorerBullet>();
        colorerBullet.target = TargetTransform;
        colorerBullet.Trajector();
    }


}
