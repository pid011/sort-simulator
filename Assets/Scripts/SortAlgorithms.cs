using System.Collections;

using UnityEngine;

public static class SortAlgorithms
{
    /// <summary>
    /// 선택 정렬
    /// </summary>
    /// <param name="sources"></param>
    /// <returns></returns>
    public static IEnumerator SelectionSort(GameObject[] sources)
    {
        for (int i = 0; i < sources.Length - 1; i++)
        {
            int min = i;
            for (int j = i + 1; j < sources.Length; j++)
            {
                if (sources[min].IsBiggerThen(sources[j]))
                {
                    min = j;
                }
            }

            if (min != i)
            {
                SelectObject(sources[i]);
                SelectObject(sources[min]);

                Swap(sources[i], sources[min]);
                yield return new WaitForSeconds(0.1f);

                UnselectObject(sources[i]);
                UnselectObject(sources[min]);
            }
        }
    }

    /// <summary>
    /// 버블 정렬
    /// </summary>
    /// <param name="sources"></param>
    /// <returns></returns>
    public static IEnumerator BubbleSort(GameObject[] sources)
    {
        int n = sources.Length;
        while (n > 1)
        {
            int swappedIdx = 0;
            for (int i = 1; i <= n - 1; i++)
            {
                if (sources[i - 1].IsBiggerThen(sources[i]))
                {
                    SelectObject(sources[i - 1]);
                    SelectObject(sources[i]);

                    Swap(sources[i - 1], sources[i]);
                    swappedIdx = i;
                    yield return new WaitForFixedUpdate();

                    UnselectObject(sources[i - 1]);
                    UnselectObject(sources[i]);
                }

            }
            n = swappedIdx;
        }
    }

    /// <summary>
    /// a가 b보다 큰지 비교 (a > b)
    /// </summary>
    /// <param name="a"></param>
    /// <param name="b"></param>
    /// <returns></returns>
    private static bool IsBiggerThen(this GameObject a, GameObject b)
    {
        return a.transform.position.z > b.transform.position.z;
    }

    private static void Swap(GameObject a, GameObject b)
    {
        var tmp = b.transform.position.z - a.transform.position.z;
        a.transform.position += Vector3.forward * tmp;
        b.transform.position += Vector3.back * tmp;
    }

    private static void SelectObject(GameObject obj)
    {
        obj.GetComponent<MeshRenderer>().material.color = Color.red;
    }

    private static void UnselectObject(GameObject obj)
    {
        obj.GetComponent<MeshRenderer>().material.color = Color.white;
    }
}
