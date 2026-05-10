public class StringEventListeners : ReactiveVariableListener<string>
{
    public override void Respond(string value)
    {
        OnEvent?.Invoke();
    }
}
