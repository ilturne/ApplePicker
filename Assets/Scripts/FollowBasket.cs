using UnityEngine;

public class FollowBasket : MonoBehaviour
{
    private GameObject targetBasket;
    public float followSpeed = 10f;

    public void SetTarget(GameObject target)
    {
        targetBasket = target;
    }

    private void Update()
    {
        if (targetBasket != null)
        {
            // Follow the X position of the basket below it
            transform.position = new Vector3(targetBasket.transform.position.x, transform.position.y, transform.position.z);
        }
    }
}
