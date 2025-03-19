using System;

public class EventBus
{
    public Action DoorOpened;
    public Action ChaseEvent;

    //PlayerSanity actions
    public Action FearAdditionalEvent;
    public Action FearFullEvent;
}
