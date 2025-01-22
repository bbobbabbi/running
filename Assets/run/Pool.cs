using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using System.Collections.Generic;
using UnityEngine.EventSystems;

public class Pool : Singleton<Pool>
{
    private string PrefabAddress = "Assets/run/Ground 1.prefab";  // Addressable에 설정한 이름
    public int initialPoolSize = 4;  // 초기 풀 크기
    public bool isPoolReady = false;
    private Queue<GameObject> mapPool = new Queue<GameObject>();  
    private GameObject mapPrefab;  
    private bool isPrefabLoaded = false;
    GameObject maps;
    void Awake()
    {
        // 프리팹 로드

        maps = new GameObject("MapPool");
        Addressables.LoadAssetAsync<GameObject>(PrefabAddress).Completed += OnPrefabLoaded;
    }

    // 프리팹이 로드되었을 때 호출되는 메서드
    void OnPrefabLoaded(AsyncOperationHandle<GameObject> handle)
    {
        if (handle.Status == AsyncOperationStatus.Succeeded)
        {
            mapPrefab = handle.Result;
            isPrefabLoaded = true;

            // 풀 초기화
            InitializePool();
        }
        else
        {
            Debug.LogError("프리팹 로드 실패!");
        }
    }

    // 풀 초기화
    void InitializePool()
    {
        // foodPrefab이 null이 아닌지 확인
        if (mapPrefab == null)
        {
            Debug.LogError("프리팹이 로드되지 않았습니다.");
            return;
        }

        for (int i = 0; i < initialPoolSize; i++)
        {
            GameObject map = Instantiate(mapPrefab);
            map.transform.parent = maps.transform;
            map.SetActive(false);  // 풀에 생성된 오브젝트를 비활성화 상태로 둡니다.
            mapPool.Enqueue(map);  // Queue에 넣습니다.
        }
        isPoolReady = true;
    }

    // 풀에서 오브젝트 가져오기
    public GameObject GetMap()
    {

        // 큐에서 비활성화된 Food 오브젝트를 하나 꺼냅니다.
        if (mapPool.Count > 0)
        {
            GameObject map = mapPool.Dequeue();  // 큐에서 하나 꺼냄
            map.SetActive(true);  // 오브젝트를 활성화하고 반환
            return map;
        }
        else
        {
            // 큐에 오브젝트가 없으면 새로운 오브젝트를 생성
             GameObject newMap = Instantiate(mapPrefab);
            newMap.SetActive(true);
            return newMap;
        }
    }

    // 풀에 오브젝트 반납
    public void ReturnMap(GameObject map)
    {
        map.SetActive(false);  // 오브젝트 비활성화
        mapPool.Enqueue(map);  // 큐에 반환
    }

    void OnDestroy()
    {
        // 풀에 있는 모든 오브젝트를 풀어줍니다.
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
