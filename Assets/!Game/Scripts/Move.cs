using UnityEngine;
using static UnityEngine.GraphicsBuffer;

namespace MainMenu
{
    public class Move : MonoBehaviour
    {
        [SerializeField] private Transform _unitPosisiton;
        public float speed = 3f;
        


        void Update()
        {
            transform.position = Vector2.MoveTowards(transform.position, _unitPosisiton.position, speed * Time.deltaTime);
            if (_unitPosisiton.position == transform.position)
            {
                transform.position = new Vector2(Random.Range(-10f, -30f), transform.position.y);
            }
        }
    }
}
