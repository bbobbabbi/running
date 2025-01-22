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
        await LoadAnimalsAsync();  // �񵿱� �ε�
        _ = SpawnAnimal();
    }

    // UniTask�� ����� �񵿱� �ε�
    private async UniTask LoadAnimalsAsync()
    {
            prefabAddress = "Assets/Food_Pizza_01.prefab";
            AsyncOperationHandle<GameObject> handle = Addressables.LoadAssetAsync<GameObject>(prefabAddress);
            await handle.Task;  // Addressables �ε��� �Ϸ�� ������ ��ٸ�

            if (handle.Status == AsyncOperationStatus.Succeeded)
            {
                gas = (handle.Result);
            }
            else
            {
                Debug.LogError("gas ������ �ε� ����!");
            }
     
    }

    // SpawnAnimal�� ���� �ð� �������� ȣ��
    private async UniTask SpawnAnimal()
    {
        while (true) {
            var objgas = Instantiate(gas);
            objgas.transform.position = new Vector3(gasPos[Random.Range(0, 3)],1, 21);
            await UniTask.Delay(Random.Range(2000, 3000));
        }
    }
}
