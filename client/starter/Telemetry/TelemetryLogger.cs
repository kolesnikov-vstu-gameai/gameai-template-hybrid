using System;
using UnityEngine;

namespace GameAI.Telemetry
{
    [Serializable]
    public class TelemetryEvent
    {
        public string session_id;
        public string player_id;
        public string event_type;
        public string timestamp;
        public string payload_json;
    }

    /// <summary>Собирает события и отправляет на сервер через ServerClient.</summary>
    public class TelemetryLogger : MonoBehaviour
    {
        [SerializeField] private Net.ServerClient client;
        private readonly string _sessionId = Guid.NewGuid().ToString("N");
        public string PlayerId = "anon";

        public void Log(string eventType, string payloadJson = "{}")
        {
            var e = new TelemetryEvent
            {
                session_id = _sessionId, player_id = PlayerId, event_type = eventType,
                timestamp = DateTime.UtcNow.ToString("o"), payload_json = payloadJson
            };
            StartCoroutine(client.PostJson("/telemetry", JsonUtility.ToJson(e), null));
        }
    }
}
