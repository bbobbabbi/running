using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using System.Collections.Generic;
using UnityEngine.EventSystems;

public class Pool : Singleton<Pool>
{
    private string PrefabAddress = "Assets/run/Ground 1.prefab";  // Addressable�� ������ �̸�
    public int initialPoolSize = 4;  // �ʱ� Ǯ ũ��
    public bool isPoolReady = false;
    private Queue<GameObject> mapPool = new Queue<GameObject>();  
    private GameObject mapPrefab;  
    private bool isPrefabLoaded = false;
    GameObject maps;
    void Awake()
    {
        // ������ �ε�

        maps = new GameObject("MapPool");
        Addressables.LoadAssetAsync<GameObject>(PrefabAddress).Completed += OnPrefabLoaded;
    }

    // �������� �ε�Ǿ��� �� ȣ��Ǵ� �޼���
    void OnPrefabLoaded(AsyncOperationHandle<GameObject> handle)
    {
        if (handle.Status == AsyncOperationStatus.Succeeded)
        {
            mapPrefab = handle.Result;
            isPrefabLoaded = true;

            // Ǯ �ʱ�ȭ
            InitializePool();
        }
        else
        {
            Debug.LogError("������ �ε� ����!");
        }
    }

    // Ǯ �ʱ�ȭ
    void InitializePool()
    {
        // foodPrefab�� null�� �ƴ��� Ȯ��
        if (mapPrefab == null)
        {
            Debug.LogError("�������� �ε���� �ʾҽ��ϴ�.");
            return;
        }

        for (int i = 0; i < initialPoolSize; i++)
        {
            GameObject map = Instantiate(mapPrefab);
            map.transform.parent = maps.transform;
            map.SetActive(false);  // Ǯ�� ������ ������Ʈ�� ��Ȱ��ȭ ���·� �Ӵϴ�.
            mapPool.Enqueue(map);  // Queue�� �ֽ��ϴ�.
        }
        isPoolReady = true;
    }

    // Ǯ���� ������Ʈ ��������
    public GameObject GetMap()
    {

        // ť���� ��Ȱ��ȭ�� Food ������Ʈ�� �ϳ� �����ϴ�.
        if (mapPool.Count > 0)
        {
            GameObject map = mapPool.Dequeue();  // ť���� �ϳ� ����
            map.SetActive(true);  // ������Ʈ�� Ȱ��ȭ�ϰ� ��ȯ
            return map;
        }
        else
        {
            // ť�� ������Ʈ�� ������ ���ο� ������Ʈ�� ����
             GameObject newMap = Instantiate(mapPrefab);
            newMap.SetActive(true);
            return newMap;
        }
    }

    // Ǯ�� ������Ʈ �ݳ�
    public void ReturnMap(GameObject map)
    {
        map.SetActive(false);  // ������Ʈ ��Ȱ��ȭ
        mapPool.Enqueue(map);  // ť�� ��ȯ
    }

    void OnDestroy()
    {
        // Ǯ�� �ִ� ��� ������Ʈ�� Ǯ���ݴϴ�.
        while (mapPool.Count > 0)
        {
            GameObject map = mapPool.Dequeue();
            if (map != null)
            {
                Destroy(map);
            }
        }
    }
}
