using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class UIHideHelp : MonoBehaviour
{
    public List<Graphic> uiElements; // ������ UI �������� ��� ���������
    public float fadeDuration = 1.0f; // ��������� ���������

    private void Start()
    {
        StartCoroutine(StartFadeOutAfterDelay(5f));
    }

    IEnumerator StartFadeOutAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        StartFadeOut();
    }

    // ����� ��� ������� ���������
    public void StartFadeOut()
    {
        StartCoroutine(FadeOutElements());
    }

    // Coroutine ��� ���������
    private IEnumerator FadeOutElements()
    {
        float timer = 0f;
        List<Color> initialColors = new List<Color>();

        // ���������� ���������� �������
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

        // ϳ��� ���������� ������������ �����-�������� � 0
        foreach (Graphic uiElement in uiElements)
        {
            Color newColor = uiElement.color;
            newColor.a = 0;
            uiElement.color = newColor;
        }
    }
}
