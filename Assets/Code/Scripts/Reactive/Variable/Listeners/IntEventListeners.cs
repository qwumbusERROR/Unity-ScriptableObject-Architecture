public class IntEventListeners : ReactiveVariableListener<int>
{
    public override void Respond(int value)
    {
        OnEvent?.Invoke();
    }
}
