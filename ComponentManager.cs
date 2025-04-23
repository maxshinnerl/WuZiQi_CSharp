// ComponentManager.cs

namespace Wuziqi
{
    //  Storing components
    public class ComponentManager
    {
        /* A dictionary for matching types to other  dictionaries that maps entity IDs to compoments
        {type: {int entity: T component}}

        NOTE: This setup only allows an entity to have one component of each type.
        If an entity needs multiple components of the same type (i.e. StatusEffectComponent),
        then the inner dictionary should be Dictionary<int, List<object>> or something.
        */
        private Dictionary<Type, Dictionary<int, object>> components = new Dictionary<Type, Dictionary<int, object>>();

        // A dictionary for keeping track of 
        private Dictionary<int, HashSet<Type>> ActiveEntities = [];

        public List<int> GetActiveEntities()
        {
            return ActiveEntities.Keys.ToList();
        }

        public void AddComponent<T>(int entity, T component) where T : class
        {
            // Add type : {entity : component} to the dictionary
            var type = typeof(T);
            
            // since the value stored is in the inner dictionary, need to add the inner dictionary as value to type key before referencing
            if (!components.ContainsKey(type))
            {
                components[type] = [];
            }

            components[type][entity] = component;

            if (!ActiveEntities.ContainsKey(entity))
            {
                ActiveEntities[entity] = [];
            }
            ActiveEntities[entity].Add(type);
        }

        public T? GetComponent<T> (int entity)
        {
            // Retreive the component for a given Entity ID
            var type = typeof(T);

            if (components.ContainsKey(type) && components[type].ContainsKey(entity))
            {
                return (T)components[type][entity]; // have to type cast T here
            }

            return default;
        
        }

        public IEnumerable<int> GetEntitiesWithComponent<T>()
        {
            var type = typeof(T);

            if (components.ContainsKey(type))
            {
                return components[type].Keys;  // keys of inner dict is the entities with this component type
            }
            return [];
        }

        public void RemoveComponent(int entity, Type componentType)
        {
            /* Remove a component from an entity*/

            if (components.ContainsKey(componentType))
            {
                // remove the enntity from this component type dict
                components[componentType].Remove(entity);

                // remove the component type if there are no more of this component type
                if (components[componentType].Count == 0)
                {
                    components.Remove(componentType);
                }

            }

            // Remove from active entities' tracked component types
            if (!ActiveEntities.ContainsKey(entity)) return;
            if  (ActiveEntities[entity].Contains(componentType))
            {
                ActiveEntities[entity].Remove(componentType);
            }

        }

        public void DeleteEntity(int entity)
        {
            if (!ActiveEntities.ContainsKey(entity)) return;

            foreach(Type componentType in ActiveEntities[entity])
            {
                RemoveComponent(entity, componentType);
            }

            ActiveEntities.Remove(entity);
        }

        public List<object> GetEntityComponents(int entityID)
        {
            List<object> components = [];
            if(ActiveEntities.ContainsKey(entityID))
            {
                //Console.WriteLine("TODO");
                foreach(Type t in ActiveEntities[entityID])
                {
                    components.Add(t);
                }
            }

            return components;
        }
    }
}