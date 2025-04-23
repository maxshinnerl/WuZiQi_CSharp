// Entities.cs

namespace Wuziqi
{
    public static class EntityManager
    {
        private static int nextId = 0;
        public static int CreateEntity()
        {
            return nextId++;
        }
    }
}