using System.Collections.Generic;
using UnityEngine;

namespace Utilities
{
    public class SystemEngine : ISystem
    {
        private readonly List<ISystem> _systems = new List<ISystem>();
        
        public void Update(float deltaTime)
        {
            foreach (var system in _systems)      
            {
                system.Update(deltaTime);
            }
        }
        
        public void Add(ISystem system)
        {
            _systems.Add(system);
        }

        public void Clear()
        {
            _systems.Clear();;
        }
    }
}