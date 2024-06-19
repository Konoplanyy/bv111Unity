using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class UIHideHelp : MonoBehaviour
{
    public List<Graphic> uiElements; // Список UI елементів для затухання
    public float fadeDuration = 1.0f; // Тривалість затухання

    private void Start()
    {
        StartCoroutine(StartFadeOutAfterDelay(5f));
    }

    IEnumerator StartFadeOutAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        StartFadeOut();
    }

    // Метод для запуску затухання
    public void StartFadeOut()
    {
        StartCoroutine(FadeOutElements());
    }

    // Coroutine для затухання
    private IEnumerator FadeOutElements()
    {
        float timer = 0f;
        List<Color> initialColors = new List<Color>();

        // Збереження початкових кольорів
        foreach (Graphic uiElement in uiElements)
        {
            initialColors.Add(uiElement.color);
        }

        while (timer < fadeDuration)
        {
            timer += Time.deltaTime;
            float alpha = Mathf.Lerp(1, 0, timer / fadeDuration);

            for (int i = 0; i < uiElements.Count; i++)
            {
                Color newColor = initialColors[i];
                newColor.a = alpha;
                uiElements[i].color = newColor;
            }

            yield return null;
        }

        // Після завершення встановлюємо альфа-значення в 0
        foreach (Graphic uiElement in uiElements)
        {
            Color newColor = uiElement.color;
            newColor.a = 0;
            uiElement.color = newColor;
        }
    }
}
