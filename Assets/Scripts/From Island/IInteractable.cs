using System.Collections;
public interface IInteractable 
{ 
    void CheckProximityPanel(bool IsReallyClose);
    void CheckAction();
    IEnumerator Dialog();
}
