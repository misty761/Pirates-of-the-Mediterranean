using UnityEngine;

public class TextUp : MonoBehaviour
{
    public int speed = 10;
    Vector2 initPos;

    private void Start()
    {
        initPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector2.up * Time.deltaTime * speed);
    }

    private void OnDisable()
    {
        transform.position = initPos;
    }
}
