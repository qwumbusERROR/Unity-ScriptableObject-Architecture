using UnityEngine;

public class StringEventListeners : ReactiveVariableListener<string>
{
    public override void Respond(string value)
    {
        Debug.Log(value);

        OnEvent?.Invoke();
    }
}
