using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class player : MonoBehaviour
{
    Rigidbody rb;
    Animator anim;
    float moveH, moveV;
    public float speed;
    string dir = "right";
    string state = "Free";

    public GameObject objectToThrow; // O objeto que você quer lançar
    public float throwForce = 10.0f;  // A força com que o objeto será lançado

    //Sounds
    public AudioSource tallGrassSound;

    IEnumerator WaitExecute(float seconds, System.Action action){
        yield return new WaitForSeconds(seconds);
        action.Invoke();
    }
    void toFree(){
        state = "Free";
    }
    void toAtack(){
        state = "Atack";
    }

    void Start(){
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
    }
    void Update(){
        PoisonPowder();
        if(state != "Atack"){
            Animation();
        }
        
        MudadorDeCenas_paraTestes();
    }
    void FixedUpdate(){
        Moviment();
    }
    void Moviment(){
        if(state == "Free"){
            Vector3 horizontalMovement = new Vector3(moveH, 0, moveV).normalized * speed * Time.deltaTime;
            rb.velocity = new Vector3(horizontalMovement.x, rb.velocity.y, horizontalMovement.z);
            moveH = Input.GetAxisRaw("Horizontal");
            moveV = Input.GetAxisRaw("Vertical");
        }
        else{
            rb.velocity = new Vector3(0, rb.velocity.y, 0);
            moveH = 0;
            moveV = 0;
        }           
        
    }
    void Animation(){
        if(moveH > 0 && moveV == 0) dir = "right";
        if(moveH > 0 && moveV < 0) dir = "down_right";
        if(moveH == 0 && moveV < 0) dir = "down";
        if(moveH < 0 && moveV < 0) dir = "down_left";
        if(moveH < 0 && moveV == 0) dir = "left";
        if(moveH < 0 && moveV > 0) dir = "up_left";
        if(moveH == 0 && moveV > 0) dir = "up";
        if(moveH > 0 && moveV > 0) dir = "up_right";

        if(moveH == 0 && moveV == 0 && state == "Free"){
            switch(dir){
                case "right": anim.SetInteger("inteiro", 11); break;
                case "down_right": anim.SetInteger("inteiro", 12); break;
                case "down": anim.SetInteger("inteiro", 13); break;
                case "down_left": anim.SetInteger("inteiro", 14); break;
                case "left": anim.SetInteger("inteiro", 15); break;
                case "up_left": anim.SetInteger("inteiro", 16); break;
                case "up": anim.SetInteger("inteiro", 17); break;
                case "up_right": anim.SetInteger("inteiro", 18); break;
            }
        }else if(state != "Atack"){
            switch(dir){
                case "right": anim.SetInteger("inteiro", 1); break;
                case "down_right": anim.SetInteger("inteiro", 2); break;
                case "down": anim.SetInteger("inteiro", 3); break;
                case "down_left": anim.SetInteger("inteiro", 4); break;
                case "left": anim.SetInteger("inteiro", 5); break;
                case "up_left": anim.SetInteger("inteiro", 6); break;
                case "up": anim.SetInteger("inteiro", 7); break;
                case "up_right": anim.SetInteger("inteiro", 8); break;
            }
        }
    }
    void OnTriggerEnter(Collider other){
        switch(other.gameObject.tag){
            case "Grass": 
                other.GetComponent<Animator>().SetInteger("inteiro", 1); 
                    tallGrassSound.Play(); 
                
            break;
        }
    }
    void OnTriggerExit(Collider other){
        switch(other.gameObject.tag){
            case "Grass": other.GetComponent<Animator>().SetInteger("inteiro", 2); break;
        }
    }

    void MudadorDeCenas_paraTestes(){
        if(Input.GetKeyDown(KeyCode.N)){
            SceneManager.LoadScene(1);
        }
        if(Input.GetKeyDown(KeyCode.B)){
            SceneManager.LoadScene(0);
        }
    }
    void PoisonPowder()
    {

        if (Input.GetMouseButtonDown(0) && state == "Free")
        {
            toAtack();
            StartCoroutine(WaitExecute(1, () => toFree() ));
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
                switch(angle){
                    case 0: anim.SetInteger("inteiro", 21); dir="right"; break;
                    case 1: anim.SetInteger("inteiro", 28); dir="up_right"; break;
                    case 2: anim.SetInteger("inteiro", 27); dir="up"; break;
                    case 3: anim.SetInteger("inteiro", 26); dir="up_left"; break;
                    case 4: anim.SetInteger("inteiro", 25); dir="left"; break;
                    case 5: anim.SetInteger("inteiro", 24); dir="down_left"; break;
                    case 6: anim.SetInteger("inteiro", 23); dir="down"; break;
                    case 7: anim.SetInteger("inteiro", 22); dir="down_right"; break;
                    case 8: anim.SetInteger("inteiro", 21); dir="right"; break;
                }
            }
        }
    }

}
