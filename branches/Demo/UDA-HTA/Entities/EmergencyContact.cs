namespace Entities
{
    public class EmergencyContact
    {
        public long EmergencyContactId { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Phone { get; set; }

        // Indica si el contacto debe ser eliminado cuando se esta 
        // actualizando la informacion de los contactos de emergencia
        public bool DeleteContact { get; set; }
    }
}
