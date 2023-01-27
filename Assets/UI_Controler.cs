using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Controler : MonoBehaviour
{
    public List<GameObject> UiImages;
    public float FadingSpeed = 0.025f;
    public float FadingTimeSpeed = 0.025f;
    public GF_PlayerSelection gF_PlayerSelection;
    public int index = 0;

    public void Start()
    {
        if (UiImages.Count != 0)
        {
            StartCoroutine(FadeIN());
        }
    }

    IEnumerator FadeIN()
    {
        UiImages[index].gameObject.GetComponent<Image>().color = new Color(UiImages[index].gameObject.GetComponent<Image>().color.r, UiImages[index].gameObject.GetComponent<Image>().color.g, UiImages[index].gameObject.GetComponent<Image>().color.b, 0.01f);
        if (index == 0)
        {
            UiImages[index].gameObject.GetComponent<Image>().color = new Color(UiImages[index].gameObject.GetComponent<Image>().color.r, UiImages[index].gameObject.GetComponent<Image>().color.g, UiImages[index].gameObject.GetComponent<Image>().color.b, 0.4f);
        }
        UiImages[index].SetActive(true);
        float alpha = UiImages[index].gameObject.GetComponent<Image>().color.a;
        while (alpha <= 1f)
        {
            alpha += FadingSpeed;
            UiImages[index].gameObject.GetComponent<Image>().color = new Color(UiImages[index].gameObject.GetComponent<Image>().color.r, UiImages[index].gameObject.GetComponent<Image>().color.g, UiImages[index].gameObject.GetComponent<Image>().color.b, alpha);
            yield return new WaitForSeconds(FadingTimeSpeed);
        }
        UiImages[index].gameObject.GetComponent<Image>().color = new Color(UiImages[index].gameObject.GetComponent<Image>().color.r, UiImages[index].gameObject.GetComponent<Image>().color.g, UiImages[index].gameObject.GetComponent<Image>().color.b, 1.1f);
        yield return new WaitForSeconds(1);
    }

    public IEnumerator FadeOut()
    {
        UiImages[index].gameObject.GetComponent<Image>().color = new Color(UiImages[index].gameObject.GetComponent<Image>().color.r, UiImages[index].gameObject.GetComponent<Image>().color.g, UiImages[index].gameObject.GetComponent<Image>().color.b, 1f);

        float alpha = UiImages[index].gameObject.GetComponent<Image>().color.a;

        while (alpha > 0.4f)
        {
            alpha -= FadingSpeed;
            UiImages[index].gameObject.GetComponent<Image>().color = new Color(UiImages[index].gameObject.GetComponent<Image>().color.r, UiImages[index].gameObject.GetComponent<Image>().color.g, UiImages[index].gameObject.GetComponent<Image>().color.b, alpha);
            yield return new WaitForSeconds(FadingTimeSpeed);
        }

        if ((index + 1) != UiImages.Count)
        {
            UiImages[index].SetActive(false);
            gF_PlayerSelection.UiImageOneTime = true;
            index++;
            StartCoroutine(FadeIN());
        }
        yield return new WaitForSeconds(1);
    }

}