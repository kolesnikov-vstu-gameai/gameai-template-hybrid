using System.Collections;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;

namespace GameAI.Net
{
    /// <summary>Тонкий HTTP-клиент к FastAPI-серверу (см. contracts/openapi.yaml).</summary>
    public class ServerClient : MonoBehaviour
    {
        [SerializeField] private string baseUrl = "http://localhost:8000";

        public IEnumerator PostJson(string path, string json, System.Action<string> onDone)
        {
            using var req = new UnityWebRequest(baseUrl + path, "POST");
            req.uploadHandler = new UploadHandlerRaw(Encoding.UTF8.GetBytes(json));
            req.downloadHandler = new DownloadHandlerBuffer();
            req.SetRequestHeader("Content-Type", "application/json");
            yield return req.SendWebRequest();
            if (req.result == UnityWebRequest.Result.Success) onDone?.Invoke(req.downloadHandler.text);
            else Debug.LogWarning($"[ServerClient] {path}: {req.error}");
        }
    }
}
