using System;

// Class memeber updated for naming convention related changes.
public class MenuItem
{
    public bool IsActive { get; set; }
    public string IconColor { get; set; }
    public string Label { get; set; }
    public Guid ReferenceId { get; set; }

    protected virtual void OnClickCallback(object e)
    {
        EventHandler<object> handler = ClickCallback;
        handler?.Invoke(this, e);
    }
    public event EventHandler<object> ClickCallback;
    public void InvokeClickCallback(object e)
    {
        OnClickCallback(e);
    }
}
