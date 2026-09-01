from fastapi.testclient import TestClient

from gameai_server.app import app

client = TestClient(app)


def test_health():
    assert client.get("/health").json()["status"] == "ok"


def test_decision_stub():
    r = client.post("/decision", json={"session_id": "s1", "recent_deaths": 5})
    assert r.status_code == 200
    assert 0 <= r.json()["difficulty"] <= 1
