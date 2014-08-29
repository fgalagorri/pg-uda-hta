namespace Entities
{
    public class Drug
    {
        public long? Id { get; set; }
        public string Category { get; set; }
        public string Active { get; set; }
        public string Name { get; set; }

        // Para filtrado en la aplicación
        public string ActiveAndName { get; set; }

        public Drug()
        {
        }

        public Drug(string category, string active, string name)
        {
            Category = category;
            Active = active;
            Name = name;
            ActiveAndName = active + " " + name;
        }

        public bool Equals(Drug d)
        {
            return Category == d.Category && Active == d.Active && Name == d.Name;
        }
    }
}
