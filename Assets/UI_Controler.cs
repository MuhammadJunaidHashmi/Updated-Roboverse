using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Controler : MonoBehaviour
{
    public List<GameObject> UiImages;
    public float FadingSpeed = 0.25f;
    int index = 0;

    public void Start()
    {
        for (int i = 0; i < 1; i++)
        {
            StartCoroutine(FadeIN(UiImages[i]));
        } 
    }

    IEnumerator FadeIN(GameObject obj)
    {
        Debug.Log("In start");
        obj.SetActive(true);
        float alpha = obj.gameObject.GetComponent<Image>().color.a;

        while (alpha <= 1)
        {
            Debug.Log("out-" + alpha);
            alpha -= Time.deltaTime * FadingSpeed;
            obj.gameObject.GetComponent<Image>().color = new Color(obj.gameObject.GetComponent<Image>().color.r, obj.gameObject.GetComponent<Image>().color.g, obj.gameObject.GetComponent<Image>().color.b, alpha);
        }
        yield return new WaitForSeconds(UiImages.Count - 1);
        Debug.Log("In end");
        StartCoroutine(FadeOut(obj));
    }
    
    IEnumerator FadeOut(GameObject obj)
    {
        index++;
        Debug.Log("Out start");
        float alpha = obj.gameObject.GetComponent<Image>().color.a;
        while (alpha > 0.3f)
        {
            Debug.Log("out-" + alpha);
            alpha -= Time.deltaTime * FadingSpeed;
            obj.gameObject.GetComponent<Image>().color = new Color(obj.gameObject.GetComponent<Image>().color.r, obj.gameObject.GetComponent<Image>().color.g, obj.gameObject.GetComponent<Image>().color.b, alpha);
            yield return null;
            if (alpha < 0.5f)
            {
                if (index <= UiImages.Count)
                    StartCoroutine(FadeIN(UiImages[index]));
            }
        }
        Debug.Log("Out end");
        yield return new WaitForSeconds(1);
        obj.SetActive(false);
    }
}