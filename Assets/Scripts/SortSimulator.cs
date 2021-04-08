using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SortSimulator : MonoBehaviour
{
    private readonly static List<Func<GameObject[], IEnumerator>> s_algorithms = new List<Func<GameObject[], IEnumerator>>
    {
        SortAlgorithms.SelectionSort,
        SortAlgorithms.BubbleSort,
    };

    [SerializeField] private Dropdown _sortAlgorithmsDropdown;
    [SerializeField] private Button _startButton;
    [SerializeField] private Button _resetButton;
    [SerializeField] private GameObject _targetObject;
    [SerializeField] private Vector2 _startPosition;
    [SerializeField] private int size;

    private GameObject[] _sources = null;

    private void Awake()
    {
        Debug.Assert(_sortAlgorithmsDropdown);
        Debug.Assert(_startButton);
        Debug.Assert(_resetButton);
        Debug.Assert(_targetObject);

        _startButton.interactable = false;
        InitAlgorithmsDropdown();
    }

    private void Start()
    {
        StartCoroutine(RandomInstantiates());
    }

    public void StartSorting()
    {
        _startButton.interactable = false;
        Sorting(s_algorithms[_sortAlgorithmsDropdown.value]);
    }

    public void ResetSimulator()
    {
        SceneManager.LoadScene(0);
    }

    private void InitAlgorithmsDropdown()
    {
        var options = new List<string>();
        foreach (var item in s_algorithms)
        {
            options.Add(item.Method.Name);
        }

        _sortAlgorithmsDropdown.AddOptions(options);
    }

    private void Sorting(Func<GameObject[], IEnumerator> sortAlgorithm)
    {
        if (_sources == null) return;
        StartCoroutine(sortAlgorithm(_sources.ToArray()));
    }

    private IEnumerator RandomInstantiates()
    {
        var numbers = Enumerable.Range(0, size).ToArray();
        for (int i = 0; i < numbers.Length - 1; i++)
        {
            var randIdx = UnityEngine.Random.Range(i, numbers.Length);

            var tmp = numbers[i];
            numbers[i] = numbers[randIdx];
            numbers[randIdx] = tmp;
        }

        _sources = new GameObject[size];

        var wait = new WaitForFixedUpdate();// new WaitForSeconds(0.05f);

        for (int x = 0; x < size; x++)
        {
            var pos = new Vector3(x, _targetObject.transform.position.y, numbers[x]);
            var obj = Instantiate(_targetObject, pos, Quaternion.identity, transform);
            _sources[x] = obj;

            yield return wait;
        }

        _startButton.interactable = true;
    }
}
