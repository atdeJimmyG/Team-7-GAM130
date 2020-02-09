public interface IIntractable
{
    float MaxRange { get; }

    void OnStartHover();
    void OnIntract();
    void OnEndHover();
}