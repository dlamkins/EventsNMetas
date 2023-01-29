using System;
using System.Collections.Generic;

namespace EventsNMetas {
    internal class DependencyContainer {

        private Dictionary<Type, object>  _dependencies = new Dictionary<Type, object>();
        private List<DependencyContainer> _subtainers   = new List<DependencyContainer>();

        public DependencyContainer With<T>(T dependency) {
            _dependencies[typeof(T)] = dependency;

            return this;
        }

        public DependencyContainer Including(DependencyContainer container) {
            _subtainers.Add(container);

            return this;
        }

        public T Use<T>() {
            // Pull from this container.
            if (_dependencies.TryGetValue(typeof(T), out object dependency)) {
                return (T)dependency;
            }

            // Fallback to subtainers.
            foreach (var container in _subtainers) {
                var subpendency = container.Use<T>();

                if (subpendency != null) {
                    return subpendency;
                }
            }

            // Doesn't exist.
            return default;
        }

        public void Terminate() {
            _dependencies = new Dictionary<Type, object>(0);
            _subtainers   = new List<DependencyContainer>(0);
        }

    }
}
