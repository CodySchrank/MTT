using System.Collections;
using System.Collections.Generic;

namespace Example.Resources
{
    public class World
    {
        public string Name { get; set; }
        public ICollection<Animal> Animals { get; set; }
        public KingdomResource Kingdom { get; set; }

        public World() {
            this.Animals = new Collection<Animal>();
        }

        public World(string name) {
            this.Name = name;
        }
    }
}