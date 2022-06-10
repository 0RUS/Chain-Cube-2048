using System.Collections;
using UnityEngine;

public class Cube : MonoBehaviour
{
    public static Cube Instanse;
    public Rigidbody rb;
    public CharacterController character;

    [SerializeField]
    private Material material;
    public float Value = 1;
    public bool isReleased = false;
    public bool isPressed = false;
    private bool isMerged = false;
    private void Awake()
    {
        if (Instanse == null)
            Instanse = this;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (isReleased)
        {
            GameController.gameOver = other.CompareTag("Death");
            if (other.CompareTag(tag) && !isMerged)
            {
                Value++;
                int pow = (int)Mathf.Pow(2, Value);
                SetMaterial(gameObject, pow.ToString());
                tag = pow.ToString();
                GameController.Instanse.AddPoints(pow);
                rb.AddForce(new Vector3(transform.position.x, transform.position.y + 2000, transform.position.z), ForceMode.Impulse);
                Destroy(other.gameObject);
                StartCoroutine(Merge());
            }
        }
    }
    public void SetMaterial(GameObject x, string s)
    {
        foreach (Renderer r in x.GetComponentsInChildren<Renderer>())
        {
            r.material = Resources.Load($"Materials/{s}", typeof(Material)) as Material;
        }
    }

    IEnumerator Merge()
    {
        isMerged = true;
        yield return new WaitForSeconds(0.1f);
        isMerged = false;
    }
}
