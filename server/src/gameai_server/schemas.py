from datetime import datetime

from pydantic import BaseModel, Field


class TelemetryEvent(BaseModel):
    session_id: str
    player_id: str
    event_type: str = Field(examples=["death", "level_complete", "item_pickup"])
    timestamp: datetime
    payload: dict = {}


class DecisionRequest(BaseModel):
    session_id: str
    recent_deaths: int = 0
    time_in_level_s: float = 0.0


class DecisionResponse(BaseModel):
    difficulty: float = Field(ge=0, le=1)
    reason: str
