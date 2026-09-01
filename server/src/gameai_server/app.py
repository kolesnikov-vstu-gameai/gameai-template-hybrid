"""FastAPI-сервер: приём телеметрии и выдача решений ИИ-модуля."""

from fastapi import FastAPI

from .schemas import DecisionRequest, DecisionResponse, TelemetryEvent

app = FastAPI(title="GameAI server", version="0.1.0")
_events: list[TelemetryEvent] = []  # замените на БД (см. db.py)


@app.get("/health")
def health() -> dict:
    return {"status": "ok"}


@app.post("/telemetry", status_code=202)
def ingest(event: TelemetryEvent) -> dict:
    _events.append(event)
    return {"accepted": 1, "total": len(_events)}


@app.post("/decision", response_model=DecisionResponse)
def decide(req: DecisionRequest) -> DecisionResponse:
    # Здесь вызывается ваш ML/LLM/RL-модуль. Пока — заглушка.
    difficulty = 0.5 if req.recent_deaths < 3 else 0.3
    return DecisionResponse(difficulty=difficulty, reason="stub")
