

public interface ITouchable
{
   public bool IsFingerTouchScreen { get; set; }

    public void FingerTouch();

    public void ScreenTouched();
}
