namespace XYZC.Core;

public interface IInputLabel
{
    public object Value { get; set; }
    public virtual void Active(){}
    public virtual void Deactive(){}
}