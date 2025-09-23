using UnityEngine;

public interface ITrafficInterace
{
    void EnterState();
    void UpdateState();
    void ExitState();
}

public class GreenState: ITrafficInterace
{
   public void EnterState()
    {

    }
    public void UpdateState()
    {

    }
    public void ExitState()
    {

    }
}

public class YellowState: ITrafficInterace
{
    public void EnterState()
    {

    }

    public void UpdateState()
    {

    }

    public void ExitState()
    {

    }
}

public class RedState : ITrafficInterace
{
    public void EnterState()
    {

    }

    public void UpdateState()
    {

    }

    public void ExitState()
    {

    }
}