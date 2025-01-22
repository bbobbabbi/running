using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class SpawnManager : MonoBehaviour
{
    private GameObject gas;
    private string prefabAddress;
    private List<float> gasPos = new List<float>();

    async void Start()
    {
        gasPos.Add(3.3f); gasPos.Add(0f); gasPos.Add(-3.3f);
        await LoadAnimalsAsync();  // 비동기 로딩
        _ = SpawnAnimal();
    }

    // UniTask를 사용한 비동기 로딩
    private async UniTask LoadAnimalsAsync()
    {
            prefabAddress = "Assets/Food_Pizza_01.prefab";
            AsyncOperationHandle<GameObject> handle = Addressables.LoadAssetAsync<GameObject>(prefabAddress);
            await handle.Task;  // Addressables 로딩이 완료될 때까지 기다림

            if (handle.Status == AsyncOperationStatus.Succeeded)
            {
                gas = (handle.Result);
            }
            else
            {
                Debug.LogError("gas 프리팹 로드 실패!");
            }
     
    }

    // SpawnAnimal을 랜덤 시간 간격으로 호출
    private async UniTask SpawnAnimal()
    {
        while (true) {
            var objgas = Instantiate(gas);
            objgas.transform.position = new Vector3(gasPos[Random.Range(0, 3)],1, 21);
            await UniTask.Delay(Random.Range(2000, 3000));
        }
    }
}
