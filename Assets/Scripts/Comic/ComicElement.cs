using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class ComicElement : MonoBehaviour {

    public enum Show {Fade, Right, Big};

    public float time = 1f;
    public float startSize = 1.4f;
    public Vector2 offset = new Vector2(1f, 0f);
    public bool lastOnPage = false;
    public Show show;

    float alpha = 0f;
    float size;
    Vector2 pos;
    Vector2 startPos;
    bool appearing = true;
    

	void Start () {
        switch (show)
        {
            case Show.Fade:
                UpdateColor();
                break;
            case Show.Right:
                startPos = GetComponent<RectTransform>().position;
                pos = GetComponent<RectTransform>().position + new Vector3(offset.x, offset.y);
                UpdatePosition();
                break;
            case Show.Big:
                size = startSize;
                UpdateSize();
                break;
            default:
                break;
        }
    }

    private void Update()
    {
        if (appearing)
        {
            switch (show)
            {
                case Show.Fade:
                    UpdateColor();
                    break;
                case Show.Right:
                    UpdatePosition();
                    break;
                case Show.Big:
                    UpdateSize();
                    break;
                default:
                    break;
            }
        }
            
    }

    void UpdatePosition()
    {
        pos -= new Vector2(offset.x * Time.deltaTime / time, offset.y * Time.deltaTime / time);
        if(Mathf.Abs(pos.x - startPos.x) < 0.15f && Mathf.Abs(pos.y - startPos.y) < 0.15f)
        {
            appearing = false;
            pos = startPos;
        }
        GetComponent<RectTransform>().position = pos;
    }

    void UpdateSize()
    {
        size -= (startSize - 1) * Time.deltaTime / time;
        if(size <= 1)
        {
            size = 1;
            appearing = false;
        }
        GetComponent<RectTransform>().localScale = new Vector3(size, size, 1);
    }

    void UpdateColor()
    {
        Image image = GetComponent<Image>();
        alpha +=Time.deltaTime / time;
        if(alpha >= 1f)
        {
            appearing = false;
            alpha = 1f;
        }
        Color col = image.color;
        col.a = alpha;
        image.color = col;
    }
}
