using UnityEngine;

public class ThrowObject : MonoBehaviour
{
    public GameObject objectToThrow; // O objeto que você quer lançar
    public float throwForce = 10.0f;  // A força com que o objeto será lançado

    void Update()
    {

        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit))
            {
                GameObject thrownObject = Instantiate(objectToThrow, transform.position, Quaternion.identity);
                Rigidbody rb = thrownObject.AddComponent<Rigidbody>();
                rb.useGravity = false;
                Vector3 throwDirection = (hit.point - transform.position).normalized;
                rb.AddForce(throwDirection * throwForce, ForceMode.VelocityChange);

                Collider playerCollider = GetComponent<CapsuleCollider>();
                Collider objectCollider = thrownObject.GetComponent<SphereCollider>();

                if (playerCollider != null && objectCollider != null)
                {
                    Physics.IgnoreCollision(playerCollider, objectCollider);
                }

                // Faz o objeto lançado enfrentar a direção do lançamento
                thrownObject.transform.LookAt(hit.point);

                //Me da um angulo em relação ao clique no chão
                float angle = Mathf.Atan2(throwDirection.z, throwDirection.x) * Mathf.Rad2Deg;

                // Ajusta os ângulos para como eles realmente são na matemática.
                angle += 0; 
                if (angle < 0) angle += 360;
                //---------------
                angle = Mathf.RoundToInt(angle / 45);
                Debug.Log(angle); 
            }
        }
    }
}