using UnityEngine;

public class ColorerBullet : MonoBehaviour
{
    public AnimationCurve Trajectory;
    public Transform target;
    public float Speed;

    private int SegmentCount = 15; 
    private int _count; 
    private Vector3[] _trajectory;
    private Transform _transform;

     
    public void Trajector()
    {
        _transform = GetComponent<Transform>();
        Vector3[] points = new Vector3[SegmentCount];
        Vector3 step = (target.transform.position - _transform.position) / SegmentCount;
        points[0] = _transform.position;
        for (int i = 0; i < SegmentCount; i++)
        {
            Vector3 position = _transform.position + (step * i);
            position.y += Trajectory.Evaluate(i / (float)SegmentCount);
            points[i] = position; 
        } 
        for (int i = 0; i < points.Length; i++)
        {
            points[i].y = points[i].y - 1;
        }
        StartMove(points);

    }

    public void StartMove(Vector3[] trajectory)
    {
        _transform.position = trajectory[0]; 
        _trajectory = trajectory;
        _count = 0;
    }
   
    void Update()
    { 
      if(_count < _trajectory.Length)  Move();
    }

    public void Move()
    {
        _transform.position = Vector3.MoveTowards(_transform.position, _trajectory[_count], Speed * Time.deltaTime);
        if (_transform.position == _trajectory[_count])
        {
            _count++;
            if (_count >= _trajectory.Length)
            {
                _transform.position = new Vector3(_transform.position.x, _transform.position.y-2, _transform.position.z);
               if(target!=null) target.gameObject.GetComponent<Obstacle>().Explosion();
                Destroy(gameObject, 0.2f);
            }
        }
    }
}
