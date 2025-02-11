using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Transition : MonoBehaviour
{
    private static GameObject m_canvas;

    private GameObject m_overlay;


    private void Awake()
    {
        //tao doi tuong chuyen canh
        m_canvas = new GameObject("TransitionCanvas");
        var canvas = m_canvas.AddComponent<Canvas>();
        canvas.renderMode = RenderMode.ScreenSpaceOverlay;
        // khong xoa UI khi ket thuc chuyen canh
        DontDestroyOnLoad(canvas);
    }

    //lop hieu ung chuyen canh
    public static void LoadLevel(string PlayGame,float duration,Color color)
    {
        var fade = new GameObject("Transition");
        fade.AddComponent<Transition>();
        fade.GetComponent<Transition>().StartFade(PlayGame, duration, color);

        //di chuyen phan tu Ui Bo cuc nay sang bo  cuc khac
        fade.transform.SetParent(m_canvas.transform, false);
        fade.transform.SetAsLastSibling();
    }

    public void StartFade(string PlayGame,float duration,Color fadeColor)
    {

        StartCoroutine(RunFade(PlayGame,duration,fadeColor));
    }

    private IEnumerator RunFade(string PlayGame,float duration,Color fadeColor)
    {
        //tạo hình ảnh 
        var bgTex = new Texture2D(1, 1);
        bgTex.SetPixel(0, 0, fadeColor);
        bgTex.Apply();

        //hiển thị hình ảnh và render trong suot de chuyen qua scene kia
        m_overlay = new GameObject();
        var image = m_overlay.AddComponent<Image>();
        var rect = new Rect(0,0,bgTex.width,bgTex.height);
        var sprite = Sprite.Create(bgTex,rect,new Vector2(0.5f,0.5f),1);
        image.material.mainTexture = bgTex;
        image.sprite = sprite;  
        var newColor = image.color;
        image.color = newColor;
        image.canvasRenderer.SetAlpha(0.0f);

        //set kick thuoc anh
        m_overlay.transform.localScale = new Vector3(1, 1, 1);
        m_overlay.GetComponent<RectTransform>().sizeDelta = m_canvas.GetComponent<RectTransform>().sizeDelta;
        m_overlay.transform.SetParent(m_canvas.transform,false);
        m_overlay.transform.SetAsFirstSibling();
        

        // bat dau fade in
        var time = 0.0f;
        var halfDuration = duration / 2.0f;
        while(time < halfDuration)
        {
            time += Time.deltaTime;
            image.canvasRenderer.SetAlpha(Mathf.InverseLerp(0, 1, time / halfDuration));

        }

        //hinh anh hien thi hoan toan
        image.canvasRenderer.SetAlpha(1.0f);
        //render den frame cuoi cung
        yield return new WaitForEndOfFrame();

        SceneManager.LoadScene(PlayGame);

       // bat dau fade out
        time=0.0f;

        while(time < halfDuration)
        {
            time += Time.deltaTime;
            image.canvasRenderer.SetAlpha(Mathf.InverseLerp(1,0, time / halfDuration));
            yield return new WaitForEndOfFrame();
        }

        image.canvasRenderer.SetAlpha(0.0f);
        yield return new WaitForEndOfFrame();
        Destroy(m_canvas);
    }

}
