using UnityEngine;

/*
    굽기 전의 Sheet를 굽는 오븐

*/

public class Oven : MonoBehaviour
{
    public GameObject sheet_bake;
    public float operatingTime = 4.0f;
    private float coolTime;
    private bool coolDown = false;
    void Start()
    {
        
    }

    void Update()
    {
        if(coolDown){
            coolTime -= Time.deltaTime;
            if(coolTime <= 0){
                coolDown = false;
                Instantiate(sheet_bake, transform.position, Quaternion.identity);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other) {
        Debug.Log(other.gameObject);

        Sheet sheet = other.GetComponent<Sheet>();
        if(sheet != null){
            coolTime = operatingTime;
            coolDown = true;
            Destroy(other.gameObject);
        }
    }
}
