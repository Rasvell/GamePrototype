using System.Collections;
using UnityEngine;
 
public class Ball : MonoBehaviour 
{
    public ShootController shootController;
    public float Speed = 2f;
    public float JumpForce = 10f;

    private Transform _transform;
    private Rigidbody rigidbody;
    [HideInInspector]
    public bool canMove;
    private bool Ijump;
    private Color normalColor;
    private int damageStep;
    private Coroutine coroutine;

    private void Start()
    {
        normalColor = GetComponent<MeshRenderer>().material.color;
       
        rigidbody = GetComponent<Rigidbody>();
    }

    private void Move()
    {
        Vector3 target = new Vector3(_transform.position.x, _transform.position.y, _transform.position.z + 1);
        _transform.position = Vector3.MoveTowards(_transform.position, target, Speed * Time.deltaTime);
    }

    public void Update()
    {
        if (canMove)
        {
            Move();
            if (!Ijump) Jump();
        }
    }

    public void Jump()
    {
        Ijump = true;
        rigidbody.AddForce(Vector3.up * JumpForce);
        if (canMove) Invoke("Jump", 2.0f);
        else Ijump = false;
    }

    public void Mover()
    {
        shootController.canCreate = false;
        canMove = true;
    }

    public IEnumerator CanDamage()
    {
        GetComponent<MeshRenderer>().material.color = Color.red;
        while (damageStep < 25)
        {
            damageStep++;
            shootController.Scaler(_transform, -0.02f);
            yield return new WaitForSeconds(0.01f);
        }
        GetComponent<MeshRenderer>().material.color = normalColor;
        coroutine = null;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<Obstacle>())
        {
            
            damageStep -= 25;
           coroutine=  StartCoroutine(CanDamage());
        }
        if (other.gameObject.tag == "Finish")
        {
            FindObjectOfType<GameManager>().GetComponent<GameManager>().Win();
        }
        
    }

    

    public void SetStartScale(float scale)
    {
        _transform = GetComponent<Transform>();
        _transform.localScale  = new Vector3( scale, scale, scale); 
    }
}
