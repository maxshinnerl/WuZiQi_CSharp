// Systems.cs

namespace Wuziqi
{
    // NOTE: in Unity, deltaTime is calculated and provided as Time.deltaTime automatically
    // Here, it's hardcoded in Main to be 0.016

    public abstract class System(ComponentManager cm)
    {
        protected ComponentManager CM = cm;

        // abstract method means all subclasses MUST implement their own Update() method with 'override'
        public abstract void Update();

    }

    public class DisplayBlahSystem(ComponentManager CM) : System(CM)
    {
        public override void Update()
        {
            throw new NotImplementedException();
        }
    }
}