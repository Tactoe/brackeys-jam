using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class Position
{
    public int x;
    public int y;
}

public class PlayerCharacter : MonoBehaviour
{
    private Position pos = new Position();

    [SerializeField] private GameObject bulletPrefab;
    // Start is called before the first frame update
    void Start()
    {
        pos.x = 0;
        pos.y = 0;
        UpdatePos();
    }

    // Update is called once per frame
    void Update()
    {
        if (ArrowKeyPressed())
        {
            pos.x += (int)Input.GetAxisRaw("Horizontal");
            pos.y += (int)Input.GetAxisRaw("Vertical");
            UpdatePos();
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            var tmp = Instantiate(bulletPrefab);
            tmp.transform.position = transform.position;
            tmp.transform.forward = transform.forward;
        }
    }

    void UpdatePos()
    {
        var transform1 = transform;
        transform1.position = new Vector3(pos.x, transform1.position.y, pos.y);
    }

    bool ArrowKeyPressed()
    {
        return (Input.GetKeyDown(KeyCode.LeftArrow) ||
                Input.GetKeyDown(KeyCode.RightArrow) ||
                Input.GetKeyDown(KeyCode.UpArrow) ||
                Input.GetKeyDown(KeyCode.DownArrow));
    }
}
